using GameStore.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.Library.Interface
{
    public interface IGameStoreRepository
    {
        IEnumerable<Customer> GetCustomers(string search = null);
        Library.Models.Customer GetCustomer(string firstName, string lastName);
        Library.Models.Customer GetCustomerById(int id);
        IEnumerable<Library.Models.GameOrder> GetGameOrders();
        IEnumerable<Game> GetGames();
        Library.Models.GameOrder GetRecentOrderByCustomerId(int customerId);
        IEnumerable<Library.Models.GameOrder> GetGameOrdersByCustomerId(int customerId);
        StoreLocation GetStoreById(int id);
        Library.Models.GameOrder GetGameOrderById(int id);
        Library.Models.Game GetGameById(int gameId);
        int CustomerIdFromOrderId(int orderId);
        IEnumerable<Library.Models.OrderItem> GetOrderItems(int orderId);
        int StoreIdFromOrderId(int orderId);
        IEnumerable<StoreLocation> GetGameStores(string search = null);
        int GetOrderItemIdByCustomerId(int orderId);
        int GetGameIdByOrderItemId(int orderItemId);
        void AddOrder(Library.Models.GameOrder gameOrder);
        void Save();

    }
}
