using Es.Udc.DotNet.ModelUtil.Dao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Model.Daos.UserDao;
using Ninject;
using System;
using System.Data.Entity;
using System.Transactions;

namespace Test.Daos
{
    /// <summary>
    ///This is a test class for IGenericDaoTest and is intended
    ///to contain all IGenericDaoTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IGenericDaoTest
    {
        private static IKernel kernel;

        private User user;
        private static IUserDao userDao;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
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

        //
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            kernel = TestManager.ConfigureNInjectKernel();

            userDao = kernel.Get<IUserDao>();
        }

        //
        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            TestManager.ClearNInjectKernel(kernel);
        }

        //
        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
            transactionScope = new TransactionScope();
            user = new User();
            user.login = "jsmith";
            user.password = "password";
            user.name = "John";
            user.surnames = "Smith Holmes";
            user.email = "jsmith@acme.com";
            user.language = "en";
            user.country = "US";
            user.address = "221B Baker Street";

            userDao.Create(user);
        }

        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            transactionScope.Dispose();
        }

        #endregion Additional test attributes

        [TestMethod()]
        public void DAO_FindTest()
        {
            try
            {
                User actual = userDao.Find(user.userId);

                Assert.AreEqual(user, actual, "User found does not correspond with the original one.");
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod()]
        public void DAO_ExistsTest()
        {
            try
            {
                bool userExists = userDao.Exists(user.userId);

                Assert.IsTrue(userExists);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod()]
        public void DAO_NotExistsTest()
        {
            try
            {
                bool userNotExists = userDao.Exists(-1);

                Assert.IsFalse(userNotExists);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        /// <summary>
        ///A test for Update
        ///</summary>
        [TestMethod()]
        public void DAO_UpdateTest()
        {
            try
            {
                user.name = "Juan";
                user.surnames = "González";
                user.email = "jgonzalez@acme.es";
                user.language = "es";
                user.country = "ES";
                user.password = "contraseña";

                userDao.Update(user);

                User actual = userDao.Find(user.userId);

                Assert.AreEqual(user, actual);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        /// <summary>
        ///A test for Remove
        [TestMethod()]
        public void DAO_RemoveTest()
        {
            try
            {
                userDao.Remove(user.userId);

                bool userExists = userDao.Exists(user.userId);

                Assert.IsFalse(userExists);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        /// <summary>
        ///A test for Create
        ///</summary> 
        [TestMethod()]
        public void DAO_CreateTest()
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                User newUser = new User();
                newUser.login = "login";
                newUser.password = "password";
                newUser.name = "John";
                newUser.surnames = "Smith";
                newUser.email = "john.smith@acme.com";
                newUser.address = "221B Baker Street";
                newUser.language = "en";
                newUser.country = "US";

                userDao.Create(newUser);

                bool userExists = userDao.Exists(newUser.userId);

                Assert.IsTrue(userExists);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        ///A test for Attach
        ///</summary>
        [TestMethod()]
        public void DAO_AttachTest()
        {
            User user = userDao.Find(this.user.userId);
            userDao.Remove(user.userId);   // removes the user created in MyTestInitialize();

            // First we get CommonContext from GenericDAO...
            DbContext dbContext = ((GenericDaoEntityFramework<User, Int64>)userDao).Context;

            // Check the user is not in the context now (EntityState.Detached notes that entity is not tracked by the context)
            Assert.AreEqual(dbContext.Entry(user).State, EntityState.Detached);

            // If we attach the entity it will be tracked again
            userDao.Attach(user);


            // EntityState.Unchanged = entity exists in context and in DataBase with the same values 
            Assert.AreEqual(dbContext.Entry(user).State, EntityState.Unchanged);

        }
    }
}
