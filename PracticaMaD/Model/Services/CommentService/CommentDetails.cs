

using System;
using System.Collections.Generic;

namespace Model.Services.CommentService
{
    [Serializable()]
    public class CommentDetails
    {
        #region Properties Region
        public long CommentId { get; private set; }
        public long ProductId { get; private set; }
        public long UserId { get; private set; }
        public DateTime? Date { get; private set; }
        public string Message { get; private set; }

        #endregion

        public CommentDetails(long commentId, long productId, long userId, DateTime? date, string message)
        {
            CommentId = commentId;
            ProductId = productId;
            UserId = userId;
            Date = date;
            Message = message;
        }

        public override bool Equals(object obj)
        {
            return obj is CommentDetails details &&
                   CommentId == details.CommentId &&
                   ProductId == details.ProductId &&
                   UserId == details.UserId &&
                   Date == details.Date &&
                   Message == details.Message;
        }

        public override int GetHashCode()
        {
            int hashCode = -621982224;
            hashCode = hashCode * -1521134295 + CommentId.GetHashCode();
            hashCode = hashCode * -1521134295 + ProductId.GetHashCode();
            hashCode = hashCode * -1521134295 + UserId.GetHashCode();
            hashCode = hashCode * -1521134295 + Date.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Message);
            return hashCode;
        }

        public override String ToString()
        {
            return "[ CommentId = " + CommentId + " | " +
                 "ProductId = " + ProductId + " | " +
                 "UserId = " + UserId + " | " +
                 "Date = " + Date + " | " +
                 "Message = " + Message + " ]";
        }

    }
}
