using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ControlPostgres.Contexto.Entities
{
    public partial class TbEstadosolicitude
    {
        public TbEstadosolicitude()
        {
            TbSolicitudes = new HashSet<TbSolicitude>();
        }

        public int EstadosId { get; set; }
        [Display(Name = "Estado de la Solicitud")]
        public string EstadosNombre { get; set; }

        public virtual ICollection<TbSolicitude> TbSolicitudes { get; set; }
    }
}
