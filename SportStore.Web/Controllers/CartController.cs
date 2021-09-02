using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SportStore.Web.Infrastructure;
using SportStore.Web.Models;
using SportStore.Web.Models.VM;

namespace SportStore.Web.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository _repository;
        private Cart _cart;

        public CartController(IProductRepository repo,Cart cartService)
        {
            _repository = repo;
            _cart = cartService;
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = _cart,
                ReturnUrl = returnUrl
            });
        }
        public RedirectToActionResult AddToCart(int productId,string returnUrl)
        {
            Product product = _repository.GetProducts.FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                _cart.AddProductToCart(product,1);
            }
            return RedirectToAction("Index",new {returnUrl});
        }

        public RedirectToActionResult RemoveFromCart(int productId, string returnUrl)
        {
            Product product = _repository.GetProducts.FirstOrDefault(p => p.ProductId == productId);
            if (product!= null)
            {
                _cart.RemoveProduct(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        //private Cart GetCartFromSession()
        //{
        //    Cart c = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
        //    return c;
        //}
        //private void SaveCart(Cart cart)
        //{
        //    HttpContext.Session.SetJson("Cart", cart);
        //}
    }
}
