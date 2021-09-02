using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportStore.Web.Models;
using Xunit;

namespace SportStore.Tests
{
    public class CartTests
    {
        [Fact]
        public void Cart_CanAddNewProduct()
        {
            Product p1 = new Product {ProductId = 1, Name = "Pl" };
            Product p2 = new Product {ProductId = 2, Name = "Р2" };

            Cart target = new Cart();

            target.AddProductToCart(p1,1);
            target.AddProductToCart(p2,3);

            CartLine[] results = target.Lines.ToArray();

            Assert.Equal(2, results.Length);
            Assert.Equal(p1, results[0].Product);
            Assert.Equal(p2, results[1].Product);

        }
        [Fact]
        public void CanAddQuantity_ForExistingLine()
        {
            Product p1 = new Product { ProductId = 1, Name = "Pl" };
            Product p2 = new Product { ProductId = 2, Name = "Р2" };

            Cart target = new Cart();
            target.AddProductToCart(p1, 1);
            target.AddProductToCart(p1, 1);
            target.AddProductToCart(p2, 1);
            target.AddProductToCart(p2, 10);

            CartLine[] results = target.Lines.ToArray();

            Assert.Equal(2, results.Length);
            Assert.Equal(2, results[0].Quantity);
            Assert.Equal(11, results[1].Quantity);
        }

        [Fact]
        public void CanRemoveLine_FromCart()
        {
            Product p1 = new Product { ProductId = 1, Name = "Pl" };
            Product p2 = new Product { ProductId = 2, Name = "Р2" };
            Product p3 = new Product { ProductId = 3, Name = "Р3" };

            Cart target = new Cart();
            target.AddProductToCart(p1, 2);
            target.AddProductToCart(p2, 5);
            target.AddProductToCart(p3, 3);
            target.AddProductToCart(p1, 1);

            target.RemoveProduct(p1);

            Assert.Equal(0, target.Lines.Count(p => p.Product == p1));
            Assert.Equal(2, target.Lines.Count());

        }
    }
}
