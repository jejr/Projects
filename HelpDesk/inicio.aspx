<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.Master" CodeBehind="inicio.aspx.cs" Inherits="HelpDesk.inicio" %>

<asp:Content runat="server" ContentPlaceHolderID="head">

</asp:Content>


<asp:Content runat="server" ContentPlaceHolderID="Body">
    <h2>Publicaciones</h2>

    <div id="dvMessage" style="background-color: green;text-align: center; color: #fff; font-size: 20px;"></div>
    <br />


    <asp:Repeater runat="server" ID="rpPublicaciones">
        <HeaderTemplate>
    <table class="publicaciones">
        <thead>
            <tr>
                <th  style="width:137px;">
                    No. Publicacion
                </th>
                <th>
                    Encabezado
                </th>

                <th style="width:200px;">
                    Usuario
                </th>

                <th style="width:125px;">
                    Fecha
                </th>
                <th style="width:50px;">

                </th>
            </tr>
        </thead>
        <tbody>

        </HeaderTemplate>
        
        <ItemTemplate>

            <tr>

                <td>
                    <%# Eval("publicacion_id") %>
                </td>
                <td>
                    <%# Eval("encabezado") %>

                </td>
                <td>
                    <%# Eval("usuario") %>
                </td>

                <td>
                    <%# string.Format("{0:dd/ MMM / yyyy}",Eval("fecha")) %>
                </td>

                <td>
                    <a href="DetallePublicacion.aspx?pid=<%#  Eval("publicacion_id") %>">
                        ver
                    </a>

                    <div runat="server" visible='<%# Session["user"] != null %>' class="contexmenu">
                      <i>▼</i>  

                        <ul>
                            <li>
                                <a data-pid href="fn.aspx?fn=spid&pid=<%#  Eval("publicacion_id") %>">
                                    Seguir esta publicacion
                                </a>
                            </li>

                             <li>
                                <a data-suid href="fn.aspx?fn=suid&uid=<%#  Eval("usuario_id") %>"> 
                                    Seguir esta persona
                                </a>
                            </li>
                        </ul>
                    </div>
                </td>
            </tr>
        </ItemTemplate>

        <FooterTemplate>

        </tbody>
    </table>
 </FooterTemplate>


    </asp:Repeater>




</asp:Content>