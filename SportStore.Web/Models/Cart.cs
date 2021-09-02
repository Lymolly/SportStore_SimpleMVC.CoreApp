using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Web.Models
{
    [Serializable]
    public class Cart
    {
        private List<CartLine> _cartLines = new List<CartLine>();

        public virtual void AddProductToCart(Product product,int quantity)
        {
            CartLine cartLine = _cartLines.FirstOrDefault(p => p.Product.ProductId == product.ProductId);
            if (cartLine == null)
            {
                _cartLines.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                cartLine.Quantity += quantity;
            }
        }

        public virtual void RemoveProduct(Product product)
        {
            if (product != null)
            {
                _cartLines.RemoveAll(p => p.Product.ProductId == product.ProductId);
            }
        }

        public virtual decimal ComputeTotalSum() => _cartLines.Sum(p => p.Product.Price * p.Quantity);
        public virtual void ClearCart() => _cartLines.Clear();
        public virtual IEnumerable<CartLine> Lines => _cartLines;
    }
    public class CartLine
    {
        public int CartLineId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
