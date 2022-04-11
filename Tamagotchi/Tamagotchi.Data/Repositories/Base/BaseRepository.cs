using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Tamagotchi.Data.Repositories.Base.Interfaces;

namespace Tamagotchi.Data.Repositories.Base;
public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class, new()
{
    private readonly Context _context;
    private readonly IEnumerable<PropertyInfo> _primaryKeyProperties;
    
    protected BaseRepository(Context context)
    {
        _context = context;

        if (_context != null)
            Table = _context.Set<TEntity>();

        if (_context == null || Table == null)
            throw new InvalidOperationException(
                $"Can not find IRepository instance for type {typeof(TEntity).FullName}");

        _primaryKeyProperties =
            _context.Model.FindEntityType(typeof(TEntity))?.FindPrimaryKey()?.Properties.Select(p => p.PropertyInfo)
                .ToList() ?? new List<PropertyInfo>();
    }

    protected DbSet<TEntity> Table { get; }

    public virtual IQueryable<TEntity> GetReadOnlyQuery()
    {
        return Table.AsNoTracking();
    }

    public virtual IQueryable<TEntity> GetChangeTrackingQuery()
    {
        return Table.AsTracking();
    }

    public virtual async Task<TEntity> FindByIdAsync(params object[] id)
    {
        ThrowIfInvalidPrimaryKey(id, _primaryKeyProperties);

        return await Table.FindAsync(id);
    }

    private static void ThrowIfInvalidPrimaryKey(object[] id, IEnumerable<PropertyInfo> primaryKeyProperties)
    {
        var keyProperties = primaryKeyProperties as PropertyInfo[] ?? primaryKeyProperties.ToArray();
        if (id.Length != keyProperties.Length)
            throw new ArgumentException(
                $"The primary key of entity {typeof(TEntity).Name} consist of {keyProperties.Length} properties. The value provided has {id.Length} values",
                nameof(id));

        for (var i = 0; i < keyProperties.Length; i++)
        {
            var primaryKeyProperty = keyProperties.ElementAtOrDefault(i);
            var idPart = id[i];

            if (primaryKeyProperty is not null && idPart?.GetType() != primaryKeyProperty.PropertyType)
                throw new NotSupportedException(
                    $"The PrimaryKey part '{primaryKeyProperty.Name}' with type '{primaryKeyProperty.PropertyType.FullName}' is not same as provided value type '{idPart?.GetType().FullName}'");
        }
    }

    public void Remove(TEntity entity)
    {
        Table.Remove(entity);
    }

    public void RemoveRange(IEnumerable<TEntity> entities)
    {
        Table.RemoveRange(entities);
    }

    public async Task<TEntity> AddAsync(TEntity newEntity, CancellationToken cancellationToken = default)
    {
        return (await Table.AddAsync(newEntity, cancellationToken)
            .ConfigureAwait(false)).Entity;
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> newEntities)
    {
        var enumerable = newEntities as TEntity[] ?? newEntities.ToArray();

        await Table.AddRangeAsync(enumerable).ConfigureAwait(false);
    }
    public IUnitOfWork UnitOfWork => _context;

    public TEntity Update(TEntity entity)
    {
        return Table.Update(entity).Entity;
    }
}