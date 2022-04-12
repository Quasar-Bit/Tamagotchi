using MediatR;
using Tamagotchi.Application.Organizations.Base.DTOs;

namespace Tamagotchi.Application.Organizations.Commands.Update.DTOs
{
    public class UpdateOrganizationCommand : GetOrganization, IRequest<GetOrganization>
    {
    }
}