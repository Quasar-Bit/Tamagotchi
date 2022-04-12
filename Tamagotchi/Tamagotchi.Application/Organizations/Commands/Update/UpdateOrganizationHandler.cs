using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tamagotchi.Application.Base;
using Tamagotchi.Application.Organizations.Base.DTOs;
using Tamagotchi.Application.Organizations.Commands.Create.DTOs;
using Tamagotchi.Application.Organizations.Commands.Update.DTOs;
using Tamagotchi.Data.Repositories.Interfaces;

namespace Tamagotchi.Application.Organizations.Commands.Update;

internal class UpdateOrganizationHandler : BaseRequestHandler, IRequestHandler<UpdateOrganizationCommand, GetOrganization>
{
    private readonly IOrganizationRepository _organizationRepository;

    public UpdateOrganizationHandler(
        IOrganizationRepository organizationRepository,
        IMapper mapper) : base(mapper)
    {
        _organizationRepository = organizationRepository;
    }

    public async Task<GetOrganization> Handle(UpdateOrganizationCommand request,
        CancellationToken cancellationToken)
    {
        var existingOrganization = await _organizationRepository.GetChangeTrackingQuery().FirstOrDefaultAsync(x => x.Id == request.Id, new CancellationToken());

        if (existingOrganization == null)
            return null;

        var result = _organizationRepository.Update(request.Adapt(existingOrganization));

        await _organizationRepository.UnitOfWork.SaveChangesAsync(new CancellationToken());

        return Mapper.Map<GetOrganization>(result);
    }
}