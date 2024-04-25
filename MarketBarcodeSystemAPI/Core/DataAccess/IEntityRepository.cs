using MarketBarcodeSystemAPI.Core.Entities;
using System.Linq.Expressions;
using System.Security.Principal;

namespace MarketBarcodeSystemAPI.Core.DataAccess
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        void Add(T entity);
        void AddList(List<T> entities);
        void Delete(T entity);
        void DeleteList(List<T> entities);
        void Update(T entity);
        void UpdateList(List<T> entities);
        T Get(Expression<Func<T, bool>> filter);
        List<T> GetAll(Expression<Func<T, bool>> filter = null);
    }
}
