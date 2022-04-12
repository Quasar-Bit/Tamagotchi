using MediatR;
using Tamagotchi.Application.Animals.Base.DTOs;

namespace Tamagotchi.Application.Animals.Commands.Update.DTOs
{
    public class UpdateAnimalCommand : GetAnimal, IRequest<GetAnimal>
    {
    }
}