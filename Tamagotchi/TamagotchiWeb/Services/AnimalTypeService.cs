
using TamagotchiWeb.Services.Base;
using TamagotchiWeb.Models;
using TamagotchiWeb.Services.DTOs.OutPut;
using TamagotchiWeb.Services.Interfaces;
using Tamagotchi.Data.Entities;
using MediatR;
using Tamagotchi.Application.Settings.Queries.GetAll.DTOs;

namespace TamagotchiWeb.Services
{
    public class AnimalTypeService : BaseService, IAnimalTypeService
    {
        private const string PageSegment = "types";
        private const string GetAnimalTypesSegment = Constants.BaseApiController + PageSegment;

        private readonly IMediator _mediator;
        public AnimalTypeService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<GetAnimalTypes> GetAnimalTypes()
        {
            var token = await _mediator.Send(new GetAppSettingsQuery { Name = "PetFinderToken" });

            var result = await MakeApiCall<AnimalTypesDto>(GetAnimalTypesSegment, HttpMethod.Get, token?.Value);

            return await Task.FromResult(MappingInventory(result.Data));
        }

        private static GetAnimalTypes MappingInventory(AnimalTypesDto data = null)
        {
            var content = data.types.Select(x => new AnimalType
            {
                Name = x.name,
                Coats = string.Join(",", x.coats),
                Colors = string.Join(",", x.colors),
                Genders = string.Join(",", x.genders),
            });

            return new GetAnimalTypes
            {
                AnimalTypes = content,
            };
        }
    }
}