using System;
using System.Drawing;
using System.IO;

namespace HelpDesk
{
    public partial class Resumen : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated)
                Response.Redirect("~/login.aspx");

            hdnValue.Value = Code.Solicitud.Resumen();



        }
    }
}