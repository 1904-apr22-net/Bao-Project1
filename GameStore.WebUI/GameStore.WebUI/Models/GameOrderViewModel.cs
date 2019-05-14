using GameStore.Library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.WebUI.Models
{
    public class GameOrderViewModel
    {
        [Display(Name = "Order Id")]
        public int OrderId { get; set; }

        [Display(Name = "Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Display(Name = "Store")]
        public int StoreId { get; set; }
        public StoreLocation StoreLocation { get; set; }

        [Display(Name = "Order Time")]
        public DateTime OrderTime { get; set; }

        public OrderItem OrderItem { get; set; }
        [Display(Name = "Games")]
        public List<Game> Games { get; set; }

        [Display(Name ="List of Games")]
        public IEnumerable<Game> ListOfGames { get; set; }
        public Game Game { get; set; }
    }
}
