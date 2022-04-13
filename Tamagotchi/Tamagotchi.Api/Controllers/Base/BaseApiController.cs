using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tamagotchi.Api.Models;
using Tamagotchi.Api.Models.Base;
using Tamagotchi.Data.DataTableProcessing;
using ApplicationException = Tamagotchi.Application.Exceptions.ApplicationException;

namespace Tamagotchi.Api.Controllers.Base;

[Authorize]
[Route("api/v1/[controller]")]
[ApiController]
public abstract class BaseApiController<T> : ControllerBase, IDisposable
{
    private const string RequiredField = "The '{0}' field is required.";

    protected BaseApiController(IMediator mediator, ILogger<T> logger)
    {
        Mediator = mediator;
        Logger = logger;
    }

    protected ILogger<T> Logger { get; }
    protected IMediator Mediator { get; }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected async Task<IActionResult> CommonQueryMethod<TResult>(object query)
    {
        var answer = new ResultResponse<TResult>();
        try
        {
            var result = await Mediator.Send(query);
            answer.Model = (TResult)result;

            return Ok(answer);
        }
        catch (ApplicationException ex)
        {
            answer.Error = ex.Message;
            return BadRequest(answer);
        }
        catch (Exception ex)
        {
#if !DEBUG
            Logger.LogCritical(ex.Message);
#endif
            answer.Error = ex.Message;
            return BadRequest(answer);
        }
    }

    protected ActionResult Error(IEnumerable<string> errors)
    {
        foreach (var item in errors)
            ModelState.AddModelError("errors", item);

        return BadRequest(ModelState);
    }

    protected DtParameters GetStandardParameters()
    {
        return new DtParameters
        {
            Start = 0,
            Draw = 1,
            Length = 10,
            Search = new DtSearch(),
            Order = new List<DtOrder> { new DtOrder { Column = 0, Dir = DtOrderDir.Asc } }.ToArray(),
            AdditionalValues = new List<string> { string.Empty }.AsEnumerable(),
            Columns = new List<DtColumn>
            {
                new DtColumn { Searchable = true, Orderable = true, Data = "id", Search = new DtSearch() },
                new DtColumn { Searchable = true, Orderable = true, Data = "name", Search = new DtSearch() },
                new DtColumn { Searchable = true, Orderable = true, Data = "type", Search = new DtSearch() },
                new DtColumn { Searchable = true, Orderable = true, Data = "primaryBreed", Search = new DtSearch() },
                new DtColumn { Searchable = true, Orderable = true, Data = "gender", Search = new DtSearch() },
                new DtColumn { Searchable = true, Orderable = true, Data = "age", Search = new DtSearch() },
                new DtColumn { Searchable = true, Orderable = true, Data = "primaryColor", Search = new DtSearch() },
                new DtColumn { Searchable = true, Orderable = true, Data = "organizationId", Search = new DtSearch() }
            }.ToArray()
        };
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