using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HelpDesk
{
    public partial class DetallePublicacion : System.Web.UI.Page
    {
        JavaScriptSerializer json = new JavaScriptSerializer();
        protected void Page_Load(object sender, EventArgs e)
        {
            pnlComentar.Visible = Request.IsAuthenticated;
            pnlRegistrate.Visible = !Request.IsAuthenticated;

            if (!IsPostBack)
                this.MostrarDetalle();
        }


        void MostrarDetalle()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["pid"]))
            {
                int pid = int.Parse(Request.QueryString["pid"]);
                int usuario_id = 0;

                if (Request.IsAuthenticated)
                {
                    var usuario = Code.Login.GetUsuario(User.Identity.Name);
                    usuario_id = usuario.usuario_id;
                }

                var publicacion = Code.Publicaciones.GetPublicacion(pid, usuario_id);
                lblNo.Text = publicacion.publicacion_id.ToString();
                lblEncabezado.Text = publicacion.encabezado;
                lblUsuario.Text = string.Format("{0} {1}", publicacion.HD_Usuario.nombres, publicacion.HD_Usuario.apellidos);
                lblFecha.Text = publicacion.fecha.ToString("dd / MMM/ yyyy");
                lblContenido.Text = publicacion.contenido;

                var p = new { fn = "pidcoment", pid = pid };
                hdnparametro.Value = json.Serialize(p);

                rpComentarios.DataSource = from pc in publicacion.HD_Publicacion_Comentarios
                                           select new { pc.comentario, pc.fecha, usuario = pc.HD_Usuario.nombres + " " + pc.HD_Usuario.apellidos };

                rpComentarios.DataBind();
            }

        }


    }
}