using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Ninject;
using Twitler.Data.Context;
using Twitler.Data.Repositories;
using Twitler.Domain.Interfaces;
using Twitler.Utils.Encryptors;

namespace Twitler.DI
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<ITwitlerContext>().To<TwitlerContext>().WithConstructorArgument("dbConnectionString", "TwitlerDB");

            //Repositories
            kernel.Bind<IUserRepository>().To<UserRepository>();

            //Utils
            kernel.Bind<IEncryptor>().To<MD5Encryptor>().InSingletonScope();


        }

    }
}
