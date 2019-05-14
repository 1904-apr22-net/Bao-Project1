using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameStore.DataAccess
{
    public static class Mapper
    {
        public static Library.Models.Customer Map(Entities.Customer customer) => new Library.Models.Customer
        {
            CustomerId = customer.CustomerId,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            StoreId = customer.DefaultStore
        };
        public static Entities.Customer Map(Library.Models.Customer customer) => new Entities.Customer
        {
            CustomerId = customer.CustomerId,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            DefaultStore = customer.StoreId
        };

        public static Library.Models.StoreLocation Map(Entities.GameStore store) => new Library.Models.StoreLocation
        {
            Id = store.StoreId,
            Name = store.Name,
            State = store.State
        };

        public static Entities.GameStore Map(Library.Models.StoreLocation gameStore) => new Entities.GameStore
        {
            StoreId = gameStore.Id,
            Name = gameStore.Name,
            State = gameStore.State
        };

        public static Library.Models.GameOrder Map(Entities.GameOrder gameOrder) => new Library.Models.GameOrder
        {
            Id = gameOrder.OrderId,
            CustomerId = gameOrder.CustomerId,
            StoreId = gameOrder.StoreId,
            OrderTime = gameOrder.OrderTime
        };

        public static Entities.GameOrder Map(Library.Models.GameOrder gameOrder) => new Entities.GameOrder
        {
            OrderId = gameOrder.Id,
            CustomerId = gameOrder.CustomerId,
            StoreId = gameOrder.StoreId,
            OrderTime = gameOrder.OrderTime
        };

        public static Library.Models.OrderItem Map(Entities.OrderItem orderItem) => new Library.Models.OrderItem
        {
            OrderItemId = orderItem.OrderItemId,
            Quantity = (int)orderItem.Quantity,
            Game = Map(orderItem.Game),
            GameOrder = Map(orderItem.Order)
        };

        public static Entities.OrderItem Map(Library.Models.OrderItem orderItem) => new Entities.OrderItem
        {
            OrderItemId = orderItem.OrderItemId,
            Quantity = orderItem.Quantity,
            Game = Map(orderItem.Game),
            Order = Map(orderItem.GameOrder)
        };

        public static Library.Models.Game Map(Entities.Game game) => new Library.Models.Game
        {
            GameId = game.GameId,
            Name = game.Name,
            Price = game.Price,
        };

        public static Entities.Game Map(Library.Models.Game game) => new Entities.Game
        {
            GameId = game.GameId,
            Name = game.Name,
            Price = game.Price
        };

        public static Library.Models.ItemInventory Map(Entities.ItemInventory itemInventory) => new Library.Models.ItemInventory
        {
            InventoryId = itemInventory.ItemInventoryId,
            Quantity = (int)itemInventory.Quantity,
            StoreLocation = Map(itemInventory.Store),
            Game = Map(itemInventory.Game)
        };

        public static Entities.ItemInventory Map(Library.Models.ItemInventory itemInventory) => new Entities.ItemInventory
        {
            ItemInventoryId = itemInventory.InventoryId,
            Quantity = itemInventory.Quantity,
            Store = Map(itemInventory.StoreLocation),
            Game = Map(itemInventory.Game)
        };

        public static IEnumerable<Library.Models.Customer> Map(IEnumerable<Entities.Customer> customers)
        {
            return customers.Select(Map);
        }

        public static IEnumerable<Entities.Customer> Map(IEnumerable<Library.Models.Customer> customers) =>
            customers.Select(Map);

        public static IEnumerable<Library.Models.StoreLocation> Map(IEnumerable<Entities.GameStore> stores)
        {
            return stores.Select(Map);
        }

        public static IEnumerable<Entities.GameStore> Map(IEnumerable<Library.Models.StoreLocation> stores) =>
            stores.Select(Map);

        public static IEnumerable<Library.Models.GameOrder> Map(IEnumerable<Entities.GameOrder> gameOrder)
        {
            return gameOrder.Select(Map);
        }

        public static IEnumerable<Entities.GameOrder> Map(IEnumerable<Library.Models.GameOrder> gameOrders) =>
            gameOrders.Select(Map);

        public static IEnumerable<Library.Models.OrderItem> Map(IEnumerable<Entities.OrderItem> orderItems)
        {
            return orderItems.Select(Map);
        }

        public static IEnumerable<Entities.OrderItem> Map(IEnumerable<Library.Models.OrderItem> orderItems) =>
            orderItems.Select(Map);

        public static IEnumerable<Library.Models.Game> Map(IEnumerable<Entities.Game> games)
        {
            return games.Select(Map);
        }

        public static IEnumerable<Entities.Game> Map(IEnumerable<Library.Models.Game> games) =>
            games.Select(Map);
    }
}
