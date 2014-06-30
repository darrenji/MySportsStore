using System.Data.Entity;
using System.Runtime.Remoting.Messaging;
using MySportsStore.IDAL;
using MySportsStore.Model;

namespace MySportsStore.DAL
{
    public class DbContextFactory : IDbContextFactory
    {
        //获取当前EF上下文的唯一实例
        public System.Data.Entity.DbContext GetCurrentThreadInstance()
        {
            DbContext obj = CallContext.GetData(typeof (EfDbContext).FullName) as DbContext;
            if (obj == null)
            {
                obj = new EfDbContext();
                CallContext.SetData(typeof(EfDbContext).FullName, obj);
            }
            return obj;
        }
    }
}