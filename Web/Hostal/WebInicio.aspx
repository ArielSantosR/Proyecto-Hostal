<%@ Page Title="" Language="C#" MasterPageFile="~/Hostal/HostalM.Master" AutoEventWireup="true" CodeBehind="WebInicio.aspx.cs" Inherits="Web.WebInicio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../css/slider.css"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<!--        FIN DE HEADER        -->

<!--        COMIENZO DE BANNER        -->
<div class="container">
<div class="slideshow-container">

	<div class="mySlides fade">
	  <div class="numbertext">1 / 3</div>
	  <img src="../images/imagen1.jpg" style="width:100%"/>
	  <div class="text">Hostal</div>
	</div>

	<div class="mySlides fade">
	  <div class="numbertext">2 / 3</div>
	  <img src="../images/minuta.jpg" style="width:100%"/>
	  <div class="text">Minutas Personalizadas</div>
	</div>

	<div class="mySlides fade">
	  <div class="numbertext">3 / 3</div>
	  <img src="../images/accesorios.jpg" style="width:100%"/>
	  <div class="text">Elige los Accesorios que desees</div>
	</div>

	<a class="prev" onclick="plusSlides(-1)">&#10094;</a>
	<a class="next" onclick="plusSlides(1)">&#10095;</a>

</div>
	<br/>

	<div style="text-align:center">
	  <span class="dot" onclick="currentSlide(1)"></span> 
	  <span class="dot" onclick="currentSlide(2)"></span> 
	  <span class="dot" onclick="currentSlide(3)"></span> 
    </div>
<!--        FIN DE BANNER        -->
<!--        COMIENZO DE CONTENIDO        -->
<div class="secciones row">
    <div class="seccion col-md-4">
	
		<div>
        <img src="../images/habitacion1.jpeg"/>
        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. 
            Pellentesque cursus tortor nec elit porta, cursus tincidunt 
            dolor commodo. Orci varius natoque penatibus et magnis dis 
            parturient montes, nascetur ridiculus mus. Nam tincidunt, 
            lacus ut fermentum facilisis, urna odio cursus felis, quis gravida 
            purus tellus at magna. In id iaculis nunc.</p>
		</div>
    </div>
    <div class="seccion col-md-4">
        <img src="../images/habitacion2.jpg"/>
        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. 
                Pellentesque cursus tortor nec elit porta, cursus tincidunt 
                dolor commodo. Orci varius natoque penatibus et magnis dis 
                parturient montes, nascetur ridiculus mus. Nam tincidunt, 
                lacus ut fermentum facilisis, urna odio cursus felis, quis gravida 
                purus tellus at magna. In id iaculis nunc.</p>
    </div>
    <div class="seccion col-md-4">
        <img src="../images/habitacion3.jpg"/>
        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. 
                Pellentesque cursus tortor nec elit porta, cursus tincidunt 
                dolor commodo. Orci varius natoque penatibus et magnis dis 
                parturient montes, nascetur ridiculus mus. Nam tincidunt, 
                lacus ut fermentum facilisis, urna odio cursus felis, quis gravida 
                purus tellus at magna. In id iaculis nunc.</p>
    </div>
</div>
</div>
<!--        FIN DE CONTENIDO  -->

<!--        MAPA      -->

	<iframe src="https://www.google.com/maps/embed?pb=!1m14!1m8!1m3!1d13323.400180015154!2d-70.5935865!3d-33.4010762!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x9662cf255f110d3f%3A0xde0a6462bd1cdae5!2sHotel+Kennedy!5e0!3m2!1ses-419!2scl!4v1523495975450" width="100%" height="220" style="text-align: center; bottom:0; margin-top: 20px; margin-bottom: 20px;"></iframe>


  <script src="../js/scripts.js"></script>
</asp:Content>

