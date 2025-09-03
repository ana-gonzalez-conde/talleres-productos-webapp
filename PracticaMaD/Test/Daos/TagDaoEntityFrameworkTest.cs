using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Ninject;
using System.Collections.Generic;
using System.Transactions;
using Test;
using Model.Daos.TagDao;
using Model;

namespace Test.Daos
{
    [TestClass]
    public class TagDaoEntityFrameworkTest
    {

        private static IKernel kernel;
        private static ITagDao tagDao;

        private static long tagId1 = 1;
        private static string tagName1 = "Wrench";

        private static long tagId2 = 2;
        private static string tagName2 = "Set";

        private static long tagId3 = 3;
        private static string tagName3 = "Drill";

        private static long tagId4 = 4;
        private static string tagName4 = "Power";

        private static long tagId5 = 5;
        private static string tagName5 = "Car Care";

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
            tagDao = kernel.Get<ITagDao>();
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
        public void FindByTagNameTest()
        {

            Tag tag = new Tag();
            tag.tagId = tagId1;
            tag.name = tagName1;

            Tag foundTag = tagDao.FindByTagName(tagName1);

            Assert.AreEqual(tag, foundTag);



        }

        [TestMethod]
        public void GetTopTagsTest()
        {

            Tag tag1 = new Tag();
            tag1.tagId = tagId1;
            tag1.name = tagName1;

            Tag tag2 = new Tag();
            tag2.tagId = tagId2;
            tag2.name = tagName2;

            Tag tag3 = new Tag();
            tag3.tagId = tagId3;
            tag3.name = tagName3;

            Tag tag4 = new Tag();
            tag4.tagId = tagId4;
            tag4.name = tagName4;

            Tag tag5 = new Tag();
            tag5.tagId = tagId5;
            tag5.name = tagName5;

            List<Tag> tags = tagDao.GetTopTags(5);

            Assert.AreEqual(tag1, tags[4]);
            Assert.AreEqual(tag2, tags[3]);
            Assert.AreEqual(tag3, tags[2]);
            Assert.AreEqual(tag4, tags[1]);
            Assert.AreEqual(tag5, tags[0]);



        }
    }
}
