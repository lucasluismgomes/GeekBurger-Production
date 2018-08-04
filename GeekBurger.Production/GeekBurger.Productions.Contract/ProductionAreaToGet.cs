using System;
using System.Collections.Generic;

namespace GeekBurger.Productions.Contract
{
    public class ProductionAreaToGet
    {
        public Guid StoreId { get; set; }
        public Guid ProductionAreaId { get; set; }
        public ICollection<string> Restrictions { get; set; }
        public bool On { get; set; }
    }
}
