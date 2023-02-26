using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryProX
{
    public class Item
    {
        public string name { get; set; }
        public string description { get; set; }
        public int quantity { get; set; }
        public float price { get; set; }

        public Item() { }
        public Item(string name, int quantity)
        {
            this.name = name;
            this.quantity = quantity;
        }

        public Item(string name, string description, int quantity, float price)
        {
            this.name = name;
            this.description = description;
            this.quantity = quantity;
            this.price = price;
        }
    }
}
