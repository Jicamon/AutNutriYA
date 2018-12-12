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
    public class Recordatorio2Controller : ControllerBase
    {
        RecordatorioRepo repo = new RecordatorioRepo();
        // GET: api/Recordatorio2
        [HttpGet("GetRecordatorios")]
        public ActionResult<List<Recordatorio>> Get()
        {
            var model = repo.LeerRecordatorio().Result;
            if(model == null)
                return NotFound();
            return model;
        }

        // GET: api/Recordatorio2/5
        [HttpGet("GetRecordatorios/{PK}/{RK}")]
        public ActionResult<Recordatorio> Get(string PK, string RK)
        {
            var model = repo.LeerPorPKRK(PK, RK).Result;
            if(model == null){
                return NotFound();
            }
            return model;
        }

        // POST: api/Recordatorio2
        [HttpPost("PostRecordatorios")]
        public ActionResult<bool> Post(Recordatorio recordatorio)
        {
            var exist=repo.existeAsync(recordatorio.Nombre,recordatorio.Dia).Result;
            if (exist)
            {
                var resultado = repo.ActualizarRecordatorio(recordatorio).Result;
                return resultado;
            }
            return repo.CrearRecordatorio(recordatorio);
            
        }

        // PUT: api/Recordatorio2/5
        /*[HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }*/

        // DELETE: api/ApiWithActions/5
        [HttpDelete("DeleteRecordatorios/{PK}/{RK}")]
        public ActionResult<bool> Delete(string PK, string RK)
        {
            var p = repo.LeerPorPKRK(PK, RK).Result;
            if(p == null){
                return NotFound();
            }
            var resultado = repo.BorrarRecordatorio(p).Result;
            return true;
        }
    }
}
