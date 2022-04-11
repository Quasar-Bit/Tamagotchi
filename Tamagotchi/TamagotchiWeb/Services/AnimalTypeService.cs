﻿
using TamagotchiWeb.Services.Base;
using TamagotchiWeb.Models;
using TamagotchiWeb.Services.DTOs.OutPut;
using TamagotchiWeb.Services.Interfaces;
using Tamagotchi.Data.Entities;

namespace TamagotchiWeb.Services
{
    public class AnimalTypeService : BaseService, IAnimalTypeService
    {
        private const string PageSegment = "types";
        private const string GetAnimalTypesSegment = Constants.BaseApiController + PageSegment;

        public async Task<GetAnimalTypes> GetAnimalTypes()
        {
            var result = await MakeApiCall<AnimalTypesDto>(GetAnimalTypesSegment, HttpMethod.Get);

            return await Task.FromResult(MappingInventory(result.Data));
        }

        private GetAnimalTypes MappingInventory(AnimalTypesDto data = null)
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