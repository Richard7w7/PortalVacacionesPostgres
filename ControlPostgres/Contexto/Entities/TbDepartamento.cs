using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ControlPostgres.Contexto.Entities
{
    public partial class TbDepartamento
    {
        public TbDepartamento()
        {
            TbCargos = new HashSet<TbCargo>();
            TbEmpleados = new HashSet<TbEmpleado>();
            TbSolicitudes = new HashSet<TbSolicitude>();
        }

        public int DeptoId { get; set; }
        [Display(Name = "Departamento al que pertenece")]
        public string DeptoNombre { get; set; }
        public string DeptoDescripcion { get; set; }


        public virtual ICollection<TbCargo> TbCargos { get; set; }
        public virtual ICollection<TbEmpleado> TbEmpleados { get; set; }
        public virtual ICollection<TbSolicitude> TbSolicitudes { get; set; }
    }
}
