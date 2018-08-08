using GeekBurger.Productions.Repository;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace GeekBurger.Productions.Extension
{
    public static class ProductionContextExtensions
    {
        public static void Seed(this ProductionsContext context)
        {
            context.ProductionAreas.RemoveRange(context.ProductionAreas);
            context.Stores.RemoveRange(context.Stores);
            context.SaveChanges();

            var productionTxt = File.ReadAllText("production.json");
            var production = JsonConvert.DeserializeObject<List<Model.ProductionArea>>(productionTxt);

            var storeTxt = File.ReadAllText("store.json");
            var store = JsonConvert.DeserializeObject<List<Model.Store>>(storeTxt);
             
            context.Stores.AddRange(store);
            context.ProductionAreas.AddRange(production);

            context.SaveChanges();
        }
    }
}
