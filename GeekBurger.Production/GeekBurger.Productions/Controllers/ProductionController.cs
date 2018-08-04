using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GeekBurger.Productions.Contract;
using GeekBurger.Productions.Helper;
using GeekBurger.Productions.Model;
using GeekBurger.Productions.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeekBurger.Productions.Controllers
{
    [Route("api/production"), Produces("application/json")]
    public class ProductionController : Controller
    {
        private IProductionAreaRepository _productionAreaRepository;
        private IMapper _mapper;

        public ProductionController(IProductionAreaRepository productionAreaRepository, IStoreRepository storeRepository, IMapper mapper)
        {
            _productionAreaRepository = productionAreaRepository;
            _mapper = mapper;
        }

        [HttpGet("areas", Name= "GetProductionArea")]
        public IActionResult GetProductionArea(Guid id)
        {
            var productionArea = _productionAreaRepository.GetProductionById(id);

            if (productionArea == null)
            {
                return NotFound();
            }

            var productionAreaToGet = _mapper.Map<ProductionAreaToGet>(productionArea);

            return Ok(productionAreaToGet);
        }

        [HttpPost()]
        public IActionResult AddProductionArea([FromBody] ProductionAreaToUpsert productionAreaToAdd)
        {
            if (productionAreaToAdd == null)
                return BadRequest();

            var productionArea = _mapper.Map<ProductionArea>(productionAreaToAdd);

            if (productionArea.StoreId == Guid.Empty)
                return new UnprocessableEntityResult(ModelState);

            _productionAreaRepository.Add(productionArea);
            _productionAreaRepository.Save();

            var productionAreaToGet = _mapper.Map<ProductionAreaToGet>(productionArea);

            return CreatedAtRoute("GetProduct",
                new { id = productionAreaToGet.ProductionAreaId },
                productionAreaToGet);
        }
    }
}