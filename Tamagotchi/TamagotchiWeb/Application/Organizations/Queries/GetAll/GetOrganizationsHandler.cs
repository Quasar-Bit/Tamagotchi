using AutoMapper;
using MediatR;
using TamagotchiWeb.Application.Organizations.Base.DTOs;
using TamagotchiWeb.Application.Organizations.Queries.GetAll.DTOs;
using TamagotchiWeb.Data.DataTableProcessing;
using TamagotchiWeb.Data.Repositories.Interfaces;
using TamagotchiWeb.Extensions;

namespace TamagotchiWeb.Application.Organizations.Queries.GetAll
{
    public class GetOrganizationsHandler : IRequestHandler<GetOrganizationsQuery, DtResult<GetOrganization>>
    {
        private readonly IMapper _mapper;
        private readonly IOrganizationRepository _organizationRepository;

        public GetOrganizationsHandler(
            IOrganizationRepository organizationRepository,
            IMapper mapper)
        {
            _organizationRepository = organizationRepository;
            _mapper = mapper;
        }

        public async Task<DtResult<GetOrganization>> Handle(GetOrganizationsQuery request,
            CancellationToken cancellationToken)
        {
            IEnumerable<GetOrganization> organizations;

            var dtParameters = request.DtParameters;

            organizations = _organizationRepository.GetReadOnlyQuery()
                .Select(x => new GetOrganization
                {
                    id = x.id,
                    phone = x.phone,
                    name = x.name,
                    email = x.email,
                    website = x.website,
                    address1 = x.address1,
                    organizationId = x.organizationId
                });

            var total = organizations.Count();

            var searchBy = dtParameters.Search?.Value;

            if (!string.IsNullOrEmpty(searchBy))
                organizations = organizations.Where(s => s.name.ContainsInsensitive(searchBy) ||
                                                         s.email.ContainsInsensitive(searchBy) ||
                                                         s.organizationId.ContainsInsensitive(searchBy) ||
                                                         s.phone.ContainsInsensitive(searchBy) ||
                                                         s.website.ContainsInsensitive(searchBy) ||
                                                         s.address1.ContainsInsensitive(searchBy)
                );

            var orderableProperty = nameof(GetOrganization.id);
            var toOrderAscending = true;
            if (dtParameters.Order != null && dtParameters.Length > 0)
            {
                orderableProperty = dtParameters.Columns[dtParameters.Order.FirstOrDefault().Column].Data.CapitalizeFirst();
                toOrderAscending = dtParameters.Order.FirstOrDefault().Dir == DtOrderDir.Asc;
            }

            //var orderedSubscriptions = toOrderAscending
            //    ? subscriptions.OrderBy(x => x.GetPropertyValue(orderableProperty))
            //    : subscriptions.OrderByDescending(x => x.GetPropertyValue(orderableProperty));

            var result = new DtResult<GetOrganization>
            {
                Draw = dtParameters.Draw,
                RecordsTotal = total,
                RecordsFiltered = organizations.Count(),
                Data = organizations
                .Skip(dtParameters.Start)
                .Take(dtParameters.Length)
            };

            return await Task.FromResult(result);
        }
    }
}