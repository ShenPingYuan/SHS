using SHS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHS.IRepository
{
    /// <summary>
    /// 增删改查分页，公共接口
    /// </summary>
    /// <typeparam name="T">接口泛型</typeparam>
    public interface IBaseRepository<T> where T : class, new()
    {
        IEnumerable<T> LoadEntities(System.Linq.Expressions.Expression<Func<T, bool>> whereLambdaExpression);
        IQueryable<T> LoadEntitiesAsIQueryable(System.Linq.Expressions.Expression<Func<T, bool>> whereLambdaExpression);

        IEnumerable<T> GetAllEntities();
        IQueryable<T> GetAllEntitiesAsIQueryable();
        IEnumerable<T> LoadPageEntities<S>(int pageIndex, int pageSize, out int totalCount,
            System.Linq.Expressions.Expression<Func<T, bool>> whereLambdaExpression,
            System.Linq.Expressions.Expression<Func<T, S>> orderByLambdaExpression, bool isAsc);
        IQueryable<T> LoadPageEntitiesAsIQueryable<S>(int pageIndex, int pageSize, out int totalCount,
            System.Linq.Expressions.Expression<Func<T, bool>> whereLambdaExpression,
            System.Linq.Expressions.Expression<Func<T, S>> orderByLambdaExpression, bool isAsc);
        bool DeleteEntity(T entity);
        bool DeleteEntities(IEnumerable<T> entities);
        bool EditEntity(T entity);
        T AddEntity(T entity);
        //以下接口为上面接口的异步接口
        Task<bool> DeleteEntityAsync(T entity);
        Task<bool> DeleteEntitiesAsync(IEnumerable<T> entities);
        Task<bool> EditEntityAsync(T entity);
        Task<T> AddEntityAsync(T entity);
        Task<int> GetCountAsync(T entity);
    }
}
