using MediatR;
using Tamagotchi.Application.Organizations.Base.DTOs;
using Tamagotchi.Data.DataTableProcessing;

namespace Tamagotchi.Application.Organizations.Queries.GetAll.DTOs
{
    public class GetOrganizationsQuery : IRequest<DtResult<GetOrganization>>
    {
        public DtParameters DtParameters { get; set; }
    }
}