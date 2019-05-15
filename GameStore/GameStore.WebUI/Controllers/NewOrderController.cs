using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.WebUI.Controllers
{
    public class NewOrderController : Controller
    {
        // GET: NewOrder
        public ActionResult Index()
        {
            return View();
        }

        // GET: NewOrder/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: NewOrder/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NewOrder/Create
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

        // GET: NewOrder/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: NewOrder/Edit/5
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

        // GET: NewOrder/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: NewOrder/Delete/5
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