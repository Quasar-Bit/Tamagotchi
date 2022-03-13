using AutoMapper;
using MediatR;
using TamagotchiWeb.Application.AnimalTypes.Base.DTOs;
using TamagotchiWeb.Application.AnimalTypes.Quieries.GetAll.DTOs;
using TamagotchiWeb.Data.DataTableProcessing;
using TamagotchiWeb.Data.Repositories.Interfaces;
using TamagotchiWeb.Extensions;

namespace TamagotchiWeb.Application.AnimalTypes.Quieries.GetAll
{
    public class GetAnimalTypesHandler : IRequestHandler<GetAnimalTypesQuery, DtResult<GetAnimalType>>
    {
        private readonly IMapper _mapper;
        private readonly IAnimalTypeRepository _animalTypeRepository;

        public GetAnimalTypesHandler(
            IAnimalTypeRepository animalTypeRepository,
            IMapper mapper)
        {
            _animalTypeRepository = animalTypeRepository;
            _mapper = mapper;
        }

        public async Task<DtResult<GetAnimalType>> Handle(GetAnimalTypesQuery request,
            CancellationToken cancellationToken)
        {
            IEnumerable<GetAnimalType> subscriptions;

            var dtParameters = request.DtParameters;

            var userId = dtParameters.AdditionalValues.ElementAt(0);

            subscriptions = _animalTypeRepository.GetReadOnlyQuery()
                .Select(_mapper.Map<GetAnimalType>);

            var total = subscriptions.Count();

            var searchBy = dtParameters.Search?.Value;

            if (!string.IsNullOrEmpty(searchBy))
                subscriptions = subscriptions.Where(s => s.Colors.ContainsInsensitive(searchBy) ||
                                                         s.Name.ContainsInsensitive(searchBy) ||
                                                         s.Coats.ContainsInsensitive(searchBy) ||
                                                         s.Genders.ContainsInsensitive(searchBy)
                );

            var orderableProperty = nameof(GetAnimalType.Name);
            var toOrderAscending = true;
            if (dtParameters.Order != null && dtParameters.Length > 0)
            {
                orderableProperty = dtParameters.Columns[dtParameters.Order.FirstOrDefault().Column].Data.CapitalizeFirst();
                toOrderAscending = dtParameters.Order.FirstOrDefault().Dir == DtOrderDir.Asc;
            }

            var orderedSubscriptions = toOrderAscending
                ? subscriptions.OrderBy(x => x.GetPropertyValue(orderableProperty))
                : subscriptions.OrderByDescending(x => x.GetPropertyValue(orderableProperty));

            var result = new DtResult<GetAnimalType>
            {
                Draw = dtParameters.Draw,
                RecordsTotal = total,
                RecordsFiltered = orderedSubscriptions.Count(),
                Data = orderedSubscriptions
                .Skip(dtParameters.Start)
                .Take(dtParameters.Length)
            };

            return await Task.FromResult(result);
        }
    }
}
