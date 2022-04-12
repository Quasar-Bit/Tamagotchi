using MapsterMapper;
using MediatR;
using Tamagotchi.Application.Animals.Queries.GetUnicId.DTOs;
using Tamagotchi.Application.Base;
using Tamagotchi.Data.Repositories.Interfaces;

namespace Tamagotchi.Application.Animals.Queries.GetUnicId;

internal class GetUnicIdHandler : BaseRequestHandler, IRequestHandler<GetUnicIdQuery, int>
{
    private readonly IAnimalRepository _animalRepository;

    public GetUnicIdHandler(
        IAnimalRepository animalRepository,
        IMapper mapper) : base(mapper)
    {
        _animalRepository = animalRepository;
    }

    public async Task<int> Handle(GetUnicIdQuery request,
        CancellationToken cancellationToken)
    {
        var maxIdNumber = _animalRepository.GetReadOnlyQuery().Max(x => x.AnimalId);

        return await Task.FromResult((int)maxIdNumber + 1);
    }
}