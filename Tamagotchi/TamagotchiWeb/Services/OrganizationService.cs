using TamagotchiWeb.Services.Base;
using TamagotchiWeb.Models;
using TamagotchiWeb.Services.DTOs.OutPut;
using TamagotchiWeb.Services.Interfaces;

namespace TamagotchiWeb.Services
{
    public class OrganizationService : BaseService, IOrganizationService
    {
        private const string PageSegment = "organizations?limit=100&page={0}";
        private const string GetOrganizationsSegment = Constants.BaseApiController + PageSegment;

        public async Task<GetOrganizations> GetOrganizations(int page)
        {
            var result = await MakeApiCall<OrganizationsDto>(string.Format(GetOrganizationsSegment, page), HttpMethod.Get);

            return await Task.FromResult(MappingInventory(result.Data));
        }

        private GetOrganizations MappingInventory(OrganizationsDto data = null)
        {
            var content = data.organizations.Select(x => new Entities.Organization
            {
                organizationId = x.id,
                name = x.name,
                email = x.email,
                phone = x.phone,
                monday = x.hours.monday,
                tuesday = x.hours.tuesday,
                wednesday = x.hours.wednesday,
                thursday = x.hours.thursday,
                friday = x.hours.friday,
                saturday = x.hours.saturday,
                sunday = x.hours.sunday,
                url = x.url,
                website = x.website,
                mission_statement = x.mission_statement,
                adoptionPolicy = x.adoption.policy,
                adoptionUrl = x.adoption.url,
                facebook = x.social_media.facebook,
                twitter = x.social_media.twitter,
                youtube = x.social_media.youtube,
                instagram = x.social_media.instagram,
                pinterest = x.social_media.pinterest,
                photos = string.Join(",", x.photos?.Select(x => x.full)),
                primaryPhoto = x.photos?.FirstOrDefault()?.full,
                primaryIcon = x.photos?.FirstOrDefault()?.small,
                address1 = x.address?.address1,
                address2 = x.address?.address2,
                city = x.address?.city,
                state = x.address?.state,
                postcode = x.address?.postcode,
                country = x.address?.country
            });

            return new GetOrganizations
            {
                Organizations = content,
                Pagination = data.pagination
            };
        }
    }
}