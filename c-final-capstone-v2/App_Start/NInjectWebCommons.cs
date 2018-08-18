using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace c_final_capstone_v2.App_Start
{
    using System;
    using System.Web;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using System.Configuration;
    using DAL;
    using Ninject.Web.Common.WebHost;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<ICatSqlDao>().To<CatSqlDao>().WithConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings["CatStoneConnection"].ConnectionString);
            kernel.Bind<ISkillDao>().To<SkillDao>().WithConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings["CatStoneConnection"].ConnectionString);
            kernel.Bind<IUserDao>().To<AdminDao>().WithConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings["CatStoneConnection"].ConnectionString);
            kernel.Bind<IUserDao>().To<UserDao>().WithConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings["CatStoneConnection"].ConnectionString);
        }
    }
}