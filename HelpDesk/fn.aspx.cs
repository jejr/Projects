using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HelpDesk
{
    public partial class fn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string fn = Request.QueryString["fn"];

            if (string.IsNullOrEmpty(fn))
                return;

            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();

            if (fn == "pidcoment")
                PublicacionComentario();

            else if (fn == "spid")
                SeguirPublicacion();

            else if (fn == "suid")
                SeguirUsuario();

        }



        void PublicacionComentario()
        {
            int pid = 0;
            int.TryParse(Request.QueryString["pid"], out pid);
            string cmm = Request.QueryString["cmm"];
            var usuario = Code.Login.GetUsuario(User.Identity.Name);
            Code.Publicaciones.PublicarComentario(pid, cmm, usuario.usuario_id);


            string result = "<div class=\"comentario\">";
            result += " <div> ";
            result += " <span> ";
            result += cmm;
            result += "</span>";
            result += " </div>";
            result += " <hr />";
            result += " <div><span>";
            result += string.Format("{0} {1}", usuario.nombres, usuario.apellidos);
            result += "</span><span> ";
            result += DateTime.Now.ToString("dd / MMM/ yyyy");
            result += "</span> </div> </div>";

            Response.Write(result);
        }

        void SeguirPublicacion()
        {
            int pid = 0;
            int.TryParse(Request.QueryString["pid"], out pid);
            var usuario = Code.Login.GetUsuario(User.Identity.Name);
            string result = "";

            if (Code.Publicaciones.SeguirPublicacion(pid, usuario.usuario_id))
            {
                result = "{\"message\":\" Esta siguiendo esta publicacion. \"}";
            }

            Response.Write(result);
        }

        private void SeguirUsuario()
        {
            var seguidor = Code.Login.GetUsuario(User.Identity.Name);
            int usuario_id = 0;
            int.TryParse(Request.QueryString["uid"], out usuario_id);
            string result = "";

            if (Code.Usuario.SeguirUsuario(usuario_id, seguidor.usuario_id))
            {
                result = "{\"message\":\" Esta siguiendo a esta persona. \"}";
            }
            Response.Write(result);
        }


    }
}