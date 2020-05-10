using Microsoft.EntityFrameworkCore;
using SHS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SHS.Repository
{
    /// <summary>
    /// 公共增删改查方法实现类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseRepository<T> where T : class, new()
    {
        private readonly ApplicationDbContext _dbContext;

        public BaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public T AddEntity(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public bool DeleteEntity(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return _dbContext.SaveChanges() > 0;

        }

        public bool EditEntity(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            return _dbContext.SaveChanges() > 0;
        }

        public IEnumerable<T> LoadEntities(Expression<Func<T, bool>> whereLambdaExpression)
        {
            return _dbContext.Set<T>().Where(whereLambdaExpression);
        }
        public IQueryable<T> LoadEntitiesAsIQueryable(Expression<Func<T, bool>> whereLambdaExpression)
        {
            return _dbContext.Set<T>().Where(whereLambdaExpression);
        }
        public IEnumerable<T> GetAllEntities()
        {
            return _dbContext.Set<T>();
        }
        public IQueryable<T> GetAllEntitiesAsIQueryable()
        {
            return _dbContext.Set<T>();
        }
        public IEnumerable<T> LoadPageEntities<S>(int pageIndex,
            int pageSize, out int totalCount,
            Expression<Func<T, bool>> whereLambdaExpression,
            Expression<Func<T, S>> orderByLambdaExpression, bool isAsc)
        {
            var tem = _dbContext.Set<T>().Where(whereLambdaExpression);
            totalCount = tem.Count();
            if (isAsc)
            {
                tem = tem.OrderBy(orderByLambdaExpression).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
            else
            {
                tem = tem.OrderByDescending(orderByLambdaExpression).Skip(pageIndex).Take(pageSize);
            }
            return tem;
        }
        public IQueryable<T> LoadPageEntitiesAsIQueryable<S>(int pageIndex,
            int pageSize, out int totalCount,
            Expression<Func<T, bool>> whereLambdaExpression,
            Expression<Func<T, S>> orderByLambdaExpression, bool isAsc)
        {
            var tem = _dbContext.Set<T>().Where(whereLambdaExpression);
            totalCount = tem.Count();
            if (isAsc)
            {
                tem = tem.OrderBy(orderByLambdaExpression).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
            else
            {
                tem = tem.OrderByDescending(orderByLambdaExpression).Skip(pageIndex).Take(pageSize);
            }
            return tem;
        }
        //以下方法为上面方法的异步实现
        public async Task<T> AddEntityAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<bool> DeleteEntityAsync(T entity)
        {
            await Task.Run(() =>
            {
                _dbContext.Set<T>().Remove(entity);
            });
            return await _dbContext.SaveChangesAsync() > 0;

        }
        public async Task<bool> EditEntityAsync(T entity)
        {
            await Task.Run(() =>
            {
                _dbContext.Set<T>().Update(entity);
            });
            return await _dbContext.SaveChangesAsync() > 0;
        }
        public async Task<int> GetCountAsync(T entity) => await _dbContext.Set<T>().CountAsync<T>();
        public bool DeleteEntities(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
            return _dbContext.SaveChanges() > 0;
        }
        public async Task<bool> DeleteEntitiesAsync(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
