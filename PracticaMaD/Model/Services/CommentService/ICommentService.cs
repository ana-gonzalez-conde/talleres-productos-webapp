using Es.Udc.DotNet.ModelUtil.Transactions;
using Model.Daos.CommentDao;
using System;
using System.Collections.Generic;
using Model.Daos.TagDao;

namespace Model.Services.CommentService
{
    public interface ICommentService
    {

        ICommentDao CommentDao { set;  }

        ITagDao TagDao { set; }

        [Transactional]
        int GetNumberOfCommentsFromTag(long tagId);

        [Transactional]
        void AddTagsToComment(long commentId, List<string> tagNames);

        [Transactional]
        List<TagDetails> GetTagsOfComment(long commentId);

        [Transactional]
        void RemoveTagFromComment(long commentId, long tagId);

        [Transactional]
        List<TagDetails> GetAllTags();
        
        [Transactional]
        long AddComment(long userId, long productId, String message);
        
        [Transactional]
        CommentBlock FindProductComments(long productId, int startIndex, int count);
        
        [Transactional]
        CommentDetails FindCommentById(long commentId);

        [Transactional]
        TagDetails FindTagByName(string name);

        [Transactional]
        void EditComment(long commentId, string newMessage);
        
        [Transactional]
        void DeleteComment(long commentId);

        [Transactional]
        List<Tag> GetTopNTags(int n);
    }
}
