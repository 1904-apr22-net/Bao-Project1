using System;
using System.Collections.Generic;

namespace GameStore.DataAccess.Entities
{
    public partial class Game
    {
        public Game()
        {
            ItemInventory = new HashSet<ItemInventory>();
            OrderItem = new HashSet<OrderItem>();
        }

        public int GameId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<ItemInventory> ItemInventory { get; set; }
        public virtual ICollection<OrderItem> OrderItem { get; set; }
    }
}
