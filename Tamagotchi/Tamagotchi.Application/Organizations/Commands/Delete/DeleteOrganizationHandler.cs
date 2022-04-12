using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tamagotchi.Application.Base;
using Tamagotchi.Application.Organizations.Base.DTOs;
using Tamagotchi.Application.Organizations.Commands.Delete.DTOs;
using Tamagotchi.Data.Repositories.Interfaces;

namespace Tamagotchi.Application.Organizations.Commands.Delete;

internal class DeleteOrganizationHandler : BaseRequestHandler, IRequestHandler<DeleteOrganizationCommand, GetOrganization>
{
    private readonly IOrganizationRepository _organizationRepository;

    public DeleteOrganizationHandler(
        IOrganizationRepository organizationRepository,
        IMapper mapper) : base(mapper)
    {
        _organizationRepository = organizationRepository;
    }

    public async Task<GetOrganization> Handle(DeleteOrganizationCommand request,
        CancellationToken cancellationToken)
    {
        var animalType = await _organizationRepository.GetChangeTrackingQuery().FirstOrDefaultAsync(x => x.Id == request.Id, new CancellationToken());

        if (animalType == null)
            return null;

        _organizationRepository.Remove(animalType);

        await _organizationRepository.UnitOfWork.SaveChangesAsync(new CancellationToken());

        return Mapper.Map<GetOrganization>(request);
    }
}