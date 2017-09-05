namespace Core.Cache
{
    public interface ICacheFactory
    {
        ICacher CreCacher();

        ICacheProvider CreataeProvider();
    }
}