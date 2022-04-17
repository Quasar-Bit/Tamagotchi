
using MediatR;
using Tamagotchi.Application.Organizations.Base.DTOs;

namespace Tamagotchi.Application.Organizations.Queries.GetAll.DTOs
{
    public class GetAllOrganizationsQuery : IRequest<IEnumerable<GetOrganization>>
    {
    }
}