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
    public class OrderControllerTests
    {
        [Fact]
        public void Cannot_Checkout_EmptyCart()
        {
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
            Cart cart = new Cart();
            Order order = new Order();
            OrderController target = new OrderController(mock.Object, cart);

            ViewResult result = target.Checkout(order) as ViewResult;
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()),Times.Never);

            Assert.True(string.IsNullOrEmpty(result?.ViewName));
            Assert.False(result?.ViewData.ModelState.IsValid);
        }
        [Fact]
        public void Cannot_Checkout_InvalidShippingDetails()
        {
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
            Cart cart = new Cart();
            cart.AddProductToCart(new Product(), 1);
            OrderController target = new OrderController(mock.Object, cart);


            target.ModelState.AddModelError("error", "Invalid data!");

            ViewResult result = target.Checkout(new Order()) as ViewResult;
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);

            Assert.True(string.IsNullOrEmpty(result?.ViewName));
            Assert.False(result?.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void Checkout_Succefuly()
        {
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
            Cart cart = new Cart();
            cart.AddProductToCart(new Product(), 1);

            OrderController target = new OrderController(mock.Object, cart);
            RedirectToActionResult result = target.Checkout(new Order()) as RedirectToActionResult;

            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Once);

            Assert.Equal("Completed",result?.ActionName);
        }
    }
}
