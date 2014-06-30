using System.Configuration;
using System.Web.Mvc;
using MySportsStore.BLL;
using MySportsStore.IBLL;
using MySportsStore.WebUI.Abstract;
using MySportsStore.WebUI.Concrete;
using Ninject;

namespace MySportsStore.WebUI.Extension
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, System.Type controllerType)
        {
            return controllerType == null ? null : (IController) ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            ninjectKernel.Bind<IProductService>().To<ProductService>();

            //EmailSettings emailSettings = new EmailSettings()
            //{
            //    WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
            //};
            ninjectKernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>();
        }
    }
}