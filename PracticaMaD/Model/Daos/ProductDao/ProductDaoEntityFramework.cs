using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Model.Daos.ProductDao
{
    public class ProductDaoEntityFramework :
         GenericDaoEntityFramework<Product, Int64>, IProductDao
    {
        public List<Product> FindProducts(string name, int startIndex, int count, long categoryId)
        {
            DbSet<Product> products = Context.Set<Product>();

            var result = (from p in products
                          where p.name.ToLower().Contains(name.ToLower()) && p.categoryId == categoryId
                          orderby p.productId
                          select p).Skip(startIndex).Take(count).ToList();

            return result;
        }

        public List<Product> FindProducts(string name, int startIndex, int count)
        {
            DbSet<Product> products = Context.Set<Product>();

            var result = (from p in products
                          where p.name.ToLower().Contains(name.ToLower())
                          orderby p.productId
                          select p).Skip(startIndex).Take(count).ToList();

            return result;
        }
    }
}
