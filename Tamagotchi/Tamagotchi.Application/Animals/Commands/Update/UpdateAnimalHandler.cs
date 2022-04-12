using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tamagotchi.Application.Animals.Base.DTOs;
using Tamagotchi.Application.Animals.Commands.Update.DTOs;
using Tamagotchi.Application.Base;
using Tamagotchi.Data.Repositories.Interfaces;

namespace Tamagotchi.Application.Animals.Commands.Update;

internal class UpdateAnimalHandler : BaseRequestHandler, IRequestHandler<UpdateAnimalCommand, GetAnimal>
{
    private readonly IAnimalRepository _animalRepository;

    public UpdateAnimalHandler(
        IAnimalRepository animalRepository,
        IMapper mapper) : base(mapper)
    {
        _animalRepository = animalRepository;
    }

    public async Task<GetAnimal> Handle(UpdateAnimalCommand request,
        CancellationToken cancellationToken)
    {
        var existingAnimal = await _animalRepository.GetChangeTrackingQuery().FirstOrDefaultAsync(x => x.Id == request.Id, new CancellationToken());

        if (existingAnimal == null)
            return null;

        var result = _animalRepository.Update(request.Adapt(existingAnimal));

        await _animalRepository.UnitOfWork.SaveChangesAsync(new CancellationToken());

        return Mapper.Map<GetAnimal>(result);
    }
}