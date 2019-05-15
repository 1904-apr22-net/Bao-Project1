using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.Library.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int Quantity { get; set; }
        public int GameId { get; set; }

        public int GameOrdId { get; set; }
        public Game Game { get; set; }
        public GameOrder GameOrder { get; set; }
    }
}
