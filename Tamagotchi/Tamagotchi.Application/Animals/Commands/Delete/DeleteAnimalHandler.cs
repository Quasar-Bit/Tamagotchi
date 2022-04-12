using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tamagotchi.Application.Animals.Base.DTOs;
using Tamagotchi.Application.Animals.Commands.Delete.DTOs;
using Tamagotchi.Application.Base;
using Tamagotchi.Data.Repositories.Interfaces;

namespace Tamagotchi.Application.Animals.Commands.Delete;

internal class DeleteAnimalHandler : BaseRequestHandler, IRequestHandler<DeleteAnimalCommand, GetAnimal>
{
    private readonly IAnimalRepository _animalRepository;

    public DeleteAnimalHandler(
        IAnimalRepository animalRepository,
        IMapper mapper) : base(mapper)
    {
        _animalRepository = animalRepository;
    }

    public async Task<GetAnimal> Handle(DeleteAnimalCommand request,
        CancellationToken cancellationToken)
    {
        var animal = await _animalRepository.GetChangeTrackingQuery().FirstOrDefaultAsync(x => x.Id == request.Id, new CancellationToken());

        if (animal == null)
            return null;

        _animalRepository.Remove(animal); 

        await _animalRepository.UnitOfWork.SaveChangesAsync(new CancellationToken());

        return Mapper.Map<GetAnimal>(request);
    }
}