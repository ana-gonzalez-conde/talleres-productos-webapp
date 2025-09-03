using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Model.Daos.CommentDao;
using Ninject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Test.Daos
{
    /// <summary>
    /// Descripción resumida de UnitTest1
    /// </summary>
    [TestClass]
    public class ICommentDaoEntityFrameworkTest
    {
        private static IKernel kernel;
        private static ICommentDao commentDao;

        private TransactionScope transactionScope;

        private TestContext testContextInstance;

        public ICommentDaoEntityFrameworkTest()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        [TestMethod]
        public void FindByProductIdTest()
        {

            int startIndex = 0;
            int count = 2;

            List<Comment> comments = commentDao.FindByProductId(1L, startIndex, count);

            // These are the only Comments for this product
            // (1, 1, '2024-03-20', 'Excellent wrench set! Sturdy and reliable.')
            // (1, 1, '2024-03-21', 'Amazing, I recommend still.')
            Assert.AreEqual(comments.Count, 2);
            Assert.AreEqual(comments[0].message, "Excellent wrench set! Sturdy and reliable.");
            Assert.AreEqual(comments[1].message, "Amazing, I recommend still.");

        }

        /// <summary>
        ///Obtiene o establece el contexto de las pruebas que proporciona
        ///información y funcionalidad para la serie de pruebas actual.
        ///</summary>
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
            commentDao = kernel.Get<ICommentDao>();
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
        #endregion
    }
}
