using System;
using System.Collections.Generic;

namespace GameStore.DataAccess.Entities
{
    public partial class GameStore
    {
        public GameStore()
        {
            Customer = new HashSet<Customer>();
            GameOrder = new HashSet<GameOrder>();
            ItemInventory = new HashSet<ItemInventory>();
        }

        public int StoreId { get; set; }
        public string Name { get; set; }
        public string State { get; set; }

        public virtual ICollection<Customer> Customer { get; set; }
        public virtual ICollection<GameOrder> GameOrder { get; set; }
        public virtual ICollection<ItemInventory> ItemInventory { get; set; }
    }
}
