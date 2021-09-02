using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Web.Models
{
    public class FakeRepository : IProductRepository
    {
        public IQueryable<Product> GetProducts => new List<Product>
        {
            new Product{Name = "Product1",Price=50},
            new Product{Name = "Product2",Price=60},
            new Product{Name = "Product3",Price=70},
            new Product{Name = "Product4",Price=20},

        }.AsQueryable();

        public void SaveProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
