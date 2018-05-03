using System.Collections;
using System.Collections.Generic;

namespace SUD.Models
{
    public class Concept
    {

        public int ConceptId { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Egress> Egress { get; set; }
    }
}