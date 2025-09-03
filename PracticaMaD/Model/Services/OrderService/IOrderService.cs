using Es.Udc.DotNet.ModelUtil.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Services.OrderService
{
    public interface IOrderService
    {
        [Transactional]
        Int64 CreateOrder(long userId, List<OrderLinePurchaseDetails> orderLines, Int64 cardId, string shippingAddress, string DescriptiveName, bool expressShipping);
        [Transactional]
        List<OrderDetails> GetOrdersByUserId(Int64 userId);
        [Transactional]
        List<OrderLineDisplayDetails> GetLineOrdersByOrderId(Int64 orderId);
    }

}
