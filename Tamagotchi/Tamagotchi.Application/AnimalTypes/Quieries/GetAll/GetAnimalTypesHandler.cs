
using MapsterMapper;
using MediatR;
using System.Linq.Expressions;
using Tamagotchi.Application.AnimalTypes.Base.DTOs;
using Tamagotchi.Application.AnimalTypes.Quieries.GetAll.DTOs;
using Tamagotchi.Application.Base;
using Tamagotchi.Application.Extensions;
using Tamagotchi.Data.DataTableProcessing;
using Tamagotchi.Data.Repositories.Interfaces;

namespace Tamagotchi.Application.AnimalTypes.Quieries.GetAll
{
    public class GetAnimalTypesHandler : BaseRequestHandler, IRequestHandler<GetAnimalTypesQuery, DtResult<GetAnimalType>>
    {
        private readonly IAnimalTypeRepository _animalTypeRepository;

        public GetAnimalTypesHandler(
            IAnimalTypeRepository animalTypeRepository,
            IMapper mapper) : base(mapper)
        {
            _animalTypeRepository = animalTypeRepository;
        }

        public async Task<DtResult<GetAnimalType>> Handle(GetAnimalTypesQuery request,
            CancellationToken cancellationToken)
        {
            var animalTypes = _animalTypeRepository.GetReadOnlyQuery().Select(Mapper.Map<GetAnimalType>);

            var searchBy = request.DtParameters.Search?.Value;

            Expression<Func<GetAnimalType, bool>> filter = x => x.Coats.ContainsInsensitive(searchBy) ||
                                                         x.Colors.ContainsInsensitive(searchBy) ||
                                                         x.Genders.ContainsInsensitive(searchBy) ||
                                                         x.Name.ContainsInsensitive(searchBy);

            return await Parametrization(animalTypes.AsQueryable(), request.DtParameters, filter, nameof(GetAnimalType.Name));
        }
    }
}