using MediatR;
using Tamagotchi.Application.AnimalTypes.Base.DTOs;
using Tamagotchi.Data.DataTableProcessing;

namespace Tamagotchi.Application.AnimalTypes.Quieries.GetAll.DTOs
{
    public class GetAnimalTypesQuery : IRequest<DtResult<GetAnimalType>>
    {
        public DtParameters DtParameters { get; set; }
    }
}
