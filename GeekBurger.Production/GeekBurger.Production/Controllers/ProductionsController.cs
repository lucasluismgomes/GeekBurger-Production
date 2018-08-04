using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeekBurger.Production.Contract.Model;
using Microsoft.AspNetCore.Mvc;

namespace GeekBurger.Production.Controllers
{
    [Route("api/productions"), Produces("application/json")]
    public class ProductionsController : Controller
    {
        List<ProductionArea> ProductionAreas = new List<ProductionArea>();

        public ProductionsController()
        {
        }

        [HttpGet("areas")]
        public IActionResult GetAreas()
        {
            return Ok(ProductionAreas);
        }

        [HttpGet("areas/{productionid}")]
        public IActionResult GetAreaByProductionId(Guid productionId)
        {
            var productionArea = ProductionAreas
                .FirstOrDefault(x => x.ProductionId == productionId);

            if (productionArea == null)
            {
                return NotFound();
            }

            return Ok(productionArea);
        }
    }
}