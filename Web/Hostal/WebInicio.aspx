<%@ Page Title="" Language="C#" MasterPageFile="~/Hostal/HostalM.Master" AutoEventWireup="true" CodeBehind="WebInicio.aspx.cs" Inherits="Web.WebInicio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<!--        FIN DE HEADER        -->

<!--        COMIENZO DE BANNER        -->
    <style>
  /* Make the image fully responsive */
  .carousel-inner img {
      width: 100%;
      height: 100%;
  }
  </style>

    <div id="demo" class="carousel slide" data-ride="carousel">

  <!-- Indicators -->
  <ul class="carousel-indicators">
    <li data-target="#demo" data-slide-to="0" class="active"></li>
    <li data-target="#demo" data-slide-to="1"></li>
    <li data-target="#demo" data-slide-to="2"></li>
  </ul>
  
  <!-- The slideshow -->
  <div class="carousel-inner">
    <div class="carousel-item active">
      <img src="../images/imagen1.jpg" alt="Hostal"/>
        <div class="carousel-caption" style="background: rgba(64, 66, 66,.8)">
            <h3>Hostal</h3>
            <p>Visite Nuestro Hostal</p>
      </div>  
    </div>
    <div class="carousel-item">
      <img src="../images/minuta.jpg" alt="Minutas Personalizadas" width="1000" height="350"/>
        <div class="carousel-caption" style="background: rgba(64, 66, 66,.8)">
            <h3>Minutas Personalizadas</h3>
            <p>Elija las Minutas de su preferencia</p>
      </div>  
    </div>
    <div class="carousel-item">
      <img src="../images/accesorios.jpg" alt="Elige los Accesorios que desees" width="1000" height="350"/>
        <div class="carousel-caption" style="background: rgba(64, 66, 66,.8)">
        <h3>Distintas habitaciones</h3>
        <p>Elige la habitación que desees</p>
      </div>  
    </div>
  </div>
  <!-- Left and right controls -->
  <a class="carousel-control-prev" href="#demo" data-slide="prev">
    <span class="carousel-control-prev-icon"></span>
  </a>
  <a class="carousel-control-next" href="#demo" data-slide="next">
    <span class="carousel-control-next-icon"></span>
  </a>
</div>

<!--        FIN DE BANNER        -->
<!--        COMIENZO DE CONTENIDO        -->
<div class="container">
<div class="secciones row">
    <div class="seccion col-md-4">
	
		<div>
        <img src="../images/habitacion1.jpeg"/>
        <p>Grandes habitaciones amplias e iluminadas, perfectas para darte el mejor confort durante tu estadía.</p>
		</div>
    </div>
    <div class="seccion col-md-4">
        <img src="../images/habitacion2.jpg"/>
        <p>Ofecemos un especial servicio de alimentación para quienes deseen llegar a su habitación y que la comida los esté esperando.</p>
    </div>
    <div class="seccion col-md-4">
        <img src="../images/habitacion3.jpg"/>
        <p>Una ubicación inmejorable, nunca tendrá problemas para llegar a su destino.</p>
    </div>
</div>
</div>
<!--        FIN DE CONTENIDO  -->

<!--        MAPA      -->

	<iframe src="https://www.google.com/maps/embed?pb=!1m14!1m8!1m3!1d13323.400180015154!2d-70.5935865!3d-33.4010762!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x9662cf255f110d3f%3A0xde0a6462bd1cdae5!2sHotel+Kennedy!5e0!3m2!1ses-419!2scl!4v1523495975450" width="100%" height="220" style="text-align: center; bottom:0; margin-top: 20px; margin-bottom: 42px;"></iframe>

<script type="text/javascript">
        $('.carousel').carousel({
          interval: 200
        })
</script>

</asp:Content>

