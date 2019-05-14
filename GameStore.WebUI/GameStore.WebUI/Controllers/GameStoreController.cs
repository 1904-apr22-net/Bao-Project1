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
    public class GameStoreController : Controller
    {
        public IGameStoreRepository Repo { get; }

        public GameStoreController(IGameStoreRepository repo) =>
            Repo = repo ?? throw new ArgumentNullException(nameof(repo));


        // GET: GameStore
        public ActionResult Index([FromQuery]string search = "")
        {
            IEnumerable<StoreLocation> gameStores = Repo.GetGameStores(search);
            IEnumerable<StoreViewModel> viewModels = gameStores.Select(x => new StoreViewModel
            {
                StoreId = x.Id,
                Name = x.Name,
                State = x.State
            });
            return View(viewModels);
        }

        // GET: GameStore/Details/5
        public ActionResult Details(int id)
        {
            //StoreLocation storeLocation = Repo.GetStoreById(id);
            //var viewModel = new StoreViewModel
            //{
            //    StoreId = storeLocation.Id,
            //    Name = storeLocation.Name,
            //    State = storeLocation.State
            //};
            return View();
        }

        // GET: GameStore/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GameStore/Create
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

        // GET: GameStore/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GameStore/Edit/5
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

        // GET: GameStore/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GameStore/Delete/5
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