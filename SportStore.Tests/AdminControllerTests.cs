using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
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

        [Fact]
        public void Edit_CanEditProduct()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();

            mock.Setup(r => r.GetProducts).Returns(new Product[]
            {
                new Product{ProductId = 1,Name = "Product1"},
                new Product{ProductId = 2,Name = "Product2"},
                new Product{ProductId = 3,Name = "Product3"},
                new Product{ProductId = 4,Name = "Product4"}
            }.AsQueryable());
            AdminController target = new AdminController(mock.Object);

            var product1 = GetViewModel<Product>(target.Edit(1));
            var product2 = GetViewModel<Product>(target.Edit(3));

            Assert.Equal(1, product1.ProductId);
            Assert.Equal(3, product2.ProductId);
        }
        [Fact]
        public void Edit_CanNotEditProduct_InvalidId()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();

            mock.Setup(r => r.GetProducts).Returns(new Product[]
            {
                new Product{ProductId = 1,Name = "Product1"},
                new Product{ProductId = 2,Name = "Product2"},
                new Product{ProductId = 3,Name = "Product3"},
                new Product{ProductId = 4,Name = "Product4"}
            }.AsQueryable());
            AdminController target = new AdminController(mock.Object);

            var product1 = GetViewModel<Product>(target.Edit(5));
            var product2 = GetViewModel<Product>(target.Edit(0));

            Assert.Null(product1);
            Assert.Null(product2);
        }

        [Fact]
        public void Edit_CreateNewProduct_Valid()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            Mock<ITempDataDictionary> tempData = new Mock<ITempDataDictionary>();

            int productsCount = mock.Object.GetProducts.Count();

            AdminController target = new AdminController(mock.Object)
            {
                TempData = tempData.Object
            };
            Product product = new Product() { Name = "Test product" };
            IActionResult result = target.Edit(product);

            mock.Verify(m => m.SaveProduct(product));
            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index",(result as RedirectToActionResult)?.ActionName);
            Assert.Equal(1,productsCount + 1);
        }
        [Fact]
        public void Edit_CannotSaveNewProduct()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();

            AdminController target = new AdminController(mock.Object);
            Product product = new Product() { Name = "Test product" };
            target.ModelState.AddModelError("test_error","Invalid data!");

            IActionResult result = target.Edit(product);

            mock.Verify(m => m.SaveProduct(It.IsAny<Product>()),Times.Never);
            Assert.IsType<ViewResult>(result);
        }

        private T GetViewModel<T>(IActionResult actionResult) where T : class
        {
            return (actionResult as ViewResult)?.ViewData.Model as T;
        }
    }
}
