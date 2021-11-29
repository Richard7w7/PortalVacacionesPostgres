using ControlPostgres.Contexto.Entities;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ControlPostgres.RepositorioClases
{
    public class Generadorpdf
    {
        enum EstadoSolicitud
        {
            Enviada = 1,
            Revision_I = 2,
            Aprobada = 4,
            Denegado = 5
        }

        public Generadorpdf()
        {

        }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        DateTime ultimodescanso;
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        DateTime InicioLaboral;
        public string GenerateInvestorDocument(TbSolicitude tbsolitude)
        {
            string fullName = string.Concat(tbsolitude.SolicitudId, " ","_Codigo_Empleado_", tbsolitude.Empleado.EmpleadoCodigo);

            //RUTAS RELATIVAS
            string filepath = @"wwwroot\Templates\"; //rutas windows
            string newfilepath = @"wwwroot\Templates\";
            //RUTAS WINDOWS
            //string newfilepath = @"C:\Users\richa\Documents\Solicitudes\";
            //string filepath = @"C:\Users\richa\Documents\";
            //string filepath = @"wwwroot\Templates\";
            
            //RUTAS UBUNTU
            //string filepath = @"/var/www/PortalWebUbuntu/wwwroot/";
            //string newfilepath = @"/home/richard/Documentos/Solicitudes/";
            //string filepath = @"/var/www/PortalWebUbuntu/wwwroot/Templates/";
            string filenameExisting = @"Template.pdf";

            string fileNameNew = @"Solicitud #" + fullName.Replace(" ", "").Trim() + ".pdf";

            string fullNewPath = newfilepath + fileNameNew;
            string fullExistingPath = filepath + filenameExisting;

            using (var existingFileStream = new FileStream(fullExistingPath,FileMode.Open))

                using (var newFileStream = new FileStream(fullNewPath, FileMode.Create))
            {
                var pdfReader = new PdfReader(existingFileStream);
                var stamper = new PdfStamper(pdfReader, newFileStream);
                AcroFields fields = stamper.AcroFields;
                fields.SetField("txtFecha", tbsolitude.SolicitudFecha.ToShortDateString());
                fields.SetField("txtCodigoEmpleado", tbsolitude.Empleado.EmpleadoCodigo);
                fields.SetField("txtNombre", tbsolitude.Empleado.EmpleadoNombre1+" "+tbsolitude.Empleado.EmpleadoApellido1);
                fields.SetField("txtDireccion", tbsolitude.Empleado.EmpleadoDireccion);
                fields.SetField("txtPuesto", tbsolitude.Cargo.CargoNombre);
                fields.SetField("txtInicioLaboral", tbsolitude.Empleado.FechaIngreso.ToShortDateString());
                fields.SetField("txtCantiDias", Convert.ToString(tbsolitude.CantidadDias));
                fields.SetField("txtCorrespondePeriodo",tbsolitude.PeriodoVacas);
                string[] arrayfechas = tbsolitude.FechasSeleccionadas.Split(',');
                fields.SetField("txtInicioVacas", arrayfechas[0]);
                fields.SetField("txtFinVacas", arrayfechas.Last());
                InicioLaboral = Convert.ToDateTime(arrayfechas.Last());
                InicioLaboral = InicioLaboral.AddDays(1);
                fields.SetField("txtRegresoLaboral",InicioLaboral.ToShortDateString());
                fields.SetField("txtComentario", tbsolitude.Comentario);
                fields.SetField("txtNotificado1", tbsolitude.Empleado.EmpleadoNombre1 + tbsolitude.Empleado.EmpleadoApellido1);
                fields.SetField("txtNotificado2", tbsolitude.Empleado.EmpleadoCodigo);
                fields.SetField("txtNotificado1", tbsolitude.Empleado.EmpleadoNombre1 + tbsolitude.Empleado.EmpleadoApellido1);
                fields.SetField("txtNotificado2", tbsolitude.Empleado.EmpleadoCodigo);
                
                string[] arrayestadoseleJefe = tbsolitude.EstadoSeleJefe.Split(' ');
                string[] arrayestadoseleDirector = tbsolitude.EstadoSeleDirector.Split(' ');
                fields.SetField("txtEstadoJefeInmediato", arrayestadoseleJefe[0]);
                fields.SetField("txtNombreJefeInmediato", arrayestadoseleJefe[1] + " " + arrayestadoseleJefe[2]);
                fields.SetField("txtPuestoJefeInmediato", "Jefe Inmediato");
                fields.SetField("txtEstadoDirector", arrayestadoseleDirector[0]);
                fields.SetField("txtNombreDirector", arrayestadoseleDirector[1] + " " + arrayestadoseleDirector[2]);
                fields.SetField("txtPuestoDirector", "Director");
                if(tbsolitude.EstadosId == (int)EstadoSolicitud.Aprobada) { 
                fields.SetField("txtEstado", "Aprobada");
                }
                else if(tbsolitude.EstadosId == (int)EstadoSolicitud.Denegado)
                {
                    fields.SetField("txtEstado", "Denegada");
                }
                stamper.FormFlattening = true;
                stamper.Close();

                pdfReader.Close();

                //return fileNameNew;
                return fullNewPath;

            }
        }
        
        public string GenerateInvestorDocumentUser(TbSolicitude tbsolitude)
        {
            string fullName = string.Concat(tbsolitude.SolicitudId, "", "_Codigo_Empleado_", tbsolitude.Empleado.EmpleadoCodigo);

            //RUTAS RELATIVAS
            string filepath = @"wwwroot\Templates\"; //rutas windows
            string newfilepath = @"wwwroot\Templates\";
            //RUTAS WINDOWS
            //string newfilepath = @"C:\Users\richa\Documents\Solicitudes\";
            //string filepath = @"C:\Users\richa\Documents\";
            //string filepath = @"wwwroot\Templates\";

            //RUTAS UBUNTU
            //string filepath = @"/var/www/PortalWebUbuntu/wwwroot/";
            //string newfilepath = @"/home/richard/Documentos/Solicitudes/";
            //string filepath = @"/var/www/PortalWebUbuntu/wwwroot/Templates/";
            string filenameExisting = @"Template.pdf";

            string fileNameNew = @"Solicitud #" + fullName.Replace(" ", "").Trim() + ".pdf";

            string fullNewPath = newfilepath + fileNameNew;
            string fullExistingPath = filepath + filenameExisting;

            using (var existingFileStream = new FileStream(fullExistingPath, FileMode.Open))

            using (var newFileStream = new FileStream(fullNewPath, FileMode.Create))
            {
                var pdfReader = new PdfReader(existingFileStream);
                var stamper = new PdfStamper(pdfReader, newFileStream);
                AcroFields fields = stamper.AcroFields;
                fields.SetField("txtFecha", tbsolitude.SolicitudFecha.ToShortDateString());
                fields.SetField("txtCodigoEmpleado", tbsolitude.Empleado.EmpleadoCodigo);
                fields.SetField("txtNombre", tbsolitude.Empleado.EmpleadoNombre1 + " " + tbsolitude.Empleado.EmpleadoApellido1);
                fields.SetField("txtDireccion", tbsolitude.Empleado.EmpleadoDireccion);
                fields.SetField("txtPuesto", tbsolitude.Cargo.CargoNombre);
                fields.SetField("txtInicioLaboral", tbsolitude.Empleado.FechaIngreso.ToShortDateString());
                fields.SetField("txtCorrespondePeriodo", tbsolitude.PeriodoVacas);
                fields.SetField("txtCantiDias", Convert.ToString(tbsolitude.CantidadDias));
                string[] arrayfechas = tbsolitude.FechasSeleccionadas.Split(',');
                fields.SetField("txtInicioVacas", arrayfechas[0]);
                fields.SetField("txtFinVacas", arrayfechas.Last());
                InicioLaboral = Convert.ToDateTime(arrayfechas.Last());
                InicioLaboral = InicioLaboral.AddDays(1);
                fields.SetField("txtRegresoLaboral", InicioLaboral.ToShortDateString());
                fields.SetField("txtComentario", tbsolitude.Comentario);
                fields.SetField("txtNotificado1", tbsolitude.Empleado.EmpleadoNombre1 + tbsolitude.Empleado.EmpleadoApellido1);
                fields.SetField("txtNotificado2", tbsolitude.Empleado.EmpleadoCodigo);
                
                string[] arrayestadoseleJefe = tbsolitude.EstadoSeleJefe.Split(' ');
                
                fields.SetField("txtEstadoJefeInmediato", arrayestadoseleJefe[0]);
                fields.SetField("txtNombreJefeInmediato", arrayestadoseleJefe[1] +" "+ arrayestadoseleJefe[2]);
                fields.SetField("txtPuestoJefeInmediato","Jefe Inmediato");
                if (tbsolitude.EstadoSeleDirector != null) {
                    string[] arrayestadoseleDirector = tbsolitude.EstadoSeleDirector.Split(' ');
                    fields.SetField("txtEstadoDirector", arrayestadoseleDirector[0]);
                    fields.SetField("txtNombreDirector", arrayestadoseleDirector[1] + " " + arrayestadoseleDirector[2]);
                    fields.SetField("txtPuestoDirector", "Director");
                    fields.SetField("txtEstado", tbsolitude.Estados.EstadosNombre);
                }
                else
                {
                    if (tbsolitude.EstadosId == (int)EstadoSolicitud.Aprobada)
                    {
                        fields.SetField("txtEstado", "Aprobada");
                    }
                    else if (tbsolitude.EstadosId == (int)EstadoSolicitud.Denegado)
                    {
                        fields.SetField("txtEstado", "Denegada");
                    }
                }
                

                stamper.FormFlattening = true;
                stamper.Close();

                pdfReader.Close();

                //return fileNameNew;
                return fullNewPath;

            }
        }

        public string GenerateInvestorDocumentJefe(TbSolicitude tbsolitude)
        {
            string fullName = string.Concat(tbsolitude.SolicitudId, " ", "_Codigo_Empleado_", tbsolitude.Empleado.EmpleadoCodigo);

            //RUTAS RELATIVAS
            string filepath = @"wwwroot\Templates\"; //rutas windows
            string newfilepath = @"wwwroot\Templates\";
            //RUTAS WINDOWS
            //string newfilepath = @"C:\Users\richa\Documents\Solicitudes\";
            //string filepath = @"C:\Users\richa\Documents\";
            //string filepath = @"wwwroot\Templates\";

            //RUTAS UBUNTU
            //string filepath = @"/var/www/PortalWebUbuntu/wwwroot/";
            //string newfilepath = @"/home/richard/Documentos/Solicitudes/";
            //string filepath = @"/var/www/PortalWebUbuntu/wwwroot/Templates/";
            string filenameExisting = @"Template.pdf";

            string fileNameNew = @"Solicitud #" + fullName.Replace(" ", "").Trim() + ".pdf";

            string fullNewPath = newfilepath + fileNameNew;
            string fullExistingPath = filepath + filenameExisting;

            using (var existingFileStream = new FileStream(fullExistingPath, FileMode.Open))

            using (var newFileStream = new FileStream(fullNewPath, FileMode.Create))
            {
                var pdfReader = new PdfReader(existingFileStream);
                var stamper = new PdfStamper(pdfReader, newFileStream);
                AcroFields fields = stamper.AcroFields;
                fields.SetField("txtFecha", tbsolitude.SolicitudFecha.ToShortDateString());
                fields.SetField("txtCodigoEmpleado", tbsolitude.Empleado.EmpleadoCodigo);
                fields.SetField("txtNombre", tbsolitude.Empleado.EmpleadoNombre1 + " " + tbsolitude.Empleado.EmpleadoApellido1);
                fields.SetField("txtDireccion", tbsolitude.Empleado.EmpleadoDireccion);
                fields.SetField("txtPuesto", tbsolitude.Cargo.CargoNombre);
                fields.SetField("txtInicioLaboral", tbsolitude.Empleado.FechaIngreso.ToShortDateString());
                fields.SetField("txtCantiDias", Convert.ToString(tbsolitude.CantidadDias));
                fields.SetField("txtCorrespondePeriodo", tbsolitude.PeriodoVacas);
                string[] arrayfechas = tbsolitude.FechasSeleccionadas.Split(',');
                fields.SetField("txtInicioVacas", arrayfechas[0]);
                fields.SetField("txtFinVacas", arrayfechas.Last());
                InicioLaboral = Convert.ToDateTime(arrayfechas.Last());
                InicioLaboral = InicioLaboral.AddDays(1);
                fields.SetField("txtRegresoLaboral", InicioLaboral.ToShortDateString());
                fields.SetField("txtComentario", tbsolitude.Comentario);
                fields.SetField("txtNotificado1", tbsolitude.Empleado.EmpleadoNombre1 + tbsolitude.Empleado.EmpleadoApellido1);
                fields.SetField("txtNotificado2", tbsolitude.Empleado.EmpleadoCodigo);
                fields.SetField("txtNotificado1", tbsolitude.Empleado.EmpleadoNombre1 + tbsolitude.Empleado.EmpleadoApellido1);
                fields.SetField("txtNotificado2", tbsolitude.Empleado.EmpleadoCodigo);

                string[] arrayestadoseleJefe = tbsolitude.EstadoSeleJefe.Split(' ');
                fields.SetField("txtEstadoJefeInmediato", arrayestadoseleJefe[0]);
                fields.SetField("txtNombreJefeInmediato", arrayestadoseleJefe[1] + " " + arrayestadoseleJefe[2]);
                fields.SetField("txtPuestoJefeInmediato", "Jefe Inmediato");
                if (tbsolitude.EstadosId == (int)EstadoSolicitud.Aprobada)
                {
                    fields.SetField("txtEstado", "Aprobada");
                }
                else if (tbsolitude.EstadosId == (int)EstadoSolicitud.Denegado)
                {
                    fields.SetField("txtEstado", "Denegada");
                }
                stamper.FormFlattening = true;
                stamper.Close();

                pdfReader.Close();

                //return fileNameNew;
                return fullNewPath;
                

            }
        }
    }
}
