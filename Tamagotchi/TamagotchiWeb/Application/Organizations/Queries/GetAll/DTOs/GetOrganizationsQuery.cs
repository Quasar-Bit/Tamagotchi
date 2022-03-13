using MediatR;
using TamagotchiWeb.Application.Organizations.Base.DTOs;
using TamagotchiWeb.Data.DataTableProcessing;

namespace TamagotchiWeb.Application.Organizations.Queries.GetAll.DTOs
{
    public class GetOrganizationsQuery : IRequest<DtResult<GetOrganization>>
    {
        public DtParameters DtParameters { get; set; }
    }
}