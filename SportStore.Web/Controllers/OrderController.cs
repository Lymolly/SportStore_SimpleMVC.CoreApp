using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportStore.Web.Models;

namespace SportStore.Web.Controllers
{
    public class OrderController : Controller
    {
        IOrderRepository repo;
        Cart cart;

        public OrderController(IOrderRepository repository,Cart cartService)
        {
            repo = repository;
            cart = cartService;
        }
        [HttpGet]
        public IActionResult Checkout()
        {
            return View(new Order());
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (!cart.Lines.Any())
            {
                ModelState.AddModelError("", "No items in cart");
            }
            if (ModelState.IsValid)
            {
                order.Lines = cart.Lines.ToList();
                repo.SaveOrder(order);
                return RedirectToAction(nameof(Completed));
            }
            return View(order);
        }

        [HttpGet]
        public IActionResult MarkNotShipped()
        { 
            return View(repo.Orders.Where(o => !o.Shipped));
        }
        [HttpPost]
        public IActionResult MarkShipped(int orderId)
        {
            Order order = repo.Orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order != null)
            {
                order.Shipped = true;
                repo.SaveOrder(order);
            }

            return RedirectToAction(nameof(MarkNotShipped));
        }

        public IActionResult Completed()
        {
            cart.ClearCart();
            return View();
        }
    }
}
