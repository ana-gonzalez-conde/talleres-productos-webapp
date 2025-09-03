using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Model.Daos.OrderLineDao
{
    public class OrderLineDaoEntityFramework :
        GenericDaoEntityFramework<OrderLine, (Int64, Int64)>, IOrderLineDao
    {
        #region Public Constructors

        /// <summary>
        /// Public Constructor
        /// </summary>
        public OrderLineDaoEntityFramework()
        {
        }

        #endregion Public Constructors

        public List<OrderLine> FindByOrderId(Int64 orderId)
        {
            DbSet<OrderLine> orderLines = Context.Set<OrderLine>();

            var result = orderLines
                         .Where(ol => ol.orderId == orderId)
                         .ToList();

            // Si no se encontraron líneas de pedido, se lanza una excepción
            if (!result.Any())
            {
                throw new InstanceNotFoundException(orderId.ToString(), typeof(OrderLine).FullName);
            }

            return result;
        }
    }
}
