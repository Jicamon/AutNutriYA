using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutNutriYA.Controllers
{
    [Authorize(Roles = "Nutriologo")]

    public class IngredientesController : Controller
    {   
        //int Hola = 3;
        IngredientesRepository repo = new IngredientesRepository();
        
        // GET: Ingredientes
        public ActionResult Index()
        {
            List<Ingrediente> model = repo.LeerIngrediente().Result;
            return View(model);
        }

        // GET: Ingredientes/Details/5
        public ActionResult Details(string PK, string RK)
        {
            var model = repo.LeerPorPKRK(PK,RK).Result;
            if(model == null){
                return NotFound();
            }
            return View(model);
        }

        // GET: Ingredientes/Create
        public ActionResult Create()
        {
            var model = new Ingrediente();
            return View(model);
        }

        // POST: Ingredientes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Ingrediente model)
        {
            try
            {
                var resultado = repo.CrearIngrediente(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Ingredientes/Edit/5
        public ActionResult Edit(string PK, string RK)
        {   
            var model = repo.LeerPorPKRK(PK, RK).Result;
            if(model == null){
                return NotFound();
            }

            return View(model);
        }

        // POST: Ingredientes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Ingrediente model)
        {   


            if(model == null){
                return NotFound();
            }

            var pack = repo.LeerPorPKRK(model.Tipo, model.Nombre).Result;

            try
            {
                pack.Tipo = model.Tipo;
                pack.Nombre = model.Nombre;


                var resultado = repo.ActualizarIngrediente(pack).Result;
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Ingredientes/Delete/5
        public ActionResult Delete(string PK, string RK)
        {   
            var model = repo.LeerPorPKRK(PK, RK).Result;

            if(model == null){
                return NotFound();
            }

            return View(model);
        }

        // POST: Ingredientes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Ingrediente model, IFormCollection collection)
        {
            if(model == null){
                return NotFound();
            }

            var pack = repo.LeerPorPKRK(model.Tipo, model.Nombre).Result;

            try
            {
                
                var resultado = repo.BorrarIngrediente(model).Result;
                
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}