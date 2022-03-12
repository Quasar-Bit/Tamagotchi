using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApplicationException = TamagotchiWeb.Exceptions.ApplicationException;

namespace Tamagotchi.Api.Controllers.Base;

[Authorize]
[Route("api/v1/[controller]")]
[ApiController]
public abstract class BaseApiController<T> : ControllerBase, IDisposable
{
    private const string RequiredField = "The '{0}' field is required.";

    protected BaseApiController(IMediator mediator, ILogger<T> logger)
    {
        Logger = logger;
        Mediator = mediator;
    }

    protected ILogger<T> Logger { get; }
    protected IMediator Mediator { get; }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected async Task<IActionResult> CommonQueryMethod(object query)
    {
        try
        {
            return Ok(await Mediator.Send(query));
        }
        catch (ApplicationException ex)
        {
            return Error(new[] { ex.Message });
        }
        catch (Exception ex)
        {
#if DEBUG
#else
            Logger.LogCritical(ex.Message);
#endif
            return BadRequest(ex.Message);
        }
    }

    protected ActionResult Error(IEnumerable<string> errors)
    {
        foreach (var item in errors)
            ModelState.AddModelError("errors", item);

        return BadRequest(ModelState);
    }

    protected string RequiredString(string field)
    {
        return string.Format(RequiredField, field);
    }

    protected virtual void Dispose(bool disposing)
    {
        // Cleanup
    }

    ~BaseApiController()
    {
        Dispose(false);
    }
}