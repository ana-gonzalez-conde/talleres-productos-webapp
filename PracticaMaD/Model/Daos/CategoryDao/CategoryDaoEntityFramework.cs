using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Model.Daos.CategoryDao
{
    public class CategoryDaoEntityFramework : GenericDaoEntityFramework<Category, Int64>, ICategoryDao
    {
        #region Public Constructors

        /// <summary>
        /// Public Constructor
        /// </summary>
        public CategoryDaoEntityFramework()
        {
      
        }


        #endregion Public Constructors
    }
}
