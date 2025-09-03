using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Transactions;
using System;
using System.Collections.Generic;

namespace Model.Daos.CommentDao
{
    public interface ICommentDao : IGenericDao<Comment, Int64>
    {
        /// <summary>
        /// Finds the comments of a concrete product
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        List<Comment> FindByProductId(long productId, int startIndex,
            int count);
    }
}
