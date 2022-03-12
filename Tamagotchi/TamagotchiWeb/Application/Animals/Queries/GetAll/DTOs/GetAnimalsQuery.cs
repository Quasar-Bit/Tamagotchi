using MediatR;
using TamagotchiWeb.Application.Animals.Base.DTOs;
using TamagotchiWeb.Data.DataTableProcessing;

namespace TamagotchiWeb.Application.Animals.Queries.GetAll.DTOs
{
    public class GetAnimalsQuery : IRequest<DtResult<GetAnimal>>
    {
        public DtParameters DtParameters { get; set; }
    }
}