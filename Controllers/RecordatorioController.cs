using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutNutriYA.Controllers
{
    [Authorize(Roles="Nutriologo")]
    public class RecordatorioController : Controller
    {
        RecordatorioRepo repo = new RecordatorioRepo();
        // GET: Recordatorio
        public ActionResult Index()
        {
            List<Recordatorio> model = repo.LeerRecordatorio();
            System.Threading.Thread.Sleep(500);
            return View(model);
        }

        // GET: Recordatorio/Details/5
        public ActionResult Details(string PK, string RK)
        {
            System.Threading.Thread.Sleep(1000); 
            var model = repo.LeerPorPKRK(PK,RK);
            if(model == null){
                return NotFound();
            }
            return View(model);
        }

        // GET: Recordatorio/Create
        public ActionResult Create()
        {
            var model = new Recordatorio();
            return View(model);
        }

        // POST: Recordatorio/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Recordatorio model)
        {
            try
            {
                var resultado = repo.CrearRecordatorio(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Recordatorio/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Recordatorio/Edit/5
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

        // GET: Recordatorio/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Recordatorio/Delete/5
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