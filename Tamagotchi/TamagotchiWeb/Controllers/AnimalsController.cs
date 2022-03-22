using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TamagotchiWeb.Application.Animals.Base.DTOs;
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

        public AnimalsController(
            IMapper mapper,
            IAnimalRepository animalRepository,
            ILogger<AnimalsController> logger) : base(logger)
        {
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
                IEnumerable<GetAnimal> animals;

                var dtParameters = data;

                animals = _animalRepository.GetReadOnlyQuery()
                    .Select(x => new GetAnimal
                    {
                        Id = x.id,
                        Name = x.name,
                        Type = x.type
                        //AnimalId = x.animalId,
                        //PrimaryBreed = x.primaryBreed,
                        //Gender = x.gender,
                        //Age = x.age,
                        //PrimaryColor = x.primaryColor,
                        //OrganizationId = x.organizationId
                    });

                var total = animals.Count();

                var searchBy = dtParameters.Search?.Value;

                if (!string.IsNullOrEmpty(searchBy))
                    animals = animals.Where(s => s.Type.ContainsInsensitive(searchBy) ||
                                                             s.Name.ContainsInsensitive(searchBy)
                    );

                var orderableProperty = nameof(GetAnimal.AnimalId);
                var toOrderAscending = true;
                if (dtParameters.Order != null && dtParameters.Length > 0)
                {
                    orderableProperty = dtParameters.Columns[dtParameters.Order.FirstOrDefault().Column].Data.CapitalizeFirst();
                    toOrderAscending = dtParameters.Order.FirstOrDefault().Dir == DtOrderDir.Asc;
                }

                var orderedAnimals = toOrderAscending
                    ? animals.OrderBy(x => x.GetPropertyValue(orderableProperty))
                    : animals.OrderByDescending(x => x.GetPropertyValue(orderableProperty));

                var result = new DtResult<GetAnimal>
                {
                    Draw = dtParameters.Draw,
                    RecordsTotal = total,
                    RecordsFiltered = orderedAnimals.Count(),
                    Data = orderedAnimals
                    .Skip(dtParameters.Start)
                    .Take(dtParameters.Length)
                };

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