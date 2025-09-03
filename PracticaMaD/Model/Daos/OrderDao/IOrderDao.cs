using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;

namespace Model.Daos.OrderDao
{
    public interface IOrderDao : IGenericDao<Order, Int64>
    {
        List<Order> FindByUserId(Int64 userId);
    }
}
