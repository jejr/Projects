using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HelpDesk
{
    public partial class CrearPublicacion : System.Web.UI.Page
    {
        JavaScriptSerializer json = new JavaScriptSerializer();
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!Request.IsAuthenticated)
                Response.Redirect("~/login.aspx");
        }



        protected void btnCrear_Click(object sender, EventArgs e)
        {
            var valida = new Controles.Validacion.Validar();

            if (!valida.Comprobacion(dvCont))
            {
                Controles.Mensajes.MessageError(valida.alert);
                return;
            }

            var usuario = Code.Login.GetUsuario(User.Identity.Name);

            if (Code.Publicaciones.CrearPublicacion(usuario.usuario_id, txtEncabezado.Text.Trim(), txtContenido.Text.Trim()))
            {
                Controles.Mensajes.MessageCorrecto("TU CONTENIDO SE HA PUBLICADO CORRECTAMENTE");
                txtEncabezado.Clear();
                txtContenido.Clear();
            }
            else
                Controles.Mensajes.MessageError("Ha ocurrido un error con la publicacion, revisa los datos e intenta nuevamente.");
        }
    }
}