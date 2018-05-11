using System;
using System.Collections.Generic;

namespace SUD.Models
{
    public class Kardex
    {

        public int KardexId { get; set; }

        public int CellarId { get; set; }

        public int ProductId { get; set; }

        public DateTime Date { get; set; }

        public string Document { get; set; }

        public float Entry { get; set; }

        public float Egress { get; set; }

        public float Balance { get; set; }

        public decimal LastCost { get; set; }

        public decimal AverageCost { get; set; }

        public virtual ICollection<EgressDetails> EgressDetails { get; set; }

        public virtual ICollection<InventoryDetail> InventoryDetails { get; set; }
    }
}