using System;
using System.Collections.Generic;

namespace GeekBurger.Production.Contract.Model
{
    public class ProductionArea
    {
        public Guid ProductionId { get; set; }
        public List<string> Restrictions { get; set; }
        public bool On { get; set; }
    }
}
