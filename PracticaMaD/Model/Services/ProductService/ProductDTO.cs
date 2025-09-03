using System;
using System.Collections.Generic;

namespace Model.Services.ProductService
{
    /// <summary>
    /// VO Class which contains the product details
    /// </summary>
    [Serializable()]
    public class ProductDTO
    {
        #region Properties Region

        public long ProductId { get; private set; }

        public String Name { get; private set; }

        public float Price { get; private set; }
        
        public DateTime AddingDate { get; private set; }

        public long? CategoryId { get; private set; }


        #endregion Properties Region

        public ProductDTO(long productId, string name, float price, DateTime addingDate, long? categoryId)
        {
            ProductId = productId;
            Name = name;
            Price = price;
            AddingDate = addingDate;
            CategoryId = categoryId;
        }


        public override String ToString()
        {
           return  "[ Name = " + Name + " | " +
                "Price = " + Price + " | " +
                "AddingDate = " + AddingDate + " | " +
                "CategoryId = " + CategoryId + " | " +
                "ProductId = " + ProductId + " ]";
        }

        public override bool Equals(object obj)
        {
            return obj is ProductDTO dTO &&
                   ProductId == dTO.ProductId &&
                   Name == dTO.Name &&
                   Price == dTO.Price &&
                   AddingDate == dTO.AddingDate &&
                   CategoryId == dTO.CategoryId;
        }

        public override int GetHashCode()
        {
            int hashCode = 2061049573;
            hashCode = hashCode * -1521134295 + ProductId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + Price.GetHashCode();
            hashCode = hashCode * -1521134295 + AddingDate.GetHashCode();
            hashCode = hashCode * -1521134295 + CategoryId.GetHashCode();
            return hashCode;
        }
    }
}
