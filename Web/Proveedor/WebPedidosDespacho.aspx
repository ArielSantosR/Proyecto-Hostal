﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Proveedor/ProveedorM.Master" AutoEventWireup="true" CodeBehind="WebPedidosDespacho.aspx.cs" Inherits="Web.Proveedor.WebPedidosDespacho" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="alert alert-danger alert-dismissible" id="alerta" runat="server">
  <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
  <strong>Error!</strong> <asp:Literal ID="error" runat="server"></asp:Literal>
</div>

<div class="alert alert-success alert-dismissible" id="alerta_exito" runat="server">
  <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
  <strong>Éxito!</strong> <asp:Literal ID="exito" runat="server"></asp:Literal>
</div>

    <% if (gvPedidoDespacho.Rows.Count != 0) { %>
    <div class="container">
        <div class="row" style="margin-top: 20px; margin-bottom: 70px;">
            <div class="main-center">
	            <div class="row">
	                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 text-center">
		                <h1>Pedidos por Despachar</h1>
	                </div>
                </div>
            </div>
        </div>
    </div>
    <% } else { %>
    <div class="container">
        <div class="row" style="margin-top: 20px; margin-bottom: 20px;">
            <div class="main-center">
	            <div class="row">
	                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 text-center">
		                <h3>No tiene pedidos por despachar</h3>
	                </div>
                </div>
            </div>
        </div>
    </div>
    <% } %>

    <div style="display: flex; justify-content: center; margin-bottom: 70px">
        <asp:GridView ID="gvPedidoDespacho" AutoGenerateColumns="false" runat="server" ForeColor="#333333" GridLines="Vertical">      
                            <AlternatingRowStyle BackColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                            <Columns>
                                <asp:BoundField DataField="NUMERO_PEDIDO" HeaderText="Número de Pedido" />
                                <asp:BoundField DataField="FECHA_PEDIDO" HeaderText="Fecha de Pedido" />
                                <asp:BoundField DataField="ESTADO_PEDIDO" HeaderText="Estado de Pedido" />
                                <asp:BoundField DataField="NUMERO_RECEPCION" HeaderText="Número de Recepción" />
                                <asp:BoundField DataField="RUT_EMPLEADO" HeaderText="RUT Empleado" />
                                <asp:BoundField DataField="RUT_PROVEEDOR" HeaderText="RUT Proveedor" />
                                <asp:BoundField DataField="ESTADO_DESPACHO" HeaderText="Estado de Despacho" />
                                <asp:BoundField DataField="COMENTARIO" HeaderText="Comentario" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnInfo2"  CssClass="btn btn-info" CommandArgument='<%#Eval("NUMERO_PEDIDO")%>' OnClick="btnInfo2_Click" text="Ver Detalle" runat="server"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
        </div>

</asp:Content>