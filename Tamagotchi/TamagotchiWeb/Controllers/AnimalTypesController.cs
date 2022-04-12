using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tamagotchi.Application.AnimalTypes.Base.DTOs;
using Tamagotchi.Application.AnimalTypes.Quieries.GetAll.DTOs;
using TamagotchiWeb.Controllers.Base;
using Tamagotchi.Data.DataTableProcessing;
using Tamagotchi.Application.AnimalTypes.Commands.Create.DTOs;
using Tamagotchi.Application.AnimalTypes.Commands.Update.DTOs;
using Tamagotchi.Application.AnimalTypes.Commands.Delete.DTOs;

namespace TamagotchiWeb.Controllers
{
    public class AnimalTypesController : BaseController<AnimalTypesController>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public AnimalTypesController(
            IMediator mediator,
            IMapper mapper,
            ILogger<AnimalTypesController> logger) : base(logger)
        {
            _mediator = mediator;
            _mapper = mapper;
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
        public async Task<IActionResult> GetPaginatedTable(DtParameters data)
        {
            try
            {
                var result = await _mediator.Send(new GetAnimalTypesQuery { DtParameters = data });
                
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new JsonResult(null);
            }
        }

        [HttpPost]
        public IActionResult OpenPopup(GetAnimalType model)
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
        public async Task<IActionResult> CreateOrUpdate(GetAnimalType model)
        {
            try
            {
                if(model.Name == model.Coats)
                {
                    ModelState.AddModelError("isMatchError", "The Name cannot exactly match coats.");
                }
                if(ModelState.IsValid)
                {
                    ModelState.Clear();
                    if (model.Id is 0)
                    {
                        var result = await _mediator.Send(_mapper.Map<CreateAnimalTypeCommand>(model));
                        if (result != null)
                            TempData["success"] = "Animal type has created successfully.";
                    }
                    else
                    {
                        var result = await _mediator.Send(_mapper.Map<UpdateAnimalTypeCommand>(model));
                        if (result == null)
                            return NotFound();
                        else
                            TempData["success"] = "Animal type has updated successfully.";
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
        public async Task<IActionResult> Delete(GetAnimalType model)
        {
            try
            {
                var result = await _mediator.Send(new DeleteAnimalTypeCommand { Id = model.Id });
                if (result == null)
                    return NotFound();

                TempData["success"] = "Animal type deleted successfully.";
                return RedirectToActionPermanent(nameof(Index));
            }
            catch (Exception ex)
            {
                return GetErrorView(ex);
            }
        }
    }
}

//        [HttpPost]
//        public async Task<IActionResult> Synch(string gg)
//        {
//            var firstRequest = await _animalService.GetOrganizations(1);

//            //for (int i = 6; i <= firstRequest.Pagination.total_pages; i++)
//            //{
//            //    var answer = await _animalService.GetOrganizations(i);

//            //    foreach (var item in answer.Organizations)
//            //    {
//            //        _db.Organizations.Add(item);
//            //    }

//            //    _db.SaveChanges();
//            //}

//            return RedirectToAction("index");
//        }