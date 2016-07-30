<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="CrearPublicacion.aspx.cs" Inherits="HelpDesk.CrearPublicacion" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="Body" runat="server">

    <h2>Crear Publicacion</h2>

    <br />

    <asp:HiddenField ClientIDMode="Static" runat="server" ID="hdnparametro" />


    <div runat="server" id="dvCont">
    <fieldset>
        <legend>Encabezado
        </legend>

        <table style="width:100%;">
            <tr>
                <td colspan="2">
                    <b>Encabezado</b>
                    <ctl:TextBoxLast ID="txtEncabezado" runat="server" CanEmpty="false" MaxLength="50" Mensaje="Debe especificar el encabezado" style="width:80%;"></ctl:TextBoxLast>
                </td>
              
            </tr>

            <tr>
                <td><b>Fecha</b></td>
                <td>
                    <span> <%= String.Format("{0:dd / MMM / yyyy}",DateTime.Now) %> </span>
                </td>
            </tr>
        </table>



    </fieldset>


    <fieldset>
        <legend>Contenido
        </legend>


       <ctl:TextBoxLast ID="txtContenido" runat="server" MaxLength="200" CanEmpty="false" Mensaje="Debe Especificar el contenido" TextMode="MultiLine" style="width:100%;height:300px;"></ctl:TextBoxLast>

    </fieldset>

    <br />
    <div style="width:95%;margin:auto;">
    <ctl:ButtonLast runat="server" ID="btnCrear" Text="Crear la publicacion" OnClick="btnCrear_Click" CausesValidation="true" />
    </div>
</div>


</asp:Content>
