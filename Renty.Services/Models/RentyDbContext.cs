using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Renty.Services.Models
{
    public class RentyDbContext:DbContext
    {
        public RentyDbContext():base("RentyDb")
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Item> Items { get; set; }
    }
}