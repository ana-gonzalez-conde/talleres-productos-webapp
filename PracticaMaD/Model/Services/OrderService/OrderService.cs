using Es.Udc.DotNet.ModelUtil.Transactions;
using Model.Daos.OrderDao;
using Model.Daos.OrderLineDao;
using Model.Daos.BankCardDao;
using Model.Daos.ProductDao;
using Model.Daos.UserDao;
using Model.Services.Exceptions;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Model.Services.OrderService
{
    public class OrderService : IOrderService
    {
        [Inject]
        public IOrderDao OrderDao { private get; set; }

        [Inject]
        public IOrderLineDao OrderLineDao { private get; set; }

        [Inject]
        public IBankCardDao BankCardDao { private get; set; }

        [Inject]
        public IProductDao ProductDao { private get; set; }

        [Inject]
        public IUserDao UserDao { private get; set; }

        /// <exception cref="UserNotAuthenticatedException"/>
        /// <exception cref="IncorrectBankCardException"/>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="InstanceNotFoundException"/>
        /// <summary>
        /// Allows to create a new order
        /// </summary>
        [Transactional]
        public Int64 CreateOrder(long userId, List<OrderLinePurchaseDetails> orderLinesDetails, Int64 cardId, string address, string descriptiveName, bool expressShipping)
        {
            
            ValidateUser(userId);
            ValidateCard(userId, cardId);
            ValidateOrderDetails(address, descriptiveName);
            UpdateProductStock(orderLinesDetails);

            Order order = CreateOrderEntry(userId, cardId, address, descriptiveName, expressShipping);
            decimal totalPrice = CreateOrderLines(order.orderId, orderLinesDetails);
            UpdateOrderTotalPrice(order, totalPrice);

            return order.orderId;
        }

        /// <summary>
        /// Allow to get all orders of an user by its id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Transactional]
        public List<OrderDetails> GetOrdersByUserId(Int64 userId)
        {
            ValidateUser(userId);

            List<Order> orders =
                OrderDao.FindByUserId(userId);
            List<OrderDetails> orderDetailsList = orders.Select(order => new OrderDetails(
                order.orderId,
                order.purchasingDate,
                order.name,
                order.totalPrice,
                order.expressShipping
             )).ToList();

            return orderDetailsList;
        }

        /// <summary>
        /// Allow to get all line orders of an order by its id
        /// </summary>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public List<OrderLineDisplayDetails> GetLineOrdersByOrderId(Int64 orderId)
        {
            List<OrderLine> lineOrders =
                OrderLineDao.FindByOrderId(orderId);
            var orderLineDetailsList = new List<OrderLineDisplayDetails>();

            foreach (var lineOrder in lineOrders)
            {
                var product = ProductDao.Find(lineOrder.productId);
                var orderLineDetail = new OrderLineDisplayDetails(
                    lineOrder.productId,
                    lineOrder.units,
                    lineOrder.price,
                    product.name,
                    product.description,
                    product.image
                );
                orderLineDetailsList.Add(orderLineDetail);
            }

            return orderLineDetailsList;
        }

        // Verifica que el usuario existe y está autenticado.
        private void ValidateUser(long userId)
        {
            if (!UserDao.Exists(userId))
            {
                throw new UserNotAuthenticatedException("El usuario no está autenticado o no existe.");
            }
        }

        // Verificar que la tarjeta bancaria pertenece al usuario.
        private void ValidateCard(long userId, long cardId)
        {
            var card = BankCardDao.Find(cardId);
            if (card.userId != userId)
            {
                throw new IncorrectBankCardException("La tarjeta no pertenece al usuario");
            }
        }

        // Verifica que la dirección de envío y el nombre descriptivo no sean nulos o vacíos.
        private void ValidateOrderDetails(string address, string descriptiveName)
        {
            if (string.IsNullOrWhiteSpace(address) || string.IsNullOrWhiteSpace(descriptiveName))
            {
                throw new ArgumentException("La dirección de envío y el nombre descriptivo no pueden estar vacíos.");
            }
        }

        // Verifica si hay suficiente stock para realizar el pedido, actualiza el stock resultante.
        private void UpdateProductStock(List<OrderLinePurchaseDetails> orderLinesDetails)
        {
            foreach (var lineDetail in orderLinesDetails)
            {
                var product = ProductDao.Find(lineDetail.ProductId);
                if (product.stock < lineDetail.Units)
                {
                    throw new InsufficientStockExcepcion("Producto sin stock suficiente.");
                }
                product.stock -= lineDetail.Units;
                ProductDao.Update(product);
            }
        }
        
        // Crea un pedido con el precio a 0
        private Order CreateOrderEntry(long userId, long cardId, string address, string descriptiveName, bool expressShipping)
        {
            var order = new Order
            {
                userId = userId,
                cardId = cardId,
                address = address,
                name = descriptiveName,
                expressShipping = expressShipping,
                purchasingDate = DateTime.Now,
                totalPrice = 0 // Se actualizará después de crear las líneas de pedido.
            };
            OrderDao.Create(order);
            return order;
        }

        //Crea las lineas de pedido asociadas a un orderId de un pedido.
        private decimal CreateOrderLines(long orderId, List<OrderLinePurchaseDetails> orderLinesDetails)
        {
            decimal totalPrice = 0;
            foreach (var lineDetail in orderLinesDetails)
            {
                var orderLine = new OrderLine
                {
                    orderId = orderId,
                    productId = lineDetail.ProductId,
                    units = lineDetail.Units,
                    price = lineDetail.Price
                };
                totalPrice += lineDetail.Price * lineDetail.Units;
                OrderLineDao.Create(orderLine);
            }
            return totalPrice;
        }

        //Actualiza el precio total de un pedido despues de calcularlo
        private void UpdateOrderTotalPrice(Order order, decimal totalPrice)
        {
            order.totalPrice = totalPrice;
            OrderDao.Update(order);
        }
    }
}
