
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Tamagotchi.Api.Controllers.Base;
using Tamagotchi.Application.Animals.Base.DTOs;
using Tamagotchi.Application.Animals.Queries.GetAll.DTOs;
using Tamagotchi.Data.DataTableProcessing;
using Tamagotchi.Data.Repositories.Interfaces;
using Tamagotchi.Application.Extensions;

namespace Tamagotchi.Api.Controllers.V1.Animals;

[ApiVersion("1.0")]
[SwaggerTag("Application Api")]
public class Animals : BaseApiController<Animals>
{
    private readonly IAnimalRepository _animalRepository;
    private readonly IMapper _mapper;
    public Animals(IAnimalRepository animalRepository, ILogger<Animals> logger, IMapper mapper)
        : base(logger)
    {
        _animalRepository = animalRepository;
        _mapper = mapper;
    }

    [SwaggerOperation(Summary = "Get Animals By Different Parameters")]
    [AllowAnonymous]
    [HttpGet(nameof(GetAnimals))]
    public async Task<IActionResult> GetAnimals([FromQuery] GetAnimalsQuery query)
    {

        try
        {
            IEnumerable<GetAnimal> subscriptions;

            var dtParameters = query.DtParameters;

            subscriptions = _animalRepository.GetReadOnlyQuery()
                .Select(_mapper.Map<GetAnimal>);

            var total = subscriptions.Count();

            var searchBy = dtParameters.Search?.Value;

            if (!string.IsNullOrEmpty(searchBy))
                subscriptions = subscriptions.Where(s => s.Type.Contains(searchBy) ||
                                                         s.Name.Contains(searchBy)
                );

            var orderableProperty = nameof(GetAnimal.OrganizationId);
            var toOrderAscending = true;
            if (dtParameters.Order != null && dtParameters.Length > 0)
            {
                orderableProperty = dtParameters.Columns[dtParameters.Order.FirstOrDefault().Column].Data.CapitalizeFirst();
                toOrderAscending = dtParameters.Order.FirstOrDefault().Dir == DtOrderDir.Asc;
            }

            var orderedSubscriptions = toOrderAscending
                ? subscriptions.OrderBy(x => x.GetPropertyValue(orderableProperty))
                : subscriptions.OrderByDescending(x => x.GetPropertyValue(orderableProperty));

            var result = new DtResult<GetAnimal>
            {
                Draw = dtParameters.Draw,
                RecordsTotal = total,
                RecordsFiltered = orderedSubscriptions.Count(),
                Data = orderedSubscriptions
                .Skip(dtParameters.Start)
                .Take(dtParameters.Length)
            };

            //var gg = result.Data.ToList();
            return new JsonResult(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.StackTrace);
            return null;
        }
    }
}