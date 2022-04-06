﻿using TamagotchiWeb.Services.Base;
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
                OrganizationId = x.id,
                Name = x.name,
                Email = x.email,
                Phone = x.phone,
                Monday = x.hours.monday,
                Tuesday = x.hours.tuesday,
                Wednesday = x.hours.wednesday,
                Thursday = x.hours.thursday,
                Friday = x.hours.friday,
                Saturday = x.hours.saturday,
                Sunday = x.hours.sunday,
                Url = x.url,
                Website = x.website,
                Mission_Statement = x.mission_statement,
                AdoptionPolicy = x.adoption.policy,
                AdoptionUrl = x.adoption.url,
                Facebook = x.social_media.facebook,
                Twitter = x.social_media.twitter,
                Youtube = x.social_media.youtube,
                Instagram = x.social_media.instagram,
                Pinterest = x.social_media.pinterest,
                Photos = string.Join(",", x.photos?.Select(x => x.full)),
                PrimaryPhoto = x.photos?.FirstOrDefault()?.full,
                PrimaryIcon = x.photos?.FirstOrDefault()?.small,
                Address1 = x.address?.address1,
                Address2 = x.address?.address2,
                City = x.address?.city,
                State = x.address?.state,
                Postcode = x.address?.postcode,
                Country = x.address?.country
            });

            return new GetOrganizations
            {
                Organizations = content,
                Pagination = data.pagination
            };
        }
    }
}