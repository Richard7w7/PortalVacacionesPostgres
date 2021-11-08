using ControlPostgres.Contexto.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;



namespace ControlPostgres.RepositorioClases
{
    public class Repositorio : Controller
    {

        //creacion de enumerables para no tener numeros magicos
        enum EstadoTrabajador
        {
            Activo = 1
        }

        enum EstadoSolicitud
        {
            Enviada = 1
        }

        enum TiempoLaborando
        {
            UnanioaCincoanios=1,
            CincoaniosyUndia = 2,
            DiezaniosyUndia = 3,
        }

        enum DiasVacaciones
        {
            VeinteDias = 20,
            VeintiCinco = 25,
            TreintaDias = 30,
        }

        BD_ControlVacacionesContext bd = new BD_ControlVacacionesContext();
        TbEmpleado emp = new TbEmpleado();

        public bool Registrar(TbEmpleado model)
        {
            Boolean logico = false;
            try
            {
                model.EmpleadoEstado = (int)EstadoTrabajador.Activo;
                TbEmpleado obj = new TbEmpleado();
                obj.EmpleadoCodigo = model.EmpleadoCodigo;
                obj.EmpleadoContraseña = model.EmpleadoContraseña;
                obj.EmpleadoNombre1 = model.EmpleadoNombre1;
                obj.EmpleadoNombre2 = model.EmpleadoNombre2;
                obj.EmpleadoApellido1 = model.EmpleadoApellido1;
                obj.EmpleadoApellido2 = model.EmpleadoApellido2;
                obj.ApellidoCasada = model.ApellidoCasada;
                obj.FechaNacimiento = model.FechaNacimiento;
                obj.FechaIngreso = model.FechaIngreso;
                obj.EmpleadoTelefono = model.EmpleadoTelefono;
                obj.EmpleadoDireccion = model.EmpleadoDireccion;
                obj.CargoId = model.CargoId;
                obj.DeptoId = model.DeptoId;
                obj.VacacionesId = model.VacacionesId;
                if(model.VacacionesId == (int)TiempoLaborando.UnanioaCincoanios)
                {
                    obj.EmpDiasvacaciones = (int)DiasVacaciones.VeinteDias;
                }else if(model.VacacionesId == (int)TiempoLaborando.CincoaniosyUndia)
                {
                    obj.EmpDiasvacaciones = (int)DiasVacaciones.VeintiCinco;
                }
                else if (model.VacacionesId == (int)TiempoLaborando.DiezaniosyUndia)
                {
                    obj.EmpDiasvacaciones = (int)DiasVacaciones.TreintaDias;
                }
                obj.EmpleadoEstado = (int)EstadoTrabajador.Activo;

                bd.TbEmpleados.Add(obj);
                bd.SaveChanges();

                logico = true;

            }
            catch (Exception e)
            {

            }

            return logico;
        }

        public (int, TbEmpleado) Login(TbEmpleado usuario)
        {
            int respuesta = 0;
            var user = new TbEmpleado();
            if (!string.IsNullOrEmpty(usuario.EmpleadoCodigo) && !string.IsNullOrEmpty(usuario.EmpleadoContraseña))
            {

                BD_ControlVacacionesContext bd = new BD_ControlVacacionesContext();
                user = bd.TbEmpleados.FirstOrDefault(u => u.EmpleadoCodigo == usuario.EmpleadoCodigo && u.EmpleadoContraseña == usuario.EmpleadoContraseña);
                try
                {
                    /*si el usuario es diferente de nulo y aca con else if pues dependiendo del cargo y departamento que el usuario tenga 
                     asi mismo lo redireccionara a una vista predeterminada tomando en cuenta que no llevo mucho*/
                    if (user != null)
                    {
                        if (user.DeptoId == 1 && user.CargoId == 2)
                        {
                            var claims = new List<Claim>
                            {
                            new Claim(ClaimTypes.Name,user.EmpleadoCodigo)
                            };
                            var identity = new ClaimsIdentity(
                                claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            var principal = new ClaimsPrincipal(identity);
                            var props = new AuthenticationProperties();
                            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props).Wait();
                            //FormsAuthentication.SetAuthCookie(user.EmpleadoCodigo, true);
                            respuesta = 2;
                        }
                        else if (user.DeptoId == 1 && user.CargoId == 3)
                        {
                            var claims = new List<Claim>
                            {
                            new Claim(ClaimTypes.Name,user.EmpleadoCodigo)
                            };
                            var identity = new ClaimsIdentity(
                                claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            var principal = new ClaimsPrincipal(identity);
                            var props = new AuthenticationProperties();
                            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props).Wait();
                            //FormsAuthentication.SetAuthCookie(user.EmpleadoCodigo, true);
                            respuesta = 3;

                        }
                        else if (user.DeptoId == 1 && user.CargoId == 4)
                        {
                            var claims = new List<Claim>
                            {
                            new Claim(ClaimTypes.Name,user.EmpleadoCodigo)
                            };
                            var identity = new ClaimsIdentity(
                                claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            var principal = new ClaimsPrincipal(identity);
                            var props = new AuthenticationProperties();
                            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props).Wait();
                            //FormsAuthentication.SetAuthCookie(user.EmpleadoCodigo, true);
                            respuesta = 4;
                        }

                    }
                    else
                    {
                        respuesta = -1;
                    }

                }
                catch (Exception e)
                {

                }

            }
            else
            {
                respuesta = 0;
            }
            return (respuesta, user);
        }
        public bool CrearSolicitud(TbSolicitude registro)
        {
            Boolean logico = false;

            try
            {

                registro.SolicitudFecha = DateTime.Now.Date;
                string[] caracter = registro.FechasSeleccionadas.Split(',');
                registro.EstadosId = (int)EstadoSolicitud.Enviada;
                TbSolicitude obj = new TbSolicitude();
                obj.DetallesSolicitud = registro.DetallesSolicitud;
                obj.SolicitudFecha = registro.SolicitudFecha;
                obj.PeriodoVacas = registro.PeriodoVacas;
                obj.FechasSeleccionadas = registro.FechasSeleccionadas;
                obj.CantidadDias = caracter.Length;
                obj.EmpleadoId = registro.Empleado.EmpleadoId;
                obj.CargoId = registro.Empleado.CargoId;
                obj.DeptoId = registro.Empleado.DeptoId;
                obj.VacacionesId = registro.Empleado.VacacionesId;
                obj.EstadosId = registro.EstadosId;

                bd.TbSolicitudes.Add(obj);
                bd.SaveChanges();

                logico = true;
            }
            catch (Exception)
            {

                throw;
            }

            return logico;
        }

       public bool DiasRestantes(TbSolicitude registro, TbEmpleado usuario)
        {

            string[] caracter = registro.FechasSeleccionadas.Split(',');
            int diasantiguos = (int)usuario.EmpDiasvacaciones;
            int diasresta = registro.CantidadDias;
            int diasrestantes = (int)(usuario.EmpDiasvacaciones - caracter.Length);

            if (diasrestantes >= 0) { 
                return true;
            }
            else
            {
                return false;
            }
        }  
    }
}
