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
            var model = repo.LeerRecordatorio();
            System.Threading.Thread.Sleep(1000);
            if(model == null)
                return NotFound();
            return model;
        }

        // GET: api/Recordatorio2/5
        [HttpGet("GetRecordatorios/{PK}/{RK}")]
        public ActionResult<Recordatorio> Get(string PK, string RK)
        {
            System.Threading.Thread.Sleep(1000);
            var model = repo.LeerPorPKRK(PK, RK);
            System.Threading.Thread.Sleep(1000);
            if(model == null){
                return NotFound();
            }
            return model;
        }

        // POST: api/Recordatorio2
        [HttpPost("PostRecordatorios")]
        public ActionResult<bool> Post(Recordatorio recordatorio)
        {
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
            System.Threading.Thread.Sleep(1000);
            var p = repo.LeerPorPKRK(PK, RK);
            if(p == null){
                return NotFound();
            }
            var resultado = repo.BorrarRecordatorio(p);
            System.Threading.Thread.Sleep(1000);


            return true;
        }
    }
}
