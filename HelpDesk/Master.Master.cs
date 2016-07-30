using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HelpDesk
{
    public partial class Master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Autenticado();

            if (!IsPostBack & Request.IsAuthenticated)
                Notificaciones();

        }

        void Autenticado(bool salir = false)
        {

            bool autenticado = (Request.IsAuthenticated & !salir);

            if (autenticado)
            {
                var user = Code.Login.GetUsuario(Page.User.Identity.Name);
                lblUsuario.Text = user.nombres + " " + user.apellidos;
            }
            else
            {
                lblUsuario.Text = "Usuario Invitado";
                Session["user"] = null;
            }


            liSolicitudes.Visible = (autenticado);
            liInformes.Visible = (autenticado);
            liCrearPublicacion.Visible = (autenticado);
            liCrearSolicitud.Visible =(autenticado);
            liSolicitudes.Visible =(autenticado);
            pnlNotificacion.Visible = (autenticado);
            lklIniciar.Visible = !(autenticado);
            lklSalir.Visible = (autenticado);
        }

        void Notificaciones()
        {
            var usuario = Code.Login.GetUsuario(Page.User.Identity.Name);
            var notificaciones = Code.Usuario.NotificacionUsuario(usuario.usuario_id);
            pnlNotificacion.Visible = (notificaciones.Count > 0);
            lblCount.Text = notificaciones.Count.ToString("###,###");

            rpNotificaciones.DataSource = from n in notificaciones
                                          select new { n.comentario, n.tipo_id, n.id };
            rpNotificaciones.DataBind();

        }

        public string EvaluarLink(object tipo,object id)
        {

            switch ((Enums.TipoNotificacion)tipo)
            {
                case Enums.TipoNotificacion.NuevaPublicacion:
                    return "DetallePublicacion.aspx?pid=" + id.ToString();
                case Enums.TipoNotificacion.ComentarioPublicacion:
                    return "DetallePublicacion.aspx?pid=" + id.ToString();
                case Enums.TipoNotificacion.NuevaSolicitud:
                    return "DetalleSolicitud.aspx?sid=" + id.ToString();
                case Enums.TipoNotificacion.ComentarioSolicitud:
                    return "DetalleSolicitud.aspx?sid=" + id.ToString();

                default:
                    return "";
            }

        }

        protected void lklIniciar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/login.aspx");
        }

        protected void lklSalir_Click(object sender, EventArgs e)
        {
            System.Web.Security.FormsAuthentication.SignOut();
            Session.Abandon();
            Autenticado(true);
            Response.Redirect("~/login.aspx");
        }
    }
}