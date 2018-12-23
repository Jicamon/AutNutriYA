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
    public class platillosController : Controller
    {
        PlatillosRepo repo = new PlatillosRepo();
        IngredientesRepository ingre = new IngredientesRepository();
        // GET: platillos
        public ActionResult Index()
        {
            List<Platillo> model = repo.LeerPlatillo().Result;
            return View(model);
        }

        // GET: platillos/Details/5
        public ActionResult Details(string PK, string RK)
        {
            var model = repo.LeerPorPKRK(PK,RK).Result;
            if(model == null){
                return NotFound();
            }
            return View(model);
        }

        // GET: platillos/Create
        public ActionResult Create()
        {
            var model = new Platillo();
            var Ingredientes = ingre.LeerIngrediente().Result;
            ViewBag.Ingre = Ingredientes;
            return View(model);
        }

        // POST: platillos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Platillo model)
        {
            try
            {
                var resultado = repo.CrearPlatillo(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: platillos/Edit/5
        public ActionResult Edit(string PK, string RK)
        {
            var model = repo.LeerPorPKRK(PK, RK).Result;
            var Ingredientes = ingre.LeerIngrediente().Result;
            ViewBag.ingre = Ingredientes;
            if(model == null){
                return NotFound();
            }
            return View(model);
        }

        // POST: platillos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Platillo model)
        {
            if(model == null){
                return NotFound();
            }
            var p = repo.LeerPorPKRK(model.tipo, model.platillo).Result;
            try
            {
                p.tipo = model.tipo;
                p.platillo = model.platillo;
                p.ingredientes = model.ingredientes;
                p.calorias = model.calorias;
                p.porcion = model.porcion;

                var resultado = repo.ActualizarPlatillo(p).Result;

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: platillos/Delete/5
        public ActionResult Delete(string PK, string RK)
        {
            var model = repo.LeerPorPKRK(PK, RK).Result;
            if(model == null){
                return NotFound();
            }
            return View(model);
        }

        // POST: platillos/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Platillo model)
        {
            var pack = repo.LeerPorPKRK(model.tipo, model.platillo).Result;

            try
            {
                var resultado = repo.BorrarPlatillo(model).Result;
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}