﻿using MarketBarcodeSystemAPI.Core.Entities;
using System.Linq.Expressions;
using System.Security.Principal;

namespace MarketBarcodeSystemAPI.Core.DataAccess
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        void AddList(List<T> entities);
        List<T> GetAll(Expression<Func<T, bool>> filter = null);
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
