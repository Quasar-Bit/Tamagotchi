using MediatR;
using Tamagotchi.Application.AnimalTypes.Base.DTOs;

namespace Tamagotchi.Application.AnimalTypes.Commands.Update.DTOs
{
    public class UpdateAnimalTypeCommand : GetAnimalType, IRequest<GetAnimalType>
    {
    }
}