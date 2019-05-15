using System;
using System.Collections.Generic;

namespace GameStore.DataAccess.Entities
{
    public partial class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int GameId { get; set; }
        public int? Quantity { get; set; }

        public virtual Game Game { get; set; }
        public virtual GameOrder Order { get; set; }
    }
}
