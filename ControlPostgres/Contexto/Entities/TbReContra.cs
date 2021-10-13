using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControlPostgres.Contexto.Entities
{
    public class TbReContra
    {
        [Required(ErrorMessage ="Ingrese contraseña nueva")]
        public string Contra { get; set; }
        [Required(ErrorMessage ="Repita su contraseña")]
        public string RepetirContra { get; set; }
    }
}
