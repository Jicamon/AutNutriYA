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
        PlatillosRepo platillos = new PlatillosRepo();
        IngredientesRepository Tipo = new IngredientesRepository();
        PacientesRepository alergias = new PacientesRepository();
        public ActionResult Index(string correo)
        {
            ViewData["Correo"] = correo;
            List<Dieta> model = repo.LeerDieta(correo).Result;
            return View(model);
        }

        // GET: Dietas/Details/5
        public ActionResult Details(string PK, string RK)
        {
            
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
            GestionPlatillos(correo);         

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
            GestionPlatillos(model.Nombre);
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
                var resultado = repo.BorrarDieta(model).Result;
                return RedirectToAction("Index", new { correo = model.Nombre});
            }
            catch
            {
                return View();
            }
        }

        public void GestionPlatillos(string correo){
            List<Platillo> Platillos  = platillos.LeerPlatillo().Result;
            Paciente Paciente  = alergias.LeerPaciente(correo).Result;
            List<Platillo> Desayunos  = new List<Platillo>();
            List<Platillo> Comidas    = new List<Platillo>();
            List<Platillo> Cenas      = new List<Platillo>();
            List<Platillo> Colaciones = new List<Platillo>();
            List<Platillo> Bebidas    = new List<Platillo>();
            //int i=0;
            bool alergico=false;
            bool alergia=false;
            //string aux;
            string[] alr=null;
            if (Paciente.Alergias != null)
            {
                
                alr = Paciente.Alergias.Split(';');
                alergico = true;
                
            }
            foreach (var item in Platillos)
            {
                if (alergico)
                {
                    var ings = item.ingredientes.Split(',');
                    
                    foreach (var i in ings)
                    {
                        var ing = Tipo.LeerIngrediente(i).Result;
                        string tipo = ing.Tipo;
                        if(alr.Contains(i) || alr.Contains(tipo)){
                            alergia=true;
                            break;
                        }
                    }
                    if (alergia)
                    {
                        alergia = false;
                        continue;
                    }
                    
                }
                
                if (item.tipo=="Desayuno"){
                    Desayunos.Add(item);
                }else if (item.tipo=="Comida"){
                    Comidas.Add(item);
                }else if (item.tipo=="Cena"){
                    Cenas.Add(item);
                }else if (item.tipo=="Colacion"){
                    Colaciones.Add(item);
                }else if (item.tipo=="Bebida"){
                    Bebidas.Add(item);
                }else{
                    Desayunos.Add(item);
                    Comidas.Add(item);
                    Cenas.Add(item);
                }
            }

            ViewBag.Desayunos = Desayunos;
            ViewBag.Comidas = Comidas;
            ViewBag.Cenas = Cenas;
            ViewBag.Colaciones = Colaciones;
            ViewBag.Bebidas = Bebidas;
        }
    }
}