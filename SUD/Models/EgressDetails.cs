using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SUD.Models
{
    public class EgressDetails
    {

        public int EgressDetailsId { get; set; }

        public int EgressId { get; set; }

        public int ProductId { get; set; }

        public string Description { get; set; }

        public float Quantity { get; set; }

        public int KardexId { get; set; }

        public virtual Egress Egress { get; set; }

        public virtual Product Product { get; set; }

        public virtual Kardex Kardex { get; set; }


    }
}