
using System.Collections.Generic;
using System.Linq;
using Model.Daos.ProductDao;
using Model.Daos.UserDao;
using Ninject;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Model.Services.ProductService.Exceptions;
using System;
using Model.Daos.CategoryDao;

namespace Model.Services.ProductService
{
    public class ProductService : IProductService
    {

        [Inject]
        public IProductDao ProductDao { private get; set; }

        [Inject]
        public ICategoryDao CategoryDao { private get; set; }

        [Inject]
        public IUserDao UserDao { private get; set; }

        public object FindProducts(object text, int v1, int v2)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Allows to find products with pagination by name or/and categoryId
        /// </summary>
        /// <param name="productName"></param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [Transactional]
        public List<ProductDTO> FindProducts(string productName, int startIndex,
            int count, long? categoryId = null)
        {
            string lowerCaseProductName = productName.ToLower();
            List<Product> products;

            if (categoryId != null) 
            {
                products = ProductDao.FindProducts(lowerCaseProductName, startIndex, count, categoryId.Value);
            }
            else
            {
                products = ProductDao.FindProducts(lowerCaseProductName, startIndex, count);
            }

            List<ProductDTO> productDetailsList = products.Select(product => new ProductDTO(
                product.productId,
                product.name,
                (float)product.price,
                product.addingDate,
                product.categoryId
            )).ToList();

            return productDetailsList;
        }

        /// <summary>
        /// Allows to update the details of a product
        /// </summary>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public void UpdateProductDetails(long productId, long userId, UpdateProductDetailsInput productDetails)
        {
            User user = UserDao.Find(userId);

            if(!user.isAdmin)
            {
                throw new UserWithoutAdminPrivilegiesException("El usuario no tiene privilegios de administrador");
            }
            
            Product product = ProductDao.Find(productId);

            product.name = productDetails.Name;
            product.price = productDetails.Price;
            product.stock = productDetails.Stock;
            product.description = productDetails.Description;
            product.categoryId = productDetails.CategoryId;

            ProductDao.Update(product);
        }

        /// <summary>
        /// Allow to find the details of a product by its id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [Transactional]
        public ProductDetails FindProductDetails(long productId)
        {
            Product product = ProductDao.Find(productId);

            ProductDetails productDetails =
                new ProductDetails(product.name, (float)product.price, product.addingDate, product.stock, product.image, product.description, (long)product.categoryId);


            return productDetails;
        }

        /// <summary>
        /// Allow to know all categories
        /// </summary>
        /// <returns></returns>
        [Transactional]
        public List<Category> GetAllCategories()
        {
            return CategoryDao.GetAllElements();
        }

        /// <summary>
        /// Allow to find the details of a category by its id
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [Transactional]
        public String FindCategory(long categoryId)
        {
            return CategoryDao.Find(categoryId).name;
        }

    }
}
