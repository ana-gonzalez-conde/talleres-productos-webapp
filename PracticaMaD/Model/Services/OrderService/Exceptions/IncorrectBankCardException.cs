using System;

namespace Model.Services.Exceptions
{

    /// <summary>
    /// Exception throwed when a bank card is not allowed for a concrete loginName
    /// </summary>
    [Serializable]
    public class IncorrectBankCardException : Exception
    {
        public IncorrectBankCardException(String loginName)
            : base("Incorrect bank card exception => loginName = " + loginName)
        {
            this.LoginName = loginName;
        }

        public String LoginName { get; private set; }
    }
}
