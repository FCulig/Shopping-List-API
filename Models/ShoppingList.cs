using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoppingListAPI.Models
{
    public class ShoppingList
    {
        [Key]
        public int ID { get; set; }
        public string UserID { get; set; }

        public ICollection<ShoppingItem> Items { get; set; }
    }
}