using MapsterMapper;
using MediatR;
using Tamagotchi.Application.AnimalTypes.Base.DTOs;
using Tamagotchi.Application.AnimalTypes.Commands.Create.DTOs;
using Tamagotchi.Application.Base;
using Tamagotchi.Data.Entities;
using Tamagotchi.Data.Repositories.Interfaces;

namespace Tamagotchi.Application.AnimalTypes.Commands.Create;

internal class CreateAnimalTypeHandler : BaseRequestHandler, IRequestHandler<CreateAnimalTypeCommand, GetAnimalType>
{
    private readonly IAnimalTypeRepository _animalTypeRepository;

    public CreateAnimalTypeHandler(
        IAnimalTypeRepository animalTypeRepository,
        IMapper mapper) : base(mapper)
    {
        _animalTypeRepository = animalTypeRepository;
    }

    public async Task<GetAnimalType> Handle(CreateAnimalTypeCommand request,
        CancellationToken cancellationToken)
    {
        var animalType = Mapper.Map<AnimalType>(request);

        var result = await _animalTypeRepository.AddAsync(animalType);

        await _animalTypeRepository.UnitOfWork.SaveChangesAsync(new CancellationToken());

        return Mapper.Map<GetAnimalType>(result);
    }
}