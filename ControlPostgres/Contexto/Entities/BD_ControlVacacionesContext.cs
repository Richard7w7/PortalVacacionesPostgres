using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ControlPostgres.Contexto.Entities
{
    public partial class BD_ControlVacacionesContext : DbContext
    {
        public BD_ControlVacacionesContext()
        {
        }

        public BD_ControlVacacionesContext(DbContextOptions<BD_ControlVacacionesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TbCargo> TbCargos { get; set; }
        public virtual DbSet<TbDepartamento> TbDepartamentos { get; set; }
        public virtual DbSet<TbEmpleado> TbEmpleados { get; set; }
        public virtual DbSet<TbEstadosolicitude> TbEstadosolicitudes { get; set; }
        public virtual DbSet<TbSolicitude> TbSolicitudes { get; set; }
        public virtual DbSet<TbVacacione> TbVacaciones { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Server=localhost;Database=BD_ControlVacaciones;Username=postgres;Password=Arg4815;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Spanish_Spain.1252");

            modelBuilder.Entity<TbCargo>(entity =>
            {
                entity.HasKey(e => e.CargoId)
                    .HasName("tb_cargos_pkey");

                entity.ToTable("tb_cargos", "muni_villanueva");

                entity.HasIndex(e => e.DeptoId, "IX_tb_cargos_depto_id");

                entity.Property(e => e.CargoId).HasColumnName("cargo_id");

                entity.Property(e => e.CargoDescripcion)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("cargo_descripcion");

                entity.Property(e => e.CargoNombre)
                    .IsRequired()
                    .HasMaxLength(75)
                    .HasColumnName("cargo_nombre");

                entity.Property(e => e.DeptoId).HasColumnName("depto_id");

                entity.HasOne(d => d.Depto)
                    .WithMany(p => p.TbCargos)
                    .HasForeignKey(d => d.DeptoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_depto_id_tb_departamentos");
            });

            modelBuilder.Entity<TbDepartamento>(entity =>
            {
                entity.HasKey(e => e.DeptoId)
                    .HasName("tb_departamentos_pkey");

                entity.ToTable("tb_departamentos", "muni_villanueva");

                entity.HasIndex(e => e.DeptoNombre, "tb_departamentos_depto_nombre_key")
                    .IsUnique();

                entity.Property(e => e.DeptoId).HasColumnName("depto_id");

                entity.Property(e => e.DeptoDescripcion)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("depto_descripcion");

                entity.Property(e => e.DeptoNombre)
                    .IsRequired()
                    .HasMaxLength(75)
                    .HasColumnName("depto_nombre");
            });

            modelBuilder.Entity<TbEmpleado>(entity =>
            {
                entity.HasKey(e => e.EmpleadoId)
                    .HasName("tb_empleados_pkey");

                entity.ToTable("tb_empleados", "muni_villanueva");

                entity.HasIndex(e => e.CargoId, "IX_tb_empleados_cargo_id");

                entity.HasIndex(e => e.DeptoId, "IX_tb_empleados_depto_id");

                entity.HasIndex(e => e.VacacionesId, "IX_tb_empleados_vacaciones_id");

                entity.HasIndex(e => e.EmpleadoCodigo, "tb_empleados_empleado_codigo_key")
                    .IsUnique();

                entity.HasIndex(e => e.EmpleadoTelefono, "tb_empleados_empleado_telefono_key")
                    .IsUnique();

                entity.Property(e => e.EmpleadoId).HasColumnName("empleado_id");

                entity.Property(e => e.ApellidoCasada)
                    .HasMaxLength(25)
                    .HasColumnName("apellido_casada");

                entity.Property(e => e.CargoId).HasColumnName("cargo_id");

                entity.Property(e => e.DeptoId).HasColumnName("depto_id");

                entity.Property(e => e.EmpDiasvacaciones).HasColumnName("emp_diasvacaciones");

                entity.Property(e => e.EmpladoUltimavacafin)
                    .HasColumnType("date")
                    .HasColumnName("emplado_ultimavacafin");

                entity.Property(e => e.EmpleadoApellido1)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("empleado_apellido1");

                entity.Property(e => e.EmpleadoApellido2)
                    .HasMaxLength(25)
                    .HasColumnName("empleado_apellido2");

                entity.Property(e => e.EmpleadoCodigo)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("empleado_codigo");

                entity.Property(e => e.EmpleadoContraseña)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("empleado_contraseña");

                entity.Property(e => e.EmpleadoDireccion)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("empleado_direccion");

                entity.Property(e => e.EmpleadoEstado).HasColumnName("empleado_estado");

                entity.Property(e => e.EmpleadoNombre1)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("empleado_nombre1");

                entity.Property(e => e.EmpleadoNombre2)
                    .HasMaxLength(25)
                    .HasColumnName("empleado_nombre2");

                entity.Property(e => e.EmpleadoPermiso).HasColumnName("empleado_permiso");

                entity.Property(e => e.EmpleadoTelefono)
                    .IsRequired()
                    .HasMaxLength(12)
                    .HasColumnName("empleado_telefono");

                entity.Property(e => e.EmpleadoUltimavacainicio)
                    .HasColumnType("date")
                    .HasColumnName("empleado_ultimavacainicio");

                entity.Property(e => e.FechaIngreso)
                    .HasColumnType("date")
                    .HasColumnName("fecha_ingreso");

                entity.Property(e => e.FechaNacimiento)
                    .HasColumnType("date")
                    .HasColumnName("fecha_nacimiento");

                entity.Property(e => e.VacacionesId).HasColumnName("vacaciones_id");

                entity.HasOne(d => d.Cargo)
                    .WithMany(p => p.TbEmpleados)
                    .HasForeignKey(d => d.CargoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_cargo_id_tb_emepleados_tb_cargos");

                entity.HasOne(d => d.Depto)
                    .WithMany(p => p.TbEmpleados)
                    .HasForeignKey(d => d.DeptoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_depto_id_tb_empleados_tb_departamentos");

                entity.HasOne(d => d.Vacaciones)
                    .WithMany(p => p.TbEmpleados)
                    .HasForeignKey(d => d.VacacionesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_vacaciones_id_tb_empleados_tb_vacaciones");
            });

            modelBuilder.Entity<TbEstadosolicitude>(entity =>
            {
                entity.HasKey(e => e.EstadosId)
                    .HasName("tb_estadosolicitudes_pkey");

                entity.ToTable("tb_estadosolicitudes", "muni_villanueva");

                entity.HasIndex(e => e.EstadosNombre, "tb_estadosolicitudes_estados_nombre_key")
                    .IsUnique();

                entity.Property(e => e.EstadosId).HasColumnName("estados_id");

                entity.Property(e => e.EstadosNombre)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("estados_nombre");
            });

            modelBuilder.Entity<TbSolicitude>(entity =>
            {
                entity.HasKey(e => e.SolicitudId)
                    .HasName("tb_solicitudes_pkey");

                entity.ToTable("tb_solicitudes", "muni_villanueva");

                entity.HasIndex(e => e.CargoId, "IX_tb_solicitudes_cargo_id");

                entity.HasIndex(e => e.DeptoId, "IX_tb_solicitudes_depto_id");

                entity.HasIndex(e => e.EmpleadoId, "IX_tb_solicitudes_empleado_id");

                entity.HasIndex(e => e.EstadosId, "IX_tb_solicitudes_estados_id");

                entity.HasIndex(e => e.VacacionesId, "IX_tb_solicitudes_vacaciones_id");

                entity.Property(e => e.SolicitudId).HasColumnName("solicitud_id");

                entity.Property(e => e.CantidadDias).HasColumnName("cantidad_dias");

                entity.Property(e => e.CargoId).HasColumnName("cargo_id");

                entity.Property(e => e.Comentario)
                    .HasMaxLength(300)
                    .HasColumnName("comentario");

                entity.Property(e => e.DeptoId).HasColumnName("depto_id");

                entity.Property(e => e.DetallesSolicitud)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("detalles_solicitud");

                entity.Property(e => e.EmpleadoId).HasColumnName("empleado_id");

                entity.Property(e => e.EstadoSeleDirector)
                    .HasMaxLength(100)
                    .HasColumnName("estado_sele_director");

                entity.Property(e => e.EstadoSeleJefe)
                    .HasMaxLength(100)
                    .HasColumnName("estado_sele_jefe");

                entity.Property(e => e.EstadosId).HasColumnName("estados_id");

                entity.Property(e => e.FechasSeleccionadas)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("fechas_seleccionadas");

                entity.Property(e => e.SolicitudFecha)
                    .HasColumnType("date")
                    .HasColumnName("solicitud_fecha");

                entity.Property(e => e.VacacionesId).HasColumnName("vacaciones_id");

                entity.HasOne(d => d.Cargo)
                    .WithMany(p => p.TbSolicitudes)
                    .HasForeignKey(d => d.CargoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_cargo_id_tb_solicitudes_tb_cargos");

                entity.HasOne(d => d.Depto)
                    .WithMany(p => p.TbSolicitudes)
                    .HasForeignKey(d => d.DeptoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_depto_id_tb_solicitudes_tb_departamentos");

                entity.HasOne(d => d.Empleado)
                    .WithMany(p => p.TbSolicitudes)
                    .HasForeignKey(d => d.EmpleadoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_empleado_id_tb_solicitudes_tb_empleados");

                entity.HasOne(d => d.Estados)
                    .WithMany(p => p.TbSolicitudes)
                    .HasForeignKey(d => d.EstadosId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_estados_id_tb_solicitudes_tb_estadosolicitudes");

                entity.HasOne(d => d.Vacaciones)
                    .WithMany(p => p.TbSolicitudes)
                    .HasForeignKey(d => d.VacacionesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_vacaciones_id_tb_solicitudes_tb_vacaciones");
            });

            modelBuilder.Entity<TbVacacione>(entity =>
            {
                entity.HasKey(e => e.VacacionesId)
                    .HasName("tb_vacaciones_pkey");

                entity.ToTable("tb_vacaciones", "muni_villanueva");

                entity.HasIndex(e => e.VacacionesEstado, "tb_vacaciones_vacaciones_estado_key")
                    .IsUnique();

                entity.Property(e => e.VacacionesId).HasColumnName("vacaciones_id");

                entity.Property(e => e.VacacionesEstado)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("vacaciones_estado");

                entity.Property(e => e.VacacionesTiempo).HasColumnName("vacaciones_tiempo");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
