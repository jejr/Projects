using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HelpDesk
{
    public partial class DetalleSolicitud : System.Web.UI.Page
    {
        JavaScriptSerializer json = new JavaScriptSerializer();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated)
                Response.Redirect("~/login.aspx");

            if (!string.IsNullOrEmpty(Request.QueryString["uid"]))
                this.AsignarSolicitud();

            if (!string.IsNullOrEmpty(Request.QueryString["esid"]))
                this.CambiarEstado();

     
            this.MostrarDetalle();
        }

        private void CambiarEstado()
        {
            int sid = 0;
            int.TryParse(Request.QueryString["sid"], out sid);
            int esid = 0;
            int.TryParse(Request.QueryString["esid"], out esid);

            bool ntf = (string.IsNullOrEmpty(Request.QueryString["ntf"]));

            if (sid == 0)
                return;

            var usuario = Code.Login.GetUsuario(User.Identity.Name);
            if (!Code.Solicitud.CambiarEstado(sid,esid, usuario.usuario_id, ntf))
                Controles.Mensajes.MessageError("Hubo un problema al intentar actualizar la solicitud.");
        }

        private void AsignarSolicitud()
        {
            int sid = 0;
            int.TryParse(Request.QueryString["sid"], out sid);
            int uid = 0;
            int.TryParse(Request.QueryString["uid"], out uid);

            if (sid == 0 | uid == 0)
                return;

            var usuario = Code.Login.GetUsuario(User.Identity.Name);
            if (!Code.Solicitud.AsignarSolicitud(sid, uid, usuario.usuario_id))
                Controles.Mensajes.MessageError("Hubo un problema al intentar asignar la solicitud.");
        }

        void MostrarDetalle()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["sid"]))
            {
                int sid = int.Parse(Request.QueryString["sid"]);

                var usuario = Code.Login.GetUsuario(User.Identity.Name);
                var solicitud = Code.Solicitud.GetSolicitud(sid, usuario.usuario_id);
                lblNo.Text = solicitud.solicitud_id.ToString();
                lblEstado.Text = solicitud.HD_Estado.estado;
                lblTitulo.Text = solicitud.titulo;
                lblUsuario.Text = string.Format("{0} {1}", solicitud.HD_Usuario_Solicita.nombres, solicitud.HD_Usuario_Solicita.apellidos);
                lblFecha.Text = solicitud.fecha.ToString("dd / MMM/ yyyy");
                lblContenido.Text = solicitud.descripcion;
                lblDepartamento.Text = solicitud.HD_Departamento.departamento;
                lblProblema.Text = solicitud.HD_Problema.problema;
                lblUsuarioAsignado.Text = solicitud.HD_Usuario_Asignado != null ? solicitud.HD_Usuario_Asignado.nombres : "Sin Asignar";

                liCerrar.Visible = (solicitud.estado_id != (int)Enums.Estado.Cerrado);
                liCerrarSn.Visible = (solicitud.estado_id != (int)Enums.Estado.Cerrado);

                liSuspend.Visible = (solicitud.estado_id == (int)Enums.Estado.Abierto);
                liDesasig.Visible = (solicitud.estado_id != (int)Enums.Estado.Cerrado && solicitud.usr_asignado_id != null);
                liReabrir.Visible = (solicitud.estado_id == (int)Enums.Estado.Cerrado);
                liAbrir.Visible = (solicitud.estado_id == (int)Enums.Estado.Suspendido);

                pnlNotificacion.Visible = (solicitud.estado_id != (int)Enums.Estado.Cerrado);
                hdnparametro.Value = sid.ToString();
                rpComentarios.DataSource = from pc in solicitud.HD_Solicitud_Comentarios
                                           select new { pc.comentario, pc.fecha, usuario = pc.HD_Usuario.nombres + " " + pc.HD_Usuario.apellidos, pc.rutaFile };

                rpComentarios.DataBind();

                rpUsuarios.DataSource = from u in Code.Usuario.GetUsuariosPorDepartamento(usuario.usuario_id, usuario.departamento_id)
                                        select new { u.usuario_id, usuario = u.nombres + " " +u.apellidos };
                rpUsuarios.DataBind();
            }

        }

        protected void btnComentar_Click(object sender, EventArgs e)
        {
            var valida = new Controles.Validacion.Validar();

            if (!valida.Comprobacion(dvCont))
            {
                Controles.Mensajes.MessageError(valida.alert);
                return;
            }

            string ruta = Server.MapPath("~/Files/");
            string archivo = "";
            var usuario = Code.Login.GetUsuario(User.Identity.Name);

            if (!System.IO.Directory.Exists(ruta))
                System.IO.Directory.CreateDirectory(ruta);

            if (flArchivo.PostedFile.ContentLength > 0)
            {
                byte[] fl = new byte[flArchivo.PostedFile.ContentLength];
                flArchivo.PostedFile.InputStream.Read(fl, 0, fl.Length);

                string nombre = Guid.NewGuid().ToString();
                string ext = System.IO.Path.GetExtension(flArchivo.PostedFile.FileName);

                System.IO.File.WriteAllBytes(ruta + nombre + ext, fl);
                archivo = "Files/" + nombre + ext;
            }

            if (!Code.Solicitud.ComentarSolicitud(int.Parse(hdnparametro.Value), usuario.usuario_id, txtcomentar.Text.Trim(), archivo))
            {
                Controles.Mensajes.MessageError("Ha ocurrido un error al publicar tu comentario.");

            }
            else
                MostrarDetalle();
        }
    }
}