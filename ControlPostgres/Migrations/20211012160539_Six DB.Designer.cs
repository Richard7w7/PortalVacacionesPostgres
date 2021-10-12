﻿// <auto-generated />
using System;
using ControlPostgres.Contexto.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ControlPostgres.Migrations
{
    [DbContext(typeof(BD_ControlVacacionesContext))]
    [Migration("20211012160539_Six DB")]
    partial class SixDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "Spanish_Spain.1252")
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("ControlPostgres.Contexto.Entities.TbCargo", b =>
                {
                    b.Property<int>("CargoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("cargo_id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("CargoDescripcion")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("cargo_descripcion");

                    b.Property<string>("CargoNombre")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("character varying(75)")
                        .HasColumnName("cargo_nombre");

                    b.Property<int>("DeptoId")
                        .HasColumnType("integer")
                        .HasColumnName("depto_id");

                    b.HasKey("CargoId")
                        .HasName("tb_cargos_pkey");

                    b.HasIndex(new[] { "DeptoId" }, "IX_tb_cargos_depto_id");

                    b.ToTable("tb_cargos", "muni_villanueva");
                });

            modelBuilder.Entity("ControlPostgres.Contexto.Entities.TbDepartamento", b =>
                {
                    b.Property<int>("DeptoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("depto_id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("DeptoDescripcion")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("depto_descripcion");

                    b.Property<string>("DeptoNombre")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("character varying(75)")
                        .HasColumnName("depto_nombre");

                    b.HasKey("DeptoId")
                        .HasName("tb_departamentos_pkey");

                    b.HasIndex(new[] { "DeptoNombre" }, "tb_departamentos_depto_nombre_key")
                        .IsUnique();

                    b.ToTable("tb_departamentos", "muni_villanueva");
                });

            modelBuilder.Entity("ControlPostgres.Contexto.Entities.TbEmpleado", b =>
                {
                    b.Property<int>("EmpleadoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("empleado_id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ApellidoCasada")
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)")
                        .HasColumnName("apellido_casada");

                    b.Property<int>("CargoId")
                        .HasColumnType("integer")
                        .HasColumnName("cargo_id");

                    b.Property<int>("DeptoId")
                        .HasColumnType("integer")
                        .HasColumnName("depto_id");

                    b.Property<int?>("EmpDiasvacaciones")
                        .HasColumnType("integer")
                        .HasColumnName("emp_diasvacaciones");

                    b.Property<DateTime?>("EmpladoUltimavacafin")
                        .HasColumnType("date")
                        .HasColumnName("emplado_ultimavacafin");

                    b.Property<string>("EmpleadoApellido1")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)")
                        .HasColumnName("empleado_apellido1");

                    b.Property<string>("EmpleadoApellido2")
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)")
                        .HasColumnName("empleado_apellido2");

                    b.Property<string>("EmpleadoCodigo")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)")
                        .HasColumnName("empleado_codigo");

                    b.Property<string>("EmpleadoContraseña")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("empleado_contraseña");

                    b.Property<string>("EmpleadoDireccion")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)")
                        .HasColumnName("empleado_direccion");

                    b.Property<int>("EmpleadoEstado")
                        .HasColumnType("integer")
                        .HasColumnName("empleado_estado");

                    b.Property<string>("EmpleadoNombre1")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)")
                        .HasColumnName("empleado_nombre1");

                    b.Property<string>("EmpleadoNombre2")
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)")
                        .HasColumnName("empleado_nombre2");

                    b.Property<int?>("EmpleadoPermiso")
                        .HasColumnType("integer")
                        .HasColumnName("empleado_permiso");

                    b.Property<string>("EmpleadoTelefono")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("character varying(12)")
                        .HasColumnName("empleado_telefono");

                    b.Property<DateTime?>("EmpleadoUltimavacainicio")
                        .HasColumnType("date")
                        .HasColumnName("empleado_ultimavacainicio");

                    b.Property<DateTime>("FechaIngreso")
                        .HasColumnType("date")
                        .HasColumnName("fecha_ingreso");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("date")
                        .HasColumnName("fecha_nacimiento");

                    b.Property<int>("VacacionesId")
                        .HasColumnType("integer")
                        .HasColumnName("vacaciones_id");

                    b.HasKey("EmpleadoId")
                        .HasName("tb_empleados_pkey");

                    b.HasIndex(new[] { "CargoId" }, "IX_tb_empleados_cargo_id");

                    b.HasIndex(new[] { "DeptoId" }, "IX_tb_empleados_depto_id");

                    b.HasIndex(new[] { "VacacionesId" }, "IX_tb_empleados_vacaciones_id");

                    b.HasIndex(new[] { "EmpleadoCodigo" }, "tb_empleados_empleado_codigo_key")
                        .IsUnique();

                    b.HasIndex(new[] { "EmpleadoTelefono" }, "tb_empleados_empleado_telefono_key")
                        .IsUnique();

                    b.ToTable("tb_empleados", "muni_villanueva");
                });

            modelBuilder.Entity("ControlPostgres.Contexto.Entities.TbEstadosolicitude", b =>
                {
                    b.Property<int>("EstadosId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("estados_id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("EstadosNombre")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)")
                        .HasColumnName("estados_nombre");

                    b.HasKey("EstadosId")
                        .HasName("tb_estadosolicitudes_pkey");

                    b.HasIndex(new[] { "EstadosNombre" }, "tb_estadosolicitudes_estados_nombre_key")
                        .IsUnique();

                    b.ToTable("tb_estadosolicitudes", "muni_villanueva");
                });

            modelBuilder.Entity("ControlPostgres.Contexto.Entities.TbSolicitude", b =>
                {
                    b.Property<int>("SolicitudId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("solicitud_id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("CantidadDias")
                        .HasColumnType("integer")
                        .HasColumnName("cantidad_dias");

                    b.Property<int>("CargoId")
                        .HasColumnType("integer")
                        .HasColumnName("cargo_id");

                    b.Property<string>("Comentario")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)")
                        .HasColumnName("comentario");

                    b.Property<int>("DeptoId")
                        .HasColumnType("integer")
                        .HasColumnName("depto_id");

                    b.Property<string>("DetallesSolicitud")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("detalles_solicitud");

                    b.Property<int>("EmpleadoId")
                        .HasColumnType("integer")
                        .HasColumnName("empleado_id");

                    b.Property<string>("EstadoSeleDirector")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("estado_sele_director");

                    b.Property<string>("EstadoSeleJefe")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("estado_sele_jefe");

                    b.Property<int>("EstadosId")
                        .HasColumnType("integer")
                        .HasColumnName("estados_id");

                    b.Property<string>("FechasSeleccionadas")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("fechas_seleccionadas");

                    b.Property<DateTime>("SolicitudFecha")
                        .HasColumnType("date")
                        .HasColumnName("solicitud_fecha");

                    b.Property<int>("VacacionesId")
                        .HasColumnType("integer")
                        .HasColumnName("vacaciones_id");

                    b.HasKey("SolicitudId")
                        .HasName("tb_solicitudes_pkey");

                    b.HasIndex(new[] { "CargoId" }, "IX_tb_solicitudes_cargo_id");

                    b.HasIndex(new[] { "DeptoId" }, "IX_tb_solicitudes_depto_id");

                    b.HasIndex(new[] { "EmpleadoId" }, "IX_tb_solicitudes_empleado_id");

                    b.HasIndex(new[] { "EstadosId" }, "IX_tb_solicitudes_estados_id");

                    b.HasIndex(new[] { "VacacionesId" }, "IX_tb_solicitudes_vacaciones_id");

                    b.ToTable("tb_solicitudes", "muni_villanueva");
                });

            modelBuilder.Entity("ControlPostgres.Contexto.Entities.TbVacacione", b =>
                {
                    b.Property<int>("VacacionesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("vacaciones_id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("VacacionesEstado")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("vacaciones_estado");

                    b.Property<int>("VacacionesTiempo")
                        .HasColumnType("integer")
                        .HasColumnName("vacaciones_tiempo");

                    b.HasKey("VacacionesId")
                        .HasName("tb_vacaciones_pkey");

                    b.HasIndex(new[] { "VacacionesEstado" }, "tb_vacaciones_vacaciones_estado_key")
                        .IsUnique();

                    b.ToTable("tb_vacaciones", "muni_villanueva");
                });

            modelBuilder.Entity("ControlPostgres.Contexto.Entities.TbCargo", b =>
                {
                    b.HasOne("ControlPostgres.Contexto.Entities.TbDepartamento", "Depto")
                        .WithMany("TbCargos")
                        .HasForeignKey("DeptoId")
                        .HasConstraintName("fk_depto_id_tb_departamentos")
                        .IsRequired();

                    b.Navigation("Depto");
                });

            modelBuilder.Entity("ControlPostgres.Contexto.Entities.TbEmpleado", b =>
                {
                    b.HasOne("ControlPostgres.Contexto.Entities.TbCargo", "Cargo")
                        .WithMany("TbEmpleados")
                        .HasForeignKey("CargoId")
                        .HasConstraintName("fk_cargo_id_tb_emepleados_tb_cargos")
                        .IsRequired();

                    b.HasOne("ControlPostgres.Contexto.Entities.TbDepartamento", "Depto")
                        .WithMany("TbEmpleados")
                        .HasForeignKey("DeptoId")
                        .HasConstraintName("fk_depto_id_tb_empleados_tb_departamentos")
                        .IsRequired();

                    b.HasOne("ControlPostgres.Contexto.Entities.TbVacacione", "Vacaciones")
                        .WithMany("TbEmpleados")
                        .HasForeignKey("VacacionesId")
                        .HasConstraintName("fk_vacaciones_id_tb_empleados_tb_vacaciones")
                        .IsRequired();

                    b.Navigation("Cargo");

                    b.Navigation("Depto");

                    b.Navigation("Vacaciones");
                });

            modelBuilder.Entity("ControlPostgres.Contexto.Entities.TbSolicitude", b =>
                {
                    b.HasOne("ControlPostgres.Contexto.Entities.TbCargo", "Cargo")
                        .WithMany("TbSolicitudes")
                        .HasForeignKey("CargoId")
                        .HasConstraintName("fk_cargo_id_tb_solicitudes_tb_cargos")
                        .IsRequired();

                    b.HasOne("ControlPostgres.Contexto.Entities.TbDepartamento", "Depto")
                        .WithMany("TbSolicitudes")
                        .HasForeignKey("DeptoId")
                        .HasConstraintName("fk_depto_id_tb_solicitudes_tb_departamentos")
                        .IsRequired();

                    b.HasOne("ControlPostgres.Contexto.Entities.TbEmpleado", "Empleado")
                        .WithMany("TbSolicitudes")
                        .HasForeignKey("EmpleadoId")
                        .HasConstraintName("fk_empleado_id_tb_solicitudes_tb_empleados")
                        .IsRequired();

                    b.HasOne("ControlPostgres.Contexto.Entities.TbEstadosolicitude", "Estados")
                        .WithMany("TbSolicitudes")
                        .HasForeignKey("EstadosId")
                        .HasConstraintName("fk_estados_id_tb_solicitudes_tb_estadosolicitudes")
                        .IsRequired();

                    b.HasOne("ControlPostgres.Contexto.Entities.TbVacacione", "Vacaciones")
                        .WithMany("TbSolicitudes")
                        .HasForeignKey("VacacionesId")
                        .HasConstraintName("fk_vacaciones_id_tb_solicitudes_tb_vacaciones")
                        .IsRequired();

                    b.Navigation("Cargo");

                    b.Navigation("Depto");

                    b.Navigation("Empleado");

                    b.Navigation("Estados");

                    b.Navigation("Vacaciones");
                });

            modelBuilder.Entity("ControlPostgres.Contexto.Entities.TbCargo", b =>
                {
                    b.Navigation("TbEmpleados");

                    b.Navigation("TbSolicitudes");
                });

            modelBuilder.Entity("ControlPostgres.Contexto.Entities.TbDepartamento", b =>
                {
                    b.Navigation("TbCargos");

                    b.Navigation("TbEmpleados");

                    b.Navigation("TbSolicitudes");
                });

            modelBuilder.Entity("ControlPostgres.Contexto.Entities.TbEmpleado", b =>
                {
                    b.Navigation("TbSolicitudes");
                });

            modelBuilder.Entity("ControlPostgres.Contexto.Entities.TbEstadosolicitude", b =>
                {
                    b.Navigation("TbSolicitudes");
                });

            modelBuilder.Entity("ControlPostgres.Contexto.Entities.TbVacacione", b =>
                {
                    b.Navigation("TbEmpleados");

                    b.Navigation("TbSolicitudes");
                });
#pragma warning restore 612, 618
        }
    }
}
