
using MapsterMapper;
using MediatR;
using System.Linq.Expressions;
using TamagotchiWeb.Application.Base;
using TamagotchiWeb.Application.Organizations.Base.DTOs;
using TamagotchiWeb.Application.Organizations.Queries.GetAll.DTOs;
using TamagotchiWeb.Data.DataTableProcessing;
using TamagotchiWeb.Data.Repositories.Interfaces;
using TamagotchiWeb.Extensions;

namespace TamagotchiWeb.Application.Organizations.Queries.GetAll
{
    public class GetOrganizationsHandler : BaseRequestHandler, IRequestHandler<GetOrganizationsQuery, DtResult<GetOrganization>>
    {
        private readonly IOrganizationRepository _organizationRepository;

        public GetOrganizationsHandler(
            IOrganizationRepository organizationRepository,
            IMapper mapper) : base(mapper)
        {
            _organizationRepository = organizationRepository;
        }

        public async Task<DtResult<GetOrganization>> Handle(GetOrganizationsQuery request,
            CancellationToken cancellationToken)
        {
            var organizations = _organizationRepository.GetReadOnlyQuery()
                .Select(x => Mapper.Map<GetOrganization>(x));

            var searchBy = request.DtParameters.Search?.Value;

            Expression<Func<GetOrganization, bool>> filter = x => x.Name.Contains(searchBy) ||
                                                         x.Email.Contains(searchBy) ||
                                                         x.Phone.Contains(searchBy) ||
                                                         x.Website.Contains(searchBy) ||
                                                         x.Address1.Contains(searchBy);

            return await Parametrization(organizations, request.DtParameters, filter, nameof(GetOrganization.Website));
        }
    }
}