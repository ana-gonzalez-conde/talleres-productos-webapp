using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Services.UserService
{
    /// <summary>
    /// VO Class which contains the card details
    /// </summary>
    [Serializable()]
    public class CardDetails
    {
        #region Properties Region
        public long CardId { get; private set; }

        public Byte Type { get; private set; }

        public string Number { get; private set; }

        public string Cvv { get; private set; }
        public DateTime ExpirationData { get; private set; }

        public bool IsDefault { get; set; }

        public CardDetails(Byte type, string number, string cvv, DateTime expirationData, bool isDefault)
        {
            Type = type;
            Number = number;
            Cvv = cvv;
            ExpirationData = expirationData;
            IsDefault = isDefault;
        }

        public CardDetails(long cardId, Byte type, string number, string cvv, DateTime expirationData, bool isDefault)
        {
            CardId = cardId;
            Type = type;
            Number = number;
            Cvv = cvv;
            ExpirationData = expirationData;
            IsDefault = isDefault;
        }

        public CardDetails(Byte type, string number, string cvv, DateTime expirationData)
        {
            Type = type;
            Number = number;
            Cvv = cvv;
            ExpirationData = expirationData;
            IsDefault = false;
        }



        public override String ToString()
        {
            String strUserProfileDetails;

            strUserProfileDetails =
                "[ type = " + Type + " | " +
                "number = " + Number + " | " +
                "cvv = " + Cvv + " | " +
                "is default = " + IsDefault + " | " +
                "ExpirationData = " + ExpirationData + " ]";


            return strUserProfileDetails;
        }

        public override bool Equals(object obj)
        {
            return obj is CardDetails details &&
                   Type == details.Type &&
                   Number == details.Number &&
                   Cvv == details.Cvv &&
                   ExpirationData == details.ExpirationData &&
                   IsDefault == details.IsDefault;
        }

        public override int GetHashCode()
        {
            int hashCode = -167019591;
            hashCode = hashCode * -1521134295 + Type.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Number);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Cvv);
            hashCode = hashCode * -1521134295 + ExpirationData.GetHashCode();
            hashCode = hashCode * -1521134295 + IsDefault.GetHashCode();
            return hashCode;
        }


        #endregion


    }
}
