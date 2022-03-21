using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TamagotchiWeb.Application.Animals.Base.DTOs;
using TamagotchiWeb.Application.AnimalTypes.Base.DTOs;
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

        public AnimalTypesController(
        IMapper mapper,
               IAnimalTypeRepository animalTypeRepository,
               ILogger<AnimalTypesController> logger) : base(logger)
        {
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
                IEnumerable<GetAnimalType> animalTypes;

                var dtParameters = data;

                animalTypes = _animalTypeRepository.GetReadOnlyQuery()
                    .Select(x => new GetAnimalType
                    {
                        Name = x.name,
                        Coats = x.coats,
                        Colors = x.colors,
                        Genders = x.genders,
                        Id = x.id
                    });

                var total = animalTypes.Count();

                var searchBy = dtParameters.Search?.Value;

                if (!string.IsNullOrEmpty(searchBy))
                    animalTypes = animalTypes.Where(s => s.Coats.ContainsInsensitive(searchBy) ||
                                                             s.Colors.ContainsInsensitive(searchBy) ||
                                                             s.Genders.ContainsInsensitive(searchBy) ||
                                                             s.Name.ContainsInsensitive(searchBy)
                    );

                var orderableProperty = nameof(GetAnimal.AnimalId);
                var toOrderAscending = true;
                if (dtParameters.Order != null && dtParameters.Length > 0)
                {
                    orderableProperty = dtParameters.Columns[dtParameters.Order.FirstOrDefault().Column].Data.CapitalizeFirst();
                    toOrderAscending = dtParameters.Order.FirstOrDefault().Dir == DtOrderDir.Asc;
                }

                var orderedAnimalTypes = toOrderAscending
                    ? animalTypes.OrderBy(x => x.GetPropertyValue(orderableProperty))
                    : animalTypes.OrderByDescending(x => x.GetPropertyValue(orderableProperty));

                var result = new DtResult<GetAnimalType>
                {
                    Draw = dtParameters.Draw,
                    RecordsTotal = total,
                    RecordsFiltered = orderedAnimalTypes.Count(),
                    Data = orderedAnimalTypes
                    .Skip(dtParameters.Start)
                    .Take(dtParameters.Length)
                };
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
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