using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControlPostgres.Contexto.Entities
{
    public class TbReContra
    {
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{6,}$", ErrorMessage = "la contraseña debe tener mínimo seis caracteres, al menos una letra mayúscula, una letra minúscula y un número")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        [Required(ErrorMessage = "Ingrese contraseña nueva")]
        public string Contra { get; set; }

        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{6,}$", ErrorMessage = "la contraseña debe tener mínimo seis caracteres, al menos una letra mayúscula, una letra minúscula y un número")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        [Required(ErrorMessage = "Repita su contraseña")]
        public string RepetirContra { get; set; }
    }
}
