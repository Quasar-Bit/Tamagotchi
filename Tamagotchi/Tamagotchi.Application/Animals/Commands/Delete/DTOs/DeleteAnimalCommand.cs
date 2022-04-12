using MediatR;
using Tamagotchi.Application.Animals.Base.DTOs;

namespace Tamagotchi.Application.Animals.Commands.Delete.DTOs
{
    public class DeleteAnimalCommand : IRequest<GetAnimal>
    {
        public int Id { get; set; }
    }
}