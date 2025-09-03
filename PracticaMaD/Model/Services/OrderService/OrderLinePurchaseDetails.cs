using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Services.OrderService
{
    [Serializable()]
    public class OrderLinePurchaseDetails
    {
        public long ProductId { get; private set; }
        public int Units { get; private set; }
        public decimal Price { get; private set; }

        public OrderLinePurchaseDetails(long productId, int units, decimal price)
        {
            ProductId = productId;
            Units = units;
            Price = price;
        }

        public override bool Equals(object obj)
        {
            return obj is OrderLinePurchaseDetails details &&
                   ProductId == details.ProductId &&
                   Units == details.Units &&
                   Price == details.Price;
        }

        // The GetHashCode method is used in hashing algorithms and data 
        // structures such as a hash table. In order to ensure that it works 
        // properly, we suppose that the FirstName does not change.     
        public override int GetHashCode()
        {
            int hashCode = 1688416727;
            hashCode = hashCode * -1521134295 + ProductId.GetHashCode();
            hashCode = hashCode * -1521134295 + Units.GetHashCode();
            hashCode = hashCode * -1521134295 + Price.GetHashCode();
            return hashCode;
        }

        public override String ToString()
        {
            String strUserProfileDetails;

            strUserProfileDetails =
                "[ ProductId = " + ProductId + " | " +
                "Units = " + Units + " | " +
                "Price = " + Price + " ]";


            return strUserProfileDetails;
        }
    }
}
