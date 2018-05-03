using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SUD.Models
{
    public class Egress
    {

        public int EgressId { get; set; }

        public DateTime Date { get; set; }

        public int ConceptId { get; set; }

        public int CellarId { get; set; }

        public virtual Concept Concept { get; set; }

        public virtual Cellar Cellar { get; set; }

        public virtual ICollection<EgressDetails> EgressDetails { get; set; }


    }
}