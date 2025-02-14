using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Models
{
    public class Supplier
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string ContactInfo { get; set; }
        public  virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
