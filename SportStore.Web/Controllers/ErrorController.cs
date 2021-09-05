using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Web.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Error() => View();
    }
}
