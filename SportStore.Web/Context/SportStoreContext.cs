using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SportStore.Web.Models;

namespace SportStore.Web.Context
{
    public class SportStoreContext : DbContext
    {
        public SportStoreContext(DbContextOptions<SportStoreContext> opt) : base(opt)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
