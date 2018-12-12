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
    public class Platillos2Controller : ControllerBase
    {
        PlatillosRepo repo = new PlatillosRepo();
        // GET: api/Platillos2
        /* Asi se Accede
           api/Platillos2/GetPlatillo"
        */
        [HttpGet("GetPlatillos")]
        public ActionResult<List<Platillo>> Get()
        {
            var model = repo.LeerPlatillo().Result;
            if(model == null)
                return NotFound();
            return model;
        }

        // GET: api/Platillos2/5
        /*
            Para Buscar 
            api/platillos2/GetPlatillos/PK/RK
            Ej. api/platillos2/GetPlatillos/Otros/Quesadilla
         */
        [HttpGet("GetPlatillos/{PK}/{RK}")]
        public ActionResult<Platillo> Get(string PK, string RK)
        {
            var model = repo.LeerPorPKRK(PK, RK).Result;
            if(model == null){
                return NotFound();
            }
            return model;
        }

        /* 
        usas este link
        api/Platillos2/PostPlatillos        
        Esto es lo que se manda con formato JSON
        {"tipo":"Desayuno","platillo":"Cereal con leche","ingredientes":"Leche, Cereal","calorias":400.0,"porcion":1.0}*/

        /* POST: api/Platillos2*/
        [HttpPost("PostPlatillos")]
        public ActionResult<bool> Post( Platillo Platillo)
        {
            return repo.CrearPlatillo(Platillo);
        }

        // PUT: api/Platillos2/5

        /*
        api/Platillos2/PutPlatillos
        Le tienes que mandar todo el modelo con lo que le quieras cambiar con formato JSON
        {"tipo":"Comida","platillo":"Caldo de Pollo","ingredientes":"Caldo, Pollo, Tenis, Zanahoria","calorias":120.0,"porcion":1.0}
        
        */
        [HttpPut("PutPlatillos")]
        public ActionResult<bool> Put(Platillo model){
            
            if(model == null){
                return NotFound();
            }

            var resultado = repo.ActualizarPlatillo(model).Result;


            return resultado;
            
        }

        // DELETE: api/ApiWithActions/5
        /*
            Solo tienes que enviar la request de Delete a 
            api/platillos2/DeletePlatillos/PK/RK
            ej. api/platillos2/comida/caldo de pollo

         */
        [HttpDelete("DeletePlatillos/{PK}/{RK}")]
        public ActionResult<bool> Delete(string PK, string RK)
        {
            var p = repo.LeerPorPKRK(PK, RK).Result;
            if(p == null){
                return NotFound();
            }
            var resultado = repo.BorrarPlatillo(p).Result;


            return true;
            
        }
    }
}