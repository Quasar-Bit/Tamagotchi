
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tamagotchi.Application.Animals.Base.DTOs;
using Tamagotchi.Application.Animals.Queries.GetAll.DTOs;
using Tamagotchi.Web.Controllers.Base;
using Tamagotchi.Data.DataTableProcessing;
using Tamagotchi.Application.Animals.Commands.Create.DTOs;
using Tamagotchi.Application.Animals.Commands.Update.DTOs;
using Tamagotchi.Application.Animals.Commands.Delete.DTOs;
using Tamagotchi.Application.Animals.Queries.GetUnicId.DTOs;
using Tamagotchi.Web.Services.Interfaces;
using Tamagotchi.Application.Settings.Queries.GetAll.DTOs;
using Tamagotchi.Web.Services;
using Tamagotchi.Application.Exceptions;

namespace Tamagotchi.Web.Controllers
{
    public class AnimalsController : BaseController<AnimalsController>
    {
        private readonly IAnimalService _animalService;
        public AnimalsController(
            IMapper mapper,
            IMediator mediator,
            ITokenService tokenService,
            IAnimalService animalService,
            ILogger<AnimalsController> logger) : base(mapper, mediator, tokenService, logger)
        {
            _animalService = animalService;
        }

        public IActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return GetErrorView(ex);
            }
        }

        [HttpPost]
        public IActionResult GetAll()
        {
            try
            {
                return PartialView("_Table");
            }
            catch (Exception ex)
            {
                return GetErrorView(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetPaginatedTable(DtParameters data)
        {
            try
            {
                var result = await Mediator.Send(new GetAnimalsQuery { DtParameters = data });
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new JsonResult(null);
            }
        }

        [HttpPost]
        public IActionResult OpenPopup(GetAnimal model)
        {
            try
            {
                return PartialView("Popups/CreateUpdatePopup", model);
            }
            catch (Exception ex)
            {
                return GetErrorView(ex);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrUpdate(GetAnimal model)
        {
            try
            {
                if (model.Name == model.Type)
                {
                    ModelState.AddModelError("isMatchError", "The Name cannot exactly match Type.");
                }
                if (ModelState.IsValid)
                {
                    ModelState.Clear();
                    if (model.Id is 0)
                    {
                        model.AnimalId = await Mediator.Send(new GetUnicIdQuery());
                        var result = await Mediator.Send(Mapper.Map<CreateAnimalCommand>(model));
                        if(result != null)
                            TempData["success"] = "Animal has created successfully.";
                    }
                    else
                    {
                        var result = await Mediator.Send(Mapper.Map<UpdateAnimalCommand>(model));
                        if (result == null)
                            return NotFound();
                        else
                            TempData["success"] = "Animal has updated successfully.";
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                return GetErrorView(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(GetAnimal model)
        {
            try
            {
                var result = await Mediator.Send(new DeleteAnimalCommand { Id = model.Id });
                if(result == null)
                    return NotFound();

                TempData["success"] = "Animal has deleted successfully.";
                return RedirectToActionPermanent(nameof(Index));
            }
            catch (Exception ex)
            {
                return GetErrorView(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Synch()
        {
            try
            {
                var isSynchronizing = await Mediator.Send(new GetAppSettingsQuery { Name = "IsSynchronizing" });
                var lastCheckedUpdatedTime = DateTime.UtcNow - isSynchronizing.UpdateTime;
                if (isSynchronizing.BoolValue && lastCheckedUpdatedTime.TotalMinutes < 5)
                {
                    TempData["error"] = "Some kind of synchronization is already running...";
                    ModelState.AddModelError("synchronizing", "Some kind of synchronization is already running...");
                    return RedirectToAction("index");
                }

                var animalsTotal = await _animalService.GetAnimals(1);

                var dbOrganizations = Mediator.Send(new GetAllAnimalsQuery()).Result.ToList();

                for (int i = 1; i < animalsTotal.Pagination.total_pages; i++)
                {
                    await ToggleSinchronization(true);
                    var petFinderOrganizations = await _animalService.GetAnimals(i);

                    foreach (var item in petFinderOrganizations.Animals)
                    {
                        var obj = dbOrganizations.FirstOrDefault(x => x.Name == item.Name && x.AnimalId == item.AnimalId);
                        if (obj == null)
                        {
                            var result = await Mediator.Send(Mapper.Map<CreateAnimalCommand>(item));
                            if (result == null)
                                Console.WriteLine("Something went wrong with creation " + item.Name + " Animal");
                            else
                                Console.WriteLine("Animal " + result.Name + " has added");
                        }
                    }
                }
            }
            catch (WebServiceException ex)
            {
                if (ex.Errors.Contains("Unauthorized"))
                {
                    var isUpdatedPetFinderToken = await TokenService.GetPetFinderToken();
                    if (isUpdatedPetFinderToken)
                    {
                        await Synch();
                    }
                    else
                        ModelState.AddModelError("authorizationError", "Something went wrong with getting PetFinder token!");
                }
                else
                    ModelState.AddModelError("webServiceError", ex.Message);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("synchError", ex.Message);
            }
            finally
            {
                await ToggleSinchronization(false);
            }

            return RedirectToAction("index");
        }
    }
}