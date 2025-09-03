using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Model.Daos.TagDao
{
    public class TagDaoEntityFramework : GenericDaoEntityFramework<Tag, Int64>, ITagDao
    {

        public Tag FindByTagName(string tagName)
        {

            DbSet<Tag> tags = Context.Set<Tag>();

            var result =
                (from t in tags
                    where t.name == tagName
                    orderby t.tagId
                    select t);

            return result.FirstOrDefault();

        }

        public List<Tag> GetTopTags(int n)
        {

            DbSet<Tag> tags = Context.Set<Tag>();

            var result =
                (from t in tags
                    orderby t.tagId
                    select t).OrderByDescending(tag => tag.Comment.Count).Take(n).ToList();

            return result;

        }

    }
}
