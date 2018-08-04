using System;

namespace GeekBurger.Production.Contract.Model
{
    public class ProductionAreaChangedMessage
    {
        public Guid ProductionId { get; set; }
        public ProductionAreaToGet Production { get; set; }
    }
}
