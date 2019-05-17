using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.Library.Interface;
using GameStore.Library.Models;
using GameStore.WebUI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameStore.WebUI.Controllers
{
    public class GameOrderController : Controller
    {
        public IGameStoreRepository Repo { get; }

        public GameOrderController(IGameStoreRepository repo) =>
            Repo = repo ?? throw new ArgumentNullException(nameof(repo));

        //// GET: GameOrder
        public ActionResult Index()
        {
            IEnumerable<GameOrder> gameOrders = Repo.GetGameOrders();
            IEnumerable<GameOrderViewModel> viewModel = gameOrders.Select(x => new GameOrderViewModel
            {
                OrderId = x.Id,
                CustomerId = x.CustomerId,
                StoreId = x.StoreId,
                OrderTime = x.OrderTime,
                Customer = Repo.GetCustomerById(x.CustomerId),
                StoreLocation = Repo.GetStoreById(x.StoreId)
            });
            return View(viewModel);
        }

        // GET: GameOrder/Details/5
        public ActionResult Details(int id)//orderId
        {
            GameOrder gameOrder = Repo.GetGameOrderById(id);
            var customerId = Repo.CustomerIdFromOrderId(id);
            var storeId = Repo.StoreIdFromOrderId(id);

            var orderItemId = Repo.GetOrderItemIdByOrderId(id);
            var customerIdToStoreId = Repo.GetStoreIdByCustomerId(customerId);
            var orderItems = Repo.GetOrderItemsByOrderId(id);
            var orderItem = Repo.GetOrderItemByOrderId(id);
            var game = Repo.GetGameById(orderItem.GameId);

            List<Game> games = new List<Game>();
            List<int> qty = new List<int>();
            foreach (var item in orderItems)
            {
                games.Add(Repo.GetGameById(item.GameId));
                qty.Add(item.Quantity);
            }
            var viewModel = new GameOrderViewModel
            {
                QauntityList = qty,
                Quantity = orderItem.Quantity,
                ListOfGames = games,
                Game = game,
                OrderItem = orderItem,
                OrderItemId = orderItemId,
                OrderItems = orderItems,
                StoreLocation = Repo.GetStoreById(customerIdToStoreId),
                Customer = Repo.GetCustomerById(customerId),
                OrderId = gameOrder.Id,
                CustomerId = customerId,
                StoreId = customerIdToStoreId,
                OrderTime = gameOrder.OrderTime
            };
            return View(viewModel);
        }

        // GET: GameOrder/Create
        public ActionResult Create([FromQuery]int customerId)
        {
            var storeId = Repo.GetStoreIdByCustomerId(customerId);
            var games = Repo.GetGames();
            var orderId = Repo.GetRecentOrderIdByCustomerId(customerId);
            var orderItems = Repo.GetOrderItemsByOrderId(orderId);
            var orderItemId = Repo.GetOrderItemIdByOrderId(orderId);
            var gameId = Repo.GetGameByOrderId(orderId);

            List<int> qty = new List<int>();
            qty.Add(0);
            for (int i = 0; i < 10; i++)
            {
                qty.Add(i + 1);
            }


            var gameOrder = new GameOrderViewModel
            {
                GameId = gameId.GameId,
                OrderItemId = orderItemId,
                OrderItems = orderItems,
                ChooseQuantity = qty,
                Customer = Repo.GetCustomerById(customerId),
                StoreLocation = Repo.GetStoreById(storeId),
                ListOfGames = Repo.GetGames(),
                CustomerId = customerId
            };
            return View(gameOrder);
        }


        // POST: GameOrder/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GameOrderViewModel viewModel)
        {
            try
            {
                GameOrder gameOrder = new GameOrder();
                gameOrder.CustomerId = viewModel.CustomerId;
                gameOrder.StoreId = Repo.GetStoreIdByCustomerId(viewModel.CustomerId);
                gameOrder.OrderTime = DateTime.Now;

                Repo.AddOrder(gameOrder);
                Repo.Save();

                OrderItem orderItem = new OrderItem();
                var listOfGames = Repo.GetGames().ToList();
                for (int i = 0; i < viewModel.QauntityList.Count(); i++)
                {
                    if (viewModel.QauntityList[i] > 0)
                    {
                        orderItem.GameOrderId = Repo.GetRecentOrder().Id;
                        orderItem.GameId = listOfGames[i].GameId;
                        orderItem.Quantity = viewModel.QauntityList[i];

                        Repo.AddOrderItem(orderItem);
                        Repo.Save();
                    }
                }
                

                // TODO: Add insert logic here
                return RedirectToAction(nameof(CustomerController.Index));
            }
            catch
            {
                return RedirectToAction(nameof(CustomerController.Index));
            }
        }

        //POST: GameOrder / Create / generic
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Order()
        {
            try
            {
                GameOrder gameOrder = new GameOrder();
                gameOrder.CustomerId = 1;
                gameOrder.StoreId = Repo.GetStoreIdByCustomerId(1);
                gameOrder.OrderTime = DateTime.Now;

                Repo.AddOrder(gameOrder);
                Repo.Save();

                OrderItem orderItem = new OrderItem();
                orderItem.GameOrderId = Repo.GetRecentOrder().Id;
                orderItem.GameId = 3;
                orderItem.Quantity = 3;

                Repo.AddOrderItem(orderItem);
                //Repo.AddOrder(gameOrder);
                Repo.Save();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: GameOrder/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        // POST: GameOrder/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: GameOrder/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: GameOrder/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}