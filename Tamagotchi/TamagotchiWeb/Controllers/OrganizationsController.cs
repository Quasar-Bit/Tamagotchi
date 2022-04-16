
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tamagotchi.Application.Organizations.Base.DTOs;
using Tamagotchi.Application.Organizations.Queries.GetAll.DTOs;
using TamagotchiWeb.Controllers.Base;
using Tamagotchi.Data.DataTableProcessing;
using Tamagotchi.Application.Organizations.Commands.Create.DTOs;
using Tamagotchi.Application.Organizations.Queries.GetUnicId.DTOs;
using Tamagotchi.Application.Organizations.Commands.Update.DTOs;
using Tamagotchi.Application.Organizations.Commands.Delete.DTOs;
using TamagotchiWeb.Services.Interfaces;

namespace TamagotchiWeb.Controllers
{
    public class OrganizationsController : BaseController<OrganizationsController>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public OrganizationsController(
            IMediator mediator,
            IMapper mapper,
            ITokenService tokenService,
            ILogger<OrganizationsController> logger) : base(tokenService, logger)
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
                if (model.Name == model.OrganizationId)
                {
                    ModelState.AddModelError("isMatchError", "The Name cannot exactly match OrganizationId.");
                }
                if (ModelState.IsValid)
                {
                    ModelState.Clear();
                    if (model.Id is 0)
                    {
                        model.OrganizationId += await _mediator.Send(new GetUnicOrganizationIdQuery());
                        var result = await _mediator.Send(_mapper.Map<CreateOrganizationCommand>(model));
                        if (result != null)
                            TempData["success"] = "Organization has created successfully.";
                    }
                    else
                    {
                        var result = await _mediator.Send(_mapper.Map<UpdateOrganizationCommand>(model));
                        if (result == null)
                            return NotFound();
                        else
                            TempData["success"] = "Organization updated successfully.";
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
        public async Task<IActionResult> Delete(GetOrganization model)
        {
            try
            {
                var result = await _mediator.Send(new DeleteOrganizationCommand { Id = model.Id });
                if (result == null)
                    return NotFound();

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


//for (int i = 6; i <= firstRequest.Pagination.total_pages; i++)
//{
//    var answer = await _animalService.GetOrganizations(i);

//    foreach (var item in answer.Organizations)
//    {
//        _db.Organizations.Add(item);
//    }

//    _db.SaveChanges();
//}