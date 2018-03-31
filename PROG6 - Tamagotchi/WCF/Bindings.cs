using Ninject.Modules;
using WCF.Repository;

namespace WCF
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<ITamagotchiRepository>().To<TamagotchiRepository>();
        }
    }
}