using PracticalTest.Entities.Context;
using PracticalTest.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace PracticalTest.Repositories.Infrastructure;

public class GenericRepository<T> : IDisposable, IGenericRepository<T> where T : EntityBase
{
    protected readonly AppDbContext Context;
    private readonly DbSet<T> _dbSet;
    private bool _disposed;

    public GenericRepository(AppDbContext context)
    {
        Context = context;
        _dbSet = context.Set<T>();
    }

    public Task<IQueryable<T>> GetAll(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet;

        foreach (Expression<Func<T, object>> include in includes)
            query = query.Include(include);

        if (filter != null)
            query = query.Where(filter);

        if (orderBy != null)
            query = orderBy(query);

        return Task.FromResult(query);
    }

    public async Task<T?> GetById(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<T?> FirstOfDefaultAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet.Where(filter);
        if (includes.Any())
        {
            foreach (Expression<Func<T, object>> include in includes)
                query = query.Include(include);
        }

        return await query.FirstOrDefaultAsync();
    }

    public async Task<int> Create(T entity)
    {
        entity.CreationDate = DateTime.Now;
        entity.ModificationDate = DateTime.Now;

        await _dbSet.AddAsync(entity);
        return await Context.SaveChangesAsync();
    }

    public async Task<int> CreateBulk(List<T> entityList)
    {
        entityList.ForEach(entity =>
        {
            entity.CreationDate = DateTime.Now;
            entity.ModificationDate = DateTime.Now;
        });

        await _dbSet.AddRangeAsync(entityList);
        return await Context.SaveChangesAsync();
    }

    public async Task<bool> Update(T entity)
    {
        entity.ModificationDate = DateTime.Now;

        _dbSet.Update(entity);
        await Context.SaveChangesAsync();
        return true;
    }

    public async Task<int> Delete(T entity)
    {
        _dbSet.Remove(entity);
        return await Context.SaveChangesAsync();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
            Context.Dispose();
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
