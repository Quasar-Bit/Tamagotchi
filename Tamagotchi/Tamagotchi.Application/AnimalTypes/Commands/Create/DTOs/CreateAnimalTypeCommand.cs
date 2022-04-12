using MediatR;
using Tamagotchi.Application.AnimalTypes.Base.DTOs;

namespace Tamagotchi.Application.AnimalTypes.Commands.Create.DTOs
{
    public class CreateAnimalTypeCommand : GetAnimalType, IRequest<GetAnimalType>
    {
    }
}