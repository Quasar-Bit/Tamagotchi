using AutoMapper;
using MediatR;
using TamagotchiWeb.Application.Animals.Base.DTOs;
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
            IEnumerable<GetAnimalType> animalTypes;

            var dtParameters = request.DtParameters;

            animalTypes = _animalTypeRepository.GetReadOnlyQuery()
                .Select(x => new GetAnimalType
                {
                    Name = x.name,
                    Coats = x.coats,
                    Colors = x.colors,
                    Genders = x.genders,
                    Id = x.id
                });

            var total = animalTypes.Count();

            var searchBy = dtParameters.Search?.Value;

            if (!string.IsNullOrEmpty(searchBy))
                animalTypes = animalTypes.Where(s => s.Coats.ContainsInsensitive(searchBy) ||
                                                         s.Colors.ContainsInsensitive(searchBy) ||
                                                         s.Genders.ContainsInsensitive(searchBy) ||
                                                         s.Name.ContainsInsensitive(searchBy)
                );

            var orderableProperty = nameof(GetAnimal.AnimalId);
            var toOrderAscending = true;
            if (dtParameters.Order != null && dtParameters.Length > 0)
            {
                orderableProperty = dtParameters.Columns[dtParameters.Order.FirstOrDefault().Column].Data.CapitalizeFirst();
                toOrderAscending = dtParameters.Order.FirstOrDefault().Dir == DtOrderDir.Asc;
            }

            var orderedAnimalTypes = toOrderAscending
                ? animalTypes.OrderBy(x => x.GetPropertyValue(orderableProperty))
                : animalTypes.OrderByDescending(x => x.GetPropertyValue(orderableProperty));

            var result = new DtResult<GetAnimalType>
            {
                Draw = dtParameters.Draw,
                RecordsTotal = total,
                RecordsFiltered = orderedAnimalTypes.Count(),
                Data = orderedAnimalTypes
                .Skip(dtParameters.Start)
                .Take(dtParameters.Length)
            };

            return await Task.FromResult(result);
        }
    }
}
