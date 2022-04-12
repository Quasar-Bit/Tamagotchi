
using MapsterMapper;
using MediatR;
using System.Linq.Expressions;
using Tamagotchi.Application.Animals.Base.DTOs;
using Tamagotchi.Application.Animals.Queries.GetAll.DTOs;
using Tamagotchi.Application.Base;
using Tamagotchi.Data.DataTableProcessing;
using Tamagotchi.Data.Repositories.Interfaces;

namespace Tamagotchi.Application.Animals.Queries.GetAll;

public class GetAnimalsHandler : BaseRequestHandler, IRequestHandler<GetAnimalsQuery, DtResult<GetAnimal>>
{
    private readonly IAnimalRepository _animalRepository;

    public GetAnimalsHandler(
        IAnimalRepository animalRepository,
        IMapper mapper) : base(mapper)
    {
        _animalRepository = animalRepository;
    }

    public async Task<DtResult<GetAnimal>> Handle(GetAnimalsQuery request,
        CancellationToken cancellationToken)
    {
        //Best performance way to handle(getting and searching) 200k notes
        var animals = _animalRepository.GetReadOnlyQuery().Select(x => new GetAnimal
        {
            Id = x.Id,
            AnimalId = x.AnimalId,
            Name = x.Name,
            Type = x.Type,
            Age = x.Age,
            Gender = x.Gender,
            PrimaryBreed = x.PrimaryBreed,
            PrimaryColor = x.PrimaryColor,
            OrganizationId = x.OrganizationId
        });

        var searchBy = request.DtParameters.Search?.Value;

        Expression<Func<GetAnimal, bool>> filter = x => x.Type.Contains(searchBy) ||
                                                     x.Name.Contains(searchBy) ||
                                                     x.Gender.Contains(searchBy) ||
                                                     x.PrimaryBreed.Contains(searchBy) ||
                                                     x.OrganizationId.Contains(searchBy);

        return await Parametrization(animals, request.DtParameters, filter, nameof(GetAnimal.Name));
    }
}