using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControlPostgres.Contexto.Entities
{
    public class TbContraseña
    {
        [Required(ErrorMessage = "Ingrese su fecha de nacimiento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime fechanacimiento { get; set; }
        [Required(ErrorMessage = "Ingrese su numero de telefono")]
        public string telefono { get; set; }
        [Required(ErrorMessage = "Ingrese su codigo de empleado")]
        public string codigoempleado { get; set; }

    }
}
