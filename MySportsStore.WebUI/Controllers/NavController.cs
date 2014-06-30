
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MySportsStore.IBLL;
using Ninject;

namespace MySportsStore.WebUI.Controllers
{
    public class NavController : BaseController
    {
        [Inject]
        public IProductService ProductService { get; set; }

        public NavController()
        {
            this.AddDisposableObject(ProductService);
        }

        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;
            IEnumerable<string> categories =
                ProductService.LoadEntities(p => true).Select(p => p.Category).Distinct().OrderBy(p => p);
            return PartialView(categories);
        }

        public ActionResult Index()
        {
            return View();
        }

    }
}
