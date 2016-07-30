<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="CrearSolicitud.aspx.cs" Inherits="HelpDesk.CrearSolicitud" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="Body" runat="server">

    <h2>Crear Solicitud</h2>

    <br />

    <asp:HiddenField ClientIDMode="Static" runat="server" ID="hdnparametro" />


    <div runat="server" id="dvCont">
    <fieldset>
        <legend>Encabezado
        </legend>

        <table style="width:100%;">
            <tr>
                <td style="width:125px;">
                    <b>Titulo</b>
                   
                </td>
                <td>
 <ctl:TextBoxLast ID="txtTitulo" runat="server" CanEmpty="false" MaxLength="50" Mensaje="Debe especificar el titulo" style="width:80%;"></ctl:TextBoxLast>
                </td>
              
            </tr>

            <tr>
                <td >
                    <b>Departamento</b>

                </td>
                <td>
                    <ctl:DropDownListLast ID="cboDepartamento" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboDepartamento_SelectedIndexChanged" CanEmpty="false" Mensaje="Debe especificar el departamento"></ctl:DropDownListLast>
                </td>
            </tr>

             <tr>
                <td>
                    <b>Problema</b>
                   
                </td>
                 <td>
 <ctl:DropDownListLast ID="cboProblema" runat="server" CanEmpty="false" Mensaje="Debe especificar el problema"></ctl:DropDownListLast>
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
    <ctl:ButtonLast runat="server" ID="btnCrear" Text="Crear Solicitud" OnClick="btnCrear_Click" CausesValidation="true" />
    </div>
</div>


</asp:Content>
