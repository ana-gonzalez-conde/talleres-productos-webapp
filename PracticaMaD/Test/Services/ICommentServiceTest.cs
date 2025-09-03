using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Model.Services.CommentService;
using Test;
using Model;
using Ninject;
using Model.Services.Exceptions;
using Model.Services.CommentService.Exceptions;

namespace Test.Services
{
    [TestClass]
    public class ICommentServiceTest
    {

        private static IKernel kernel;
        private static ICommentService commentService;

        private static long userId1 = 1;
        private static long userId2 = 2;
        private static long userId3 = 3;

        private static long productId1 = 1;
        private static long productId2 = 2;
        private static long productId3 = 3;
        private static long productId4 = 4;
        private static long productId5 = 5;
        private static long productId6 = 6;

        private static long commentId1 = 1;
        private static long commentId2 = 2;
        private static long commentId3 = 3;
        private static long commentId4 = 4;
        private static long commentId5 = 5;
        private static long commentId6 = 6;

        private static int commentsCount = 1;

        private static long nonExistentCommentId = 35;

        private static long nonExistentTagId = 35;

        private static string newTagName = "testTagName";
        private static string newTagName2 = "testTagName2";

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

        private static long tagId6 = 6;
        private static string tagName6 = "Wax";

        private static long tagId7 = 7;
        private static string tagName7 = "Interior";

        private static long tagId8 = 8;
        private static string tagName8 = "Protection";

        private static long tagId9 = 9;
        private static string tagName9 = "Emergency";

        private static long tagId10 = 10;
        private static string tagName10 = "Battery";




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
            commentService = kernel.Get<ICommentService>();
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
        public void GetNumberOfCommentsFromTagTest()
        {

            int comments = commentService.GetNumberOfCommentsFromTag(tagId1);

            Assert.AreEqual(comments, commentsCount);

        }

        [TestMethod]
        [ExpectedException(typeof(TagNotFoundException))]
        public void GetNumberOfCommentsFromNonExistentTagTest()
        {

            int comments = commentService.GetNumberOfCommentsFromTag(nonExistentTagId);

        }

        [TestMethod]
        public void AddTagsToCommentTest()
        {

            TagDetails expectedTag1 = new TagDetails(tagId1, tagName1);

            commentService.AddTagsToComment(commentId1, new List<string> { tagName2, tagName3 });

            TagDetails expectedTag2 = commentService.FindTagByName(tagName2);
            TagDetails expectedTag3 = commentService.FindTagByName(tagName3);

            List<TagDetails> tags = commentService.GetTagsOfComment(commentId1);

            Assert.AreEqual(expectedTag1, tags[0]);
            Assert.AreEqual(expectedTag2, tags[1]);
            Assert.AreEqual(expectedTag3, tags[2]);

            commentService.RemoveTagFromComment(commentId1, tagId2);
            commentService.RemoveTagFromComment(commentId1, tagId3);

        }

        [TestMethod]
        public void AddNewTagToCommentTest()

        {

            TagDetails expectedTag1 = new TagDetails(tagId2, tagName2);

            commentService.AddTagsToComment(commentId2, new List<string> {newTagName, newTagName2});

            TagDetails expectedTag2 = commentService.FindTagByName(newTagName);
            TagDetails expectedTag3 = commentService.FindTagByName(newTagName2);

            List<TagDetails> tags = commentService.GetTagsOfComment(commentId2);

            Assert.AreEqual(expectedTag1, tags[0]);
            Assert.AreEqual(expectedTag2, tags[1]);
            Assert.AreEqual(expectedTag3, tags[2]);

        }


        [TestMethod]
        [ExpectedException(typeof(EmptyTagsInputException))]
        public void AddEmptyTagsListToComment()
        {
            commentService.AddTagsToComment(commentId1, new List<string> {  });

        }

        [TestMethod]
        [ExpectedException(typeof(CommentNotFoundException))]
        public void AddTagToNonExistentCommentTest()
        {

            commentService.AddTagsToComment(nonExistentCommentId, new List<string> { tagName1, tagName2 });

        }


        [TestMethod]
        public void GetTagsOfCommentTest()
        {

            TagDetails expectedTag = new TagDetails(tagId1, tagName1);

            List<TagDetails> tags = commentService.GetTagsOfComment(commentId1);

            Assert.AreEqual(expectedTag, tags[0]);

        }

        [TestMethod]
        [ExpectedException(typeof(CommentNotFoundException))]
        public void GetTagsOfNonExistentCommentTest()
        {


            List<TagDetails> tags = commentService.GetTagsOfComment(nonExistentCommentId);


        }


        [TestMethod]
        public void RemoveTagFromCommentTest()
        {

            TagDetails expectedTag = new TagDetails(tagId1, tagName1);

            commentService.AddTagsToComment(commentId1, new List<string> { tagName1, tagName2 });

            TagDetails newTag = commentService.FindTagByName(tagName2);

            commentService.RemoveTagFromComment(commentId1, newTag.TagId);

            List<TagDetails> tags = commentService.GetTagsOfComment(commentId1);

            Assert.AreEqual(expectedTag, tags[0]);


        }


        [TestMethod]
        [ExpectedException(typeof(TagNotFoundException))]
        public void RemoveNonExistentTagFromCommentTest()
        {

            commentService.RemoveTagFromComment(commentId1, nonExistentTagId);


        }


        [TestMethod]
        public void GetAllTagsTest()
        {

            TagDetails expectedTag1 = new TagDetails(tagId1, tagName1);

            TagDetails expectedTag2 = new TagDetails(tagId2, tagName2);

            TagDetails expectedTag3 = new TagDetails(tagId3, tagName3);

            TagDetails expectedTag4 = new TagDetails(tagId4, tagName4);

            TagDetails expectedTag5 = new TagDetails(tagId5, tagName5);

            TagDetails expectedTag6 = new TagDetails(tagId6, tagName6);

            TagDetails expectedTag7 = new TagDetails(tagId7, tagName7);

            TagDetails expectedTag8 = new TagDetails(tagId8, tagName8);

            TagDetails expectedTag9 = new TagDetails(tagId9, tagName9);

            TagDetails expectedTag10 = new TagDetails(tagId10, tagName10);

            List<TagDetails> tags = commentService.GetAllTags();

            Assert.AreEqual(expectedTag1, tags[0]);
            Assert.AreEqual(expectedTag2, tags[1]);
            Assert.AreEqual(expectedTag3, tags[2]);
            Assert.AreEqual(expectedTag4, tags[3]);
            Assert.AreEqual(expectedTag5, tags[4]);
            Assert.AreEqual(expectedTag6, tags[5]);
            Assert.AreEqual(expectedTag7, tags[6]);
            Assert.AreEqual(expectedTag8, tags[7]);
            Assert.AreEqual(expectedTag9, tags[8]);
            Assert.AreEqual(expectedTag10, tags[9]);

        }


        [TestMethod]
        public void AddAndFindCommentTest()
        {

            long productId = productId6;
            String message = "This is a new comment!";

            var commentId =
                commentService.AddComment(userId3, productId, message);

            var comment = commentService.FindCommentById(commentId);

            // Check data
            Assert.AreEqual(comment.Message, message);

            // transaction.Complete() is not called, so Rollback is executed.
        }

        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void FindNotExistentCommentByIdTest()
        {
            commentService.FindCommentById(-1);
        }


        [TestMethod]
        [ExpectedException(typeof(MaxAllowedCharactersExceedException))]
        public void AddCommentExceedingMaxCharacters()
        {
            // The product with id 2 has already a comment
            long productId = 2;
            String message = "very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message" +
                "very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message" +
                "very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message" +
                "very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message" +
                "very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message" +
                "very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message" +
                "very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message" +
                "very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message" +
                "very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message" +
                "very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message" +
                "very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message" +
                "very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message" +
                "very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message" +
                "very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message" +
                "very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message" +
                "very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message" +
                "very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message" +
                "very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message" +
                "very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message" +
                "very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message" +
                "very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message" +
                "very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message";

            commentService.AddComment(1, productId, message);

        }

        [TestMethod]
        public void FindCommentsByProduct()
        {

            long productId = 5;
            String message = "This is a new comment!";
            int startIndex = 0;
            int count = 5;

            var commentId = commentService.AddComment(3, productId, message);

            var comments = commentService.FindProductComments(productId, startIndex, count);

            CommentDetails expectedCommentDetails = commentService.FindCommentById(commentId);

            Assert.AreEqual(comments.Comments[1], expectedCommentDetails);
        }

        [TestMethod]
        public void FindTagByNameTest()
        {

            TagDetails tag = new TagDetails(tagId1, tagName1);

            TagDetails foundTag = commentService.FindTagByName(tagName1);

            Assert.AreEqual(tag, foundTag);

        }

        [TestMethod]
        public void EditCommentTest()
        {

            String message = "This is a new comment!";

            long newCommentId = commentService.AddComment(userId1, productId3, message);

            string newMessage = "This is my new comment!";

            commentService.EditComment(newCommentId, newMessage);

            CommentDetails foundComment = commentService.FindCommentById(newCommentId);

            Assert.AreEqual(foundComment.Message, newMessage);


        }


        [TestMethod]
        [ExpectedException(typeof(MaxAllowedCharactersExceedException))]
        public void EditCommentWithLargeMessageTest()
        {

            String message = "very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message" +
                "very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message" +
                "very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message" +
                "very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message" +
                "very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message" +
                "very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message" +
                "very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message" +
                "very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message" +
                "very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message" +
                "very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message" +
                "very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message" +
                "very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message" +
                "very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message" +
                "very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message" +
                "very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message" +
                "very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message" +
                "very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message" +
                "very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message" +
                "very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message" +
                "very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message" +
                "very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message" +
                "very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message very long message";

            commentService.EditComment(commentId1, message);


        }

        [TestMethod]
        public void DeleteCommentTest()
        {
            Boolean exceptionCaught = false;

            String message = "This is a happy comment!";

            long newCommentId = commentService.AddComment(userId3, productId4, message);

            commentService.DeleteComment(newCommentId);

            try
            {

                commentService.FindCommentById(newCommentId);

            }
            catch (InstanceNotFoundException)
            {
                exceptionCaught = true;
            }

            Assert.IsTrue(exceptionCaught);
        }

        [TestMethod]
        public void GetTopNTagsTest()
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

            List<Tag> tags = commentService.GetTopNTags(5);

            Assert.AreEqual(tag1, tags[4]);
            Assert.AreEqual(tag2, tags[3]);
            Assert.AreEqual(tag3, tags[2]);
            Assert.AreEqual(tag4, tags[1]);
            Assert.AreEqual(tag5, tags[0]);
        }

    }
}
