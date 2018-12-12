using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AutNutriYA.Controllers
{
    [Authorize(Roles="Nutriologo")]
    public class PacientesController : Controller
    {   
        
        //private IPacientesRepository _repo;
        private readonly UserManager<IdentityUser> _uManager;
        private readonly RoleManager<IdentityRole> _rManager;

        public PacientesController(/* IPacientesRepository repo, */RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            //this._repo = repo;
            this._rManager = roleManager;
            this._uManager = userManager;
        }
        PacientesRepository repo = new PacientesRepository();
        //public PacientesController(PacientesRepository pacientesRepo){
        //    repo = pacientesRepo;
        //}
        // GET: Pacientes
        public ActionResult Index()
        {
            var partitionKey = _uManager.GetUserName(HttpContext.User);
            List<Paciente> model = repo.LeerPacientes(partitionKey).Result;
            return View(model);
        }

        // GET: Pacientes/Details/5
        public ActionResult Details(string PK, string RK)
        {
            var model = repo.LeerPorPKRK(PK,RK).Result;
            if(model == null){
                return NotFound();
            }
            return View(model);
        }

        // GET: Pacientes/Create
        public ActionResult Create()
        {
            var model = new Paciente();
            return View(model);
        }

        // POST: Pacientes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Paciente model, string contrasena)
        {
            try
            {
                var resultado = repo.CrearPaciente(model, _uManager, _rManager, contrasena);
                //repo.AgregarPacienteANutriologo(model);
                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return View();
            }
        }

        // GET: Pacientes/Edit/5
        public ActionResult Edit(string PK, string RK)
        {   
            var model = repo.LeerPorPKRK(PK, RK).Result;
            if(model == null){
                return NotFound();
            }

            return View(model);
        }

        // POST: Pacientes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Paciente model)
        {   


            if(model == null){
                return NotFound();
            }

            var pack = repo.LeerPorPKRK(model.CorreoNut, model.Correo).Result;

            try
            {
                pack.NombreNut = model.NombreNut;
                pack.NombrePac = model.NombrePac;
                pack.Correo = model.Correo;
                pack.Edad = model.Edad; 
                pack.Altura = model.Altura;
                pack.Peso = model.Peso;
                pack.IMC = model.IMC;
                pack.Alergias = model.Alergias;
                pack.CorreoNut = model.CorreoNut;

                var resultado = repo.ActualizarPaciente(pack).Result;
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Pacientes/Delete/5
        public ActionResult Delete(string PK, string RK)
        {   
            var model = repo.LeerPorPKRK(PK, RK).Result;

            if(model == null){
                return NotFound();
            }

            return View(model);
        }

        // POST: Pacientes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string NombreNut, string Correo, int Edad)
        {   
            var pacienteN = repo.LeerPorPKRK(NombreNut,Correo).Result;
            if(pacienteN == null){
                return NotFound();
            }
                
                repo.BorrarPaciente(NombreNut,Correo);
                System.Threading.Thread.Sleep(800);
                return RedirectToAction(nameof(Index));
            
           
        }
    }
}