using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.Library.Models
{
    public class Customer
    {
        private string _firstName;
        private string _lastName;

        public int CustomerId { get; set; }
        public int StoreId { get; set; }
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentException("First name must not be empty.", nameof(value));
                }
                _firstName = value;
            }
        }
        public string LastName
        {
            get => _lastName;
            set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentException("Last name must not be empty.", nameof(value));
                }
                _lastName = value;
            }
        }

        public List<GameOrder> GameOrders { get; set; } = new List<GameOrder>();
        public StoreLocation DefaultStore { get; set; }
    }
}
