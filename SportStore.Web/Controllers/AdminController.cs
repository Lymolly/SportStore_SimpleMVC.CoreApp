using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportStore.Web.Models;

namespace SportStore.Web.Controllers
{
    public class AdminController : Controller
    {
        IProductRepository repo;

        public AdminController(IProductRepository repository)
        {
            repo = repository;
        }

        public ViewResult Index() => View(repo.GetProducts);
    }
}
