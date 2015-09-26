using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Ninject;
using System.Web.Mvc;
using HotCar.BLL;
using HotCar.BLL.Abstract;
using HotCar.Repositories.Abstract;
using HotCar.Repositories.Sql;

namespace HotCar.WebUI.Frontend.Code.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;

        #region Constructors

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            this._kernel = kernelParam;
            AddBindings();
        }

        #endregion

        #region Interface implementation

        public object GetService(Type serviceType)
        {
            return this._kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this._kernel.GetAll(serviceType);
        }

        #endregion

        #region Helpers

        private void AddBindings()
        {
            this._kernel.Bind<ISecurityManager>().To<SecurityManager>();
            this._kernel.Bind<IUsersManager>().To<UsersManager>();           
            this._kernel.Bind<ITripManager>().To<TripManager>();
            
            this._kernel.Bind<IUserRepository>()
                .To<UserRepository>()
                .WithConstructorArgument("connectionString",
                    ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

            this._kernel.Bind<ITripRepository>()
                .To<TripRepository>()
                .WithConstructorArgument("connectionString",
                    ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            
        }

        #endregion
    }
}