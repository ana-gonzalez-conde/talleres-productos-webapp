using Es.Udc.DotNet.ModelUtil.Dao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Model.Daos.BankCardDao;
using Model.Daos.UserDao;
using Ninject;
using System;
using System.Data.Entity;
using System.Transactions;

namespace Test.Daos
{
    [TestClass]
    public class IBankCardDaoTest
    {
        private static IKernel kernel;

        private TestContext testContextInstance;
        private BankCard bankCard;
        private static IBankCardDao bankCardDao;
        private static IUserDao userDao;
        private User user; // Usuario asociado a la tarjeta
        private TransactionScope transactionScope;
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            kernel = TestManager.ConfigureNInjectKernel();
            userDao = kernel.Get<IUserDao>();
            bankCardDao = kernel.Get<IBankCardDao>();
        }

        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            TestManager.ClearNInjectKernel(kernel);
        }

        [TestInitialize]
        public void MyTestInitialize()
        {
            transactionScope = new TransactionScope();
            // Crear y guardar un usuario de prueba
            user = new User();
            user.login = "login";
            user.password = "password";
            user.name = "John";
            user.surnames = "Smith";
            user.email = "john.smith@acme.com";
            user.address = "221B Baker Street";
            user.language = "en";
            user.country = "US";
            userDao.Create(user);

            // Crear y guardar una BankCard de prueba
            bankCard = new BankCard(); 
            bankCard.type = 1;
            bankCard.number = "1234567890123456";
            bankCard.cvv = "123";
            bankCard.expirationDate = DateTime.Now.AddYears(1);
            bankCard.isDefault = true;
            bankCard.isActive = true;
            bankCard.userId = user.userId;
            bankCardDao.Create(bankCard);
        }

        [TestCleanup]
        public void MyTestCleanup()
        {
            transactionScope.Dispose();
        }

        [TestMethod]
        public void DAO_FindBankCardTest()
        {
            try
            {
                BankCard foundBankCard = bankCardDao.Find(bankCard.cardId);
                Assert.IsNotNull(foundBankCard);
                Assert.AreEqual(bankCard.number, foundBankCard.number);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void DAO_ExistsBankCardTest()
        {
            try
            {
                bool exists = bankCardDao.Exists(bankCard.cardId);
                Assert.IsTrue(exists);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void DAO_NotExistsBankCardTest()
        {
            try
            {
                bool notExists = bankCardDao.Exists(-1);
                Assert.IsFalse(notExists);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod()]
        public void DAO_UpdateTest()
        {
            try
            {
                bankCard.type = 2;
                bankCard.number = "1234567890123456";
                bankCard.cvv = "100";

                bankCardDao.Update(bankCard);

                BankCard actual = bankCardDao.Find(bankCard.cardId);

                Assert.AreEqual(bankCard, actual);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod()]
        public void DAO_RemoveTest()
        {
            try
            {
                bankCardDao.Remove(bankCard.cardId);

                bool bankCardExists = bankCardDao.Exists(bankCard.cardId);

                Assert.IsFalse(bankCardExists);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        /// <summary>
        ///A test for Attach
        ///</summary>
        [TestMethod()]
        public void DAO_AttachTest()
        {
            BankCard bankCard = bankCardDao.Find(this.bankCard.cardId);
            bankCardDao.Remove(bankCard.cardId);   // removes the bankcard created in MyTestInitialize();

            // First we get CommonContext from GenericDAO...
            DbContext dbContext = ((GenericDaoEntityFramework<BankCard, Int64>)bankCardDao).Context;

            // Check the bankcard is not in the context now (EntityState.Detached notes that entity is not tracked by the context)
            Assert.AreEqual(dbContext.Entry(bankCard).State, EntityState.Detached);

            // If we attach the entity it will be tracked again
            bankCardDao.Attach(bankCard);


            // EntityState.Unchanged = entity exists in context and in DataBase with the same values 
            Assert.AreEqual(dbContext.Entry(bankCard).State, EntityState.Unchanged);

        }
    }
}
