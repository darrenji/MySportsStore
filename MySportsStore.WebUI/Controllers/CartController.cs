
using System.Linq;
using System.Web.Mvc;
using MySportsStore.IBLL;
using MySportsStore.Model;
using MySportsStore.WebUI.Abstract;
using MySportsStore.WebUI.Models;
using Ninject;

namespace MySportsStore.WebUI.Controllers
{
    public class CartController : BaseController
    {
        [Inject]
        public IProductService ProductService { get; set; }

        [Inject]
        public IOrderProcessor OrderProcessor { get; set; }

        public CartController()
        {
            this.AddDisposableObject(ProductService);
            this.AddDisposableObject(OrderProcessor);
        }

        public ViewResult CheckOut()
        {
            return View(new ShippingDetail());
        }

        [HttpPost]
        public ViewResult CheckOut(Cart cart, ShippingDetail shippingDetail)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("","购物车为空");
            }
            if (ModelState.IsValid)
            {
                OrderProcessor.ProcessOrder(cart, shippingDetail);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingDetail);
            }
        }

        public ActionResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                //Cart = GetCart(),
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        //添加到购物车
        public RedirectToRouteResult AddToCart(Cart cart, int Id, string returnUrl)
        {
            Product product = ProductService.LoadEntities(p => p.Id == Id).FirstOrDefault();
            if (product != null)
            {
                //GetCart().AddItem(product, 1);
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new {returnUrl});
        }

        //从购物车移除
        public RedirectToRouteResult RemoveFromCart(Cart cart, int Id, string returnUrl)
        {
            Product product = ProductService.LoadEntities(p => p.Id == Id).FirstOrDefault();
            if (product != null)
            {
                //GetCart().RemoveLine(product);
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new {returnUrl});
        }

        public ViewResult Summary(Cart cart)
        {
            return View(cart);
        }

        private Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

    }
}
