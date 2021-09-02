using System.Linq;

namespace SportStore.Web.Models
{
    public interface IProductRepository
    {
        IQueryable<Product> GetProducts { get;}
        void SaveProduct(Product product);
    }
}