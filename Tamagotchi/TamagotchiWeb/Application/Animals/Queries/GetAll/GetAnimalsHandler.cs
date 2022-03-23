
using MapsterMapper;
using MediatR;
using TamagotchiWeb.Application.Animals.Base.DTOs;
using TamagotchiWeb.Application.Animals.Queries.GetAll.DTOs;
using TamagotchiWeb.Data.DataTableProcessing;
using TamagotchiWeb.Data.Repositories.Interfaces;
using TamagotchiWeb.Entities;
using TamagotchiWeb.Extensions;

namespace TamagotchiWeb.Application.Animals.Queries.GetAll;

public class GetAnimalsHandler : IRequestHandler<GetAnimalsQuery, DtResult<GetAnimal>>
{
    private readonly IMapper _mapper;
    private readonly IAnimalRepository _animalRepository;

    public GetAnimalsHandler(
        IAnimalRepository animalRepository,
        IMapper mapper)
    {
        _animalRepository = animalRepository;
        _mapper = mapper;
    }

    public async Task<DtResult<GetAnimal>> Handle(GetAnimalsQuery request,
        CancellationToken cancellationToken)
    {
        IEnumerable<GetAnimal> animals;

        var dtParameters = request.DtParameters;

        animals = _animalRepository.GetReadOnlyQuery().Select(x => _mapper.Map<GetAnimal>(x));
        
        var total = animals.Count();

        var searchBy = dtParameters.Search?.Value;

        if (!string.IsNullOrEmpty(searchBy))
            animals = animals.Where(s => s.Type.ContainsInsensitive(searchBy) ||
                                                     s.Name.ContainsInsensitive(searchBy)
            );

        var orderableProperty = nameof(Animal.animalId);
        var toOrderAscending = true;
        if (dtParameters.Order != null && dtParameters.Length > 0)
        {
            orderableProperty = dtParameters.Columns[dtParameters.Order.FirstOrDefault().Column].Data.CapitalizeFirst();
            toOrderAscending = dtParameters.Order.FirstOrDefault().Dir == DtOrderDir.Asc;
        }

        //var orderedAnimals = toOrderAscending
        //    ? animals.OrderBy(x => x.GetPropertyValue(orderableProperty))
        //    : animals.OrderByDescending(x => x.GetPropertyValue(orderableProperty));

        //var answer = animals.Select(x => MappSubscription(x));

        var result = new DtResult<GetAnimal>
        {
            Draw = dtParameters.Draw,
            RecordsTotal = total,
            RecordsFiltered = animals.Count(),
            Data = animals
            .Skip(dtParameters.Start)
            .Take(dtParameters.Length)
        };

        //Mapping if would needed

        return await Task.FromResult(result);
    }

    //private GetAnimal MappSubscription(GetAnimal x)
    //{
    //    return new GetAnimal
    //    {
    //        Name = x.name,
    //        Type = x.type,
    //        AnimalId = x.animalId,
    //        Id = x.id
    //    };
    //}
}


//animals = _animalRepository.GetReadOnlyQuery().Select(x => new GetAnimal 
//        {
//            Name = x.name, 
//            Type = x.type, 
//            AnimalId = x.animalId, 
//            Id = x.id,
//            OrganizationId = x.organizationId,
//            PrimaryBreed = x.primaryBreed,
//            Age = x.age,
//            Gender = x.gender,
//            PrimaryColor = x.primaryColor
//        });