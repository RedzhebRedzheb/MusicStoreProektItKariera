using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MusicStore.Models
{
    public class Order
    {
        public virtual int Id { get; set; }
        public string UserId {  get; set; }
        public virtual IdentityUser User { get; set; }

        public virtual DateTime OrderDate { get; set; }
        public virtual decimal TotalPrice { get; set; }
        public virtual string Status { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
