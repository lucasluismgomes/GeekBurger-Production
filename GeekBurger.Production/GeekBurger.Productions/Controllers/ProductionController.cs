using AutoMapper;
using GeekBurger.Productions.Contract;
using GeekBurger.Productions.Helper;
using GeekBurger.Productions.Model;
using GeekBurger.Productions.Repository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GeekBurger.Productions.Controllers
{
    [Route("api/production"), Produces("application/json")]
    public class ProductionController : Controller
    {
        private readonly IProductionAreaRepository _productionAreaRepository;
        private readonly IMapper _mapper;

        public ProductionController(IProductionAreaRepository productionAreaRepository, IStoreRepository storeRepository, IMapper mapper)
        {
            _productionAreaRepository = productionAreaRepository;
            _mapper = mapper;
        }

        [HttpGet("areas", Name = "GetProductionArea")]
        public IActionResult GetProductionArea(Guid id)
        {
            var productionArea = _productionAreaRepository.GetProductionAreaById(id);

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

            return CreatedAtRoute("GetProductionArea",
                new { id = productionAreaToGet.ProductionAreaId },
                productionAreaToGet);
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateProductionArea(Guid id, [FromBody] JsonPatchDocument<ProductionAreaToUpsert> productionAreaPatch)
        {
            if (productionAreaPatch == null || id == null || id == Guid.Empty)
                return BadRequest();

            ProductionArea productionArea = _productionAreaRepository.GetProductionAreaById(id);

            if (productionArea == null)
            {
                return NotFound();
            }

            var productionAreaToUpdate = _mapper.Map<ProductionAreaToUpsert>(productionArea);
            productionAreaPatch.ApplyTo<ProductionAreaToUpsert>(productionAreaToUpdate, ModelState);

            productionArea = _mapper.Map(productionAreaToUpdate, productionArea);

            if (productionArea.StoreId == Guid.Empty)
                return new UnprocessableEntityResult(ModelState);

            _productionAreaRepository.Update(productionArea);
            _productionAreaRepository.Save();

            var productionAreaToGet = _mapper.Map<ProductionAreaToGet>(productionArea);

            return CreatedAtRoute("GetProductionArea",
                new { id = productionAreaToGet.ProductionAreaId },
                productionAreaToGet);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var productionArea = _productionAreaRepository.GetProductionAreaById(id);

            _productionAreaRepository.Remove(productionArea);
            _productionAreaRepository.Save();

            return NoContent();
        }
    }
}