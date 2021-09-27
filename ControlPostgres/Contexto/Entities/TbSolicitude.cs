﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ControlPostgres.Contexto.Entities
{
    public partial class TbSolicitude
    {
        public int SolicitudId { get; set; }
        public int EmpleadoId { get; set; }
        [Display(Name = "Cargo que desempeña")]
        public int CargoId { get; set; }
        [Display(Name = "Departamento al que Pertenece")]
        public int DeptoId { get; set; }
        [Display(Name = "Tiempo Laborando")]
        public int VacacionesId { get; set; }
        [Display(Name = "Estado de la Solicitud")]
        public int EstadosId { get; set; }
        [Display(Name = "Fecha de la Solicitud")]
        public DateTime SolicitudFecha { get; set; }
        [Display(Name = "Detalles de la Solicitud")]
        public string DetallesSolicitud { get; set; }
        [Display(Name = "Fechas Seleccionadas")]
        public string FechasSeleccionadas { get; set; }
        [Display(Name = "Cantidad de Dias")]
        public int CantidadDias { get; set; }
        public string Comentario { get; set; }

        public virtual TbCargo Cargo { get; set; }
        public virtual TbDepartamento Depto { get; set; }
        public virtual TbEmpleado Empleado { get; set; }
        public virtual TbEstadosolicitude Estados { get; set; }
        public virtual TbVacacione Vacaciones { get; set; }
    }
}
