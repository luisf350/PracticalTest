﻿using PracticalTest.Domain.Contract;
using PracticalTest.Entities.Entities;
using PracticalTest.Repositories.Infrastructure;
using System.Linq.Expressions;

namespace PracticalTest.Domain.Impementation;

public class DomainBase<TEntity> : IDomainBase<TEntity>
    where TEntity : EntityBase
{
    protected readonly IGenericRepository<TEntity> Repository;

    public DomainBase(IGenericRepository<TEntity> repository)
    {
        Repository = repository;
    }

    public async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        params Expression<Func<TEntity, object>>[] includes)
    {
        return await Repository.GetAll(filter, orderBy, includes);
    }

    public async Task<TEntity?> Find(Guid id)
    {
        return await Repository.GetById(id);
    }

    public async Task<TEntity?> FirstOfDefaultAsync(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includes)
    {
        return await Repository.FirstOfDefaultAsync(filter, includes);
    }

    public async Task<int> Create(TEntity entity)
    {
        return await Repository.Create(entity);
    }

    public async Task<bool> Update(TEntity entity)
    {
        return await Repository.Update(entity);
    }

    public async Task<int> Delete(TEntity entity)
    {
        return await Repository.Delete(entity);
    }
}
