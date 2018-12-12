using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutNutriYA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Dietas2Controller : ControllerBase
    {
        DietasRepo repo = new DietasRepo();
        // GET: api/DietasApi
        /* Asi se Accede
           api/Dietas2/GetDietas"
        */
        [HttpGet("GetDietas")]
        public ActionResult<List<Dieta>> Get()
        {
            var model = repo.LeerDieta().Result;
            if(model == null)
                return NotFound();
            return model;
            
        }

        // GET: api/DietasApi/5
        /*
            Para Buscar 
            api/Dietas2/GetDietas/PK/RK
         */
        [HttpGet("GetDietas/{PK}/{RK}")]
        public ActionResult<Dieta> Get(string PK, string RK)
        {
            var model = repo.LeerPorPKRK(PK, RK).Result;
            if(model == null){
                return NotFound();
            }
            return model;
        }

        [HttpGet("GetDietas/{PK}")]
        public ActionResult<List<Dieta>> Get(string PK)
        {
            var model = repo.LeerDieta(PK).Result;
            if(model == null){
                return NotFound();
            }
            return model;
        }

        // POST: api/DietasApi
        /* 
        usas este link
        api/Dietas2/PostDietas        
        Esto es lo que se manda con formato JSON*/

        [HttpPost("PostDietas")]
        public ActionResult<bool> Post(Dieta dieta)
        {
            return repo.CrearDieta(dieta);
        }


        // PUT: api/DietasApi/5
        /*
        api/Dietas2/PutDietas
        Le tienes que mandar todo el modelo con lo que le quieras cambiar con formato JSON
        
        */
        [HttpPut("PutDietas")]
        public ActionResult<bool> Put(Dieta model){
            
            if(model == null){
                return NotFound();
            }

            var resultado = repo.ActualizarDieta(model).Result;


            return resultado;
            
        }


        // DELETE: api/ApiWithActions/5
        /*
            Solo tienes que enviar la request de Delete a 
            api/Dietas2/DeleteDietas/PK/RK

         */
        [HttpDelete("DeleteDietas/{PK}/{RK}")]
        public ActionResult<bool> Delete(string PK, string RK)
        {
            var p = repo.LeerPorPKRK(PK, RK).Result;
            if(p == null){
                return NotFound();
            }
            var resultado = repo.BorrarDieta(p).Result;


            return resultado;
            
        }
    }
}
