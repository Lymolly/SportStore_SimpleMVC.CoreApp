using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SportStore.Web.Context;

namespace SportStore.Web.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            SportStoreContext ctx = app.ApplicationServices.GetRequiredService<SportStoreContext>();
            ctx.Database.Migrate();
            if (!ctx.Products.Any())
            {
                ctx.Products.AddRange(
                    new Product
                    {
                        Name = "Thinking Сар",
                        Description = "Improve brain efficiency Ьу 75i",
                        Category = "Chess",
                        Price = 16
                    },
                    new Product
                    {
                        Name = "Chair",
                        Description = "Imprfsdfs 75i",
                        Category = "Huita",
                        Price = 22
                    },
                    new Product
                    {
                        Name = "No Сар",
                        Description = "Improve qwqwqwi",
                        Category = "Huita",
                        Price = 12
                    },
                    new Product
                    {
                        Name = "Zalupa",
                        Description = "Idsldaldla",
                        Category = "Chess",
                        Price = 25
                    });
                ctx.SaveChanges();
            }
        }
    }
}
