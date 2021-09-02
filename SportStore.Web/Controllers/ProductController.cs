using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportStore.Web.Models;
using SportStore.Web.Models.VM;

namespace SportStore.Web.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repo;
        public int PageSize { get; set; }

        public ProductController(IProductRepository repository)
        {
            repo = repository;
            PageSize = 2;
        }
        public ViewResult Products(string category,int productPage = 1)
        {
            //var res = repo.GetProducts.ToList();
            var res = ProductsHelper(category,productPage);
            return View("../Products",res);
        }

        private ProductViewModel ProductsHelper(string category, int productPage)
        {
            return new ProductViewModel
            {
                Products =
                    repo.GetProducts
                        .Where(p => category == null || p.Category == category)
                        .OrderBy(p => p.ProductId)
                        .Skip((productPage - 1) * PageSize)
                        .Take(PageSize),

                PaginInfo = new PaginInfo
                {
                    CurrentPage =productPage,
                    ItemsOnPage = PageSize,
                    TotalItems = repo.GetProducts.Count()
                },
                CurrentCategory = category
            };
        }
    }
}
