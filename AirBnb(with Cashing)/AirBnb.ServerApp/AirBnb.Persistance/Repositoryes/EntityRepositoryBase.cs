using AirBnb.Domain.Common.Models;
using AirBnb.Persistence.Caching;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AirBnb.Persistence.Repositories;

public class EntityRepositoryBase<TEntity, TContext>(TContext dbContext, ICacheBroker cacheBroker)
    where TEntity : class, IEntity
    where TContext : DbContext
{
    protected TContext DbContext => dbContext;

    protected IQueryable<TEntity> Get(Expression<Func<TEntity, bool>>? predicate = default, bool asNoTracking = false)
    {
        var initialQuery = DbContext.Set<TEntity>().AsQueryable();

        if (predicate is not null)
            initialQuery = initialQuery.Where(predicate);

        if (asNoTracking)
            initialQuery = initialQuery.AsNoTracking();

        return initialQuery;
    }

    protected async ValueTask<TEntity?> GetByIdAsync(Guid id, bool asNoTracking = false,
        CancellationToken cancellationToken = default)
    {
        return await cacheBroker.GetOrSetAsync<TEntity>(id.ToString(), async () =>
        {
            var initialQuery = DbContext.Set<TEntity>().AsQueryable();

            if (asNoTracking)
                initialQuery = initialQuery.AsNoTracking();

            return await initialQuery.SingleOrDefaultAsync(entity => entity.Id == id, cancellationToken)
                   ?? throw new ArgumentException("Entity not found! " + nameof(TEntity));
        });
    }

    protected async ValueTask<IList<TEntity>> GetByIdsAsync(
        IEnumerable<Guid> ids,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
    )
    {
        var initialQuery = DbContext.Set<TEntity>().AsQueryable();

        if (asNoTracking) initialQuery = initialQuery.AsNoTracking();

        initialQuery = initialQuery.Where(entity => ids.Contains(entity.Id));

        return await initialQuery.ToListAsync(cancellationToken: cancellationToken);
    }

    protected async ValueTask<TEntity> CreateAsync(TEntity entity, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        await DbContext.AddAsync(entity, cancellationToken);

        await cacheBroker.SetAsync(entity.Id.ToString(), entity);

        if (saveChanges) await DbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }


    protected async ValueTask<TEntity> UpdateAsync(TEntity entity, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        DbContext.Update(entity);

        await cacheBroker.SetAsync(entity.Id.ToString(), entity);

        if (saveChanges) await DbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }

    protected async ValueTask<TEntity?> DeleteAsync(TEntity entity, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        DbContext.Set<TEntity>().Remove(entity);

        await cacheBroker.DeleteAsync(entity.Id.ToString());

        if (saveChanges) await DbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }

    protected async ValueTask<TEntity?> DeleteByIdAsync(Guid id, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        var entity = await DbContext.Set<TEntity>().FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken) ??
                     throw new ArgumentException($"{nameof(TEntity)} - not found!");

        DbContext.Set<TEntity>().Remove(entity);

        await cacheBroker.DeleteAsync(entity.Id.ToString());

        if (saveChanges)
            await DbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }
}