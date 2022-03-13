using MediatR;
using TamagotchiWeb.Application.AnimalTypes.Base.DTOs;
using TamagotchiWeb.Data.DataTableProcessing;

namespace TamagotchiWeb.Application.AnimalTypes.Quieries.GetAll.DTOs
{
    public class GetAnimalTypesQuery : IRequest<DtResult<GetAnimalType>>
    {
        public DtParameters DtParameters { get; set; }
    }
}
