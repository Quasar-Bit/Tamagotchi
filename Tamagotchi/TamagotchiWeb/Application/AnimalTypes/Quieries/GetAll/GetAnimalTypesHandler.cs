﻿
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Linq.Expressions;
using TamagotchiWeb.Application.AnimalTypes.Base.DTOs;
using TamagotchiWeb.Application.AnimalTypes.Quieries.GetAll.DTOs;
using TamagotchiWeb.Application.Base;
using TamagotchiWeb.Data.DataTableProcessing;
using TamagotchiWeb.Data.Repositories.Interfaces;
using TamagotchiWeb.Extensions;

namespace TamagotchiWeb.Application.AnimalTypes.Quieries.GetAll
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
            var dtParameters = request.DtParameters;

            var animalTypes = _animalTypeRepository.GetReadOnlyQuery().Select(x => new GetAnimalType
            {
                Id = x.id,
                Name = x.name,
                Coats = x.coats,
                Colors = x.colors,
                Genders = x.genders
            });

            var searchBy = dtParameters.Search?.Value;

            Expression<Func<GetAnimalType, bool>> filter = x => x.Coats.ContainsInsensitive(searchBy) ||
                                                         x.Colors.ContainsInsensitive(searchBy) ||
                                                         x.Genders.ContainsInsensitive(searchBy) ||
                                                         x.Name.ContainsInsensitive(searchBy);

            return await Parametrization(animalTypes, request.DtParameters, filter, nameof(GetAnimalType.Name));
        }
    }
}
