using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutNutriYA.Controllers
{
    [Authorize(Roles="Admin")]
    public class NutriologosController : Controller
    {
        
        // GET: Nutriologo
        NutriologosRepository repo = new NutriologosRepository();
        public ActionResult Index()
        {
            List<Nutriologo> model = repo.LeerNutriologos();
            System.Threading.Thread.Sleep(800);
            return View(model);
        }

        // GET: Nutriologo/Details/5
        public ActionResult Details(string correo, string nombre)
        {
            var model = repo.LeerNutriologo(correo,nombre);
            if(model == null){
                return NotFound();
            }
            return View(model);
        }

        // GET: Nutriologo/Create
        public ActionResult Create()
        {
            var model = new Nutriologo();
            return View(model);
        }

        // POST: Nutriologo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Nutriologo model)
        {
            try
            {
                var resultado = repo.CrearNutriologo(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Nutriologo/Edit/5
        public ActionResult Edit(string correo,string nombre)
        {
            var model = repo.LeerNutriologo(correo,nombre);
            if(model == null){
                return NotFound();
            }

            return View(model);
        }

        // POST: Nutriologo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Nutriologo model)
        {
            var newNutriologo = repo.LeerNutriologo(model.Correo,model.Nombre);

            if(model == null){
                return NotFound();
            }

            try
            {
                // TODO: Add update logic here

                newNutriologo.Pacientes = model.Pacientes;
                newNutriologo.Telefono = model.Telefono;
                newNutriologo.Direccion = model.Direccion;

                var resultado = repo.ActualizarNutriologo(newNutriologo);
                System.Threading.Thread.Sleep(800);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Nutriologo/Delete/5
        public ActionResult Delete(string correo,string nombre)
        {
            
            var model = repo.LeerNutriologo(correo,nombre);
            if(model == null){
                return NotFound();
            }

            return View(model);
        }

        // POST: Nutriologo/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string Correo,string Nombre,int Telefono)
        {
            //try
            //{
                // TODO: Add delete logic here
                var newNutriologo = repo.LeerNutriologo(Correo,Nombre);

                if(newNutriologo == null){
                return NotFound();
                }
                var resultado = repo.BorrarNutriologo(newNutriologo);
                System.Threading.Thread.Sleep(800);
                return RedirectToAction(nameof(Index));
            /* }
            catch
            {
                return View();
            }*/
        }
    }
}