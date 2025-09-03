using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Daos.ProductDao
{
    public interface IProductDao : IGenericDao<Product, Int64>
    {

        List<Product> FindProducts(string name, int startIndex,
            int count, long categoryId);
        List<Product> FindProducts(string name, int startIndex,
            int count);

    }
}
