using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tamagotchi.Application.AnimalTypes.Base.DTOs;
using Tamagotchi.Application.AnimalTypes.Commands.Delete.DTOs;
using Tamagotchi.Application.Base;
using Tamagotchi.Data.Repositories.Interfaces;

namespace Tamagotchi.Application.AnimalTypes.Commands.Delete;

internal class DeleteAnimalTypeHandler : BaseRequestHandler, IRequestHandler<DeleteAnimalTypeCommand, GetAnimalType>
{
    private readonly IAnimalTypeRepository _animalTypeRepository;

    public DeleteAnimalTypeHandler(
        IAnimalTypeRepository animalTypeRepository,
        IMapper mapper) : base(mapper)
    {
        _animalTypeRepository = animalTypeRepository;
    }

    public async Task<GetAnimalType> Handle(DeleteAnimalTypeCommand request,
        CancellationToken cancellationToken)
    {
        var animalType = await _animalTypeRepository.GetChangeTrackingQuery().FirstOrDefaultAsync(x => x.Id == request.Id, new CancellationToken());

        if (animalType == null)
            return null;

        _animalTypeRepository.Remove(animalType);

        await _animalTypeRepository.UnitOfWork.SaveChangesAsync(new CancellationToken());

        return Mapper.Map<GetAnimalType>(request);
    }
}