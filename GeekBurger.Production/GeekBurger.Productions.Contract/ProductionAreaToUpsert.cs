using System.Collections.Generic;

namespace GeekBurger.Productions.Contract
{
    public class ProductionAreaToUpsert
    {
        public string StoreName { get; set; }
        public List<string> Restrictions { get; set; }
        public bool On { get; set; }
    }
}
