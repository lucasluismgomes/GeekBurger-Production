using System;
using System.Collections.Generic;

namespace GeekBurger.Production.Contract.Model
{
    public class ProductionAreaToGet
    {
        public Guid StoreId { get; set; }
        public Guid ProductionAreaId { get; set; }
        public List<string> Restrictions { get; set; }
        public bool On { get; set; }
    }
}
