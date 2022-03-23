using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TamagotchiWeb.Application.Animals.Base.DTOs;
using TamagotchiWeb.Application.Animals.Queries.GetAll.DTOs;
using TamagotchiWeb.Controllers.Base;
using TamagotchiWeb.Data.DataTableProcessing;
using TamagotchiWeb.Data.Repositories.Interfaces;
using TamagotchiWeb.Entities;
using TamagotchiWeb.Extensions;

namespace TamagotchiWeb.Controllers
{
    public class AnimalsController : BaseController<AnimalsController>
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public AnimalsController(
            IMapper mapper,
            IMediator mediator,
            IAnimalRepository animalRepository,
            ILogger<AnimalsController> logger) : base(logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _animalRepository = animalRepository;
        }

        public async Task<IActionResult> Index()
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
                var result = await _mediator.Send(new GetAnimalsQuery { DtParameters = data });

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
                //if (model.Name == model.Type)
                //{
                //    ModelState.AddModelError("isMatchError", "The Name cannot exactly match Type.");
                //}
                if (ModelState.IsValid)
                {
                    ModelState.Clear();
                    if (model.Id is 0)
                    {
                        var animal = new Animal
                        {
                            animalId = model.AnimalId,
                            type = model.Type,
                            name = model.Name
                            //organizationAnimalId = model.OrganizationAnimalId,
                            //primaryBreed = model.PrimaryBreed,
                            //organizationId = model.OrganizationId
                        };

                        await _animalRepository.AddAsync(animal);
                        TempData["success"] = "Animal has created successfully.";
                    }
                    else
                    {
                        var editableAnimal = await _animalRepository.GetChangeTrackingQuery().FirstOrDefaultAsync(x => x.id == model.Id, new CancellationToken());
                        
                        if (editableAnimal != null)
                        {
                            if (editableAnimal.animalId != model.AnimalId)
                            {
                                return RedirectToAction("Index"); //fix logic
                            }

                            editableAnimal.name = model.Name;
                            editableAnimal.type = model.Type;
                            editableAnimal.animalId = model.AnimalId;
                            //editableAnimal.organizationAnimalId = model.OrganizationAnimalId;
                            //editableAnimal.primaryBreed = model.PrimaryBreed;
                            //editableAnimal.organizationId = model.OrganizationId;

                            _animalRepository.Update(editableAnimal);
                            TempData["success"] = "Animal has updated successfully.";
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                    await _animalRepository.UnitOfWork.SaveChangesAsync(new CancellationToken());
                }
                else
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
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
                var deletableAnimal = await _animalRepository.GetChangeTrackingQuery().FirstOrDefaultAsync(x => x.id == model.Id, new CancellationToken());

                if (deletableAnimal != null)
                    _animalRepository.Remove(deletableAnimal);
                else
                    return NotFound();

                await _animalRepository.UnitOfWork.SaveChangesAsync(new CancellationToken());

                TempData["success"] = "Animal has deleted successfully.";
                return RedirectToActionPermanent(nameof(Index));
            }
            catch (Exception ex)
            {
                return GetErrorView(ex);
            }
        }
    }
}