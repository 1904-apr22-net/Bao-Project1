using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.Library.Models
{
    public class GameOrder
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int StoreId { get; set; }
        public Customer Customer { get; set; }
        public StoreLocation GameStore { get; set; }
        public DateTime OrderTime { get; set; }
    }
}
