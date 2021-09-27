using System;
using System.Collections.Generic;

#nullable disable

namespace ControlPostgres.Contexto.Entities
{
    public partial class TbRole
    {
        public TbRole()
        {
            TbEmpleados = new HashSet<TbEmpleado>();
        }

        public int RolId { get; set; }
        public string RolNombre { get; set; }
        public string RolDescripcion { get; set; }

        public virtual ICollection<TbEmpleado> TbEmpleados { get; set; }
    }
}
