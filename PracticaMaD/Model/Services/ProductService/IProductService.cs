using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Model.Daos.ProductDao;
using Model.Daos.CategoryDao;
using Es.Udc.DotNet.ModelUtil.Transactions;

namespace Model.Services.ProductService
{
    public interface IProductService 
    {

        IProductDao ProductDao { set; }

        /// <summary>
        /// Finds the products.
        /// </summary>
        /// <param name="productName">Name of the product.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        [Transactional]
        List<ProductDTO> FindProducts(string productName, int startIndex,
            int count, long? categoryId = null);
            
         /// <summary>
        /// Updates product details
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="">The product details</param>
        [Transactional]
        void UpdateProductDetails(long productId, long userId, UpdateProductDetailsInput productDetails);

        /// <summary>
        /// Finds the product details
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [Transactional]
        ProductDetails FindProductDetails(long productId);

        /// <summary>
        /// Allow to know all categories
        /// </summary>
        /// <returns></returns>
        [Transactional]
        List<Category> GetAllCategories();

        /// <summary>
        /// Allow to find the details of a category by its id
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [Transactional]
        String FindCategory(long categoryId);

    }
}
