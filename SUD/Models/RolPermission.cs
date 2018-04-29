using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SUD.Models
{
    [Table("tbl_RolsPermission")]
    public class RolPermission
    {
        [Key]
        public int PermissionId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Rol:")]
        public int RolId { get; set; }

        [Display(Name = "¿Puede ver?")]
        public bool CanSee { get; set; }

        [Display(Name = "¿Puede modificar?")]
        public bool CanModify { get; set; }

        [Display(Name = "¿Puede borrar?")]
        public bool CanErase { get; set; }

        public virtual Rol Rol { get; set; }
    }
}