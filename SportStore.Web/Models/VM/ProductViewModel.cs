using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Web.Models.VM
{
    public class ProductViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PaginInfo PaginInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}
