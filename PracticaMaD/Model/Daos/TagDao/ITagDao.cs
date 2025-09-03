using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;

namespace Model.Daos.TagDao
{
    public interface ITagDao : IGenericDao<Tag, Int64>
    {

        Tag FindByTagName(string tagName);

        List<Tag> GetTopTags(int n);


    }
}
