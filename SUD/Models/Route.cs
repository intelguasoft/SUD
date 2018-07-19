using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SUD.Models
{
    [Table("tbl_Routes")]

    public class Route
    {
        [Key]
        public int RouteId { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Numero de Ruta")]
        public string RouteNumber { get; set; }

        [Display(Name = "Tipo Documento")]
        public int AccountingDocumentId { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Territorio de Ruta")]
        public string Territory { get; set; }

        public virtual AccountingDocument AccountingDocument { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<UserSud> UserSuds { get; set; }

    }
}