using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GeekBurger.Production.Model
{
    public class Production
    {
        [Key]
        public Guid ProductionId { get; set; }
        public List<string> Restrictions { get; set; }
        public bool On { get; set; }

        public Store Store { get; set; }

        [ForeignKey("StoreId")]
        public Guid IdStore { get; set; }
    }
}
