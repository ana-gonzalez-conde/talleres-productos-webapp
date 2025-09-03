

using System;
using System.Collections.Generic;

namespace Model.Services.CommentService
{
    [Serializable()]
    public class TagDetails
    {
        public long TagId { get; private set; }
        public string Name { get; private set; }

        public TagDetails(long tagId, string name)
        {
            this.TagId = tagId;
            this.Name = name;
        }

        public override bool Equals(object obj)
        {
            return obj is TagDetails details &&
                   TagId == details.TagId &&
                   Name == details.Name;
        }

        public override int GetHashCode()
        {
            int hashCode = 427487764;
            hashCode = hashCode * -1521134295 + TagId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            return hashCode;
        }

        public override String ToString()
        {
            return "[ tagId = " + TagId + " | " +
                 "Name = " + Name + " ]";
        }
    }
}
