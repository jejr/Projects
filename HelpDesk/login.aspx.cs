using System;
using DB_HD;
using System.Linq;

namespace HelpDesk
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {


            var usuario = Code.Login.Verificar(txtUsuario.Text.Trim(), txtClave.Text);

            if (usuario != null )
            {
                System.Web.Security.FormsAuthentication.SetAuthCookie(usuario.usuario , chkMantenerSesion.Checked);
                Response.Redirect("~/inicio.aspx");
            }
        }
    }
}