using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Collections.Generic;
using System.Transactions;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Model.Services.ProductService;
using Model;
using Model.Services.ProductService.Exceptions;

namespace Test.Services
{
    [TestClass()]
    public class IProductServiceTest
    {
        private static IKernel kernel;
        private static IProductService productService;

        private static long productId1 = 5;
        private static string name1 = "Jumper Cables";
        private static double price1 = 24.99;
        private static string dateString1 = "2024-03-28";
        private static DateTime addingDate1 = DateTime.Parse(dateString1);
        private static int stock1 = 25;
        private static string image1 = "jumper_cables.jpg";
        private static string description1 = "Essential for jump-starting your car in emergencies.";
        private static long categoryId1 = 1;

        private static long productId2 = 6; 
        private static string name2 = "Jumper Cables";
        private static double price2 = 27.99;
        private static string dateString2 = "2024-03-29";
        private static DateTime addingDate2 = DateTime.Parse(dateString2);
        private static int stock2 = 25;
        private static string image2 = "jumper_cables_2.jpg";
        private static string description2 = "Essential for jump-starting your car in emergencies.";
        private static long categoryId2 = 7;

        private static long productId3 = 3;
        private static string name3 = "Car Wax";
        private static double price3 = 14.99;
        private static string dateString3 = "2024-03-27";
        private static int stock3 = 50;
        private static string image3 = "car_wax.jpg";
        private static string description3 = "Protective wax for a shiny and clean car finish.";
        private static long categoryId3 = 2;

        private static long searchCategory = 7;

        private static string searchName = "Jumper Cables";
        
        private const long EXISTENT_PRODUCT_ID = 1L;

        private const long NON_EXISTENT_PRODUCT_ID = -1L;

        private const long ADMIN_USER_ID = 1L;

        private const long NON_ADMIN_USER_ID = 2L;

        private UpdateProductDetailsInput anUpdateProductDetailsInput = new UpdateProductDetailsInput("Wrench Set UPDATED", 50, 18, "wrench_set.jpg", "A set of high-quality wrenches for various sizes. UPDATED", 2);



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

        #region Additional test attributes

        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            kernel = TestManager.ConfigureNInjectKernel();
            productService = kernel.Get<IProductService>();
        }

        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            TestManager.ClearNInjectKernel(kernel);
        }

        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
            transactionScope = new TransactionScope();
        }

        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            transactionScope.Dispose();
        }

        #endregion Additional test attributes


        [TestMethod]
        public void FindProductByNameTest()
        {

            ProductDTO product1 = new ProductDTO (productId1, name1, (float) price1, addingDate1, categoryId1);
            ProductDTO product2 = new ProductDTO (productId2, name2, (float) price2, addingDate2, categoryId2);

            int startIndex = 0;
            int count = 2;


            List<ProductDTO> products = productService.FindProducts(searchName, startIndex, count);

            Assert.AreEqual(product1, products[0]);
            Assert.AreEqual(product2, products[1]);





        }

        [TestMethod]
        public void FindProductByNameAndCategoryTest()
        {

            ProductDTO product2 = new ProductDTO(productId2, name2, (float)price2, addingDate2, categoryId2);


            int startIndex = 0;
            int count = 1;

            List<ProductDTO> products = productService.FindProducts(searchName, startIndex, count, searchCategory);


            Assert.AreEqual(product2, products[0]);

        }
        
        /// <summary>
        /// A test for UpdateProductDetails
        /// </summary>
        [TestMethod]
        public void UpdateProductDetailsTest()
        {
            // This is the original object used for test:
            // ('Wrench Set', 49.99, '2024-03-24', 20, 'wrench_set.jpg', 'A set of high-quality wrenches for various sizes.', 1) with id 1

            var expected = anUpdateProductDetailsInput;

            productService.UpdateProductDetails(EXISTENT_PRODUCT_ID, ADMIN_USER_ID, expected);

            var obtained =
                productService.FindProductDetails(1);

            // Check changes

            // Date should not be updated, as it is adding date, so this test that this doesn't change
            Assert.AreEqual(obtained.AddingDate, DateTime.Parse("2024-03-24"));
            Assert.AreEqual(obtained.CategoryId, expected.CategoryId);
            Assert.AreEqual(obtained.Name, expected.Name);
            Assert.AreEqual(obtained.Description, expected.Description);
            Assert.AreEqual(obtained.Image, expected.Image);
            Assert.AreEqual(obtained.Price, expected.Price);
            Assert.AreEqual(obtained.Stock, expected.Stock);

        }

        [TestMethod]
        [ExpectedException(typeof(UserWithoutAdminPrivilegiesException))]
        public void UpdateProductProductDetailsWithNonAdminUser()
        {
           productService.UpdateProductDetails(EXISTENT_PRODUCT_ID, NON_ADMIN_USER_ID, anUpdateProductDetailsInput);
        }

        /// <summary>
        /// A test for UpdateProductDetails when the product does not exist
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void UpdateProductDetailsForNonExistingProductTest()
        {
            productService.UpdateProductDetails(NON_EXISTENT_PRODUCT_ID, ADMIN_USER_ID, anUpdateProductDetailsInput);
        }

        /// <summary>
        /// A test for FindProductDetails
        /// </summary>
        [TestMethod]
        public void FindProductDetailsTest()
        {

            // This is the original object used for test:
            // ('Wrench Set', 49.99, '2024-03-24', 20, 'wrench_set.jpg', 'A set of high-quality wrenches for various sizes.', 1) with id 1

            var expected = new ProductDetails(name3, (float)price3, DateTime.Parse(dateString3), stock3, image3, description3, categoryId3);

            var obtained =
                productService.FindProductDetails(productId3);

            // Check data
            Assert.AreEqual(expected, obtained);

            // transaction.Complete() is not called, so Rollback is executed.
        }

        /// <summary>
        /// A test for FindProductDetails when the product does not exist
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void FindProductDetailsForNonExistingProductTest()
        {
            productService.FindProductDetails(NON_EXISTENT_PRODUCT_ID);
        }

    }
}
