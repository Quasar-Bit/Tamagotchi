
using MapsterMapper;
using MediatR;
using System.Linq.Expressions;
using Tamagotchi.Application.Base;
using Tamagotchi.Application.Extensions;
using Tamagotchi.Application.Organizations.Base.DTOs;
using Tamagotchi.Application.Organizations.Queries.GetAll.DTOs;
using Tamagotchi.Data.DataTableProcessing;
using Tamagotchi.Data.Repositories.Interfaces;

namespace Tamagotchi.Application.Organizations.Queries.GetAll
{
    internal class GetOrganizationsHandler : BaseRequestHandler, IRequestHandler<GetOrganizationsQuery, DtResult<GetOrganization>>,
        IRequestHandler<GetAllOrganizationsQuery, IEnumerable<GetOrganization>>
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
                .Select(Mapper.Map<GetOrganization>);

            var searchBy = request.DtParameters.Search?.Value;

            Expression<Func<GetOrganization, bool>> filter = x => x.Name.ContainsInsensitive(searchBy) ||
                                                         x.Email.ContainsInsensitive(searchBy) ||
                                                         x.Phone.ContainsInsensitive(searchBy) ||
                                                         x.Website.ContainsInsensitive(searchBy) ||
                                                         x.Address1.ContainsInsensitive(searchBy);

            return await Parametrization(organizations.AsQueryable(), request.DtParameters, filter, nameof(GetOrganization.Website));
        }

        public async Task<IEnumerable<GetOrganization>> Handle(GetAllOrganizationsQuery request,
            CancellationToken cancellationToken)
        {
            return await Task.FromResult(_organizationRepository.GetReadOnlyQuery()
                .Select(Mapper.Map<GetOrganization>));
        }
    }
}