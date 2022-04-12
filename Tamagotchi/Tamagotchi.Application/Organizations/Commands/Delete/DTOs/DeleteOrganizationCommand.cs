using MediatR;
using Tamagotchi.Application.Organizations.Base.DTOs;

namespace Tamagotchi.Application.Organizations.Commands.Delete.DTOs
{
    public class DeleteOrganizationCommand : IRequest<GetOrganization>
    {
        public int Id { get; set; }
    }
}