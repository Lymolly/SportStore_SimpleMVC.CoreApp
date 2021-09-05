using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Web.Models.Identity
{
    public class LoginViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [UIHint("password")]
        //[DataType(DataType.Password)]
        public string Password { get; set; }

        public string ReturnUrl { get; set; } = "/";
    }
}
