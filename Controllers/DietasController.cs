using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutNutriYA.Controllers
{
    [Authorize]
    public class DietasController : Controller
    {
        DietasRepo repo = new DietasRepo();
        public ActionResult Index(string correo)
        {
            ViewData["Correo"] = correo;
            List<Dieta> model = repo.LeerDieta(correo).Result;
            //System.Threading.Thread.Sleep(500);
            return View(model);
        }

        // GET: Dietas/Details/5
        public ActionResult Details(string PK, string RK)
        {
            //System.Threading.Thread.Sleep(1000);
            
            var model = repo.LeerPorPKRK(PK,RK).Result;
            if(model == null){
                return NotFound();
            }
            return View(model);
        }

        // GET: Dietas/Create
        public ActionResult Create(string correo)
        {
            ViewData["Correo"] = correo;
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
                //System.Threading.Thread.Sleep(1000);
                return RedirectToAction("Index", new { correo = model.Nombre});
            }
            catch
            {
                return View();
            }
        }

        // GET: Dietas/Edit/5
        public ActionResult Edit(string PK, string RK)
        {
            var model = repo.LeerPorPKRK(PK, RK).Result;
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

            var p = repo.LeerPorPKRK(model.Nombre, model.Dia).Result;
            try
            {
                p.Nombre      = model.Nombre;
                p.Dia         = model.Dia;
                p.Desayuno    = model.Desayuno;
                p.ColacionM   = model.ColacionM;
                p.Comida      = model.Comida;
                p.ColacionT   = model.ColacionT;
                p.Cena        = model.Cena;
                p.Bebida1     = model.Bebida1;
                p.Bebida2     = model.Bebida2;
                p.Bebida3     = model.Bebida3;
                p.Desayuno_A  = model.Desayuno_A;
                p.ColacionM_A = model.ColacionM_A;
                p.Comida_A    = model.Comida_A;
                p.ColacionT_A = model.ColacionT_A;
                p.Cena_A      = model.Cena_A;
                p.Bebida1_A   = model.Bebida1_A;
                p.Bebida2_A   = model.Bebida2_A;
                p.Bebida3_A   = model.Bebida3_A;

                var resultado = repo.ActualizarDieta(p).Result;
                //System.Threading.Thread.Sleep(1300);

                return RedirectToAction("Index", new { correo = model.Nombre});
            }
            catch
            {
                return View();
            }
        }

        // GET: Dietas/Delete/5
        public ActionResult Delete(string PK, string RK)
        {
            var model = repo.LeerPorPKRK(PK, RK).Result;
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
            var p = repo.LeerPorPKRK(model.Nombre, model.Dia).Result;
            try
            {
                //System.Threading.Thread.Sleep(500);
                var resultado = repo.BorrarDieta(model).Result;
                //System.Threading.Thread.Sleep(500);
                return RedirectToAction("Index", new { correo = model.Nombre});
            }
            catch
            {
                return View();
            }
        }
    }
}