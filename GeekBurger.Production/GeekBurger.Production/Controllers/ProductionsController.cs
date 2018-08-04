﻿using System;
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
            ProductionAreas.AddRange(
                new List<ProductionArea>
                {
                    new ProductionArea
                    {
                        ProductionId = new Guid("90f64d70-52bd-4039-bd26-454d94aff2e7"),
                        Restrictions = new List<string>(){ "soy","dairy","gluten","peanut" },
                        On = false
                    },
                    new ProductionArea
                    {
                        ProductionId = new Guid("6fa5817f-14db-4b2e-8591-490eb32c1495"),
                        Restrictions = new List<string>(){ "dairy", "peanut" },
                        On = false
                    }
                }
            );
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