using GameStore.Library.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        [Display(Name = "Order Item")]
        public IEnumerable<OrderItem> OrderItems { get; set; }
        public OrderItem OrderItem { get; set; }
        public int OrderItemId { get; set; }
        [Display(Name = "Games")]
        public List<Game> Games { get; set; }
        public List<int> ListOfGameId { get; set; }
        public IEnumerable<GameViewModel> SomeGames { get; set; }
        public List<GameViewModel> ListGames { get; set; }
        [Display(Name ="List of Games")]
        public IEnumerable<Game> ListOfGames { get; set; }
        public Game Game { get; set; }
        [Display(Name = "Quantity")]
        public IEnumerable<int> ChooseQuantity { get; set; }
        public int Quantity { get; set; }
        public int GameId { get; set; }
        public IEnumerable<SelectListItem> qty { get; set; }
    }
}
