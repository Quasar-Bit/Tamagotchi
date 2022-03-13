//using Microsoft.AspNetCore.Mvc;
//using TamagotchiWeb.Data;
//using TamagotchiWeb.Models;
//using TamagotchiWeb.Services;
//using TamagotchiWeb.Services.DTOs.OutPut.Common;
//using TamagotchiWeb.Services.Interfaces;

//namespace TamagotchiWeb.Controllers
//{
//    public class OrganizationsController : Controller
//    {
//        private readonly IOrganizationService _animalService;
//        private readonly Context _db;
//        public OrganizationsController(Context db)
//        {
//            _db = db;
//            _animalService = new OrganizationService();
//        }
//        public IActionResult Index()
//        {
//            var result = new GetOrganizations
//            {
//                //Organizations = _db.Organizations,
//                Pagination = new Pagination
//                {
//                    total_count = _db.Organizations.Count()
//                }
//            };

//            return View(result);
//        }

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
//    }
//}



using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TamagotchiWeb.Application.Animals.Base.DTOs;
using TamagotchiWeb.Application.AnimalTypes.Base.DTOs;
using TamagotchiWeb.Application.Organizations.Base.DTOs;
using TamagotchiWeb.Controllers.Base;
using TamagotchiWeb.Data.DataTableProcessing;
using TamagotchiWeb.Data.Repositories.Interfaces;
using TamagotchiWeb.Extensions;

namespace TamagotchiWeb.Controllers
{
    public class OrganizationsController : BaseController<OrganizationsController>
    {
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IMapper _mapper;

        public OrganizationsController(
        IMapper mapper,
               IOrganizationRepository organizationRepository,
               ILogger<OrganizationsController> logger) : base(logger)
        {
            _mapper = mapper;
            _organizationRepository = organizationRepository;
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
                IEnumerable<GetOrganization> subscriptions;

                var dtParameters = data;

                subscriptions = _organizationRepository.GetReadOnlyQuery()
                    .Select(x => new GetOrganization
                    {
                        phone = x.phone,
                        name = x.name,
                        email = x.email,
                        website = x.website,
                        address1 = x.address1,
                        organizationId = x.organizationId
                    });

                var total = subscriptions.Count();

                var searchBy = dtParameters.Search?.Value;

                if (!string.IsNullOrEmpty(searchBy))
                    subscriptions = subscriptions.Where(s => s.name.ContainsInsensitive(searchBy) ||
                                                             //s.email.ContainsInsensitive(searchBy) ||
                                                             s.organizationId.ContainsInsensitive(searchBy)
                                                             //s.address1.ContainsInsensitive(searchBy)
                    );

                var orderableProperty = nameof(GetOrganization.id);
                var toOrderAscending = true;
                if (dtParameters.Order != null && dtParameters.Length > 0)
                {
                    orderableProperty = dtParameters.Columns[dtParameters.Order.FirstOrDefault().Column].Data.CapitalizeFirst();
                    toOrderAscending = dtParameters.Order.FirstOrDefault().Dir == DtOrderDir.Asc;
                }

                //var orderedSubscriptions = toOrderAscending
                //    ? subscriptions.OrderBy(x => x.GetPropertyValue(orderableProperty))
                //    : subscriptions.OrderByDescending(x => x.GetPropertyValue(orderableProperty));

                var result = new DtResult<GetOrganization>
                {
                    Draw = dtParameters.Draw,
                    RecordsTotal = total,
                    RecordsFiltered = subscriptions.Count(),
                    Data = subscriptions
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

                return GetAll();
            }
            catch (Exception ex)
            {
                return GetErrorView(ex);
            }
        }

    }
}