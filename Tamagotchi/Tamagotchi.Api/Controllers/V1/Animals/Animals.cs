
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Tamagotchi.Api.Controllers.Base;
using TamagotchiWeb.Application.Animals.Queries.GetAll.DTOs;

namespace Tamagotchi.Api.Controllers.V1.Animals;

[ApiVersion("1.0")]
[SwaggerTag("Application Api")]
public class Animals : BaseApiController<Animals>
{
    public Animals(IMediator mediator, ILogger<Animals> logger)
        : base(mediator, logger)
    {
    }

    [SwaggerOperation(Summary = "Get Animals By Different Parameters")]
    [AllowAnonymous]
    [HttpGet(nameof(GetAnimals))]
    public async Task<IActionResult> GetAnimals([FromQuery] GetAnimalsQuery query)
    {
        return await CommonQueryMethod(query);
    }
}