using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SportStore.Web.Models
{
    public class Order
    {
        [BindNever]
        public int OrderId { get; set; }
        [BindNever]
        public ICollection<CartLine> Lines { get; set; }
        [BindNever]
        public bool Shipped { get; set; }
        [Required(ErrorMessage = "Enter name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter first address line")]
        public string AdressLine1 { get; set; }
        public string AdressLine2 { get; set; }
        public string AdressLine3 { get; set; }
        [Required(ErrorMessage ="Enter a city")]
        public string City { get; set; }
        [Required(ErrorMessage ="Enter a Country")]
        public string Country { get; set; }
        public string Zip { get; set; }
        public bool GiftWrap { get; set; }
    }
}
