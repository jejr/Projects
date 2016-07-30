<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="DetalleSolicitud.aspx.cs" Inherits="HelpDesk.DetalleSolicitud" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="Body" runat="server">

    <h2>Detalle de Solicitud</h2>

    <br />

    <asp:HiddenField ClientIDMode="Static" runat="server" ID="hdnparametro" />

    <div runat="server" id="dvCont">
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
                    <td><b>Estado</b></td>
                    <td>
                        <asp:Label runat="server" ID="lblEstado"></asp:Label>
                    </td>
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                    <td>
                        <asp:Panel runat="server" ID="pnlNotificacion" CssClass="contexmenu">
                            Asignar a <i>▼</i>
                            <asp:Repeater runat="server" ID="rpUsuarios">
                                <HeaderTemplate>
                                    <ul>
                                </HeaderTemplate>

                                <ItemTemplate>
                                    <li>
                                        <a href="DetalleSolicitud.aspx?sid=<%# Request["sid"] %>&uid=<%# Eval("usuario_id")%>">
                                            <%# Eval("usuario") %>
                                        </a>

                                    </li>
                                </ItemTemplate>

                                <FooterTemplate>
                                    </ul>
                                </FooterTemplate>
                            </asp:Repeater>


                        </asp:Panel>
                    </td>

                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                    <td>
                        <asp:Panel runat="server" ID="Panel1" CssClass="contexmenu">
                            Opciones <i>▼</i>
                                    <ul>

                                        <li runat="server" id="liCerrar">
                                            <a href="DetalleSolicitud.aspx?sid=<%= Request["sid"] %>&esid=<%= (int)HelpDesk.Enums.Estado.Cerrado %>">
                                                Cerrar
                                            </a>

                                        </li>
                                        <li runat="server" id="liCerrarSn">
                                            <a href="DetalleSolicitud.aspx?sid=<%= Request["sid"] %>&esid=<%= (int)HelpDesk.Enums.Estado.Cerrado %>&ntf=0">
                                                Cerrar sin notificar
                                            </a>

                                        </li>
                                        <li runat="server" id="liSuspend">
                                            <a href="DetalleSolicitud.aspx?sid=<%= Request["sid"] %>&esid=<%= (int)HelpDesk.Enums.Estado.Suspendido %>">
                                                Suspender
                                            </a>

                                        </li>
                                        <li runat="server" id="liDesasig">
                                            <a href="DetalleSolicitud.aspx?sid=<%= Request["sid"] %>&esid=0">
                                                Desasignar
                                            </a>

                                        </li>
                                        <li runat="server" id="liReabrir">
                                            <a href="DetalleSolicitud.aspx?sid=<%= Request["sid"] %>&esid=<%= (int)HelpDesk.Enums.Estado.Abierto %>">
                                                Reabrir
                                            </a>

                                        </li>
                                        <li runat="server" id="liAbrir">
                                            <a href="DetalleSolicitud.aspx?sid=<%= Request["sid"] %>&esid=<%= (int)HelpDesk.Enums.Estado.Abierto %>">
                                                Abrir
                                            </a>

                                        </li>
                                    </ul>

                        </asp:Panel>
                    </td>

                </tr>

                <tr>
                    <td><b>Titulo</b></td>
                    <td>
                        <asp:Label runat="server" ID="lblTitulo"></asp:Label>
                    </td>
                    <td></td>
                    <td></td>
                    <td>&nbsp;</td>
                </tr>

                <tr>
                    <td><b>Departamento</b></td>
                    <td>
                        <asp:Label runat="server" ID="lblDepartamento"></asp:Label>
                    </td>
                    <td></td>
                    <td><b>Asignado a</b></td>
                    <td>
                        <asp:Label runat="server" ID="lblUsuarioAsignado"></asp:Label></td>
                </tr>


                <tr>
                    <td><b>Problema</b></td>
                    <td>
                        <asp:Label runat="server" ID="lblProblema"></asp:Label>
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
                            <br />
                            <br />
                            <a runat="server" target="_blank" href='<%# Eval("rutaFile") %>' visible='<%# (Eval("rutaFile").ToString() != "" ) %>'>Descargar Archivo.
                            </a>
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
                <ctl:TextBoxLast ID="txtcomentar" runat="server" TextMode="MultiLine" ClientIDMode="Static" CanEmpty="false" Mensaje="Debe especificar el comentario" MaxLength="200" placeholder="Comentar..."></ctl:TextBoxLast>

                <input type="file" runat="server" id="flArchivo" />
                <br />
                <br />
                <ctl:ButtonLast runat="server" ID="btnComentar" Text="Comentar" OnClick="btnComentar_Click" />
            </div>

        </asp:Panel>

    </div>
</asp:Content>
