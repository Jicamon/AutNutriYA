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
    public class Ingredientes2Controller : ControllerBase
    {
        // GET: api/Ingredientes2
        IngredientesRepository repo = new IngredientesRepository();
        // GET: api/Ingredientes2
        [HttpGet("GetIngredientes")]
        public ActionResult<List<Ingrediente>> Get()
        {
            var model = repo.LeerIngrediente();
            System.Threading.Thread.Sleep(1000);
            if(model == null)
                return NotFound();
            return model;
            
        }

        // GET: api/Ingredientes2/5
        [HttpGet("GetIngredientes/{RK}")]
        public ActionResult<Ingrediente> Get(string RK)
        {
            System.Threading.Thread.Sleep(1000);
            var model = repo.LeerIngrediente(RK);
            System.Threading.Thread.Sleep(1000);
            if(model == null){
                return NotFound();
            }
            return model;
        }
    }
}
