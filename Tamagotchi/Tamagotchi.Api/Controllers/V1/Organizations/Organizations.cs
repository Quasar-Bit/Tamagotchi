using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Tamagotchi.Api.Controllers.Base;
using MediatR;
using Tamagotchi.Application.Organizations.Queries.GetAll.DTOs;
using Tamagotchi.Application.Organizations.Base.DTOs;
using Tamagotchi.Data.DataTableProcessing;

namespace Tamagotchi.Api.Controllers.V1.Organizations;

[ApiVersion("1.0")]
[SwaggerTag("Application Api")]
public class Organizations : BaseApiController<Organizations>
{
    private readonly IMapper _mapper;
    public Organizations(
        IMapper mapper,
        IMediator mediator,
        ILogger<Organizations> logger)
        : base(mediator, logger)
    {
        _mapper = mapper;
    }

    [SwaggerOperation(Summary = "Get Organizations By Different Parameters", Description = "Several settings exist: count per page, etc...")]
    [AllowAnonymous]
    [HttpGet(nameof(GetOrganizations))]
    
    public async Task<IActionResult> GetOrganizations()
    {
        return await CommonQueryMethod<DtResult<GetOrganization>>(new GetOrganizationsQuery { DtParameters = GetStandardParameters() });
    }
}