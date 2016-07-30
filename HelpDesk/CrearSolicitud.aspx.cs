using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HelpDesk
{
    public partial class CrearSolicitud : System.Web.UI.Page
    {
        JavaScriptSerializer json = new JavaScriptSerializer();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated)
                Response.Redirect("~/login.aspx");

            if(!IsPostBack)
            {
                CargarDepartamento();
                CargarProblema(int.Parse(cboDepartamento.SelectedValue));
            }
             
        }

        private void CargarDepartamento()
        {

            cboDepartamento.DataSource = Code.Departamento.GetDepartamentos();
            cboDepartamento.DataTextField = "departamento";
            cboDepartamento.DataValueField = "departamento_id";
            cboDepartamento.DataBind();
        }

        private void CargarProblema(int departamento_id)
        {

            cboProblema.DataSource = Code.Problema.GetProblema(departamento_id);
            cboProblema.DataTextField = "problema";
            cboProblema.DataValueField = "problema_id";
            cboProblema.DataBind();
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

            if (Code.Solicitud.CrearSolicitud(txtTitulo.Text.Trim(),int.Parse(cboDepartamento.SelectedValue),int.Parse(cboProblema.SelectedValue),txtContenido.Text.Trim(),usuario.usuario_id))
            {
                Controles.Mensajes.MessageCorrecto("TU SOLICITUD SE HA PUBLICADO CORRECTAMENTE");
                txtTitulo.Clear();
                txtContenido.Clear();
                CargarDepartamento();
            }
            else
                Controles.Mensajes.MessageError("Ha ocurrido un error con la solicitud, revisa los datos e intenta nuevamente.");
        }

        protected void cboDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarProblema(int.Parse(cboDepartamento.SelectedValue));
        }
    }
}