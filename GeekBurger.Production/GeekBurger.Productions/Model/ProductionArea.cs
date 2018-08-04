using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GeekBurger.Production.Model
{
    public class ProductionArea
    {
        [Key]
        public Guid ProductionAreaId { get; set; }

        //public ICollection<string> Restrictions { get; set; } = new List<string>();
        public bool On { get; set; }

        public Store Store { get; set; }

        [ForeignKey("StoreId")]
        public Guid StoreId { get; set; }
    }
}
