
using MapsterMapper;
using MediatR;
using System.Linq.Expressions;
using Tamagotchi.Application.Base;
using Tamagotchi.Application.Organizations.Base.DTOs;
using Tamagotchi.Application.Organizations.Queries.GetAll.DTOs;
using Tamagotchi.Data.DataTableProcessing;
using Tamagotchi.Data.Repositories.Interfaces;

namespace Tamagotchi.Application.Organizations.Queries.GetAll
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