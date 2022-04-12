using MapsterMapper;
using MediatR;
using Tamagotchi.Application.Base;
using Tamagotchi.Application.Organizations.Base.DTOs;
using Tamagotchi.Application.Organizations.Commands.Create.DTOs;
using Tamagotchi.Data.Entities;
using Tamagotchi.Data.Repositories.Interfaces;

namespace Tamagotchi.Application.Organizations.Commands.Create;

internal class CreateOrganizationHandler : BaseRequestHandler, IRequestHandler<CreateOrganizationCommand, GetOrganization>
{
    private readonly IOrganizationRepository _organizationRepository;

    public CreateOrganizationHandler(
        IOrganizationRepository organizationRepository,
        IMapper mapper) : base(mapper)
    {
        _organizationRepository = organizationRepository;
    }

    public async Task<GetOrganization> Handle(CreateOrganizationCommand request,
        CancellationToken cancellationToken)
    {
        var organization = Mapper.Map<Organization>(request);

        var result = await _organizationRepository.AddAsync(organization);

        await _organizationRepository.UnitOfWork.SaveChangesAsync(new CancellationToken());

        return Mapper.Map<GetOrganization>(result);
    }
}