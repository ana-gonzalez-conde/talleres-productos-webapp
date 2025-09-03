using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System.Collections.Generic;
using System.Transactions;
using Model.Daos.ProductDao;
using Model;

namespace Test.Daos
{
    [TestClass]
    public class IProductDaoEntityFrameworkTest
    {

        private static IKernel kernel;
        private static IProductDao productDao;

        private const string searchName = "Jumper Cables";
        private const long searchCategory = 7;



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
            productDao = kernel.Get<IProductDao>();
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
        public void FindByNameTest()
        {

            int startIndex = 0;
            int count = 2;

            List<Product> products = productDao.FindProducts(searchName, startIndex, count);

            Assert.AreEqual(products.Count, count);

        }

        [TestMethod]
        public void FindByNameAndCategoryTest()
        {

            int startIndex = 0;
            int count = 1;

            List<Product> products =
                productDao.FindProducts(searchName, startIndex, count, searchCategory);

            Assert.AreEqual(products.Count, count);

        }
    }
}
