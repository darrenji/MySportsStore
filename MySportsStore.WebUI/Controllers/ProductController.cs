
using System.Web.Mvc;
using MySportsStore.IBLL;
using MySportsStore.WebUI.Models;
using Ninject;

namespace MySportsStore.WebUI.Controllers
{
    public class ProductController : BaseController
    {
        [Inject]
        public IProductService ProductService { get; set; }

        public ProductController()
        {
            this.AddDisposableObject(ProductService);
        }

        public int PageSize = 4;
        public ViewResult List(string category, int page = 1)
        {
            //return View(ProductService.LoadEntities(p => true));
            int totalCount = 0;
            //return View(ProductService.LoadPageEntities(
            //    p => true,
            //    p => p.Id,
            //    PageSize,
            //    page,
            //    out totalCount,
            //    true));
            ProductsListViewModel viewModel = new ProductsListViewModel()
            {
                Products = ProductService.LoadPageEntities(p => category == null ? true : p.Category == category, p => p.Id, PageSize, page, out  totalCount, true),
                PagingInfo = new PagingInfo(){CurrentPage = page, ItemsPerPage = PageSize, TotalItems = category == null ? ProductService.Count(p => true) : ProductService.Count(p => p.Category == category)},
                CurrentCategory = category
            };
            return View(viewModel);
        }

        public ActionResult Index()
        {
            return View();
        }

        

    }
}
