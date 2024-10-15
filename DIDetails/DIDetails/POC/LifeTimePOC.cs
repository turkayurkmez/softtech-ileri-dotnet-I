namespace DIDetails.POC
{
    public interface ISingleton
    {
        Guid Guid { get; }
    }


    public interface ITransient
    {
        Guid Guid { get; }
    }
    public interface IScoped
    {
        Guid Guid { get; }
    }

    public class Singleton : ISingleton
    {
        public Singleton()
        {
               Guid = Guid.NewGuid();
        }
        public Guid Guid { get; }
    }

    public class Transient : ITransient
    {
        public Transient()
        {
            Guid = Guid.NewGuid();
        }
        public Guid Guid { get; }
    }

    public class Scoped : IScoped
    {
        public Scoped()
        {
            Guid = Guid.NewGuid();
        }
        public Guid Guid { get; }
    }


    public class LifeTimeService
    {
        public ISingleton Singleton { get; set; }
        public ITransient Transient { get; set; }
        public IScoped Scoped { get; set; }
        public LifeTimeService(ISingleton singleton, ITransient transient, IScoped scoped)
        {
            Singleton = singleton;
            Transient = transient;
            Scoped = scoped;
        }
        
    }

}
