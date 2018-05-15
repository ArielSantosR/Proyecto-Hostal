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
                            lalalalalalalala
                        </div>
                       <strong> <li data-toggle="collapse" data-target="#persona" style="cursor: pointer;">¿Puedo reservar habitaciones para mas de una persona?</li></strong>
                        <div id="persona" class="collapse">
                            lalalalalalalal
                        </div>
                       <strong> <li data-toggle="collapse" data-target="#categoria" style="cursor: pointer;">¿Cuales son las diferencias entre las categorías de habitaciones?</li></strong>
                        <div id="categoria" class="collapse">
                            lalalalalalal
                        </div>
                       <strong> <li data-toggle="collapse" data-target="#dolares" style="cursor: pointer;">¿Se puede pagar con dólares?</li></strong>
                        <div id="dolares" class="collapse">
                            lalalalallaala
                        </div>
                       <strong> <li data-toggle="collapse" data-target="#cancelar" style="cursor: pointer;">¿Que hago si quiero cancelar mi reserva?</li></strong>
                        <div id="cancelar" class="collapse">
                            alalalalaalal
                        </div>
		            </ul>
	        </div>
        </div>
    </div>
</div>
</div>
</asp:Content>
