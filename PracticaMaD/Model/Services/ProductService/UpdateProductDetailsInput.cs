using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Services.ProductService
{
    [Serializable()]
    public class UpdateProductDetailsInput
    {
        #region Properties Region

        public String Name { get; private set; }

        public float Price { get; private set; }

        public int Stock { get; private set; }

        public String Image { get; private set; }

        public String Description { get; private set; }

        public long CategoryId { get; private set; }

        #endregion Properties Region

        public UpdateProductDetailsInput(string name, float price, 
            int stock, string image, string description, long categoryId)
        {
            Name = name;
            Price = price;
            Stock = stock;
            Image = image;
            Description = description;
            CategoryId = categoryId;
        }

        public UpdateProductDetailsInput(string name, float price,
    int stock, string description, long categoryId)
        {
            Name = name;
            Price = price;
            Stock = stock;
            Description = description;
            CategoryId = categoryId;
        }

        public override bool Equals(object obj)
        {
            return obj is UpdateProductDetailsInput input &&
                   Name == input.Name &&
                   Price == input.Price &&
                   Stock == input.Stock &&
                   Image == input.Image &&
                   Description == input.Description &&
                   CategoryId == input.CategoryId;
        }

        public override int GetHashCode()
        {
            int hashCode = 1264973884;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + Price.GetHashCode();
            hashCode = hashCode * -1521134295 + Stock.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Image);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description);
            hashCode = hashCode * -1521134295 + CategoryId.GetHashCode();
            return hashCode;
        }

        public override String ToString()
        {
            return "[ Name = " + Name + " | " +
                 "Price = " + Price + " | " +
                 "Stock = " + Stock + " | " +
                 "Image = " + Image + " | " +
                 "Description = " + Description +
                 "CategoryId = " + CategoryId + " ]";
        }
    }
}
