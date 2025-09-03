using Model.Services.ProductService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Services.OrderService
{
    public class CartUnit
    {

        #region Properties Region

        public long ProductId { get; private set; }

        public ProductDetails ProductDetails { get; private set; }

        public long Quantity { get; set; }

        public CartUnit(long productId, ProductDetails productDetails, long quantity)
        {
            ProductId = productId;
            ProductDetails = productDetails;
            Quantity = quantity;
        }

        public bool EnoughStock(long quantityToAdd)
        {
            return ProductDetails.Stock >= Quantity+quantityToAdd;
        }

        public override bool Equals(object obj)
        {
            return obj is CartUnit unit &&
                   ProductId == unit.ProductId &&
                   EqualityComparer<ProductDetails>.Default.Equals(ProductDetails, unit.ProductDetails) &&
                   Quantity == unit.Quantity;
        }

        public override int GetHashCode()
        {
            int hashCode = 1938624771;
            hashCode = hashCode * -1521134295 + ProductId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<ProductDetails>.Default.GetHashCode(ProductDetails);
            hashCode = hashCode * -1521134295 + Quantity.GetHashCode();
            return hashCode;
        }


        public override String ToString()
        {
            return "[ ProductDetails = " + ProductDetails + " | " +
                "Quantity = " + Quantity + " | " +
                 "ProductId = " + ProductId + " ]";
        }
        #endregion Properties Region



    }
}
