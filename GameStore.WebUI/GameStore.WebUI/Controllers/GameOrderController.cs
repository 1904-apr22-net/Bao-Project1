using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.Library.Interface;
using GameStore.Library.Models;
using GameStore.WebUI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.WebUI.Controllers
{
    public class GameOrderController : Controller
    {
        public IGameStoreRepository Repo { get; }

        public GameOrderController(IGameStoreRepository repo) =>
            Repo = repo ?? throw new ArgumentNullException(nameof(repo));

        //// GET: GameOrder
        //public ActionResult Index()
        //{
        //    IEnumerable<GameOrder> gameOrders = Repo.GetGameOrders();
        //    IEnumerable<GameOrderViewModel> viewModel = gameOrders.Select(x => new GameOrderViewModel
        //    {
        //        OrderId = x.Id,
        //        CustomerId = x.CustomerId,
        //        StoreId = x.StoreId,
        //        OrderTime = x.OrderTime,
        //        Customer = Repo.GetCustomerById(x.CustomerId),
        //        StoreLocation = Repo.GetStoreById(x.StoreId)
        //    });
        //    return View(viewModel);
        //}

        // GET: GameOrder/Details/5
        public ActionResult Details(int id)
        {
            GameOrder gameOrder = Repo.GetGameOrderById(id);
            var customerId = Repo.CustomerIdFromOrderId(id);
            var storeId = Repo.StoreIdFromOrderId(id);
            var orderItemId = Repo.GetOrderItemIdByCustomerId(id);
            var gameId = Repo.GetGameIdByOrderItemId(orderItemId);
            Game game = Repo.GetGameById(gameId);
            var viewModel = new GameOrderViewModel
            {
                OrderId = gameOrder.Id,
                CustomerId = customerId,
                StoreId = storeId,
                OrderTime = gameOrder.OrderTime,
                Game = game
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
            var gameOrder = new GameOrderViewModel
            {
                CustomerId = customerId
            };
            return View(gameOrder);
        }

        // POST: GameOrder/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("CustomerId")]GameOrderViewModel viewModel)
        {
            try
            {
                // TODO: Add insert logic here
                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }
                Customer customer = Repo.GetCustomerById(viewModel.CustomerId);
                var gameOrder = new GameOrder
                {
                    Id = viewModel.OrderId,
                    CustomerId = viewModel.CustomerId,
                    StoreId = viewModel.StoreId,
                    OrderTime = viewModel.OrderTime
                };

                return RedirectToAction(nameof(CustomerController.Details),
                    "Customer", new { id = viewModel.CustomerId});
            }
            catch
            {
                return View(viewModel);
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