//using MediatR;
using Microsoft.AspNetCore.Mvc;
using TamagotchiWeb.Application.Animals.Base.DTOs;
using TamagotchiWeb.Application.Animals.Queries.GetAll.DTOs;
using TamagotchiWeb.Controllers.Base;
using TamagotchiWeb.Data.DataTableProcessing;
using TamagotchiWeb.Data.Repositories.Interfaces;
using TamagotchiWeb.Services;

namespace TamagotchiWeb.Controllers
{
    public class AnimalsController : BaseController<AnimalsController>
    {
        private readonly IAnimalRepository _animalRepository;
        //private readonly IMediator _mediator;

        public AnimalsController(
        //IMediator mediator,
               IAnimalRepository animalRepository,
               ILogger<AnimalsController> logger) : base(logger)
        {
            //_mediator = mediator;
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
                var result = _animalRepository.GetReadOnlyQuery()
                    .Select(x => new GetAnimal
                    {
                        Name = x.name,
                        Age = x.age
                    });
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