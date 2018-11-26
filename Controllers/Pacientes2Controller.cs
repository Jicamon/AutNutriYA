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
    public class Pacientes2Controller : ControllerBase
    {
        PacientesRepository repo = new PacientesRepository();
        // GET: api/Pacientes2
        /* Asi se Accede
           api/Pacientes2/GetPacientes"
        */
        [HttpGet("GetPacientes")]
        public ActionResult<List<Paciente>> Get()
        {
            var model = repo.LeerPaciente();
            System.Threading.Thread.Sleep(1000);
            if(model == null)
                return NotFound();
            return model;
        }

        // GET: api/Pacientes2/5
        /*
            Para Buscar 
            api/Pacientes2/GetPacientes/PK/RK
            Ej. api/Pacientes2/GetPacientes/Otros/Quesadilla
         */
        [HttpGet("GetPacientes/{PK}/{RK}")]
        public ActionResult<Paciente> Get(string PK, string RK)
        {
            System.Threading.Thread.Sleep(1000);
            var model = repo.LeerPorPKRK(PK, RK);
            System.Threading.Thread.Sleep(1000);
            if(model == null){
                return NotFound();
            }
            return model;
        }

        /* 
        usas este link
        api/Pacientes2/PostPacientes        
        Esto es lo que se manda con formato JSON
        {"tipo":"Desayuno","Pacientes":"Cereal con leche","ingredientes":"Leche, Cereal","calorias":400.0,"porcion":1.0}*/

        /* POST: api/Pacientes2*/
        /* 
        [HttpPost("PostPacientes")]
        public ActionResult<bool> Post( Paciente Pacientes)
        {
            return repo.CrearPaciente(Pacientes);
        }
        */
        // PUT: api/Pacientes2/5

        /*
        api/Pacientes2/PutPacientes
        Le tienes que mandar todo el modelo con lo que le quieras cambiar con formato JSON
        {"tipo":"Comida","Pacientes":"Caldo de Pollo","ingredientes":"Caldo, Pollo, Tenis, Zanahoria","calorias":120.0,"porcion":1.0}
        
        */
        
        [HttpPut("PutPacientes")]
        public ActionResult<bool> Put(Paciente model){
            
            if(model == null){
                return NotFound();
            }

            var resultado = repo.ActualizarPaciente(model);;
            System.Threading.Thread.Sleep(1000);


            return resultado;
            
        }
        
        // DELETE: api/ApiWithActions/5
        /*
            Solo tienes que enviar la request de Delete a 
            api/Pacientes2/DeletePacientes/PK/RK
            ej. api/Pacientes2/comida/caldo de pollo

         */
         /*
        [HttpDelete("DeletePacientes/{PK}/{RK}")]
        public ActionResult<bool> Delete(string PK, string RK)
        {
            System.Threading.Thread.Sleep(1000);
            var p = repo.LeerPorPKRK(PK, RK);
            if(p == null){
                return NotFound();
            }
            var resultado = repo.BorrarPacientes(p);
            System.Threading.Thread.Sleep(1000);


            return true;
            
        } */
    }
}