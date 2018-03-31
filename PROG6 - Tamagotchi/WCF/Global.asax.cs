using Ninject;
using Ninject.Web.Common;

namespace WCF
{
    public class Global : NinjectHttpApplication
    {
        protected override IKernel CreateKernel()
        {
            return new StandardKernel(new Bindings());
        }
    }
}