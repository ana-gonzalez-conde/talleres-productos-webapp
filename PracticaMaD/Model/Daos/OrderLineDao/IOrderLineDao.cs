using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;

namespace Model.Daos.OrderLineDao
{
    public interface IOrderLineDao: IGenericDao<OrderLine, (Int64, Int64)>
    {
        List<OrderLine> FindByOrderId(Int64 orderId);
    }
}
