using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShoppingListAPI.Models
{
    public class ShoppingListDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ShoppingCategory> Categories { get; set; }
        public DbSet<ShoppingItem> Items { get; set; }
        public DbSet<ShoppingList> Lists { get; set; }

        public ShoppingListDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ShoppingListDbContext Create()
        {
            return new ShoppingListDbContext();
        }

    }
}