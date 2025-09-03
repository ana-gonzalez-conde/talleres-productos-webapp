using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Services.OrderService
{
    [Serializable()]
    public class OrderDetails
    {
        public Int64 OrderId { get; private set; }
        public DateTime PurchasingDate { get; private set; }
        public string DescriptiveName { get; private set; }
        public decimal TotalPrice { get; private set; }
        public bool ExpressShipping { get; private set; }

        public OrderDetails(Int64 orderId, DateTime purchasingDate, string descriptiveName, decimal totalPrice, bool expressShipping)
        {
            OrderId = orderId;
            PurchasingDate = purchasingDate;
            DescriptiveName = descriptiveName;
            TotalPrice = totalPrice;
            ExpressShipping = expressShipping;
        }

        public override String ToString()
        {
            String strUserProfileDetails;

            strUserProfileDetails =
                "OrderId = " + OrderId + " | " +
                "PurchasingDate = " + PurchasingDate + " | " +
                "DescriptiveName = " + DescriptiveName + " | " +
                "ExpressShipping = " + ExpressShipping + " | " +
                "TotalPrice = " + TotalPrice + " ]";


            return strUserProfileDetails;
        }

        public override bool Equals(object obj)
        {
            return obj is OrderDetails details &&
                   OrderId == details.OrderId &&
                   PurchasingDate == details.PurchasingDate &&
                   DescriptiveName == details.DescriptiveName &&
                   ExpressShipping == details.ExpressShipping &&
                   TotalPrice == details.TotalPrice;
        }

        public override int GetHashCode()
        {
            int hashCode = -1039942357;
            hashCode = hashCode * -1521134295 + OrderId.GetHashCode();
            hashCode = hashCode * -1521134295 + PurchasingDate.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DescriptiveName);
            hashCode = hashCode * -1521134295 + TotalPrice.GetHashCode();
            hashCode = hashCode * -1521134295 + ExpressShipping.GetHashCode();
            return hashCode;
        }
    }
}