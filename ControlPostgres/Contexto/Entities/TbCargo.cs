using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ControlPostgres.Contexto.Entities
{
    public partial class TbCargo
    {
        public TbCargo()
        {
            TbEmpleados = new HashSet<TbEmpleado>();
            TbSolicitudes = new HashSet<TbSolicitude>();
        }

        public int CargoId { get; set; }
        public int DeptoId { get; set; }
        [Display(Name = "Cargo que ocupa")]
        public string CargoNombre { get; set; }
        public string CargoDescripcion { get; set; }

        public virtual TbDepartamento Depto { get; set; }
        public virtual ICollection<TbEmpleado> TbEmpleados { get; set; }
        public virtual ICollection<TbSolicitude> TbSolicitudes { get; set; }
    }
}
