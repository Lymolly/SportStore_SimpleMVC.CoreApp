using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportStore.Web.Models;

namespace SportStore.Web.Components
{
    public class NavMenuViewComponent : ViewComponent
    {
        private IProductRepository repo;

        public NavMenuViewComponent(IProductRepository repository)
        {
            repo = repository;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            var res = repo.GetProducts.Select(x => x.Category).Distinct().OrderBy(x => x).ToList();
            return View("Default",res);
        }
    }
}
