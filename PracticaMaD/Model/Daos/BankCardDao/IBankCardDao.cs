using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
namespace Model.Daos.BankCardDao
{
    public interface IBankCardDao : IGenericDao<BankCard, Int64>
    {
        BankCard FindDefaultCardByUserId(Int64 userId);
        List<BankCard> FindCardsByUserId(Int64 userId);
        void DeactivateDefaultCard(Int64 userId);
    }
}
