using MapsterMapper;
using MediatR;
using Tamagotchi.Application.Animals.Base.DTOs;
using Tamagotchi.Application.Animals.Commands.Create.DTOs;
using Tamagotchi.Application.Base;
using Tamagotchi.Data.Entities;
using Tamagotchi.Data.Repositories.Interfaces;

namespace Tamagotchi.Application.Animals.Commands.Create;

internal class CreateAnimalHandler : BaseRequestHandler, IRequestHandler<CreateAnimalCommand, GetAnimal>
{
    private readonly IAnimalRepository _animalRepository;

    public CreateAnimalHandler(
        IAnimalRepository animalRepository,
        IMapper mapper) : base(mapper)
    {
        _animalRepository = animalRepository;
    }

    public async Task<GetAnimal> Handle(CreateAnimalCommand request,
        CancellationToken cancellationToken)
    {
        var animal = Mapper.Map<Animal>(request);

        var result = await _animalRepository.AddAsync(animal);

        await _animalRepository.UnitOfWork.SaveChangesAsync(new CancellationToken());

        return Mapper.Map<GetAnimal>(result);
    }
}