using System;

namespace GeekBurger.Productions.Contract
{
    public class ProductionAreaChangedMessage
    {
        public Guid ProductionId { get; set; }
        public ProductionAreaToGet ProductionArea { get; set; }
    }
}
