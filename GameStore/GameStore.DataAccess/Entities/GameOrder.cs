using System;
using System.Collections.Generic;

namespace GameStore.DataAccess.Entities
{
    public partial class GameOrder
    {
        public GameOrder()
        {
            OrderItem = new HashSet<OrderItem>();
        }

        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int StoreId { get; set; }
        public DateTime OrderTime { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual GameStore Store { get; set; }
        public virtual ICollection<OrderItem> OrderItem { get; set; }
    }
}
