using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Model.Daos.OrderDao;
using Model.Daos.UserDao;
using Model.Daos.CategoryDao;
using Model.Daos.ProductDao;
using Model.Daos.BankCardDao;
using Model.Daos.OrderLineDao;
using Model.Services.Exceptions;
using Model.Services.OrderService;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Ninject;
using System;
using System.Collections.Generic;
using System.Transactions;
using System.Linq;
using Model.Services.ProductService;

namespace Test.Services
{
    /// <summary>
    /// This is a test class for IOrderServiceTest and is intended
    /// to contain all IOrderServiceTest Unit Tests
    /// </summary>
    [TestClass]
    public class IOrderServiceTest
    {
        private static IKernel kernel;
        private static IOrderService orderService;
        private static IOrderDao orderDao;
        private static IUserDao userDao;
        private static ICategoryDao categoryDao;
        private static IProductDao productDao;
        private static IBankCardDao bankCardDao;
        private static IOrderLineDao orderLineDao;

        private User user1;
        private User user2;

        private BankCard bankCard1;
        private BankCard bankCard2;
        private BankCard bankCard3;

        private Category category1;
        private Category category2;
        private Category category3;

        private Product product1;
        private Product product2;
        private Product product3;

        CartUnit CartUnit;
        CartUnit CartUnit2;

        ShoppingCartActions cart = new ShoppingCartActions();
        /// <summary>
        /// Gets or sets the test context which provides information about and functionality for the
        /// current test run.
        /// </summary>


        private TransactionScope transactionScope;

        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }


        /// <summary>
        /// A test for CreateOrder
        /// </summary>
        [TestMethod]
        public void CreateOrderTest()
        {
                // Assume existing userId, cardId and list of OrderLineDetails

                string address = "Test Address";
                string descriptiveName = "Test Order";
                List<OrderLinePurchaseDetails> orderLinesDetails = new List<OrderLinePurchaseDetails>
                    {
                        new OrderLinePurchaseDetails(product1.productId, 2, 100m), // Assuming productId 1 exists
                        new OrderLinePurchaseDetails(product2.productId, 1, 50m)  // Assuming productId 2 exists
                    };

                // Call CreateOrder method
                var orderId = orderService.CreateOrder(user1.userId, orderLinesDetails, bankCard1.cardId, address, descriptiveName, true);

                // Asserts to verify if the order was created successfully
                Assert.IsTrue(orderId > 0, "Order was not created successfully.");

                var order = orderService.GetOrdersByUserId(user1.userId);
                Assert.IsNotNull(order, "Order was not found after creation.");

                var orderLineDisplayDetails = orderService.GetLineOrdersByOrderId(orderId);

                // Assert
                foreach (var expectedLineDetail in orderLinesDetails)
                {
                    var actualLineDetail = orderLineDisplayDetails.FirstOrDefault(
                        line => line.ProductId == expectedLineDetail.ProductId &&
                                line.Units == expectedLineDetail.Units &&
                                line.Price == expectedLineDetail.Price);

                    Assert.IsNotNull(actualLineDetail, $"Expected order line for product ID {expectedLineDetail.ProductId} was not found.");
                }
        }

        [TestMethod]
        public void AddCartUnitTest()
        {
            // Arrange: Assume cart is empty initially for this test (resetting to ensure clean state)
            cart.ResetShoppingCart();

            // Act: Add new cart unit
            bool addResult = cart.AddCartUnit(CartUnit);

            // Assert: Check if the cart unit was added
            Assert.IsTrue(addResult, "CartUnit should be added successfully.");
            Assert.AreEqual(1, cart.cartUnits.Count, "There should be one CartUnit in the cart.");

            // Act: Add the same cart unit again to check quantity increment
            addResult = cart.AddCartUnit(CartUnit);

            // Assert: Check if the quantity was incremented
            Assert.IsTrue(addResult, "CartUnit quantity should be incremented.");
            Assert.AreEqual(11, cart.cartUnits.First().Quantity, "Quantity of the CartUnit should now be 11.");
        }

        [TestMethod]
        public void AddCartUnitWithoutStockTest()
        {
            // Arrange
            CartUnit cartUnitLowStock = new CartUnit(5, new ProductDetails("Low Stock Item", 19.99f, DateTime.Now, 1, "low_stock.jpg", "Almost out of stock!", 1), 1);

            // Act
            cart.ResetShoppingCart();
            bool firstAddResult = cart.AddCartUnit(cartUnitLowStock); // Should succeed
            bool secondAddResult = cart.AddCartUnit(cartUnitLowStock); // Should fail

            // Assert
            Assert.IsTrue(firstAddResult, "First addition should succeed.");
            Assert.IsFalse(secondAddResult, "Second addition should fail due to insufficient stock.");
        }

        [TestMethod]
        public void RemoveCartUnitTest()
        {
            // Arrange: Add a product to the cart
            cart.ResetShoppingCart();
            cart.AddCartUnit(CartUnit);

            // Act: Remove the product
            cart.RemoveCartUnit(CartUnit.ProductId);

            // Assert: Cart should be empty
            Assert.AreEqual(0, cart.cartUnits.Count, "Cart should be empty after removal.");
        }

        [TestMethod]
        public void SetQuantityTest()
        {
            // Arrange: Add a product and set a new quantity
            cart.ResetShoppingCart();
            cart.AddCartUnit(CartUnit);
            long newQuantity = 5;

            // Act
            cart.SetQuantity(CartUnit.ProductId, newQuantity);

            // Assert
            Assert.AreEqual(newQuantity, cart.cartUnits.First().Quantity, "Quantity should be updated to 5.");
        }

        [TestMethod]
        public void CalculateTotalPriceTest()
        {
            // Arrange: Add multiple products
            cart.ResetShoppingCart();
            cart.AddCartUnit(CartUnit);
            cart.AddCartUnit(CartUnit2);

            // Act: Calculate total price
            float totalPrice = cart.GetTotalPrice();

            // Assert
            float expectedTotal = (CartUnit.ProductDetails.Price * CartUnit.Quantity) + (CartUnit2.ProductDetails.Price * CartUnit2.Quantity);
            Assert.AreEqual(expectedTotal, totalPrice, "Total price calculation is incorrect.");
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "El nombre descriptivo del pedido no puede estar vacío.")]
        public void CreateOrderWithoutDescriptiveNameTest()
        {
            string address = "Test Address";
            string descriptiveName = ""; // Nombre descriptivo vacío
            List<OrderLinePurchaseDetails> orderLinesDetails = new List<OrderLinePurchaseDetails>
                    {
                        new OrderLinePurchaseDetails(product1.productId, 2, 100m), // Assuming productId 1 exists
                        new OrderLinePurchaseDetails(product2.productId, 1, 50m)  // Assuming productId 2 exists
                    };


            // Intentar crear el pedido
            var orderId = orderService.CreateOrder(user1.userId, orderLinesDetails, bankCard1.cardId, address, descriptiveName, true);

            // Si llega aquí, el test ha fallado
            Assert.Fail("Se esperaba una excepción por nombre descriptivo vacío.");
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "La dirección del pedido no puede estar vacía.")]
        public void CreateOrderWithoutAddressTest()
        {
            string address = "";
            string descriptiveName = "Descriptive Name"; // Nombre descriptivo vacío
            List<OrderLinePurchaseDetails> orderLinesDetails = new List<OrderLinePurchaseDetails>
                    {
                        new OrderLinePurchaseDetails(product1.productId, 2, 100m), // Assuming productId 1 exists
                        new OrderLinePurchaseDetails(product2.productId, 1, 50m)  // Assuming productId 2 exists
                    };


            // Intentar crear el pedido
            var orderId = orderService.CreateOrder(user1.userId, orderLinesDetails, bankCard1.cardId, address, descriptiveName, true);

            // Si llega aquí, el test ha fallado
            Assert.Fail("Se esperaba una excepción por dirección vacía.");
        }

        [TestMethod]
        [ExpectedException(typeof(UserNotAuthenticatedException), "El usuario no está autenticado o no existe.")]
        public void CreateOrderWithNonexistentUserTest()
        {
            string address = "Test Address";
            string descriptiveName = "Test Order";
            Int64 userId = -1; // ID de usuario inexistente
            List<OrderLinePurchaseDetails> orderLinesDetails = new List<OrderLinePurchaseDetails>
                    {
                        new OrderLinePurchaseDetails(product1.productId, 2, 100m), // Assuming productId 1 exists
                        new OrderLinePurchaseDetails(product2.productId, 1, 50m)  // Assuming productId 2 exists
                    };

            var orderId = orderService.CreateOrder(userId, orderLinesDetails, bankCard1.cardId, address, descriptiveName, true);

            Assert.Fail("Se esperaba una excepción por usuario inexistente.");
        }

        [TestMethod]
        [ExpectedException(typeof(IncorrectBankCardException), "La tarjeta bancaria no pertenece al usuario.")]
        public void CreateOrderWithIncorrectCardidTest()
        {
            string address = "Test Address";
            string descriptiveName = "Test Order";
            List<OrderLinePurchaseDetails> orderLinesDetails = new List<OrderLinePurchaseDetails>
                    {
                        new OrderLinePurchaseDetails(product1.productId, 2, 100m), // Assuming productId 1 exists
                        new OrderLinePurchaseDetails(product2.productId, 1, 50m)  // Assuming productId 2 exists
                    };

            var orderId = orderService.CreateOrder(user1.userId, orderLinesDetails, bankCard2.cardId, address, descriptiveName, true);

            Assert.Fail("Se esperaba una excepción por tarjeta bancaria incorrecta.");
        }

        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException), "La tarjeta bancaria no esta registrada.")]
        public void CreateOrderWithNonexistentCardidTest()
        {
            string address = "Test Address";
            string descriptiveName = "Test Order";
            Int64 cardId = -1;
            List<OrderLinePurchaseDetails> orderLinesDetails = new List<OrderLinePurchaseDetails>
                    {
                        new OrderLinePurchaseDetails(product1.productId, 2, 100m), // Assuming productId 1 exists
                        new OrderLinePurchaseDetails(product2.productId, 1, 50m)  // Assuming productId 2 exists
                    };

            var orderId = orderService.CreateOrder(user1.userId, orderLinesDetails, cardId, address, descriptiveName, true);

            Assert.Fail("Se esperaba una excepción por por tarjeta bancaria inexistente.");
        }

        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException), "Instance not found")]
        public void CreateOrderWithNonexistentProductTest()
        {
            string address = "Test Address";
            string descriptiveName = "Test Order";
            List<OrderLinePurchaseDetails> orderLinesDetails = new List<OrderLinePurchaseDetails>
                {
                    new OrderLinePurchaseDetails(-1, 15, 1000m), // ID de producto inexistente
                };

            var orderId = orderService.CreateOrder(user1.userId, orderLinesDetails, bankCard1.cardId, address, descriptiveName, true);

            Assert.Fail("Se esperaba una excepción por producto inexistente.");
        }

        [TestMethod]
        [ExpectedException(typeof(InsufficientStockExcepcion), "Producto sin stock suficiente.")]
        public void CreateOrderWithoutStockTest()
        {
            string address = "Test Address";
            string descriptiveName = "Test Order";
            List<OrderLinePurchaseDetails> orderLinesDetails = new List<OrderLinePurchaseDetails>
                {
                    new OrderLinePurchaseDetails(product1.productId, 125, 10000m),
                };

            var orderId = orderService.CreateOrder(user1.userId, orderLinesDetails, bankCard1.cardId, address, descriptiveName, true);

            Assert.Fail("Se esperaba una excepción por producto sin stock.");
        }

        [TestMethod]
        public void GetOrdersByUserIdTest()
        {
                // Pedido de prueba
                List<OrderLinePurchaseDetails> orderLinesDetails = new List<OrderLinePurchaseDetails>
                {
                    new OrderLinePurchaseDetails(product1.productId, 1, 100m),
                    new OrderLinePurchaseDetails(product2.productId, 2, 200m)
                };

                string testAddress = user1.address; // Usar dirección del usuario
                string descriptiveName = "Pedido de Prueba";

                var createdOrderId = orderService.CreateOrder(user1.userId, orderLinesDetails, bankCard1.cardId, testAddress, descriptiveName, true);
                Assert.IsTrue(createdOrderId > 0, "El pedido de prueba no se creó correctamente.");

                // Actuar - Recuperar los pedidos del usuario
                var ordersDetails = orderService.GetOrdersByUserId(user1.userId);

                // Verificar
                Assert.IsNotNull(ordersDetails, "La lista de detalles de pedidos no debe ser nula.");
                Assert.IsTrue(ordersDetails.Count > 0, "Debería al menos un pedido para el usuario.");

                var retrievedOrder = ordersDetails.FirstOrDefault(od => od.OrderId == createdOrderId);
                Assert.IsNotNull(retrievedOrder, "El pedido creado debe estar en la lista recuperada.");
                Assert.AreEqual(descriptiveName, retrievedOrder.DescriptiveName, "El nombre descriptivo del pedido no coincide.");
                Assert.AreEqual(orderLinesDetails.Sum(ol => ol.Price * ol.Units), retrievedOrder.TotalPrice, "El precio total del pedido no coincide.");

        }

        [TestMethod]
        public void GetLineOrdersByOrderIdTest()
        {
                // Crea un pedido de prueba
                var orderId = orderService.CreateOrder(
                    user1.userId,
                    new List<OrderLinePurchaseDetails>{
                        new OrderLinePurchaseDetails(product1.productId, 2, 200m),
                        new OrderLinePurchaseDetails(product2.productId, 1, 100m)
                    },
                    bankCard1.cardId,
                    user1.address,
                    "Pedido con Líneas",
                    true
                );

                // Recupera las líneas de pedido del pedido creado
                var lineOrders = orderService.GetLineOrdersByOrderId(orderId);

                // Verificar
                Assert.IsNotNull(lineOrders, "Debería devolver líneas de pedido.");
                Assert.IsTrue(lineOrders.Count == 2, "Debería tener 2 líneas de pedido.");

                // Verifica que los productos y cantidades coincidan con lo creado
                foreach (var lineOrder in lineOrders)
                {
                    Assert.IsTrue(
                        (lineOrder.ProductId == product1.productId || lineOrder.ProductId == product2.productId) &&
                        lineOrder.Units > 0,
                        "Las líneas de pedido no coinciden con las esperadas."
                    );
                }

        }

        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void GetLineOrdersByNonexistentOrderIdTest()
        {
            // Intenta recuperar líneas de pedido de un ID de pedido inexistente
            var lineOrders = orderService.GetLineOrdersByOrderId(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(UserNotAuthenticatedException))]
        public void GetOrderByNonexistentUserIdTest()
        {
            // Intenta recuperar un pedido con ID de usuario inexistente
            var Orders = orderService.GetOrdersByUserId(-1);
        }

        #region Additional test attributes
        [TestInitialize]
        public void MyTestInitialize()
        {
            transactionScope = new TransactionScope();
            InicializateUsers();
            InitializeBankCards();
            InitializeCategories();
            InitializeProducts();
            InicializeCart();
        }

        private void InicializeCart()
        {
            ProductDetails productDetails1 = new ProductDetails("Car Wax", 14.99f, DateTime.Parse("2024-03-29"), 50, "car_wax.jpg", "Protective wax for a shiny and clean car finish.", 2);
            ProductDetails productDetails2 = new ProductDetails("Car Wax Ultimate", 34.99f, DateTime.Parse("2024-03-29"), 50, "car_wax2.jpg", "Protective wax for a shiny and clean car finish ultimate.", 2);
            CartUnit = new CartUnit(3,productDetails1,10);
            CartUnit2 = new CartUnit(4, productDetails2, 2);

        }

        private void InicializateUsers()
        {
                user1 = new User
                {
                    login = "userTest1",
                    password = "password1",
                    name = "Nombre1",
                    surnames = "Apellidos1",
                    address = "Dirección 1",
                    email = "email1@test.com",
                    language = "es",
                    country = "ES"
                };

                user2 = new User
                {
                    login = "userTest2",
                    password = "password2",
                    name = "Nombre2",
                    surnames = "Apellidos2",
                    address = "Dirección 2",
                    email = "email2@test.com",
                    language = "es",
                    country = "ES"
                };

                userDao.Create(user1);
                userDao.Create(user2);
        }

        private void InitializeBankCards()
        {
                bankCard1 = new BankCard
                {
                    type = 1,
                    number = "1111222233334444",
                    cvv = "123",
                    expirationDate = DateTime.Now.AddYears(3),
                    isDefault = true,
                    isActive = true,
                    userId = user1.userId
                };

                bankCard2 = new BankCard
                {
                    type = 2,
                    number = "5555666677778888",
                    cvv = "456",
                    expirationDate = DateTime.Now.AddYears(3),
                    isDefault = true,
                    isActive = true,
                    userId = user2.userId
                };

                bankCard3 = new BankCard
                {
                    type = 1,
                    number = "1111222233335555",
                    cvv = "323",
                    expirationDate = DateTime.Now.AddYears(3),
                    isDefault = false,
                    isActive = true,
                    userId = user1.userId
                };

                bankCardDao.Create(bankCard1);
                bankCardDao.Create(bankCard2);
                bankCardDao.Create(bankCard3);
        }

        private void InitializeCategories()
        {
                category1 = new Category { name = "Aceites y lubricantes" };
                category2 = new Category { name = "Filtros" };
                category3 = new Category { name = "Baterías" };

                categoryDao.Create(category1);
                categoryDao.Create(category2);
                categoryDao.Create(category3);
        }

        private void InitializeProducts()
        {
                product1 = new Product
                {
                    name = "Aceite Motor 5W-30",
                    price = 45.99,
                    addingDate = DateTime.Now,
                    stock = 100,
                    image = "url_a_imagen_aceite.jpg",
                    description = "Aceite sintético para motores diésel y gasolina. Proporciona una excelente protección y rendimiento del motor bajo cualquier condición.",
                    categoryId = category1.categoryId
                };

                product2 = new Product
                {
                    name = "Filtro de aire",
                    price = 15.75,
                    addingDate = DateTime.Now,
                    stock = 50,
                    image = "url_a_imagen_filtro_aire.jpg",
                    description = "Filtro de aire de alto flujo, mejora la entrada de aire y la eficiencia del combustible.",
                    categoryId = category2.categoryId
                };

                // Producto sin stock
                product3 = new Product
                {
                    name = "Batería para coche 70Ah",
                    price = 89.95,
                    addingDate = DateTime.Now,
                    stock = 0, // Sin stock
                    image = "url_a_imagen_bateria.jpg",
                    description = "Batería de arranque de alta durabilidad y rendimiento para todo tipo de vehículos.",
                    categoryId = category3.categoryId
                };

                productDao.Create(product1);
                productDao.Create(product2);
                productDao.Create(product3);
        }

        // Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            kernel = TestManager.ConfigureNInjectKernel();
            orderDao = kernel.Get<IOrderDao>();
            orderLineDao = kernel.Get<IOrderLineDao>();
            userDao = kernel.Get<IUserDao>();
            bankCardDao = kernel.Get<IBankCardDao>();
            categoryDao = kernel.Get<ICategoryDao>();
            productDao = kernel.Get<IProductDao>();

            orderService = kernel.Get<IOrderService>();

        }

        // Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup]
        public static void MyClassCleanup()
        {
            TestManager.ClearNInjectKernel(kernel);
        }

        // Use TestCleanup to run code after each test has run
        [TestCleanup]
        public void MyTestCleanup()
        {
            transactionScope.Dispose();
        }

        #endregion Additional test attributes
    }
}
