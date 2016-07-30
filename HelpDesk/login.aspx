<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="HelpDesk.login" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>



<asp:Content  ContentPlaceHolderID="Body" runat="server">


    <fieldset>
        <legend>Datos para Iniciar Sesion</legend>


        <table>
            <tr>
                <td colspan="2">Usuario:</td>
            </tr>
            <tr>
                <td colspan="2">
<asp:TextBox runat="server" ID="txtUsuario"></asp:TextBox>
                </td>
                
            </tr>

            <tr>
                <td colspan="2">Password:</td>            
            </tr>
            <tr>
                <td colspan="2">
                    <asp:TextBox runat="server" ID="txtClave" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="chkMantenerSesion" runat="server" />
                    <asp:Label runat="server" AssociatedControlID="chkMantenerSesion">Manener Sesion</asp:Label>
                </td>
                <td>
                    <asp:Button runat="server" id="btnLogin" Text="Entrar" OnClick="btnLogin_Click" />
                </td>
            </tr>

        </table>

    </fieldset>


</asp:Content>
