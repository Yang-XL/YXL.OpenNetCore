namespace Core.Cache
{
    public interface ICacheFactory
    {
        ICache CreateCacher();

        ICacheProvider CreateProvider();
    }
}