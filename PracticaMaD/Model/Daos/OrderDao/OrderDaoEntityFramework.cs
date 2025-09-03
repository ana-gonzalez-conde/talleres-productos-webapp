using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Model.Daos.OrderDao
{
    public class OrderDaoEntityFramework: GenericDaoEntityFramework<Order, Int64>, IOrderDao
    {
        #region Public Constructors

        /// <summary>
        /// Public Constructor
        /// </summary>
        public OrderDaoEntityFramework()
        {
        }

        public List<Order> FindByUserId(Int64 userId)
        {
            List<Order> order = null;
            DbSet<Order> orderProfiles = Context.Set<Order>();
            var orders = from o in orderProfiles
                         where o.userId == userId
                         orderby o.purchasingDate descending
                         select o;
            order = orders.ToList();

            return order;
        }

        #endregion Public Constructors

    }
}