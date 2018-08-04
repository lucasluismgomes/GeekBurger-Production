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
        public static void Seed(this ProductionsContext context)
        {
            context.Production.RemoveRange(context.Production);
            context.Store.RemoveRange(context.Store);
            context.SaveChanges();

            var productionTxt = File.ReadAllText("production.json");
            var production = JsonConvert.DeserializeObject<List<Model.ProductionArea>>(productionTxt);

            var storeTxt = File.ReadAllText("store.json");
            var store = JsonConvert.DeserializeObject<List<Model.Store>>(storeTxt);
             
            context.Store.AddRange(store);
            context.Production.AddRange(production);

            context.SaveChanges();
        }
    }
}
