using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TamagotchiWeb.Application.Animals.Base.DTOs;
using TamagotchiWeb.Application.AnimalTypes.Base.DTOs;
using TamagotchiWeb.Application.AnimalTypes.Quieries.GetAll.DTOs;
using TamagotchiWeb.Controllers.Base;
using TamagotchiWeb.Data.DataTableProcessing;
using TamagotchiWeb.Data.Repositories.Interfaces;
using TamagotchiWeb.Entities;
using TamagotchiWeb.Extensions;

namespace TamagotchiWeb.Controllers
{
    public class AnimalTypesController : BaseController<AnimalTypesController>
    {
        private readonly IAnimalTypeRepository _animalTypeRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public AnimalTypesController(
            IMediator mediator,
            IMapper mapper,
            IAnimalTypeRepository animalTypeRepository,
            ILogger<AnimalTypesController> logger) : base(logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _animalTypeRepository = animalTypeRepository;
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

        //[HttpPost]
        //public async Task<IActionResult> GetAll()
        //{
        //    try
        //    {
        //        return PartialView("_Table");
        //    }
        //    catch (Exception ex)
        //    {
        //        return GetErrorView(ex);
        //    }
        //}

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
                        var animalType = new AnimalType
                        {
                            coats = model.Coats,
                            colors = model.Colors,
                            name = model.Name,
                            genders = model.Genders
                        };

                        await _animalTypeRepository.AddAsync(animalType);
                        TempData["success"] = "Animal type created successfully.";
                    }
                    else
                    {
                        var editableAnimalType = await _animalTypeRepository.GetChangeTrackingQuery().FirstOrDefaultAsync(x => x.id == model.Id, new CancellationToken());

                        if(editableAnimalType != null)
                        {
                            editableAnimalType.coats = model.Coats;
                            editableAnimalType.colors = model.Colors;
                            editableAnimalType.name = model.Name;
                            editableAnimalType.genders = model.Genders;

                            _animalTypeRepository.Update(editableAnimalType);
                            TempData["success"] = "Animal type updated successfully.";
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                    await _animalTypeRepository.UnitOfWork.SaveChangesAsync(new CancellationToken());
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
        public async Task<IActionResult> Delete(GetAnimalType model)
        {
            try
            {
                var deletableAnimalType = await _animalTypeRepository.GetChangeTrackingQuery().FirstOrDefaultAsync(x => x.id == model.Id, new CancellationToken());

                if (deletableAnimalType != null)
                    _animalTypeRepository.Remove(deletableAnimalType);
                else
                    return NotFound();

                await _animalTypeRepository.UnitOfWork.SaveChangesAsync(new CancellationToken());
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