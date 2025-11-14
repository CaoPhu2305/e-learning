using Data_Oracle;
using Data_Oracle.Context;
using Data_Oracle.Interfaces;
using Data_Oracle.Repositories;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Common.WebHost;
using Services.Implamentatios;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebActivatorEx;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;



[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(e_learning.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(e_learning.App_Start.NinjectWebCommon), "Stop")]


namespace e_learning.App_Start
{
    public class NinjectWebCommon
    {

        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }



        private static void RegisterServices(IKernel kernel)
        {
            // DbContext
            kernel.Bind<OracleDBContext>().ToSelf().InRequestScope();

            // Repository
            kernel.Bind<IUserRepository>().To<UserRepository>();

            kernel.Bind<IRoleRepository>().To<RoleRepository>();

            kernel.Bind<IRbacRepository>().To<RbacRepository>();

            kernel.Bind<IAccountRepository>().To<AccountRepositoty>();

            kernel.Bind<ICourseRepository>().To<CourseRepository>();

            // Service
            kernel.Bind<IAccountService>().To<AccountService>().Named("App");

            kernel.Bind<IAccountService>().To<AccountOracleService>().Named("Oracle");

            kernel.Bind<IPasswordHasher>().To<PBKDF2PasswordHasher>();

            kernel.Bind<IAuthorizationService>().To<AuthorizationService>();

            kernel.Bind<IUserService>().To<UserService>();

            kernel.Bind<ICourseService>().To<CourseService>();


        }


    }
}