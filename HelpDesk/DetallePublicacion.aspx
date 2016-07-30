<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="DetallePublicacion.aspx.cs" Inherits="HelpDesk.DetallePublicacion" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="Body" runat="server">

    <h2>Detalle de Publicacion</h2>

    <br />

    <asp:HiddenField ClientIDMode="Static" runat="server" ID="hdnparametro" />


    <fieldset>
        <legend>Encabezado
        </legend>

        <table>

            <tr>
                <td><b>NO.</b></td>
                <td>
                    <asp:Label runat="server" ID="lblNo"></asp:Label>
                </td>
            </tr>
            <tr>
                <td><b>Encabezado</b></td>
                <td>
                    <asp:Label runat="server" ID="lblEncabezado"></asp:Label>
                </td>
            </tr>

            <tr>
                <td><b>Usuario</b></td>
                <td>
                    <asp:Label runat="server" ID="lblUsuario"></asp:Label>
                </td>
            </tr>

            <tr>
                <td><b>Fecha</b></td>
                <td>
                    <asp:Label runat="server" ID="lblFecha"></asp:Label>
                </td>
            </tr>
        </table>



    </fieldset>


    <fieldset>
        <legend>Detalle
        </legend>


        <asp:Label runat="server" ID="lblContenido"></asp:Label>

    </fieldset>

    <br />
    <hr style="width: 95%" />

    <div id="dvComentarios">
        <asp:Repeater runat="server" ID="rpComentarios">
            <ItemTemplate>
                <div class="comentario">
                    <div>
                        <span>
                            <%# Eval("comentario") %>
                        </span>
                    </div>
                    <hr />
                    <div>
                        <span><%# Eval("usuario") %></span>
                        <span><%# string.Format("{0:dd/MMM/yyyy} {0:hh:mm tt}", Eval("fecha")) %></span>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <asp:Panel runat="server" ID="pnlComentar" class="comentar">
        <div>
            <textarea runat="server" ClientIDMode="Static" id="txtcomentar" maxlength="200" placeholder="Comentar..."></textarea>

            <button>Enviar</button>
        </div>

    </asp:Panel>
    <asp:Panel ID="pnlRegistrate" runat="server" Style="width: 95%; margin: 10px auto;">
        <span>Registrate o inicia sesion para dejar un comentario</span>
    </asp:Panel>

</asp:Content>
