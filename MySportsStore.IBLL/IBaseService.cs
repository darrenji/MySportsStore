using System;
using System.Linq;
using System.Linq.Expressions;
using MySportsStore.IDAL;

namespace MySportsStore.IBLL
{
    public interface IBaseService<T> where T : class, new()
    {
         //数据层访问统一入口工厂
        IDbSessionFactory DbSessionFactory { get; set; }

        //数据层访问统一入口
        IDbSession DbSessionContext { get; set; }

        //查询
        IQueryable<T> LoadEntities(Expression<Func<T, bool>> whereLambda);

        //分页查询
        IQueryable<T> LoadPageEntities<S>(
            Expression<Func<T, bool>> whereLambada,
            Expression<Func<T, S>> orderBy,
            int pageSize,
            int pageIndex,
            out int totalCount,
            bool isASC);

        //查询总数量
        int Count(Expression<Func<T, bool>> predicate);

        //添加
        T AddEntity(T entity);

        //批量添加
        int AddEntities(params T[] entities);

        //删除
        int DeleteEntity(T entity);

        //批量删除
        int DeleteBy(Expression<Func<T, bool>> whereLambda);

        //更新
        T UpdateEntity(T entity);

        //批量更新
        int UpdateEntities(params T[] entities);
    }
}