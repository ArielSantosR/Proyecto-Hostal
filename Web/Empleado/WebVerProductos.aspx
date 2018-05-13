<%@ Page Title="" Language="C#" MasterPageFile="~/Empleado/EmpleadoM.Master" AutoEventWireup="true" CodeBehind="WebVerProductos.aspx.cs" Inherits="Web.Empleado.WebVerProductos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <% if (gvProducto.Rows.Count != 0) { %>
    <div class="container">
        <div class="row" style="margin-top: 20px; margin-bottom: 20px;">
            <div class="main-center">
	            <div class="row">
	                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 text-center">
		                <h1>Pedidos por Despachar</h1>
	                </div>
                </div>
            </div>
        </div>
    </div>
    <% } %>

    <div style="display: flex; justify-content: center; margin-bottom: 70px">
    <asp:GridView ID="gvProducto" AutoGenerateColumns="false" runat="server" ForeColor="#333333" GridLines="Vertical">      
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
                                 <asp:BoundField DataField="ID_PRODUCTO" HeaderText="Código Producto" />
                                <asp:BoundField DataField="NOMBRE_PRODUCTO" HeaderText="Nombre" />
                                <asp:BoundField DataField="PRECIO_PRODUCTO" HeaderText="Precio" />
                                <asp:BoundField DataField="DESCRIPCION_PRODUCTO" HeaderText="Descripción" />
                                <asp:BoundField DataField="STOCK_PRODUCTO" HeaderText="Stock" />
                                <asp:BoundField DataField="STOCK_CRITICO_PRODUCTO" HeaderText="Stock Crítico" />
                                <asp:BoundField DataField="ID_FAMILIA" HeaderText="ID Familia" />
                                <asp:BoundField DataField="FECHA_VENCIMIENTO_PRODUCTO" HeaderText="Fecha de Vencimiento" />
                            </Columns>
                        </asp:GridView>
        </div>  
</asp:Content>
