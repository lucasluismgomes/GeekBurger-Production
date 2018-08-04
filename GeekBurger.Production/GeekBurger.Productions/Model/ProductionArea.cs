using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace GeekBurger.Productions.Model
{
    public class ProductionArea
    {
        [Key]
        public Guid ProductionAreaId { get; set; }

        [NotMapped]
        public ICollection<string> Restrictions { get; set; }

        public string RestrictionsList
        {
            get { return string.Join(",", Restrictions); }
            set { Restrictions = value.Split(',').ToList(); }
        }

        public bool On { get; set; }

        public Store Store { get; set; }

        [ForeignKey("StoreId")]
        public Guid StoreId { get; set; }
    }
}
