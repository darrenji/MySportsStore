using System.Data.Entity;
using MySportsStore.IDAL;

namespace MySportsStore.DAL
{
    public class DbSession : IDbSession
    {
        private IProductRepository _ProductRepository;
        public IProductRepository ProductRepository
        {
            get
            {
                if (_ProductRepository == null)
                {
                    _ProductRepository = new ProductRepository();
                }
                return _ProductRepository;
            }
            set { _ProductRepository = value; }
        }

        public int SaveChanges()
        {
            IDbContextFactory dbFactory = new DbContextFactory();
            DbContext db = dbFactory.GetCurrentThreadInstance();
            return db.SaveChanges();
        }

        public int ExeucteSql(string sql, params System.Data.SqlClient.SqlParameter[] paras)
        {
            IDbContextFactory dbFactory = new DbContextFactory();
            DbContext db = dbFactory.GetCurrentThreadInstance();
            return db.Database.ExecuteSqlCommand(sql, paras);
        }
    }
}