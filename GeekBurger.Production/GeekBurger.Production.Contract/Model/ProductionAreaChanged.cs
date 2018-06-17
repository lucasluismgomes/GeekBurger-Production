using System;

namespace GeekBurger.Production.Contract.Model
{
    public class ProductionAreaChanged
    {
        public Guid ProductionId { get; set; }
        public bool On { get; set; }
    }
}
