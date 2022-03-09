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
                id = x.id,
                organizationId = x.organization_id,
                url = x.url,
                type = x.type,
                species = x.species,
                primaryBreed = x.breeds?.primary,
                secondaryBreed = x.breeds?.secondary,
                isMixedBreed = x.breeds.mixed,
                isUnknownBreed = x.breeds.unknown,
                primaryColor = x.colors?.primary,
                secondaryColor = x.colors?.secondary,
                tertiaryColor = x.colors?.tertiary,
                age = x.age,
                gender = x.gender,
                size = x.size,
                coat = x.coat,
                //attributeId = x.attributes
                childrenEnvinronment = x.environment?.children,
                dogsEnvinronment = x.environment?.dogs,
                catsEnvinronment = x.environment?.cats,
                tags = string.Join(",", x.tags),
                name = x.name,
                description = x.description,
                organizationAnimalId = x.organization_animal_id,
                photos = string.Join(",", x.photos?.Select(x => x.full)),
                primaryPhoto = x.primary_photo_cropped?.full,
                primaryIcon = x.primary_photo_cropped?.small,
                videos = string.Join(",", x.videos?.Select(x => x.ToString())),
                status = x.status,
                status_changed_at = x.status_changed_at,
                published_at = x.published_at,
                email = x.contact?.email,
                phone = x.contact?.phone,
                address1 = x.contact?.address?.address1,
                address2 = x.contact?.address?.address2,
                city = x.contact?.address?.city,
                country = x.contact?.address?.country,
                state = x.contact?.address?.state,
                postcode = x.contact?.address?.postcode
            });

            return new GetAnimals
            {
                Animals = content,
                Pagination = data.pagination
            };
        }
    }
}
