using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Moq;
using Ninject;
using Test.Domain.Abstract;
using Test.Domain.Concrete;

namespace Test.WebUI.Infrastructure
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
            kernel.Bind<IUserRepository>().To<EFUserRepository>();
            kernel.Bind<ICustomerRepository>().To<EFCustomerRepository>();
            kernel.Bind<IContactRepository>().To<EFContactRepository>();
            kernel.Bind<ITransactionRepository>().To<EFTransactionRepository>();
            kernel.Bind<ICouponRepository>().To<EFCouponRepository>();
            kernel.Bind<IPrizeRepository>().To<EFPrizeRepository>();
            kernel.Bind<ICouponClaimPrizeRepository>().To<EFCouponClaimPrizeRepository>();
        }
    }
}