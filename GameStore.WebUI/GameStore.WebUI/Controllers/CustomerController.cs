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
    public class CustomerController : Controller
    {
        public IGameStoreRepository Repo { get; }
        public CustomerController(IGameStoreRepository repo) =>
            Repo = repo ?? throw new ArgumentNullException(nameof(repo));
        // GET: Customer
        public ActionResult Index([FromQuery]string search = "")
        {
            IEnumerable<Customer> customers = Repo.GetCustomers(search);
            IEnumerable<CustomerViewModel> viewModels = customers.Select(x => new CustomerViewModel
            {
                CustomerId = x.CustomerId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                DefaultStore = x.StoreId,
                StoreLocation = Repo.GetStoreById(x.StoreId),
                GameOrders = x.GameOrders.Select(y => new GameOrderViewModel())
                //GameOrders = Repo.GetGameOrdersByCustomerId(x.CustomerId)
                //GameOrders = x.GameOrders.Select(y => new GameOrderViewModel
                //{
                //    CustomerId = y.CustomerId,
                //    StoreId = y.StoreId,
                //    OrderId = y.Id,
                //    OrderTime = y.OrderTime
                //})
            });
            return View(viewModels);
        }

        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            Customer customers = Repo.GetCustomerById(id);
            var viewModels = new CustomerViewModel
            {
                CustomerId = customers.CustomerId,
                FirstName = customers.FirstName,
                LastName = customers.LastName,
                DefaultStore = customers.StoreId,
                StoreLocation = Repo.GetStoreById(customers.StoreId),
                ListOfGameOrders = Repo.GetGameOrdersByCustomerId(customers.CustomerId)
                //GameOrders = customers.GameOrders.Select(y => new GameOrderViewModel
                //{
                //    OrderId = y.Id,
                //    StoreId = y.StoreId,
                //    CustomerId = y.CustomerId,
                //    OrderTime = y.OrderTime
                //})
                //GameOrders = customers.GameOrders.Select(y => new GameOrderViewModel
                //{
                //    CustomerId = y.CustomerId,
                //    StoreId = y.StoreId,
                //    OrderId = y.Id,
                //    OrderTime = y.OrderTime
                //})
            };
            return View(viewModels);
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Customer/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}