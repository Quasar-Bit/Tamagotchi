using MapsterMapper;
using TamagotchiWeb.Data.DataTableProcessing;
using TamagotchiWeb.Extensions;

namespace TamagotchiWeb.Application.Base;

public abstract class BaseRequestHandler
{
    protected readonly IMapper Mapper;

    protected BaseRequestHandler(IMapper mapper)
    {
        Mapper = mapper;
    }

    protected static async Task<DtResult<T>> Parametrization<T>(IQueryable<T> items, DtParameters dtParameters, System.Linq.Expressions.Expression<Func<T, bool>> filter, string order)
    {
        var toOrderAscending = true;

        if (!string.IsNullOrEmpty(dtParameters.Search?.Value))
            items = items.Where(filter);

        if (dtParameters.Order != null && dtParameters.Length > 0)
        {
            order =
                dtParameters.Columns[dtParameters.Order.FirstOrDefault()!.Column].Data.CapitalizeFirst();
            toOrderAscending = dtParameters.Order.FirstOrDefault()!.Dir == DtOrderDir.Asc;
        }

        var enumerable = items as T[] ?? items.ToArray();

        var orderedItems = toOrderAscending
            ? enumerable.OrderBy(x => x.GetPropertyValue(order))
            : enumerable.OrderByDescending(x => x.GetPropertyValue(order));

        return await Task.FromResult(new DtResult<T>
        {
            Draw = dtParameters.Draw,
            RecordsTotal = enumerable.Length,
            RecordsFiltered = orderedItems.Count(),
            Data = orderedItems
                .Skip(dtParameters.Start)
                .Take(dtParameters.Length)
        });
    }
}