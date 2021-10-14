using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControlPostgres.Contexto.Entities
{
    public class TbActualizaDatos
    {
        [Required(ErrorMessage = "Ingrese su numero de telefono")]
        [MinLength(8, ErrorMessage = "El numero debe contener 8 digitos")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Numero de Telefono")]
        public string EmpleadoTelefono { get; set; }

        [Display(Name = "Direccion de Residencia")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public string EmpleadoDireccion { get; set; }
    }
}
