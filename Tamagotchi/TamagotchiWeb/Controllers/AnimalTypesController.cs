

//using MediatR;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TamagotchiWeb.Application.Animals.Base.DTOs;
using TamagotchiWeb.Application.AnimalTypes.Base.DTOs;
using TamagotchiWeb.Controllers.Base;
using TamagotchiWeb.Data.DataTableProcessing;
using TamagotchiWeb.Data.Repositories.Interfaces;
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

                //var gg = result.Data.ToList();
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        [HttpPost]
        public IActionResult OpenCreateUpdate(GetAnimal model)
        {
            try
            {
                return PartialView("Modal/CreateUpdatePopup", model);
            }
            catch (Exception ex)
            {
                return GetErrorView(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate(GetAnimal model)
        {
            try
            {
                //if (string.IsNullOrEmpty(model.Id))
                //    await _mediator.Send(new CreateSubscriptionCommand
                //    {
                //        Name = model.Name,
                //        Mdn = model.Mdn,
                //        UserId = model.UserId,
                //        IsActive = model.IsActive,
                //        IsCycle = model.IsCycle,
                //        PlanId = model.PlanId,
                //        ResellerId = model.ResellerId
                //    });
                //else
                //    await _mediator.Send(new UpdateSubscriptionCommand
                //    {
                //        Id = model.Id,
                //        Mdn = model.Mdn,
                //        Name = model.Name,
                //        IsActive = model.IsActive,
                //        IsCycle = model.IsCycle,
                //        PlanId = model.PlanId
                //    });
                return GetAll();
            }
            catch (Exception ex)
            {
                return GetErrorView(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(GetAnimal item)
        {
            try
            {
                //await _mediator.Send(new DeleteSubscriptionCommand { Id = item.Id });

                return GetAll();
            }
            catch (Exception ex)
            {
                return GetErrorView(ex);
            }
        }

        //public IActionResult Index()
        //{
        //    var result = new GetAnimals
        //    {
        //        //Animals = _db.Animals,
        //        Pagination = new Pagination
        //        {
        //            total_count = _db.Animals.Count()
        //        }
        //    };

        //    return View(result);
        //}

        [HttpPost]
        public async Task<IActionResult> Synch(string gg)
        {
            //var firstRequest = await _animalService.GetAnimals(1);

            //for (int i = 1801; i <= firstRequest.Pagination.total_pages; i++)
            //{
            //    var answer = await _animalService.GetAnimals(i);

            //    foreach (var item in answer.Animals)
            //    {
            //        _db.Animals.Add(item);
            //    }

            //    _db.SaveChanges();
            //}

            return RedirectToAction("index");
        }

    }
}