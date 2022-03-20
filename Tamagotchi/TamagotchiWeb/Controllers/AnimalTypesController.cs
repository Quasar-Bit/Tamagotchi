using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
                IEnumerable<GetAnimalType> subscriptions;

                var dtParameters = data;

                subscriptions = _animalTypeRepository.GetReadOnlyQuery()
                    .Select(x => new GetAnimalType
                    {
                        Name = x.name,
                        Coats = x.coats,
                        Colors = x.colors,
                        Genders = x.genders,
                        Id = x.id
                    });

                var total = subscriptions.Count();

                var searchBy = dtParameters.Search?.Value;

                if (!string.IsNullOrEmpty(searchBy))
                    subscriptions = subscriptions.Where(s => s.Coats.ContainsInsensitive(searchBy) ||
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

                var orderedSubscriptions = toOrderAscending
                    ? subscriptions.OrderBy(x => x.GetPropertyValue(orderableProperty))
                    : subscriptions.OrderByDescending(x => x.GetPropertyValue(orderableProperty));

                var result = new DtResult<GetAnimalType>
                {
                    Draw = dtParameters.Draw,
                    RecordsTotal = total,
                    RecordsFiltered = orderedSubscriptions.Count(),
                    Data = orderedSubscriptions
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
                var animalType = new AnimalType
                {
                    coats = model.Coats,
                    colors = model.Colors,
                    name = model.Name,
                    genders = model.Genders
                };

                await _animalTypeRepository.AddAsync(animalType);
                await _animalTypeRepository.UnitOfWork.SaveChangesAsync(new CancellationToken());

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
                return GetAll();
            }
            catch (Exception ex)
            {
                return GetErrorView(ex);
            }
        }
    }
}