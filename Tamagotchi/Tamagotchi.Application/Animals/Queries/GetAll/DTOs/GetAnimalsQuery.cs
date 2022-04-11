using MediatR;
using Tamagotchi.Application.Animals.Base.DTOs;
using Tamagotchi.Data.DataTableProcessing;

namespace Tamagotchi.Application.Animals.Queries.GetAll.DTOs
{
    public class GetAnimalsQuery : IRequest<DtResult<GetAnimal>>
    {
        public DtParameters DtParameters { get; set; }
    }
}