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
            foreach (var item in orderItems)
            {
                games.Add(Repo.GetGameById(item.GameId));
            }
            //var orderItemId = Repo.GetOrderItemIdByCustomerId(id);
            //var gameId = Repo.GetGameIdByOrderItemId(orderItemId);
            //Game game = Repo.GetGameById(gameId);
            var viewModel = new GameOrderViewModel
            {
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
            //IEnumerable<GameOrder> gameOrders = Repo.GetGameOrders();
            //IEnumerable<GameOrderViewModel> viewModel = gameOrders.Select(x => new GameOrderViewModel
            //{
            //    OrderId = x.Id,
            //    CustomerId = x.CustomerId,
            //    StoreId = x.StoreId,
            //    OrderTime = x.OrderTime,
            //    Customer = Repo.GetCustomerById(x.CustomerId),
            //    StoreLocation = Repo.GetStoreById(x.StoreId)
            //});
            return View(viewModel);
        }

        // GET: GameOrder/Create
        public ActionResult Create([FromQuery]int customerId)
        {
            //var orderId = Repo.GetOrderItemIdByCustomerId(customerId);
            var storeId = Repo.GetStoreIdByCustomerId(customerId);
            var games = Repo.GetGames();
            var orderId = Repo.GetRecentOrderIdByCustomerId(customerId);
            var orderItems = Repo.GetOrderItemsByOrderId(orderId);
            var orderItemId = Repo.GetOrderItemIdByOrderId(orderId);
            var gameId = Repo.GetGameIdByOrderItemId(orderId);
            //List<int> qty = new List<int>();
            //qty.Add(0);
            //for (int i = 0; i < 10; i++)
            //{
            //    qty.Add(i+1);
            //}
            var qty = GetQuantity();

            var quant = new GameOrderViewModel();
            //quant.qty = GetSelectListItems(qty);

            var gameOrder = new GameOrderViewModel
            {
                OrderId = Repo.GetRecentOrder().Id,
                qty = GetSelectListItems(qty),
                GameId = gameId,
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

        //public ActionResult SetOrder([FromQuery]int quantityValue)
        //{

        //}

        private IEnumerable<int> GetQuantity()
        {
            return new List<int>
            {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7
            };
        }

        private IEnumerable<SelectListItem> GetSelectListItems(IEnumerable<int> quantity)
        {
            var selectList = new List<SelectListItem>();

            foreach (var item in quantity)
            {
                selectList.Add(new SelectListItem
                {
                    Value = item.ToString(),
                    Text = item.ToString()
                });
            }

            return selectList;
        }

        //public ActionResult SetOrder(int orderId, int quantity, int gameId, int customerId)
        //{
        //    var storeId = Repo.GetStoreIdByCustomerId(customerId);


        //}
        //List<GameOrderViewModel> gameOrderViewModels = new List<GameOrderViewModel>();
        //int gameid;
        //public ActionResult DisplayViewModel(GameOrderViewModel viewModel)
        //{
        //    //viewModel.OrderId = Repo.GetRecentOrder().Id;
            
        //    return View(viewModel);
        //}

        // POST: GameOrder/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GameOrderViewModel gameOrder)
        {
            try
            {

                gameOrder.OrderId = Repo.GetRecentOrder().Id;

                GameOrder order = new GameOrder();
                order.Id = gameOrder.OrderId + 1;
                order.CustomerId = gameOrder.CustomerId;
                order.StoreId = gameOrder.StoreId;
                //var quantity = GetQuantity();
                //var quant = gameOrder;
                //quant.qty = GetSelectListItems(quantity);


                // TODO: Add insert logic here
                //if (!ModelState.IsValid)
                //{
                //    return View(viewModel);
                //}
                //Customer customer = Repo.GetCustomerById(viewModel.CustomerId);
                //List<Game> game = Repo.GetGames().ToList();
                //var games = new GameOrderViewModel
                //{
                //    Games = game
                //};

                //var gameOrder = new GameOrder
                //{
                //    Id = viewModel.OrderId,
                //    CustomerId = viewModel.CustomerId,
                //    StoreId = viewModel.StoreId,
                //    OrderTime = viewModel.OrderTime
                //};

                //var gameOrder = new GameOrder
                //{
                //    CustomerId = viewModel.CustomerId,
                //    StoreId = viewModel.StoreId,
                //    OrderTime = DateTime.Now
                //};

                //var orderItem = new OrderItem
                //{
                //    OrderItemId = viewModel.OrderItemId,
                //    GameId = viewModel.OrderItemId,
                //    Quantity = viewModel.Quantity
                //};

                //Repo.Create(gameOrder);
                //GameOrder newGameOrder = new GameOrder();
                //gameOrder.CustomerId = gameOrder.CustomerId;
                //gameOrder.StoreId = gameOrder.StoreId;
                //gameOrder.OrderTime = DateTime.Now;

                //OrderItem orderItem = new OrderItem();
                //orderItem.OrderItemId = viewModel.OrderItemId;
                //orderItem.GameId = viewModel.GameId;
                //orderItem.Quantity = viewModel.Quantity;
                //Repo.AddOrder(newGameOrder);
                Repo.Save();
                return RedirectToAction(nameof(CustomerController.Index));
            }
            catch
            {
                return View(gameOrder);
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
                gameOrder.StoreId = 1;
                gameOrder.OrderTime = DateTime.Now;

                Repo.AddOrder(gameOrder);

                OrderItem orderItem = new OrderItem();
                orderItem.GameOrderId = gameOrder.Id;
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
        public ActionResult Edit(int id)
        {
            return View();
        }

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
        public ActionResult Delete(int id)
        {
            return View();
        }

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