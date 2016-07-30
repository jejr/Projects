using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HelpDesk
{
    public partial class inicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
               rpPublicaciones.DataSource = Code.Publicaciones.GetPublicaciones();
                rpPublicaciones.DataBind();
            }

        }


      
    }
}