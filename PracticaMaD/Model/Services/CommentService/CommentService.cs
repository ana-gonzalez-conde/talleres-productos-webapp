using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Model.Daos.CommentDao;
using Model.Daos.ProductDao;
using Model.Services.CommentService.Exceptions;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using Model.Daos.TagDao;
using Model.Services.Exceptions;

namespace Model.Services.CommentService
{
    public class CommentService : ICommentService
    {

        [Inject]
        public ICommentDao CommentDao { private get; set; }

        [Inject]
        public ITagDao TagDao { private get; set; }
        
        [Inject]
        public IProductDao ProductDao { private get; set; }

        private int MAX_ALLOWED_MESSAGE_LENGTH = 4000;


        /// <summary>
        /// Gets the number of comments from tag.
        /// </summary>
        /// <param name="tagId">The tag identifier.</param>
        /// <returns></returns>
        /// <exception cref="Model.Services.Exceptions.TagNotFoundException"></exception>
        [Transactional]
        public int GetNumberOfCommentsFromTag(long tagId)
        {
            try
            {

                Tag tag = TagDao.Find(tagId);

                return tag.Comment.Count();

            }
            catch (Exception e)
            {
                throw new TagNotFoundException("Tag was not found", e);
            }

            

        }

        /// <summary>
        /// Adds the tag to comment.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="tagName">Name of the tag.</param>
        /// <exception cref="Model.Services.Exceptions.CommentNotFoundException"></exception>
        /// <exception cref="EmptyTagsInputException"></exception>
        [Transactional]
        public void AddTagsToComment(long commentId, List<string> tagNames)
        {
            Comment comment;
            Tag tag;

            if(tagNames.Count == 0)
            {
                throw new EmptyTagsInputException();
            }

            try
            {
                comment = CommentDao.Find(commentId);
            }
            catch (Exception e)
            {
                throw new CommentNotFoundException("Comment was not found", e );
            }

            foreach(string tagName in tagNames)
            {
                tag = TagDao.FindByTagName(tagName);

                if (tag == null)
                {

                    tag = new Tag();
                    tag.name = tagName;

                    TagDao.Create(tag);
                }

                comment.Tag.Add(tag);
            }
        }

        /// <summary>
        /// Gets the tags of comment.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        /// <returns></returns>
        /// <exception cref="Model.Services.Exceptions.CommentNotFoundException"></exception>
        [Transactional]
        public List<TagDetails> GetTagsOfComment(long commentId)
        {

            try
            {

                Comment comment = CommentDao.Find(commentId);
                List<Tag> tags = comment.Tag.ToList();

                return tags.Select(tag => new TagDetails(
                    tag.tagId,
                    tag.name
                )).ToList();


            }
            catch (Exception e)
            {
                throw new CommentNotFoundException("Comment was not found", e);
            }

            

        }

        /// <summary>
        /// Removes the tag from comment.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="tagId">The tag identifier.</param>
        /// <exception cref="Model.Services.Exceptions.CommentNotFoundException"></exception>
        [Transactional]
        public void RemoveTagFromComment(long commentId, long tagId)
        {

            try
            {

                Comment comment = CommentDao.Find(commentId);

                Tag tag = TagDao.Find(tagId);

                comment.Tag.Remove(tag);

            }
            catch (Exception e)
            {
                throw new TagNotFoundException("Tag was not found", e);
            }

            

        }

        /// <summary>
        /// Gets all tags.
        /// </summary>
        /// <returns></returns>
        [Transactional]
        public List<TagDetails> GetAllTags()
        {

            List<Tag> tags = TagDao.GetAllElements();
            return tags.Select(tag => new TagDetails(
                    tag.tagId,
                    tag.name
                )).ToList();

        }
        
        /// <summary>
        /// Add a comment to a product
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        [Transactional]
        /// <exception cref="MaxAllowedCharactersExceedException"/>
        public long AddComment(long userId, long productId, string message)
        {

            if (message.Length >= MAX_ALLOWED_MESSAGE_LENGTH)
            {
                throw new MaxAllowedCharactersExceedException("El mensaje supera el máximo de caracteres permitido");
            }

            Comment comment = new Comment();

            comment.userId = userId;
            comment.productId = productId;
            comment.message = message;
            comment.date = DateTime.Now;

            CommentDao.Create(comment);

            return comment.commentId;
        }

        /// <summary>
        /// Allow to get all comments of a product
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [Transactional]
        public CommentBlock FindProductComments(long productId, int startIndex, int count)
        {

            if (!ProductDao.Exists(productId))
            {
                throw new InstanceNotFoundException(productId,
                    typeof(Product).FullName);
            }
            List <Comment> comments = CommentDao.FindByProductId(productId, startIndex, count + 1);

            bool existMoreComments = (comments.Count == count + 1);

            if (existMoreComments)
                comments.RemoveAt(count);

            return new CommentBlock(comments.Select(comment => new CommentDetails(
                    comment.commentId,
                    comment.productId,
                    comment.userId,
                    comment.date,
                    comment.message
                )).ToList(), existMoreComments);
        }

        /// <summary>
        /// Allow to find a concrete comment by its id
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        [Transactional]
        public CommentDetails FindCommentById(long commentId)
        {

            if (!CommentDao.Exists(commentId))
            {
                throw new InstanceNotFoundException(commentId,
                    typeof(Comment).FullName);
            }
            Comment comment = CommentDao.Find(commentId);
            return new CommentDetails(comment.commentId,comment.productId, comment.userId, comment.date, comment.message);
        }

        /// <summary>
        /// Allow to get a concrete tag by its name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public TagDetails FindTagByName(string name)
        {

            Tag tag = TagDao.FindByTagName(name);
            return new TagDetails(tag.tagId, tag.name);

        }

        /// <summary>
        /// Allow to edit a concrete comment message by its id
        /// </summary>
        /// <param name="commentId"></param>
        /// <param name="newMessage"></param>
        [Transactional]
        public void EditComment(long commentId, string newMessage)
        {

            if (newMessage.Length >= MAX_ALLOWED_MESSAGE_LENGTH)
            {
                throw new MaxAllowedCharactersExceedException();
            }

            Comment comment = CommentDao.Find(commentId);

            comment.message = newMessage;
            comment.date = DateTime.Now;

            CommentDao.Update(comment);

        }
        
        /// <summary>
        /// Allow to delete a concrete comment by its id
        /// </summary>
        /// <param name="commentId"></param>
        [Transactional]
        public void DeleteComment(long commentId)
        {
            CommentDao.Remove(commentId);

        }

        [Transactional]
        public List<Tag> GetTopNTags(int n)
        {
            return TagDao.GetTopTags(n);
        }

    }
}
