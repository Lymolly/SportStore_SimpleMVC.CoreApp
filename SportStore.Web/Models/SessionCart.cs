using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SportStore.Web.Infrastructure;

namespace SportStore.Web.Models
{
    public class SessionCart : Cart
    {
        public static Cart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            SessionCart sessionCart = session?.GetJson<SessionCart>("Cart") ?? new SessionCart();
            sessionCart.Session = session;
            return sessionCart;
        }
        [JsonIgnore]
        public ISession Session { get; set; }
        public override void AddProductToCart(Product product, int quantity)
        {
            base.AddProductToCart(product, quantity);
            Session.SetJson("Cart", this);
        }
        public override void RemoveProduct(Product product)
        {
            base.RemoveProduct(product);
            Session.SetJson("Cart", this);
        }
        public override void ClearCart()
        {
            base.ClearCart();
            Session.SetJson("Cart", this);
        }
    }
}
