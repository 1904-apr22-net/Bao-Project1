using GameStore.Library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.WebUI.Models
{
    public class NewOrderViewModel
    {
        //[Display(Name = "Name")]
        //public Customer Customer { get; set; }
        public IEnumerable<Game> Games { get; set; }
        public OrderItem OrderItem { get; set; }
        public Game Game { get; set; }
        public GameOrder GameOrder{get;set;}
        public Customer Customer { get; set; }

    }
}
