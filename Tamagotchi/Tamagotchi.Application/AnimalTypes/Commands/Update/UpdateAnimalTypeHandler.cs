using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tamagotchi.Application.AnimalTypes.Base.DTOs;
using Tamagotchi.Application.AnimalTypes.Commands.Update.DTOs;
using Tamagotchi.Application.Base;
using Tamagotchi.Data.Repositories.Interfaces;

namespace Tamagotchi.Application.AnimalTypes.Commands.Update;

internal class UpdateAnimalTypeHandler : BaseRequestHandler, IRequestHandler<UpdateAnimalTypeCommand, GetAnimalType>
{
    private readonly IAnimalTypeRepository _animalTypeRepository;

    public UpdateAnimalTypeHandler(
        IAnimalTypeRepository animalTypeRepository,
        IMapper mapper) : base(mapper)
    {
        _animalTypeRepository = animalTypeRepository;
    }

    public async Task<GetAnimalType> Handle(UpdateAnimalTypeCommand request,
        CancellationToken cancellationToken)
    {
        var existingAnimalType = await _animalTypeRepository.GetChangeTrackingQuery().FirstOrDefaultAsync(x => x.Id == request.Id, new CancellationToken());

        if (existingAnimalType == null)
            return null;

        var result = _animalTypeRepository.Update(request.Adapt(existingAnimalType));

        await _animalTypeRepository.UnitOfWork.SaveChangesAsync(new CancellationToken());

        return Mapper.Map<GetAnimalType>(result);
    }
}