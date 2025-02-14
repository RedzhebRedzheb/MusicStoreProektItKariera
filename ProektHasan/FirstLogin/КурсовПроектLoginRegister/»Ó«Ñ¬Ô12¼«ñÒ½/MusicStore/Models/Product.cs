using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Models
{
    public class Product
    {
        
            public virtual int Id { get; set; }
            public virtual string Name { get; set; }
            public virtual int BrandId { get; set; }
            public virtual decimal Price { get; set; }
            public virtual int StockQuantity { get; set; }
            public virtual string Description { get; set; }
            public virtual string ImageUrl { get; set; }

            // Virtual navigation properties
            public virtual Brand Brand { get; set; }
            public virtual int CategoryId { get; set; }
            public virtual Category Category { get; set; }
            public virtual int SupplierId { get; set; }
            public virtual Supplier Supplier { get; set; }

            public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
            public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

            // Public constructor
            public Product() { }
        

    }
}
