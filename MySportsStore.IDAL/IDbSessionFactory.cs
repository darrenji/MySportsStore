namespace MySportsStore.IDAL
{
    public interface IDbSessionFactory
    {
        IDbSession GetCurrentDbSession();
    }
}