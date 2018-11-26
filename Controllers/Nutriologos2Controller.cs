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
    public class Nutriologos2Controller : ControllerBase
    {
        NutriologosRepository repo = new NutriologosRepository();
        // GET: api/Nutriologos2
        [HttpGet("GetNutriologos")]
        public ActionResult<List<Nutriologo>> Get()
        {
            var model = repo.LeerNutriologos();
            System.Threading.Thread.Sleep(1000);
            if(model == null)
                return NotFound();
            return model;
            
        }

        // GET: api/Nutriologos2/5
        [HttpGet("GetNutriologos/{RK}")]
        public ActionResult<Nutriologo> Get(string RK)
        {
            System.Threading.Thread.Sleep(1000);
            var model = repo.LeerNutriologo(RK);
            System.Threading.Thread.Sleep(1000);
            if(model == null){
                return NotFound();
            }
            return model;
        }
    }
}
