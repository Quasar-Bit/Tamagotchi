using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Tamagotchi.Api.Controllers.Base;
using MediatR;
using Tamagotchi.Application.AnimalTypes.Quieries.GetAll.DTOs;
using Tamagotchi.Data.DataTableProcessing;
using Tamagotchi.Application.AnimalTypes.Base.DTOs;

namespace Tamagotchi.Api.Controllers.V1.AnimalTypes;

[ApiVersion("1.0")]
[SwaggerTag("Application Api")]
public class AnimalTypes : BaseApiController<AnimalTypes>
{
    private readonly IMapper _mapper;
    public AnimalTypes(
        IMapper mapper,
        IMediator mediator,
        ILogger<AnimalTypes> logger)
        : base(mediator, logger)
    {
        _mapper = mapper;
    }

    [SwaggerOperation(Summary = "Get Animal Types By Different Parameters", Description = "Several settings exist: count per page, etc...")]
    [AllowAnonymous]
    [HttpGet(nameof(GetAnimalTypes))]
    public async Task<IActionResult> GetAnimalTypes()
    {
        return await CommonQueryMethod<DtResult<GetAnimalType>>(new GetAnimalTypesQuery { DtParameters = GetStandardParameters() });
    }
}