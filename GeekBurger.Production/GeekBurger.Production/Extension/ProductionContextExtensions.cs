using GeekBurger.Production.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GeekBurger.Production.Extension
{
    public static class ProductionContextExtensions
    {
        public static void Seed(this ProductionContext context)
        {

            context.Production.RemoveRange(context.Production);

            context.SaveChanges();

            var productionTxt = File.ReadAllText("production.json");
            var production = JsonConvert.DeserializeObject<List<Production>>(productionTxt);
            context.Production.AddRange(production);

            context.SaveChanges();
        }
    }
}
