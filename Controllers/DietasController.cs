using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutNutriYA.Controllers
{
    public class DietasController : Controller
    {
        DietasRepo repo = new DietasRepo();
        public ActionResult Index()
        {
            List<Dieta> model = repo.LeerDieta();
            System.Threading.Thread.Sleep(500);
            return View(model);
        }

        // GET: Dietas/Details/5
        public ActionResult Details(string PK, string RK)
        {
            System.Threading.Thread.Sleep(1000);
            
            var model = repo.LeerPorPKRK(PK,RK);
            if(model == null){
                return NotFound();
            }
            return View(model);
        }

        // GET: Dietas/Create
        public ActionResult Create()
        {
            var model = new Dieta();
            return View(model);
        }

        // POST: Dietas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Dieta model)
        {
            try
            {
                var resultado = repo.CrearDieta(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Dietas/Edit/5
        public ActionResult Edit(string PK, string RK)
        {
            var model = repo.LeerPorPKRK(PK, RK);
            if(model == null){
                return NotFound();
            }
            return View(model);
        }

        // POST: Dietas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Dieta model)
        {
             if(model == null){
                return NotFound();
            }

            var p = repo.LeerPorPKRK(model.Nombre, model.Dia);
            try
            {
                p.Nombre     = model.Nombre;
                p.Dia        = model.Dia;
                p.Desayuno   = model.Desayuno;
                p.ColacionM  = model.ColacionM;
                p.Comida     = model.Comida;
                p.ColacionT  = model.ColacionT;
                p.Cena       = model.Cena;

                var resultado = repo.ActualizarDieta(p);;
                System.Threading.Thread.Sleep(500);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Dietas/Delete/5
        public ActionResult Delete(string PK, string RK)
        {
            var model = repo.LeerPorPKRK(PK, RK);
            if(model == null){
                return NotFound();
            }
            return View(model);
        }

        // POST: Dietas/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Dieta model)
        {
            var p = repo.LeerPorPKRK(model.Nombre, model.Dia);
            try
            {
                System.Threading.Thread.Sleep(500);
                var resultado = repo.BorrarDieta(model);
                System.Threading.Thread.Sleep(500);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}