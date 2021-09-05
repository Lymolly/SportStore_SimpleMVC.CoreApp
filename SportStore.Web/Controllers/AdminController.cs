using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using SportStore.Web.Models;

namespace SportStore.Web.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        IProductRepository repo;

        public AdminController(IProductRepository repository)
        {
            repo = repository;
        }
        [HttpGet]
        public ViewResult Index() => View(repo.GetProducts);

        [HttpGet]
        public ActionResult Edit(int productId)
        {
            return View(repo.GetProducts.FirstOrDefault(p => p.ProductId == productId));
        }
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                repo.SaveProduct(product);
                TempData["Message"] = $"Product: {product.Name} has been saved.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("", "Oops, something went wrong");
                return View(product);
            }
        }

        [HttpGet]
        public IActionResult Create() => View("Edit", new Product());

        [HttpPost]
        public IActionResult Delete(int productId)
        {
            if (productId != 0)
            {
                string deletedProductName = repo.DeleteProduct(productId);
                if (deletedProductName != null)
                {
                    TempData["Message"] = $"{deletedProductName} has been deleted!";
                }
            }
            return RedirectToAction(nameof(Index));
        }


    }
}
