<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.Master" CodeBehind="Solicitudes.aspx.cs" Inherits="HelpDesk.Solicitudes" %>

<asp:Content runat="server" ContentPlaceHolderID="head">

</asp:Content>


<asp:Content runat="server" ContentPlaceHolderID="Body">
    <h2>Solicitudes
            <asp:Panel runat="server" ID="Panel1" style="float:right;" CssClass="contexmenu">
                            <asp:Label runat="server" ID="lblFIltro" Text="No Asignados" style="font-size:16px;"></asp:Label> <i>▼</i>
                                    <ul>

                                        <li>
                                            <a href="solicitudes.aspx?fn=No_Asignados">
                                                No Asignados
                                            </a>

                                        </li>
                                        <li>
                                            <a href="solicitudes.aspx?fn=Asignados">
                                                Asignados
                                            </a>

                                        </li>
                                        <li>
                                            <a href="solicitudes.aspx?fn=Abiertos">
                                                Abiertos
                                            </a>

                                        </li>

                                        <li>
                                            <a href="solicitudes.aspx?fn=Cerrados">
                                                Cerrados
                                            </a>

                                        </li>
                                        <li>
                                            <a href="solicitudes.aspx?fn=Suspendidos">
                                                Suspendidos
                                            </a>

                                        </li>
                                    </ul>

                        </asp:Panel>
    </h2>

    <div id="dvMessage" style="background-color: green;text-align: center; color: #fff; font-size: 20px;"></div>
    <br />


    <asp:Repeater runat="server" ID="rpPublicaciones">
        <HeaderTemplate>
    <table class="publicaciones">
        <thead>
            <tr>
                <th  style="width:137px;">
                    No. Solicitud
                </th>
                <th>
                    Titulo
                </th>
                <th>
                    Departamento
                </th>

                <th>
                    Problema
                </th>

                <th>
                    Estado
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
                    <%# Eval("solicitud_id") %>
                </td>
                <td>
                    <%# Eval("Titulo") %>

                </td>

                  <td>
                    <%# Eval("departamento") %>

                </td>

                  <td>
                    <%# Eval("problema") %>

                </td>

                <td>
                    <%# Eval("estado") %>

                </td>

                <td>
                    <%# Eval("usuario") %>
                </td>

                <td>
                    <%# string.Format("{0:dd/ MMM / yyyy}",Eval("fecha")) %>
                </td>

                <td>
                    <a href="DetalleSolicitud.aspx?sid=<%#  Eval("solicitud_id") %>">
                        ver
                    </a>

                </td>
            </tr>
        </ItemTemplate>

        <FooterTemplate>

        </tbody>
    </table>
 </FooterTemplate>


    </asp:Repeater>




</asp:Content>