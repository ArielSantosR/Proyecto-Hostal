<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador/AdminM.Master" AutoEventWireup="true" CodeBehind="WebVerPedido.aspx.cs" Inherits="Web.Administrador.WebVerPedido" %>
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

    <% if (gvPedido.Rows.Count != 0) { %>
    <div class="container">
        <div class="row" style="margin-top: 20px; margin-bottom: 20px;">
            <div class="main-center">
	            <div class="row">
	                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 text-center">
		                <h1>Pedidos Pendientes</h1>
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
		                <h1>No hay ningún pedido Pendiente</h1>
	                </div>
                </div>
            </div>
        </div>
    </div>
    <% } %>

    <div style="display: flex; justify-content: center; margin-bottom: 20px">
        <asp:GridView ID="gvPedido" AutoGenerateColumns="false" runat="server" ForeColor="#333333" GridLines="Vertical">      
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
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnInfo" OnClick="btnInfo_Click"  CssClass="btn btn-info" CommandArgument='<%#Eval("NUMERO_PEDIDO")%>'  text="Ver Detalle" runat="server"/>
                                        <asp:LinkButton ID="btnEditar" OnClick="btnEditar_Click" CssClass="btn btn-success" CommandArgument='<%#Eval("NUMERO_PEDIDO")%>'  text="Aceptar" runat="server"/>
                                        <asp:LinkButton ID="btnEliminar" onclick="btnEliminar_Click" CssClass="btn btn-danger" CommandArgument='<%#Eval("NUMERO_PEDIDO")%>'  text="Rechazar" runat="server"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
        </div>

     <div class="modal fade" id="exampleModal2" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-lg" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="exampleModal2Label">Detalle Pedido</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
           <div style="display: flex; justify-content: center; margin-bottom: 70px">
        <asp:GridView ID="gvDetalleHistorial" AutoGenerateColumns="false" runat="server" ForeColor="#333333" GridLines="Vertical">      
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
                                <asp:BoundField DataField="ID_DETALLE_PEDIDO" HeaderText="Detalle Pedido" />
                                <asp:BoundField DataField="NUMERO_PEDIDO" HeaderText="Número de Pedido" />
                                <asp:BoundField DataField="ID_PRODUCTO" HeaderText="ID Producto" />
                                <asp:BoundField DataField="CANTIDAD" HeaderText="Cantidad" />
                            </Columns>
                        </asp:GridView>
        </div>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
      </div>
    </div>
  </div>
</div>

</asp:Content>
