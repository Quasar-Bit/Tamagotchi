using MediatR;
using Tamagotchi.Application.Animals.Base.DTOs;

namespace Tamagotchi.Application.Animals.Commands.Create.DTOs
{
    public class CreateAnimalCommand : GetAnimal, IRequest<GetAnimal>
    {
    }
}