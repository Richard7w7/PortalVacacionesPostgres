using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlPostgres.Contexto.Entities
{
    public class Tb_Modelos
    {
        public virtual DbSet<TbCargo> TbCargos { get; set; }
        public virtual DbSet<TbDepartamento> TbDepartamentos { get; set; }
        public virtual DbSet<TbEmpleado> TbEmpleados { get; set; }
        public virtual DbSet<TbEstadosolicitude> TbEstadosolicitudes { get; set; }
        public virtual DbSet<TbSolicitude> TbSolicitudes { get; set; }
        public virtual DbSet<TbVacacione> TbVacaciones { get; set; }
    }
}
