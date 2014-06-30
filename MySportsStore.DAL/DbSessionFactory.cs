using System.Runtime.Remoting.Messaging;
using MySportsStore.IDAL;

namespace MySportsStore.DAL
{
    public class DbSessionFactory: IDbSessionFactory
    {
        public IDbSession GetCurrentDbSession()
        {
            IDbSession dbSession = CallContext.GetData(typeof (DbSession).FullName) as IDbSession;
            if (dbSession == null)
            {
                dbSession = new DbSession();
                CallContext.SetData(typeof(DbSession).FullName, dbSession);
            }
            return dbSession;
        }
    }
}