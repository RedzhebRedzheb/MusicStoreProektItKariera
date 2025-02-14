using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace UI.Data
{
    public class Contexts : IdentityDbContext
    {
        public Contexts(DbContextOptions<Contexts> options)
            : base(options)
        {
        }
    }
}
