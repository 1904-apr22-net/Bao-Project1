using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.Library.Models
{
    public class ItemInventory
    {
        private int _qty;
        public int InventoryId { get; set; }
        public StoreLocation StoreLocation { get; set; }
        public Game Game { get; set; }
        public int Quantity
        {
            get => _qty;
            set
            {
                if (value == 0)
                {
                    throw new ArgumentException("There are none left.", nameof(value));
                }
                _qty = value;
            }
        }
    }
}
