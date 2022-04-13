using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Tamagotchi.Api.Controllers.Base;
using Tamagotchi.Application.Animals.Queries.GetAll.DTOs;
using MediatR;
using Tamagotchi.Application.Animals.Base.DTOs;
using Tamagotchi.Data.DataTableProcessing;
using Tamagotchi.Application.Animals.Commands.Create.DTOs;
using Tamagotchi.Application.Animals.Commands.Delete.DTOs;
using Tamagotchi.Application.Animals.Commands.Update.DTOs;

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

    [SwaggerOperation(Summary = "Create Animal", Description = "Type and Name are required")]
    [AllowAnonymous]
    [HttpPost(nameof(CreateAnimal))]
    public async Task<IActionResult> CreateAnimal([FromBody] CreateAnimalCommand query)
    {
        return await CommonQueryMethod<GetAnimal>(query);
    }

    [SwaggerOperation(Summary = "Update Animal", Description = "Type and Name are required")]
    [AllowAnonymous]
    [HttpPost(nameof(UpdateAnimal))]
    public async Task<IActionResult> UpdateAnimal([FromBody] UpdateAnimalCommand query)
    {
        return await CommonQueryMethod<GetAnimal>(query);
    }

    [SwaggerOperation(Summary = "Delete Animal", Description = "Id is required")]
    [AllowAnonymous]
    [HttpPost(nameof(DeleteAnimal))]
    public async Task<IActionResult> DeleteAnimal([FromBody] DeleteAnimalCommand query)
    {
        return await CommonQueryMethod<GetAnimal>(query);
    }
}