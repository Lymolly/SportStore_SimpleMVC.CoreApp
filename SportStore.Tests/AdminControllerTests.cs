using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SportStore.Web.Controllers;
using SportStore.Web.Models;
using Xunit;

namespace SportStore.Tests
{
    public class AdminControllerTests
    {
        [Fact]
        public void Index_ReturnsAllProducts()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(r => r.GetProducts).Returns((new Product[]
            {
                new Product{ProductId = 1,Name = "Product1"},
                new Product{ProductId = 2,Name = "Product2"},
                new Product{ProductId = 3,Name = "Product3"},
                new Product{ProductId = 4,Name = "Product4"}
            }).AsQueryable());

            AdminController target = new AdminController(mock.Object);
            var result = GetViewModel<IEnumerable<Product>>(target.Index()).ToArray();

            Assert.Equal(4, result.Length);
            Assert.Equal("Product1", result[0].Name);
            Assert.Equal("Product3", result[2].Name);
        }

        private T GetViewModel<T>(IActionResult actionResult) where T : class
        {
            return (actionResult as ViewResult)?.ViewData.Model as T;
        }
    }
}
