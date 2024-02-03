namespace Asteroids.Runtime
{
    public interface IInitializable
    {
        void Initialize();
    }

    public interface IInitializable<in T>
    {
        void Initialize(T data);
    }
    
    public interface IInitializable<in T,in TU>
    {
        void Initialize(T data1,TU data2);
    }
    
    public interface IInitializable<in T,in TU,in TV>
    {
        void Initialize(T data1,TU data2,TV data3);
    }
}