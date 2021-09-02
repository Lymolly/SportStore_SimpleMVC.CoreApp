using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Web.Models.VM
{
    public class PaginInfo
    {
        public int TotalItems { get; set; }
        public int ItemsOnPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages => (int)Math.Ceiling((decimal)TotalItems/ItemsOnPage);
    }
}
