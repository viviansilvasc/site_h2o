<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TempoReal.aspx.cs" Inherits="WebH2O.TempoReal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link href="assets/imagens/icone.png" rel="icon" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="/assets/css/bootstrap.min.css" />

    <title>H2O Web - Consumo Tempo Real</title>
	 <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
   <script type="text/javascript">
      google.charts.load('current', {'packages':['gauge']});
      google.charts.setOnLoadCallback(drawChart);

      function drawChart() {

        var data = google.visualization.arrayToDataTable([
          ['Label', 'Value'],
          ['Ligado', 80],
          ['Desligado', 0]
        ]);

        var options = {
          width: 800, height: 480,
          redFrom: 90, redTo: 100,
          yellowFrom:75, yellowTo: 90,
          minorTicks: 5
        };

        var chart = new google.visualization.Gauge(document.getElementById('chart_div'));

        chart.draw(data, options);

        setInterval(function() {
          data.setValue(0, 1, 40 + Math.round(60 * Math.random()));
          chart.draw(data, options);
        }, 13000);
        setInterval(function() {
          data.setValue(0);
          chart.draw(data, options);
        }, 5000);
      }
    </script>
</head>
<body>
     <form id="form1" runat="server">
        <!-- ********** HEADER ********** -->
        <nav class="navbar navbar-default">
            <div class="container-fluid">

                <div class="navbar-header">
                    <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1">
                        <span class="sr-only">Toggle navigation</span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                    <a href="default.aspx">
                        <img src="assets/imagens/H2O_logo.png" alt="Logo H2O" width="90" /></a>
                </div>


                <div class="navbar-form navbar-collapse" id="bs-example-navbar-collapse-1">
                    <ul class="nav navbar-nav">
                        <li><a href="Relatorios.aspx">Relatório</a></li>
						<li class="active"><a href="TempoReal.aspx">Tempo Real</a></li>
                    </ul>
					<ul class="nav navbar-right" >
						 <asp:Button ID="btnLogout" Text="Logout" CssClass="btn btn-default" OnClick="btnLogout_Click" runat="server" />
					</ul>
                    
                </div>
            </div>
        </nav>
        <!-- ********** /HEADER ********** -->

        <!-- ********** CHARTS ********** -->
        <div id="chart_div" style="width: 100%; height: 100%; margin-left: 20%; margin-bottom: 2.5%"></div>
        <!-- ********** /CHARTS ********** -->

    </form>

    <!-- ********** FOOTER ********** -->
    <div id="rodape">
        <div class="container">
            <div class="row">
                <div class="col-md-6 centered">
                    <img src="/assets/imagens/logo_bandtec.png" alt="BandTec" width="150" />
                </div>
                <div class="col-md-6 centered">
                    <h5>h2o.bandtec@gmail.com</h5>
                </div>
            </div>
        </div>
    </div>
    <!--/F -->
</body>
</html>
