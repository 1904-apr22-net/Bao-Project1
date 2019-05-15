using GameStore.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.WebUI.Models
{
    public class OrderItemsViewModel
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int GameId { get; set; }
        public GameOrder GameOrder{ get; set;}
        public Game Game { get; set; }
        public IEnumerable<Game> Games { get; set; }
    }
}
