using Model.Services.UserService;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Model.Services.OrderService
{
    [Serializable()]
    public class PreOrderData
    {
        public string ShippingAddress { get; private set; }
        public List<CardDTO> BankCards { get; private set; }

        public PreOrderData(string shippingAddress, List<CardDTO> defaultBankCard)
        {
            ShippingAddress = shippingAddress;
            BankCards = defaultBankCard;
        }

        public CardDTO DefaultCard()
        {
            return BankCards.FirstOrDefault(card => card.IsDefault);
        }

        public override String ToString()
        {
            String strUserProfileDetails;

            strUserProfileDetails =
                 BankCards.ToString() + " | " +
                "ShippingAddress = " + ShippingAddress + " ]";


            return strUserProfileDetails;
        }

        public override bool Equals(object obj)
        {
            return obj is PreOrderData data &&
                   ShippingAddress == data.ShippingAddress &&
                   EqualityComparer<List<CardDTO>>.Default.Equals(BankCards, data.BankCards);
        }

        public override int GetHashCode()
        {
            int hashCode = 1068219295;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ShippingAddress);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<CardDTO>>.Default.GetHashCode(BankCards);
            return hashCode;
        }
    }
}
