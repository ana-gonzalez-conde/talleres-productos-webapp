using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Model.Daos.CommentDao
{
    public class CommentDaoEntityFramework :
        GenericDaoEntityFramework<Comment, Int64>, ICommentDao 
    {

        #region Public Constructors

        /// <summary>
        /// Public Constructor
        /// </summary>
        public CommentDaoEntityFramework()
        {

        }

        public List<Comment> FindByProductId(long productId, int startIndex,
            int count)
        {

            DbSet<Comment> comments = Context.Set<Comment>();

            var result =
                (from a in comments
                 where a.productId == productId
                 orderby a.date
                 select a).Skip(startIndex).Take(count).ToList();
            
            return result;

        }

        #endregion Public Constructors

        #region ICommentDao Specific Operations

        #endregion ICommentDao Specific Operations
    }
}
