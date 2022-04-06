using TamagotchiWeb.Services.Base;
using TamagotchiWeb.Models;
using TamagotchiWeb.Services.DTOs.OutPut;

namespace TamagotchiWeb.Services
{
    public class AnimalService : BaseService, IAnimalService
    {
        private const string PageSegment = "animals?page={0}&limit=100";
        private const string GetAnimalsSegment = Constants.BaseApiController + PageSegment;

        public async Task<GetAnimals> GetAnimals(int page)
        {
            var result = await MakeApiCall<AnimalsDto>(string.Format(GetAnimalsSegment, page), HttpMethod.Get);

            return await Task.FromResult(MappingInventory(result.Data));
        }

        private GetAnimals MappingInventory(AnimalsDto data = null)
        {
            var content = data.animals.Select(x => new Entities.Animal
            {
                AnimalId = x.id,
                OrganizationId = x.organization_id,
                Url = x.url,
                Type = x.type,
                Species = x.species,
                PrimaryBreed = x.breeds?.primary,
                SecondaryBreed = x.breeds?.secondary,
                IsMixedBreed = x.breeds?.mixed,
                IsUnknownBreed = x.breeds?.unknown,
                PrimaryColor = x.colors?.primary,
                SecondaryColor = x.colors?.secondary,
                TertiaryColor = x.colors?.tertiary,
                Age = x.age,
                Gender = x.gender,
                Size = x.size,
                Coat = x.coat,
                Declawed = x.attributes?.declawed,
                HouseTrained = x.attributes?.house_trained,
                ShotsCurrent = x.attributes?.shots_current,
                SpayedNeutered = x.attributes?.spayed_neutered,
                SpecialNeeds = x.attributes?.special_needs,
                ChildrenEnvinronment = x.environment?.children,
                DogsEnvinronment = x.environment?.dogs,
                CatsEnvinronment = x.environment?.cats,
                Tags = string.Join(",", x.tags),
                Name = x.name,
                Description = x.description,
                OrganizationAnimalId = x.organization_animal_id,
                Photos = string.Join(",", x.photos?.Select(x => x.full)),
                PrimaryPhoto = x.primary_photo_cropped?.full,
                PrimaryIcon = x.primary_photo_cropped?.small,
                Videos = string.Join(",", x.videos?.Select(x => x.ToString())),
                Status = x.status,
                Status_changed_at = x.status_changed_at,
                Published_at = x.published_at,
                Email = x.contact?.email,
                Phone = x.contact?.phone,
                Address1 = x.contact?.address?.address1,
                Address2 = x.contact?.address?.address2,
                City = x.contact?.address?.city,
                Country = x.contact?.address?.country,
                State = x.contact?.address?.state,
                Postcode = x.contact?.address?.postcode
            });

            return new GetAnimals
            {
                Animals = content,
                Pagination = data.pagination
            };
        }
    }
}
