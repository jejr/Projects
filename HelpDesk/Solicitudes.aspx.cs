using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HelpDesk
{
    public partial class Solicitudes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated)
                Response.Redirect("~/login.aspx");

            string criterio = "No_Asignados";
            if (!string.IsNullOrEmpty(Request.QueryString["fn"]))
                criterio = Request.QueryString["fn"];

            lblFIltro.Text = criterio.Replace("_", " ");

            rpPublicaciones.DataSource = Code.Solicitud.GetSolicitudes(criterio);
            rpPublicaciones.DataBind();

        }



    }
}