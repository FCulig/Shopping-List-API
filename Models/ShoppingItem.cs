using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoppingListAPI.Models
{
    public class ShoppingItem
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Quantity { get; set; }
        public Boolean Bought {get; set;}
        public string UserID { get; set; }

        public int ListID { get; set; }
        [JsonIgnore]
        public ShoppingList List { get; set; }

        public int CategoryID { get; set; }
        public ShoppingCategory Category { get; set; }
    }
}