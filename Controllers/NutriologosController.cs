using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AutNutriYA.Models;

namespace AutNutriYA.Controllers
{
    [Authorize(Roles="Admin")]
    public class NutriologosController : Controller
    {
        private readonly UserManager<IdentityUser> _uManager;
        private readonly RoleManager<IdentityRole> _rManager;

        public NutriologosController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
           
            this._rManager = roleManager;
            this._uManager = userManager;
        }
        // GET: Nutriologo
        NutriologosRepository repo = new NutriologosRepository();
        public ActionResult Index()
        {
            List<Nutriologo> model = repo.LeerNutriologos().Result;
            return View(model);
            
        }

        // GET: Nutriologo/Details/5
        public ActionResult Details(string correo, string nombre)
        {
            var model = repo.LeerNutriologo(correo,nombre).Result;
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
        public ActionResult Create(Nutriologo model, string contrasena)
        {
            try
            {
                var resultado = repo.CrearNutriologo(model,_uManager, _rManager, contrasena);
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
            var model = repo.LeerNutriologo(correo,nombre).Result;
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
            var newNutriologo = repo.LeerNutriologo(model.Correo,model.Nombre).Result;

            if(model == null){
                return NotFound();
            }

            try
            {
                // TODO: Add update logic here

                newNutriologo.Pacientes = model.Pacientes;
                newNutriologo.Telefono = model.Telefono;
                newNutriologo.Direccion = model.Direccion;

                var resultado = repo.ActualizarNutriologo(newNutriologo).Result;
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
            
            var model = repo.LeerNutriologo(correo,nombre).Result;
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
                var newNutriologo = repo.LeerNutriologo(Correo,Nombre).Result;

                if(newNutriologo == null){
                return NotFound();
                }
                var resultado = repo.BorrarNutriologo(newNutriologo).Result;
                return RedirectToAction(nameof(Index));
            /* }
            catch
            {
                return View();
            }*/
        }
    }
}