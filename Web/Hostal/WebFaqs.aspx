<%@ Page Title="" Language="C#" MasterPageFile="~/Hostal/HostalM.Master" AutoEventWireup="true" CodeBehind="WebFaqs.aspx.cs" Inherits="Web.Hostal.WebFaqs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="container">
<div class="row main">
    <div class="main-login main-center">
	    <div class="row">
	        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 text-center">
		        <h1>Preguntas Frecuentes </h1>
	        </div>
        </div>
	
        <div class="row">
	        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
		            <ul>
                       <strong> <li data-toggle="collapse" data-target="#mascota" style="cursor: pointer;">¿Puedo alojarme con mi mascota?</li></strong>
                            <div id="mascota" class="collapse">
                                Lamentablemente, según nuestras políticas, no aceptamos el ingreso de animales a nuestras instalaciones.
                            </div>
                        <strong><li data-toggle="collapse" data-target="#reservar" style="cursor: pointer;">¿Cuáles son los pasos para reservar?</li></strong>
                        <div id="reservar" class="collapse">
                            Para reservar primero usted debe crearse una cuenta y luego agregar huéspedes a su cuenta, a continuación podrá realizar una reserva.
                        </div>
                       <strong> <li data-toggle="collapse" data-target="#persona" style="cursor: pointer;">¿Puedo reservar habitaciones para más de una persona?</li></strong>
                        <div id="persona" class="collapse">
                            Usted podrá realizar reservas para cualquier huésped que usted halla agregado anteriormente a su cuenta, nuestra plataforma le permite agregar diferentes huéspedes a la reserva
                            por lo que podrá reservar las habitaciones que usted estime conveniente.

                        </div>
                       <strong> <li data-toggle="collapse" data-target="#categoria" style="cursor: pointer;">¿Cuales son las diferencias entre las categorías de habitaciones?</li></strong>
                        <div id="categoria" class="collapse">
                            En nuestro hostal exiten 5 categorías las cuales se diferencias por los accesorios que contienen las habitaciones.<br />
                           <strong> Habitación Bronce:</strong> <br />  Habitación básica que cuenta con cama, velador y baño privado.<br />
                           <strong>  Habitación Plata:</strong> <br /> Habitación cuenta con cama, velador, baño privado y televisor pequeño con canales nacionales. <br />
                            <strong> Habitación Oro:</strong> <br /> Habitación cuenta con cama, velador, baño privado, televisor mediano con acceso a canales pagados y refrigerador con bebestibles. <br />
                           <strong>  Habitación Platino:</strong> <br /> Habitación cuenta con cama, velador, baño privado, televisor grande con acceso a canales pagados, refrigerador con bebestibles y acceso a sala de reuniones del hostal. <br />
                           <strong>  Habitación Diamante:</strong> <br /> Habitación más lujosa del hosta, cuenta con todos los accesorios de la habitación platino a demás de contar con jacuzzi y servicio de chofer.<br />
                            <small>Bebestibles y servicio de chofer se cobran por separado.</small>

                        </div>
                       <strong> <li data-toggle="collapse" data-target="#dolares" style="cursor: pointer;">¿Se puede pagar con dólares?</li></strong>
                        <div id="dolares" class="collapse">
                            Nuestra plataforma está creada para mostrarle a los clientes que lo necesiten el valor de los servicios convertidos en dólares.
                        </div>
                       <strong> <li data-toggle="collapse" data-target="#cancelar" style="cursor: pointer;">¿Que hago si quiero cancelar mi reserva?</li></strong>
                        <div id="cancelar" class="collapse">
                            Usted podrá cancelar su reserva a través de la plataforma en el historial de sus reservas.
                        </div>
		            </ul>
	        </div>
        </div>
    </div>
</div>
</div>
</asp:Content>
