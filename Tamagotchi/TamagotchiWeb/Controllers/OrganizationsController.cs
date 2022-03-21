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

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TamagotchiWeb.Application.Organizations.Base.DTOs;
using TamagotchiWeb.Controllers.Base;
using TamagotchiWeb.Data.DataTableProcessing;
using TamagotchiWeb.Data.Repositories.Interfaces;
using TamagotchiWeb.Entities;
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
        public IActionResult OpenPopup(GetOrganization model)
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
        public async Task<IActionResult> CreateOrUpdate(GetOrganization model)
        {
            try
            {
                if (model.name == model.organizationId)
                {
                    ModelState.AddModelError("isMatchError", "The Name cannot exactly match OrganizationId.");
                }
                if (ModelState.IsValid)
                {
                    ModelState.Clear();
                    if (model.id is 0)
                    {
                        var organization = new Organization
                        {
                            phone = model.phone,
                            name = model.name,
                            email = model.email,
                            website = model.website,
                            address1 = model.address1,
                            organizationId = model.organizationId
                        };

                        await _organizationRepository.AddAsync(organization);
                        TempData["success"] = "Organization created successfully.";
                    }
                    else
                    {
                        var editableOrganization = await _organizationRepository.GetChangeTrackingQuery().FirstOrDefaultAsync(x => x.id == model.id, new CancellationToken());

                        if (editableOrganization != null)
                        {
                            editableOrganization.phone = model.phone;
                            editableOrganization.name = model.name;
                            editableOrganization.email = model.email;
                            editableOrganization.website = model.website;
                            editableOrganization.address1 = model.address1;
                            editableOrganization.organizationId = model.organizationId;

                            _organizationRepository.Update(editableOrganization);
                            TempData["success"] = "Organization updated successfully.";
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                    await _organizationRepository.UnitOfWork.SaveChangesAsync(new CancellationToken());
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
        public async Task<IActionResult> Delete(GetOrganization model)
        {
            try
            {
                var deletableOrganization = await _organizationRepository.GetChangeTrackingQuery().FirstOrDefaultAsync(x => x.id == model.id, new CancellationToken());

                if (deletableOrganization != null)
                    _organizationRepository.Remove(deletableOrganization);
                else
                    return NotFound();

                await _organizationRepository.UnitOfWork.SaveChangesAsync(new CancellationToken());

                TempData["success"] = "Organization has deleted successfully.";
                return RedirectToActionPermanent(nameof(Index));
            }
            catch (Exception ex)
            {
                return GetErrorView(ex);
            }
        }
    }
}