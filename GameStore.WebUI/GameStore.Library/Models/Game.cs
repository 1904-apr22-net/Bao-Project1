using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.Library.Models
{
    public class Game
    {
        private string _name;
        private decimal _price;

        public int GameId { get; set; }

        public string Name
        {
            get => _name;
            set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentException("Name must not be empty.", nameof(value));
                }
                _name = value;
            }
        }
        public decimal Price
        {
            get => _price;
            set
            {
                
                _price = value;
            }
        }
    }
}
