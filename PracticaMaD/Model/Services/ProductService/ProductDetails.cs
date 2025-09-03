using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Services.ProductService
{
    /// <summary>
    /// VO Class which contains the product details
    /// </summary>
    [Serializable()]
    public class ProductDetails
    {
        #region Properties Region

        public String Name { get; private set; }

        public float Price { get; private set; }
        
        public DateTime AddingDate { get; private set; }

        public int Stock { get; private set; }

        public String Image { get; private set; }

        public String Description { get; private set; }

        public long CategoryId { get; private set; }

        #endregion Properties Region

        public ProductDetails(string name, float price, DateTime addingDate,
            int stock, string image, string description, long categoryId)
        {
            Name = name;
            Price = price;
            AddingDate = addingDate;
            Stock = stock;
            Image = image;
            Description = description;
            this.CategoryId = categoryId;
        }

        public override bool Equals(object obj)
        {
            return obj is ProductDetails details &&
                   Name == details.Name &&
                   Price == details.Price &&
                   AddingDate == details.AddingDate &&
                   Stock == details.Stock &&
                   Image == details.Image &&
                   Description == details.Description &&
                   CategoryId == details.CategoryId;
        }

        public override int GetHashCode()
        {
            int hashCode = 863327228;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + Price.GetHashCode();
            hashCode = hashCode * -1521134295 + AddingDate.GetHashCode();
            hashCode = hashCode * -1521134295 + Stock.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Image);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description);
            hashCode = hashCode * -1521134295 + CategoryId.GetHashCode();
            return hashCode;
        }

        public override String ToString()
        {
           return  "[ Name = " + Name + " | " +
                "Price = " + Price + " | " +
                "AddingDate = " + AddingDate + " | " +
                "Stock = " + Stock + " | " +
                "Image = " + Image + " | " +
                "Description = " + Description +
                "CategoryId = " + CategoryId + " ]";
        }
    }
}
