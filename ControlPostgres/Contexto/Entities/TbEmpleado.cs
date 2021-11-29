using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ControlPostgres.Contexto.Entities
{
    public partial class TbEmpleado
    {
        public TbEmpleado()
        {
            TbSolicitudes = new HashSet<TbSolicitude>();
        }

        public int EmpleadoId { get; set; }
        
        [Required(ErrorMessage = "Seleccione un cargo")]
        public int CargoId { get; set; }
        
        [Required(ErrorMessage = "Seleccione un departamento")]
        public int DeptoId { get; set; }
        
        [Required(ErrorMessage = "Seleccione un tiempo laborando")]
        public int VacacionesId { get; set; }
        
        [Display(Name = "Codigo del Empleado")]
        [MaxLength(4, ErrorMessage = "el codigo de empleado solo debe contener 4 digitos")]
        [RegularExpression("([0-9]*)", ErrorMessage = "el codigo solo debe contener numeros del 0 al 9")]
        [Required(ErrorMessage = "Ingrese su codigo de empleado")]
        public string EmpleadoCodigo { get; set; }
        
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{6,}$", ErrorMessage = "la contraseña debe tener mínimo seis caracteres, al menos una letra mayúscula, una letra minúscula y un número")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        [Required(ErrorMessage = "Ingrese su contraseña")]
        public string EmpleadoContraseña { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        [Display(Name = "Primer Nombre")]
        public string EmpleadoNombre1 { get; set; }
        
        [Display(Name = "Segundo Nombre")]
        public string EmpleadoNombre2 { get; set; }
        
        [Required(ErrorMessage = "Campo obligatorio")]
        [Display(Name = "Primer Apellido")]
        public string EmpleadoApellido1 { get; set; }
        
        [Display(Name = "Segundo Apellido")]
        public string EmpleadoApellido2 { get; set; }
        
        [Display(Name = "Apellido de Casada")]
        public string ApellidoCasada { get; set; }
        
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Seleccione su fecha de nacimiento")]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime FechaNacimiento { get; set; }
        
        [Required(ErrorMessage = "Ingrese su numero de telefono")]
        [MinLength(8, ErrorMessage = "El numero debe contener 8 digitos")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "el numero de telefono solo debe contener numeros del 0 al 9")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Numero de Telefono")]
        public string EmpleadoTelefono { get; set; }
       
        [Display(Name = "Direccion de Residencia")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public string EmpleadoDireccion { get; set; }
        
        public int EmpleadoEstado { get; set; }
        
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de ingreso a la Municipalidad")]
        [Required(ErrorMessage = "Seleccione su fecha de nacimiento")]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaIngreso { get; set; }
        
        public int? EmpleadoPermiso { get; set; }
        
        public DateTime? EmpladoUltimavacafin { get; set; }
        
        public DateTime? EmpleadoUltimavacainicio { get; set; }
        
        [Display(Name = "Cantidad de dias")]
        public int? EmpDiasvacaciones { get; set; }


        public virtual TbCargo Cargo { get; set; }
        public virtual TbDepartamento Depto { get; set; }
        public virtual TbVacacione Vacaciones { get; set; }
        public virtual ICollection<TbSolicitude> TbSolicitudes { get; set; }
    }
}
