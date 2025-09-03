using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Services.OrderService
{
    [Serializable()]
    public class OrderLineDisplayDetails
    {
        public long ProductId { get; private set; }
        public int Units { get; private set; }
        public decimal Price { get; private set; }
        public string ProductName { get; private set; }
        public string ProductDescription { get; private set; }
        public string ProductImage { get; private set; }

        public OrderLineDisplayDetails(long productId, int units, decimal price, string productName, string productDescription, string productImage)
        {
            ProductId = productId;
            Units = units;
            Price = price;
            ProductName = productName;
            ProductDescription = productDescription;
            ProductImage = productImage;
        }

        public override String ToString()
        {
            String strUserProfileDetails;

            strUserProfileDetails =
                "[ ProductId = " + ProductId + " | " +
                "Units = " + Units + " | " +
                "ProductName = " + ProductName + " | " +
                "ProductDescription = " + ProductDescription + " | " +
                "ProductImage = " + ProductImage + " | " +
                "Price = " + Price + " ]";


            return strUserProfileDetails;
        }

        public override bool Equals(object obj)
        {
            return obj is OrderLineDisplayDetails details &&
                   ProductId == details.ProductId &&
                   Units == details.Units &&
                   Price == details.Price &&
                   ProductName == details.ProductName &&
                   ProductDescription == details.ProductDescription &&
                   ProductImage == details.ProductImage;
        }

        public override int GetHashCode()
        {
            int hashCode = -1183167819;
            hashCode = hashCode * -1521134295 + ProductId.GetHashCode();
            hashCode = hashCode * -1521134295 + Units.GetHashCode();
            hashCode = hashCode * -1521134295 + Price.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ProductName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ProductDescription);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ProductImage);
            return hashCode;
        }
    }
}
