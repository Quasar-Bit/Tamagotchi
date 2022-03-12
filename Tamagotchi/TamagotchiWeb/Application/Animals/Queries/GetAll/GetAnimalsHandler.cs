
using MapsterMapper;
using MediatR;
using TamagotchiWeb.Application.Animals.Base.DTOs;
using TamagotchiWeb.Application.Animals.Queries.GetAll.DTOs;
using TamagotchiWeb.Data.DataTableProcessing;
using TamagotchiWeb.Data.Repositories.Interfaces;
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
        IEnumerable<GetAnimal> subscriptions;

        var dtParameters = request.DtParameters;

        var userId = dtParameters.AdditionalValues.ElementAt(0);

        subscriptions = _animalRepository.GetReadOnlyQuery()
            .Select(_mapper.Map<GetAnimal>);

        var total = subscriptions.Count();

        var searchBy = dtParameters.Search?.Value;

        if (!string.IsNullOrEmpty(searchBy))
            subscriptions = subscriptions.Where(s => s.Type.ContainsInsensitive(searchBy) ||
                                                     s.Name.ContainsInsensitive(searchBy)
            );

        var orderableProperty = nameof(GetAnimal.Name);
        var toOrderAscending = true;
        if (dtParameters.Order != null && dtParameters.Length > 0)
        {
            orderableProperty = dtParameters.Columns[dtParameters.Order.FirstOrDefault().Column].Data.CapitalizeFirst();
            toOrderAscending = dtParameters.Order.FirstOrDefault().Dir == DtOrderDir.Asc;
        }

        var orderedSubscriptions = toOrderAscending
            ? subscriptions.OrderBy(x => x.GetPropertyValue(orderableProperty))
            : subscriptions.OrderByDescending(x => x.GetPropertyValue(orderableProperty));

        var result = new DtResult<GetAnimal>
        {
            Draw = dtParameters.Draw,
            RecordsTotal = total,
            RecordsFiltered = orderedSubscriptions.Count(),
            Data = orderedSubscriptions
            .Skip(dtParameters.Start)
            .Take(dtParameters.Length)
        };

        //Mapping if would needed
        //result.Data = result.Data.Select(x => MappSubscription(x, _userRepository.GetReadOnlyQuery().FirstOrDefault(y => y.Id == x.UserId)?.Email));

        return await Task.FromResult(result);
    }

    //private GetAnimal MappSubscription(GetAnimal x, string email)
    //{
    //    x.UserEmail = email;
    //    return x;
    //}
}