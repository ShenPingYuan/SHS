﻿using System;
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
        IQueryable<T> LoadEntities(System.Linq.Expressions.Expression<Func<T, bool>> whereLambdaExpression);
        IQueryable<T> GetAllEntities();
        IQueryable<T> LoadPageEntities<S>(int pageIndex, int pageSize, out int totalCount,
            System.Linq.Expressions.Expression<Func<T, bool>> whereLambdaExpression,
            System.Linq.Expressions.Expression<Func<T, S>> orderByLambdaExpression, bool isAsc);
        bool DeleteEntity(T entity);
        bool EditEntity(T entity);
        T AddEntity(T entity);
        //以下接口为上面接口的异步接口
        //Task<bool> DeleteEntityAsync(T entity);
        //Task<bool> EditEntityAsync(T entity);
    }
}
