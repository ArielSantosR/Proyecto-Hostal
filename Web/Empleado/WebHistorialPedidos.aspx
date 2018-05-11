<%@ Page Title="" Language="C#" MasterPageFile="~/Empleado/EmpleadoM.Master" AutoEventWireup="true" CodeBehind="WebHistorialPedidos.aspx.cs" Inherits="Web.Empleado.WebHistorialPedidos" %>
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

    <div style="display: flex; justify-content: center; margin-bottom: 20px">
        <asp:GridView ID="gvPedidoPendiente" AutoGenerateColumns="false" runat="server" ForeColor="#333333" GridLines="Vertical">      
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
                                        <asp:LinkButton ID="btnInfo"  CssClass="btn btn-info" CommandArgument='<%#Eval("NUMERO_PEDIDO")%>'  text="Ver Detalle" runat="server"/>
                                        <asp:LinkButton ID="btnEditar" CssClass="btn btn-success" CommandArgument='<%#Eval("NUMERO_PEDIDO")%>' text="Editar" runat="server"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
        </div>
    <div style="display: flex; justify-content: center; margin-bottom: 20px">
    <asp:GridView ID="gvPedidoListo" AutoGenerateColumns="false" runat="server" ForeColor="#333333" GridLines="Vertical">      
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
                                <asp:BoundField DataField="COMENTARIO" HeaderText="Comentario" />
                               <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnInfo2"  CssClass="btn btn-info disabled" enabled="false" text="Ver Detalle" runat="server"/>
                                        <asp:LinkButton ID="btnEditar2" CssClass="btn btn-success disabled" Enabled="false" text="Editar" runat="server"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
        </div>

</asp:Content>
