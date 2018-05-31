using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SUD.Models
{
    [Table("tbl_Suppliers")]
    public class Supplier
    {

        public long SupplierId { get; set; }

        public string Tradename { get; set; }

        public int DocumentTypeId { get; set; }

        public string Document { get; set; }

        public string ContactFirstName { get; set; }

        public string ContactLastName { get; set; }

        public string Address { get; set; }

        public long Phone1 { get; set; }

        public long Phone2 { get; set; }

        public string Email { get; set; }

        public string Notes { get; set; }

        public virtual DocumentType DocumentType { get; set; }

        public virtual ICollection<Purchase> Purchases { get; set; }
    }
}