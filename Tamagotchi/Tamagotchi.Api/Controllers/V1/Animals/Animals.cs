using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Tamagotchi.Api.Controllers.Base;
using Tamagotchi.Application.Animals.Queries.GetAll.DTOs;
using Tamagotchi.Data.DataTableProcessing;
using MediatR;
using Tamagotchi.Application.AnimalTypes.Quieries.GetAll.DTOs;
using Tamagotchi.Application.Animals.Base.DTOs;
using Tamagotchi.Api.Models;

namespace Tamagotchi.Api.Controllers.V1.Animals;

[ApiVersion("1.0")]
[SwaggerTag("Application Api")]
public class Animals : BaseApiController<Animals>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    public Animals(
        IMapper mapper,
        IMediator mediator,
        ILogger<Animals> logger)
        : base(logger)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [SwaggerOperation(Summary = "Get Animals By Different Parameters")]
    [AllowAnonymous]
    [HttpGet(nameof(GetAnimals))]
    public async Task<IActionResult> GetAnimals()
    {
        var answer = new ResultResponse<IEnumerable<GetAnimal>>();
        try
        {
            var result = await _mediator.Send(new GetAnimalsQuery { DtParameters = GetStandardParameters() });
            answer.Model = result.Data;
            return new JsonResult(answer);
        }
        catch (Exception ex)
        {
            answer.Error = ex.Message;
            return new JsonResult(answer);
        }
    }

    #region Local Methods

    private DtParameters GetStandardParameters()
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

    #endregion
}