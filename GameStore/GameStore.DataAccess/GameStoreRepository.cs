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

        public Library.Models.GameOrder GetRecentOrder()
        {
            return Mapper.Map(_dbContext.GameOrder.OrderByDescending(o => o.OrderId).First());

            //var order = _dbContext.GameOrder.OrderByDescending(o => o.OrderId).First();

            //if (order == null)
            //{
            //    return null;
            //}

            //return new Library.Models.GameOrder
            //{
            //    Id = order.OrderId,
            //    OrderTime = order.OrderTime,
            //    CustomerId = order.CustomerId,
            //    StoreId = order.StoreId
            //};
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
        public int GetRecentOrderIdByCustomerId(int customerId)
        {

            var order = _dbContext.GameOrder.OrderByDescending(o => o.OrderId).FirstOrDefault(o => o.CustomerId.Equals(customerId));


            return order.OrderId;
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

        public int GetOrderItemIdByOrderId(int orderId)
        {
            Entities.OrderItem orderItem = _dbContext.OrderItem.AsNoTracking()
                .First(r => r.OrderId == orderId);
            return orderItem.OrderItemId;
        }

        //public GameOrder GetQuantity()
        //{

        //}

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

        //get store id with customer id
        public int GetStoreIdByCustomerId(int customerId)
        {
            var storeId = _dbContext.Customer.FirstOrDefault(s => s.CustomerId == customerId);
            return storeId.DefaultStore;
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

        public void AddOrderItem(Library.Models.OrderItem orderItem)
        {
            _dbContext.Add(Mapper.Map(orderItem));
        }

        public int GetGameIdByOrderId(int orderId)
        {
            var orderItem = _dbContext.OrderItem.FirstOrDefault(oi => oi.OrderId.Equals(orderId));

            return orderItem.GameId;
        }

        public Library.Models.OrderItem GetOrderItemByOrderId(int orderId)
        {
            var orderItem = _dbContext.OrderItem.FirstOrDefault(oi => oi.OrderId.Equals(orderId));
            return new Library.Models.OrderItem
            {
                OrderItemId = orderItem.OrderItemId,
                GameOrderId = orderItem.OrderId,
                GameId = orderItem.GameId
            };
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

        public IEnumerable<Library.Models.OrderItem> GetOrderItemsByOrderId(int orderId)
        {
            var items = _dbContext.OrderItem;
            List<Library.Models.OrderItem> orderItems = new List<Library.Models.OrderItem>();

            foreach (var item in items)
            {
                if (item.OrderId == orderId)
                {
                    var orderItem = new Library.Models.OrderItem
                    {
                        OrderItemId = item.OrderItemId,
                        GameOrderId = item.OrderId,
                        GameId = item.GameId
                    };
                    orderItems.Add(orderItem);
                }
            }
            return orderItems.ToList();
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

        public void Create(Library.Models.GameOrder gameOrder)
        {
            //var orderEntity = new Entities.GameOrder
            //{
            //    CustomerId = gameOrder.CustomerId,
            //    StoreId = gameOrder.StoreId
            //};

            //var orderItemEntity = new Entities.OrderItem
            //{
            //    OrderId = orderItem.OrderItemId,
            //    GameId = orderItem.GameId,
            //    Quantity = orderItem.Quantity
            //};
            var orderEntity = _dbContext.GameOrder;
            //var orderItemEntity = _dbContext.OrderItem;

            Entities.GameOrder newOrderEntity = Mapper.Map(gameOrder);
            //Entities.OrderItem newOrderItemEntity = Mapper.Map(orderItem);

            orderEntity.Add(newOrderEntity);
            //orderItemEntity.Add(newOrderItemEntity);

            //_dbContext.GameOrder.Add(orderEntity);
            //_dbContext.OrderItem.Add(orderItemEntity);
            _dbContext.SaveChanges();
        }
    }
}
