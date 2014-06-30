using System.Data.SqlClient;

namespace MySportsStore.IDAL
{
    public interface IDbSession
    {
        //获取所有的仓储接口
        IProductRepository ProductRepository { get; set; }
        
        //保存所有变化
        int SaveChanges();

        //执行sql语句
        int ExeucteSql(string sql, params SqlParameter[] paras);
    }
}