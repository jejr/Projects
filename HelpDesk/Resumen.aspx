<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Resumen.aspx.cs" Inherits="HelpDesk.Resumen" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="Body" runat="server">
    
    <asp:HiddenField runat="server" id="hdnValue" ClientIDMode="Static"/>

    <h2>Resumen de Solicitudes</h2>


    <br />

    <canvas id="Informe" width="600" height="400"></canvas>
    <div class="Leyenda">
        <ul>
            <li>
                <i style="background-color:#ff8153"></i>
                Abiertos
            </li>
            <li>
                  <i style="background-color:#4acab4"></i>
                Cerrados
            </li>
            <li>
                  <i style="background-color:#ffea88"></i>
                Suspendidos
            </li>
        </ul>
    </div>
    <script src="Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="Scripts/Chart.js" type="text/javascript"></script>
    <script type="text/javascript">
        var Informe = document.getElementById('Informe').getContext('2d');
        var pieData = $.parseJSON(document.forms[0].hdnValue.value);

        new Chart(Informe, null).Pie(pieData, {
            legend: {
                    display: true,
                    labels: {
                    fontColor: 'rgb(255, 99, 132)'
                    }
            }});
    </script>

</asp:Content>
