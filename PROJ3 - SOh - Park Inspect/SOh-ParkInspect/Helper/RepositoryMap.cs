using System;

namespace SOh_ParkInspect.Helper
{
    public class RepositoryMap<TInterface, TDummyRepository, TRepository> : IRepositoryMap
        where TInterface : class
        where TDummyRepository : TInterface
        where TRepository : TInterface
    {
        public Type Interface { get; set; }
        public Type DummyRepository { get; set; }
        public Type Repository { get; set; }

        public RepositoryMap()
        {
            Interface = typeof(TInterface);
            DummyRepository = typeof(TDummyRepository);
            Repository = typeof(TRepository);
        }
    }

    public class RepositoryMap<TInterface, TDummyRepository> : IRepositoryMap
        where TInterface : class
        where TDummyRepository : TInterface
    {
        public Type Interface { get; set; }
        public Type DummyRepository { get; set; }
        public Type Repository { get; set; }

        public RepositoryMap()
        {
            Interface = typeof(TInterface);
            DummyRepository = typeof(TDummyRepository);
        }
    }

    public interface IRepositoryMap
    {
        Type Interface { get; set; }
        Type DummyRepository { get; set; }
        Type Repository { get; set; }
    }
}
