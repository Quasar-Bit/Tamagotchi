
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tamagotchi.Application.Organizations.Base.DTOs;
using Tamagotchi.Application.Organizations.Queries.GetAll.DTOs;
using Tamagotchi.Web.Controllers.Base;
using Tamagotchi.Data.DataTableProcessing;
using Tamagotchi.Application.Organizations.Commands.Create.DTOs;
using Tamagotchi.Application.Organizations.Queries.GetUnicId.DTOs;
using Tamagotchi.Application.Organizations.Commands.Update.DTOs;
using Tamagotchi.Application.Organizations.Commands.Delete.DTOs;
using Tamagotchi.Web.Services.Interfaces;
using Tamagotchi.Application.Exceptions;
using Tamagotchi.Application.Settings.Queries.GetAll.DTOs;

namespace Tamagotchi.Web.Controllers;
public class OrganizationsController : BaseController<OrganizationsController>
{
    private readonly IOrganizationService _organizationService;

    public OrganizationsController(
        IMediator mediator,
        IMapper mapper,
        ITokenService tokenService,
        IOrganizationService organizationService,
        ILogger<OrganizationsController> logger) : base(mapper, mediator, tokenService, logger)
    {
        _organizationService = organizationService;
    }

    public IActionResult Index()
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
            var result = await Mediator.Send(new GetOrganizationsQuery { DtParameters = data });

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
                    model.OrganizationId += await Mediator.Send(new GetUnicOrganizationIdQuery());
                    var result = await Mediator.Send(Mapper.Map<CreateOrganizationCommand>(model));
                    if (result != null)
                        TempData["success"] = "Organization has created successfully.";
                }
                else
                {
                    var result = await Mediator.Send(Mapper.Map<UpdateOrganizationCommand>(model));
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
            var result = await Mediator.Send(new DeleteOrganizationCommand { Id = model.Id });
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

    [HttpPost]
    public async Task<IActionResult> Synch()
    {
        try
        {
            var isSynchronizing = await Mediator.Send(new GetAppSettingsQuery { Name = "IsSynchronizing" });
            var lastCheckedUpdatedTime = DateTime.UtcNow - isSynchronizing.UpdateTime;
            if (isSynchronizing.BoolValue && lastCheckedUpdatedTime.TotalMinutes < 5)
            {
                TempData["error"] = "Some kind of synchronization is already running...";
                ModelState.AddModelError("synchronizing", "Some kind of synchronization is already running...");
                return RedirectToAction("index");
            }

            var orgTotal = await _organizationService.GetOrganizations(1);

            var dbOrganizations = Mediator.Send(new GetAllOrganizationsQuery()).Result.ToList();
            
            for (int i = 1; i < orgTotal.Pagination.total_pages; i++)
            {
                await ToggleSinchronization(true);
                var petFinderOrganizations = await _organizationService.GetOrganizations(i);
                
                foreach (var item in petFinderOrganizations.Organizations)
                {
                    var obj = dbOrganizations.FirstOrDefault(x => x.Name == item.Name && x.OrganizationId == item.OrganizationId);
                    if (obj == null)
                    {
                        var result = await Mediator.Send(Mapper.Map<CreateOrganizationCommand>(item));
                        if (result == null)
                            Console.WriteLine("Something went wrong with creation " + item.Name + " Organization");
                        else
                            Console.WriteLine("Organization " + result.Name + " has added");
                    }
                }
            }
        }
        catch (WebServiceException ex)
        {
            if (ex.Errors.Contains("Unauthorized"))
            {
                var isUpdatedPetFinderToken = await TokenService.GetPetFinderToken();
                if (isUpdatedPetFinderToken)
                {
                    await Synch();
                }
                else
                    ModelState.AddModelError("authorizationError", "Something went wrong with getting PetFinder token!");
            }
            else
                ModelState.AddModelError("webServiceError", ex.Message);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("synchError", ex.Message);
        }
        finally
        {
            await ToggleSinchronization(false);
        }

        return RedirectToAction("index");
    }
}