using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Model.Daos.BankCardDao
{
    public class BankCardDaoEntityFramework : GenericDaoEntityFramework<BankCard, Int64>, IBankCardDao
    {
        #region Public Constructors

        /// <summary>
        /// Public Constructor
        /// </summary>
        public BankCardDaoEntityFramework()
        {
        }

        public BankCard FindDefaultCardByUserId(Int64 userId)
        {
            DbSet<BankCard> bankCards = Context.Set<BankCard>();

            var result = bankCards.FirstOrDefault(b => b.userId == userId && b.isDefault);

            if (result == null)
            {
                throw new InstanceNotFoundException(userId.ToString(), typeof(BankCard).FullName);
            }

            return result;
        }

        public List<BankCard> FindCardsByUserId(Int64 userId)
        {
            DbSet<BankCard> bankCards = Context.Set<BankCard>();

            var results = bankCards.Where(b => b.userId == userId && b.isActive).ToList();

            return results;
        }

        public void DeactivateDefaultCard(Int64 userId)
        {
            DbSet<BankCard> bankCards = Context.Set<BankCard>();

            var defaultCard = bankCards.FirstOrDefault(b => b.userId == userId && b.isDefault);

            if (defaultCard != null)
            {
                defaultCard.isDefault = false;
                Context.SaveChanges();
            }
        }
        #endregion Public Constructors

    }
}