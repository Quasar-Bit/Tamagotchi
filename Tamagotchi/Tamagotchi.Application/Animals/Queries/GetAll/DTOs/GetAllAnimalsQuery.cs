using MediatR;
using Tamagotchi.Application.Animals.Base.DTOs;

namespace Tamagotchi.Application.Animals.Queries.GetAll.DTOs
{
    public class GetAllAnimalsQuery : IRequest<IEnumerable<GetAnimal>>
    {
    }
}