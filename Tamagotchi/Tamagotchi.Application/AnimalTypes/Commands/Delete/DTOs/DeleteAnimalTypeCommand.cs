using MediatR;
using Tamagotchi.Application.AnimalTypes.Base.DTOs;

namespace Tamagotchi.Application.AnimalTypes.Commands.Delete.DTOs
{
    public class DeleteAnimalTypeCommand : IRequest<GetAnimalType>
    {
        public int Id { get; set; }
    }
}