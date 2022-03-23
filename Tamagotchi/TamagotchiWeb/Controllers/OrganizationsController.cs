
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TamagotchiWeb.Application.Organizations.Base.DTOs;
using TamagotchiWeb.Application.Organizations.Queries.GetAll.DTOs;
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
        private readonly IMediator _mediator;

        public OrganizationsController(
            IMediator mediator,
            IMapper mapper,
            IOrganizationRepository organizationRepository,
            ILogger<OrganizationsController> logger) : base(logger)
        {
            _mediator = mediator;
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

        //[HttpPost]
        //public IActionResult GetAll()
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
                var result = await _mediator.Send(new GetOrganizationsQuery { DtParameters = data });

                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new JsonResult(null);
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