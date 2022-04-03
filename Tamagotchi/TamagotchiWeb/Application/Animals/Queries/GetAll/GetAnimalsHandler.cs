
using MapsterMapper;
using MediatR;
using System.Linq.Expressions;
using TamagotchiWeb.Application.Animals.Base.DTOs;
using TamagotchiWeb.Application.Animals.Queries.GetAll.DTOs;
using TamagotchiWeb.Application.Base;
using TamagotchiWeb.Data.DataTableProcessing;
using TamagotchiWeb.Data.Repositories.Interfaces;
using TamagotchiWeb.Extensions;

namespace TamagotchiWeb.Application.Animals.Queries.GetAll;

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
        var animals = _animalRepository.GetReadOnlyQuery().Select(x => new GetAnimal
        {
            Id = x.id,
            AnimalId = x.animalId,
            Name = x.name,
            Type = x.type,
            Age = x.age,
            Gender = x.gender,
            PrimaryBreed = x.primaryBreed,
            PrimaryColor = x.primaryColor,
            OrganizationId = x.organizationId
        });

        var searchBy = request.DtParameters.Search?.Value;

        Expression<Func<GetAnimal, bool>> filter = x => x.Type.ContainsInsensitive(searchBy) ||
                                                     x.Name.ContainsInsensitive(searchBy) ||
                                                     x.Gender.ContainsInsensitive(searchBy) ||
                                                     x.PrimaryBreed.ContainsInsensitive(searchBy) ||
                                                     x.OrganizationId.ContainsInsensitive(searchBy);

        return await Parametrization(animals, request.DtParameters, filter, nameof(GetAnimal.Name));
    }
}