using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportStore.Web.Models;

namespace SportStore.Web.Components
{
    public class CartSummaryViewComponent : ViewComponent
    {
        private readonly Cart _cart;
        public CartSummaryViewComponent(Cart cart)
        {
            _cart = cart;
        }

        public IViewComponentResult Invoke()
        {
            return View(_cart);
        }
    }
}
