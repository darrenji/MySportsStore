using System.Data.Entity;

namespace MySportsStore.IDAL
{
    public interface IDbContextFactory
    {
        //获取当前上下文的唯一实例
        DbContext GetCurrentThreadInstance();
    }
}