using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Tamagotchi.Api.Controllers.Base;
using Tamagotchi.Application.Animals.Queries.GetAll.DTOs;
using MediatR;
using Tamagotchi.Application.Animals.Base.DTOs;
using Tamagotchi.Data.DataTableProcessing;

namespace Tamagotchi.Api.Controllers.V1.Animals;

[ApiVersion("1.0")]
[SwaggerTag("Application Api")]
public class Animals : BaseApiController<Animals>
{
    private readonly IMapper _mapper;
    public Animals(
        IMapper mapper,
        IMediator mediator,
        ILogger<Animals> logger)
        : base(mediator, logger)
    {
        _mapper = mapper;
    }

    [SwaggerOperation(Summary = "Get Animals By Different Parameters", Description = "Several settings exist: count per page, etc...")]
    [AllowAnonymous]
    [HttpGet(nameof(GetAnimals))]
    public async Task<IActionResult> GetAnimals()
    {
        return await CommonQueryMethod<DtResult<GetAnimal>>(new GetAnimalsQuery { DtParameters = GetStandardParameters() });
    }
}