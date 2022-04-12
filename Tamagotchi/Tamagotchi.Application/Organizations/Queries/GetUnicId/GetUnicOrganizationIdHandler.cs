using MapsterMapper;
using MediatR;
using Tamagotchi.Application.Organizations.Queries.GetUnicId.DTOs;
using Tamagotchi.Application.Base;
using Tamagotchi.Data.Repositories.Interfaces;

namespace Tamagotchi.Application.Organizations.Queries.GetUnicId;

internal class GetUnicOrganizationIdHandler : BaseRequestHandler, IRequestHandler<GetUnicOrganizationIdQuery, string>
{
    private readonly IOrganizationRepository _organizationRepository;

    public GetUnicOrganizationIdHandler(
        IOrganizationRepository organizationRepository,
        IMapper mapper) : base(mapper)
    {
        _organizationRepository = organizationRepository;
    }

    public async Task<string> Handle(GetUnicOrganizationIdQuery request,
        CancellationToken cancellationToken)
    {
        var maxIdNumber = _organizationRepository.GetReadOnlyQuery().Max(x => x.Id);

        return await Task.FromResult("NEW" + ++maxIdNumber);
    }
}