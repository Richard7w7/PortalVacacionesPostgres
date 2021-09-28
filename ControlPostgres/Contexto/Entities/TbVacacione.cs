using System;
using System.Collections.Generic;

#nullable disable

namespace ControlPostgres.Contexto.Entities
{
    public partial class TbVacacione
    {
        public TbVacacione()
        {
            TbEmpleados = new HashSet<TbEmpleado>();
            TbSolicitudes = new HashSet<TbSolicitude>();
        }

        public int VacacionesId { get; set; }
        public string VacacionesEstado { get; set; }
        public int VacacionesTiempo { get; set; }

        public virtual ICollection<TbEmpleado> TbEmpleados { get; set; }
        public virtual ICollection<TbSolicitude> TbSolicitudes { get; set; }
    }
}
