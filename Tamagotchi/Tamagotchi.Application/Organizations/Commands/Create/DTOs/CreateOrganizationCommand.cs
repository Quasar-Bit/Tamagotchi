using MediatR;
using Tamagotchi.Application.Organizations.Base.DTOs;

namespace Tamagotchi.Application.Organizations.Commands.Create.DTOs
{
    public class CreateOrganizationCommand : GetOrganization, IRequest<GetOrganization>
    {
    }
}