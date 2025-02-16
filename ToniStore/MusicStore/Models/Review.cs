using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MusicStore.Models
{
    public class Review
    {
        public virtual int Id { get; set; }
        public virtual string UserId { get; set; }
        public virtual IdentityUser User { get; set; }
        public virtual int ProductId { get; set; }
        public virtual Product Product { get; set; }
        [Range(1, 5)]
        public virtual int Rating { get; set; }
        public virtual string Comment { get; set; }
        public virtual DateTime ReviewDate { get; set; }
    }
}
