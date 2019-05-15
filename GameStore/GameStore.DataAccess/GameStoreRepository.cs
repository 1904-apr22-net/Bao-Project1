using GameStore.DataAccess.Entities;
using GameStore.Library.Interface;
using GameStore.Library.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameStore.DataAccess
{
    public class GameStoreRepository : IGameStoreRepository
    {
        private readonly GameStoreContext _dbContext;

        public GameStoreRepository(GameStoreContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        //get all customers
        public IEnumerable<Library.Models.Customer> GetCustomers(string search = null)
        {
            return Mapper.Map(_dbContext.Customer);
        }


        //get customer if it matches first and last name
        public Library.Models.Customer GetCustomer(string firstName, string lastName)
        {
            var customer = _dbContext.Customer.FirstOrDefault(c => c.FirstName.Contains(firstName) && c.LastName.Contains(lastName));

            if (customer == null)
            {
                return null;
            }

            return new Library.Models.Customer
            {
                CustomerId = customer.CustomerId,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                StoreId = customer.DefaultStore
            };
        }

        //set quantity
        public void SetQuantity(int setQty, int orderId, int productId)
        {
            var orderItem = _dbContext.OrderItem.Include(oi => oi.OrderId == orderId && oi.GameId == productId);
            foreach (var item in orderItem)
            {
                item.Quantity = setQty;

                //var od = new Entities.OrderItem
                //{
                //    Quantity = setQty
                //};
            }
        }

        //get customer by id
        public Library.Models.Customer GetCustomerById(int id)
        {
            Entities.Customer customer = _dbContext.Customer.Include(c => c.GameOrder)
                .AsNoTracking().First(r => r.CustomerId == id);
            return Mapper.Map(customer);
            //var customer = _dbContext.Customer.FirstOrDefault(c => c.CustomerId.Equals(id));
            //if (customer == null)
            //{
            //    return null;
            //}
            //return new Library.Models.Customer
            //{
            //    CustomerId = customer.CustomerId,
            //    FirstName = customer.FirstName,
            //    LastName = customer.LastName,
            //    StoreId = customer.DefaultStore
            //};
        }

        //get the latest order of customer
        public Library.Models.GameOrder GetRecentOrderByCustomerId(int customerId)
        {

            var order = _dbContext.GameOrder.OrderByDescending(o => o.OrderId).FirstOrDefault(o => o.CustomerId.Equals(customerId));

            if (order == null)
            {
                return null;
            }

            return new Library.Models.GameOrder
            {
                Id = order.OrderId,
                OrderTime = order.OrderTime,
                CustomerId = order.CustomerId,
                StoreId = order.StoreId
            };
        }

        public Library.Models.GameOrder GetGameOrderById(int id)
        {
            Entities.GameOrder gameOrder = _dbContext.GameOrder.AsNoTracking()
                .First(r => r.OrderId == id);
            return Mapper.Map(gameOrder);
        }

        public int CustomerIdFromOrderId(int orderId)
        {
            Entities.GameOrder gameOrder = _dbContext.GameOrder.AsNoTracking()
                .First(r => r.OrderId == orderId);
            return gameOrder.CustomerId;
        }

        public int GetOrderItemIdByCustomerId(int orderId)
        {
            Entities.OrderItem orderItem = _dbContext.OrderItem.AsNoTracking()
                .First(r => r.OrderId == orderId);
            return orderItem.OrderItemId;
        }

        public int GetGameIdByOrderItemId(int orderItemId)
        {
            Entities.OrderItem game = _dbContext.OrderItem.AsNoTracking()
                .First(r => r.OrderItemId == orderItemId);
            return game.GameId;
        }
        public int StoreIdFromOrderId(int orderId)
        {
            Entities.GameOrder gameOrder = _dbContext.GameOrder.AsNoTracking()
                .First(r => r.OrderId == orderId);
            return gameOrder.StoreId;
        }

        //get a store by id
        public StoreLocation GetStoreById(int id)
        {
            var storeLocation = _dbContext.GameStore.FirstOrDefault(s => s.StoreId.Equals(id));

            if (storeLocation == null)
            {
                return null;
            }

            return new StoreLocation
            {
                Id = storeLocation.StoreId,
                Name = storeLocation.Name,
                State = storeLocation.State
            };
        }

        //get a game by id
        public Library.Models.Game GetGameById(int gameId)
        {
            var game = _dbContext.Game.FirstOrDefault(g => g.GameId.Equals(gameId));

            if (game == null)
            {
                return null;
            }

            return new Library.Models.Game
            {
                GameId = game.GameId,
                Name = game.Name,
                Price = game.Price
            };
        }

        public void AddOrder(Library.Models.GameOrder gameOrder)
        {
            _dbContext.Add(Mapper.Map(gameOrder));
        }

        //get all games affiliated with orderid
        public IEnumerable<Library.Models.OrderItem> GetOrderItems(int orderId)
        {
            return Mapper.Map(_dbContext.OrderItem.Where(oi => oi.OrderId == orderId).ToList());
        }

        //get game affiliated with orderid
        //public IEnumerable<Game> GetGamesByOrderId(int orderId)
        //{

        //}

        //get all games
        public IEnumerable<Library.Models.Game> GetGames()
        {
            return Mapper.Map(_dbContext.Game);
        }

        public IEnumerable<Library.Models.GameOrder> GetGameOrders()
        {
            return Mapper.Map(_dbContext.GameOrder);
        }

        public IEnumerable<Library.Models.GameOrder> GetGameOrdersByCustomerId(int customerId)
        {
            return Mapper.Map(_dbContext.GameOrder.Where(id => id.CustomerId == customerId).ToList());
        }

        //get all stores
        public IEnumerable<StoreLocation> GetGameStores(string search = null)
        {
            IQueryable<Entities.GameStore> items = _dbContext.GameStore;
            if (search != null)
            {
                items = items.Where(c => c.Name.Contains(search));
            }

            return Mapper.Map(items);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
