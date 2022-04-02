
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TamagotchiWeb.Application.Animals.Base.DTOs;
using TamagotchiWeb.Application.AnimalTypes.Base.DTOs;
using TamagotchiWeb.Application.AnimalTypes.Quieries.GetAll.DTOs;
using TamagotchiWeb.Data.DataTableProcessing;
using TamagotchiWeb.Data.Repositories.Interfaces;
using TamagotchiWeb.Entities;
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
            var dtParameters = request.DtParameters;

            var animalTypes = _animalTypeRepository.GetReadOnlyQuery().Select(x => new GetAnimalType
            {
                Id = x.id,
                Name = x.name,
                Coats = x.coats,
                Colors = x.colors,
                Genders = x.genders
            });

            var total = animalTypes.Count();

            var searchBy = dtParameters.Search?.Value;

            if (!string.IsNullOrEmpty(searchBy))
                animalTypes = animalTypes.Where(s => s.Coats.ContainsInsensitive(searchBy) ||
                                                         s.Colors.ContainsInsensitive(searchBy) ||
                                                         s.Genders.ContainsInsensitive(searchBy) ||
                                                         s.Name.ContainsInsensitive(searchBy)
                );

            //var orderableProperty = nameof(GetAnimalType.Id);
            //var toOrderAscending = true;
            //if (dtParameters.Order != null && dtParameters.Length > 0)
            //{
            //    orderableProperty = dtParameters.Columns[dtParameters.Order.FirstOrDefault().Column].Data.CapitalizeFirst();
            //    toOrderAscending = dtParameters.Order.FirstOrDefault().Dir == DtOrderDir.Asc;
            //}

            //var orderedAnimalTypes = toOrderAscending
            //    ? animalTypes.OrderBy(x => x.GetPropertyValue(orderableProperty))
            //    : animalTypes.OrderByDescending(x => x.GetPropertyValue(orderableProperty));

            var result = new DtResult<GetAnimalType>
            {
                Draw = dtParameters.Draw,
                RecordsTotal = total,
                RecordsFiltered = animalTypes.Count(),
                Data = animalTypes
                .Skip(dtParameters.Start)
                .Take(dtParameters.Length)
            };

            return await Task.FromResult(result);
        }
    }
}
