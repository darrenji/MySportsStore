using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using MySportsStore.IDAL;

namespace MySportsStore.DAL
{
    public class BaseRepository<T> : IDisposable where T : class, new()
    {
        private DbContext db;

        public BaseRepository()
        {
            IDbContextFactory dbFactory = new DbContextFactory();
            db = dbFactory.GetCurrentThreadInstance();
        }

        //查询
        public virtual IQueryable<T> LoadEntities(Expression<Func<T, bool>> whereLambda)
        {
            IQueryable<T> result = db.Set<T>().Where(whereLambda);
            return result;
        }

        //分页查询
        public virtual IQueryable<T> LoadPageEntities<S>(
            Expression<Func<T, bool>> whereLambada, 
            Expression<Func<T, S>> orderBy,
            int pageSize,
            int pageIndex,
            out int totalCount,
            bool isASC)
        {
            totalCount = db.Set<T>().Where(whereLambada).Count();
            IQueryable<T> entities = null;
            if (isASC)
            {
                entities = db.Set<T>().Where(whereLambada)
                    .OrderBy(orderBy)
                    .Skip(pageSize*(pageIndex - 1))
                    .Take(pageSize);
            }
            else
            {
                entities = db.Set<T>().Where(whereLambada)
                    .OrderByDescending(orderBy)
                    .Skip(pageSize*(pageIndex - 1))
                    .Take(pageSize);
            }
            return entities;
        }

        //查询总数量
        public virtual int Count(Expression<Func<T, bool>> predicate)
        {
            return db.Set<T>().Where(predicate).Count();
        }

        //添加
        public virtual T AddEntity(T entity)
        {
            db.Set<T>().Add(entity);
            return entity;
        }

        //批量添加 每10条记录提交一次
        public virtual int AddEntities(params T[] entities)
        {
            int result = 0;
            for (int i = 0; i < entities.Count(); i++)
            {
                if(entities[i] == null) continue;
                db.Set<T>().Add(entities[i]);
                //每累计到10条记录就提交
                if (i != 0 && i%10 == 0)
                {
                    result += db.SaveChanges();
                }
            }

            //可能还有不到10条的记录
            if (entities.Count() > 0)
            {
                result += db.SaveChanges();
            }
            return result;
        }

        //删除
        public virtual int DeleteEntity(T entity)
        {
            db.Set<T>().Attach(entity);
            db.Entry(entity).State = EntityState.Deleted;
            return -1;
        }

        //批量删除
        public virtual int DeleteBy(Expression<Func<T, bool>> whereLambda)
        {
            var entitiesToDelete = db.Set<T>().Where(whereLambda);
            foreach (var item in entitiesToDelete)
            {
                db.Entry(item).State = EntityState.Deleted;
            }
            return -1;
        }

        //更新
        public virtual T UpdateEntity(T entity)
        {
            if (entity != null)
            {
                db.Set<T>().Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
            }
            return entity;
        }

        //批量更新 每10条记录更新一次
        public virtual int UpdateEntities(params T[] entities)
        {
            int result = 0;
            for (int i = 0; i < entities.Count(); i++)
            {
                if(entities[i] == null) continue;
                db.Set<T>().Attach(entities[i]);
                db.Entry(entities[i]).State = EntityState.Modified;
                if (i != 0 && i%10 == 0)
                {
                    result += db.SaveChanges();
                }
            }

            //可能还存在不到10条的记录
            if (entities.Count() > 0)
            {
                result += db.SaveChanges();
            }
            return result;
        }

        //释放EF上下文
        public void Dispose()
        {
            db.Dispose();
        }
    }
}