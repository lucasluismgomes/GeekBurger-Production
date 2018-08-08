using AutoMapper;
using GeekBurger.Productions.Contract;
using GeekBurger.Productions.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace GeekBurger.Productions.Controllers
{
    [Route("api/productions"), Produces("application/json")]
    public class ProductionsController : Controller
    {
        private readonly IProductionAreaRepository _productionsRepository;
        private readonly IMapper _mapper;

        public ProductionsController(IProductionAreaRepository productionsRepository, IMapper mapper)
        {
            _productionsRepository = productionsRepository;
            _mapper = mapper;
        }

        [HttpGet("areas")]
        public IActionResult GetAreasFromStoreName([FromQuery] string storeName)
        {
            var productionsAreas = _productionsRepository.GetProductionByStoreName(storeName).ToList();

            var productionsToGet = _mapper.Map<IEnumerable<ProductionAreaToGet>>(productionsAreas);

            return Ok(productionsToGet);
        }
    }
}