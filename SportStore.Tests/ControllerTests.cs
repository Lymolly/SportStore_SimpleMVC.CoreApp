using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using SportStore.Web.Controllers;
using SportStore.Web.Models;
using SportStore.Web.Models.VM;
using Xunit;

namespace SportStore.Tests
{
    public class ControllerTests
    {
        [Fact]
        public void PaginateWorksCorrectly_Products()
        {
            Mock<IProductRepository> repoMock = new Mock<IProductRepository>();
            repoMock.Setup(m => m.GetProducts).Returns(new Product[]
            {
                new Product { ProductId = 1, Name = "P1" },
                new Product { ProductId = 2, Name = "P2" },
                new Product { ProductId = 3, Name = "P3" },
                new Product { ProductId = 4, Name = "P4" },
                new Product { ProductId = 5, Name = "P5" }
            }.AsQueryable());
            ProductController pc = new ProductController(repoMock.Object);
            pc.PageSize = 2;

            ProductViewModel res = pc.Products(null,2).ViewData.Model as ProductViewModel;

            Product[] products = res.Products.ToArray();
            Assert.True(products.Length == 2);
            Assert.Equal("P3", products[0].Name);
            Assert.Equal("P4", products[1].Name);
        }
        [Fact]
        public void Can_Filter_Products()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(p => p.GetProducts).Returns(new Product[]
            {
                new Product { ProductId = 1, Name = "Cat1",Category="Cat" },
                new Product { ProductId = 2, Name = "Cat2",Category="Cat" },
                new Product { ProductId = 3, Name = "Dog1",Category="Dog"},
                new Product { ProductId = 4, Name = "Cat3",Category="Cat"},
                new Product { ProductId = 5, Name = "Dog2",Category="Dog"}
            }.AsQueryable());
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;
            Product[] result = (controller.Products("Dog", 1).ViewData.Model as ProductViewModel)
                .Products.ToArray();

            Assert.Equal(2, result.Length);
            Assert.True(result[0].Name == "Dog1" && result[0].Category =="Dog");
            Assert.True(result[1].Name == "Dog2" && result[0].Category == "Dog");
        }
    }
}
