using System;

namespace GeekBurger.Production.Contract.Model
{
    public class ProductionArea
    {
        public Guid ProductionId { get; set; }
        public string Type { get; set; }
        public bool On { get; set; }
    }
}
