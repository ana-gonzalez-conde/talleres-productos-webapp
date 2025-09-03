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
    public class CardDTO
    {
        #region Properties Region
        public long CardId { get; private set; }

        public string Number { get; private set; }

        public bool IsDefault { get; set; }

        public CardDTO(long cardId, string number, bool isDefault)
        {
            CardId = cardId;
            Number = number;
            IsDefault = isDefault;
        }


        public override String ToString()
        {
            String strUserProfileDetails;

            strUserProfileDetails =
                "[ CardId = " + CardId + " | " +
                "number = " + Number + " | " +
                 "is default = " + IsDefault + " ]";


            return strUserProfileDetails;
        }

        public override bool Equals(object obj)
        {
            return obj is CardDTO dTO &&
                   CardId == dTO.CardId &&
                   Number == dTO.Number &&
                   IsDefault == dTO.IsDefault;
        }

        public override int GetHashCode()
        {
            int hashCode = -870449536;
            hashCode = hashCode * -1521134295 + CardId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Number);
            hashCode = hashCode * -1521134295 + IsDefault.GetHashCode();
            return hashCode;
        }



        #endregion


    }
}
