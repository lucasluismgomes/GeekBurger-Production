using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeekBurger.Production.Contract.Model;
using GeekBurger.Production.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GeekBurger.Production.Controllers
{
    [Route("api/productions"), Produces("application/json")]
    public class ProductionsController : Controller
    {
        private readonly IProductionRepository _productionRepository;
        
        public ProductionsController(IProductionRepository productionRepository)
        {
            _productionRepository = productionRepository;
        }

        [HttpGet("areas")]
        public IActionResult GetAreas()
        {
            var productions = _productionRepository.ListProductions();

            return Ok(productions);
        }

        [HttpGet("areas/{productionid}")]
        public IActionResult GetAreaByProductionId(Guid productionId)
        {
            var productionArea = _productionRepository.GetProductionById(productionId);

            if (productionArea == null)
            {
                return NotFound();
            }

            return Ok(productionArea);
        }
    }
}