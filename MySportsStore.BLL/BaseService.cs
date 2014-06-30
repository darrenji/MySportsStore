using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MySportsStore.DAL;
using MySportsStore.IDAL;

namespace MySportsStore.BLL
{
    public abstract class BaseService<T> : IDisposable where T:class,new()
    {
        //数据层统一访问入口工厂属性
        private IDbSessionFactory _DbSessionFactory;

        public IDbSessionFactory DbSessionFactory
        {
            get
            {
                if (_DbSessionFactory == null)
                {
                    _DbSessionFactory = new DbSessionFactory();
                }
                return _DbSessionFactory;
            }
            set { _DbSessionFactory = value; }
        }

        //数据层统一访问入口属性
        private IDbSession _DbSessionContext;

        public IDbSession DbSessionContext
        {
            get
            {
                if (_DbSessionContext == null)
                {
                    _DbSessionContext = DbSessionFactory.GetCurrentDbSession();
                }
                return _DbSessionContext;
            }
            set { _DbSessionContext = value; }
        }

        //当前Repository,在子类中实现--通过一个抽象方法在构造函数中设置
        protected IBaseRepository<T> CurrentRepository;

        //借助此方法在子类中的重写，为XXXService设置当前Repository
        public abstract bool SetCurrentRepository();

        public BaseService()
        {
            this.DisposableObjects = new List<IDisposable>();
            this.SetCurrentRepository();
        }

        //查询
        public IQueryable<T> LoadEntities(Expression<Func<T, bool>> whereLambda)
        {
            return this.CurrentRepository.LoadEntities(whereLambda);
        }

        public IQueryable<T> LoadPageEntities<S>(
            Expression<Func<T, bool>> whereLambada,
            Expression<Func<T, S>> orderBy,
            int pageSize,
            int pageIndex,
            out int totalCount,
            bool isASC)
        {
            return this.CurrentRepository.LoadPageEntities<S>(
                whereLambada,
                orderBy,
                pageSize,
                pageIndex,
                out totalCount,
                isASC);
        }

        //查询总数量
        public int Count(Expression<Func<T, bool>> predicate)
        {
            return this.CurrentRepository.Count(predicate);
        }

        //添加
        public T AddEntity(T entity)
        {
            this.CurrentRepository.AddEntity(entity);
            DbSessionContext.SaveChanges();
            return entity;
        }

        //批量添加
        public int AddEntities(params T[] entities)
        {
            return this.CurrentRepository.AddEntities(entities);
        }

        //删除
        public int DeleteEntity(T entity)
        {
            this.CurrentRepository.DeleteEntity(entity);
            return DbSessionContext.SaveChanges();
        }

        //批量删除
        public int DeleteBy(Expression<Func<T, bool>> whereLambda)
        {
            this.CurrentRepository.DeleteBy(whereLambda);
            return DbSessionContext.SaveChanges();
        }

        //更新
        public T UpdateEntity(T entity)
        {
            this.CurrentRepository.UpdateEntity(entity);
            if (this.DbSessionContext.SaveChanges() <= 0)
            {
                return null;
            }
            return entity;
        }

        //批量更新
        public int UpdateEntities(params T[] entities)
        {
            return this.CurrentRepository.UpdateEntities(entities);
        }

        public IList<IDisposable> DisposableObjects { get; private set; }

        protected void AddDisposableObject(object obj)
        {
            IDisposable disposable = obj as IDisposable;
            if (disposable != null)
            {
                this.DisposableObjects.Add(disposable);
            }
        }

        public void Dispose()
        {
            foreach (IDisposable obj in this.DisposableObjects)
            {
                if (obj != null)
                {
                    obj.Dispose();
                }
            }
        }
    }
}