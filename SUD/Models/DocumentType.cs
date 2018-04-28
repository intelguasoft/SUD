using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SUD.Models
{
    public class DocumentType
    {
        [Key]
        public int DocumentTypeId { get; set; }

        [Required(ErrorMessage ="*Ingrese el nombre del tipo de documento")]
        [Display(Name ="Tipo de Documento", Description ="Nombre del documento")]
        [MaxLength(50, ErrorMessage ="*El nombre no puede llevar mas de 50 Caracteres")]
        public string Description { get; set; }

        public virtual ICollection<DocumentType> DocumentTypes { get; set; }

    }
}