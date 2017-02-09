using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Ninject;
using Ninject.Web.Common;
using Twitler.Data.Context;
using Twitler.Data.Repositories;
using Twitler.Domain.Interfaces;
using Twitler.Domain.Model;
using Twitler.Mappers;
using Twitler.Mappers.Mappers;
using Twitler.Utils.Encryptors;
using Twitler.Utils.HashTools;

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
            kernel.Bind<ITwitlerContext>()
                .To<TwitlerContext>().InRequestScope()
                .WithConstructorArgument("dbConnectionString", "TwitlerDB");
            
            //Repositories
            kernel.Bind<IRepository<User>>().To<UserRepository>();
            kernel.Bind<IRepository<Twit>>().To<TwitRepository>();

            //Utils
            kernel.Bind<IEncryptor>().To<MD5Encryptor>().InSingletonScope();
            kernel.Bind<IHashExtractor>().To<HashExtractor>().InSingletonScope();
            kernel.Bind<IHashConverter>().To<HashConverter>().InSingletonScope();

            //mappers
            kernel.Bind<IMapper>().ToMethod(AutoMapperCreator.GetMapper).InSingletonScope();
            kernel.Bind<TwitMapper>().ToSelf().InSingletonScope();

        }

    }
}
