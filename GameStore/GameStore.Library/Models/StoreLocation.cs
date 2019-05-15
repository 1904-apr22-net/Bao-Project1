using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.Library.Models
{
    public class StoreLocation
    {
        private string _name;

        public int Id { get; set; }
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

        public string State { get; set; }
    }
}
