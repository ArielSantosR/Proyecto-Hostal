<%@ Page Title="" Language="C#" MasterPageFile="~/Proveedor/ProveedorM.Master" AutoEventWireup="true" CodeBehind="WebHistorialPedidos.aspx.cs" Inherits="Web.Proveedor.WebDespachados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="alert alert-danger alert-dismissible" id="alerta" runat="server">
        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
        <strong>Error!</strong>
        <asp:Literal ID="error" runat="server"></asp:Literal>
    </div>

    <div class="alert alert-success alert-dismissible" id="alerta_exito" runat="server">
        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
        <strong>Éxito!</strong>
        <asp:Literal ID="exito" runat="server"></asp:Literal>
    </div>

    <% if (gvPedidoDespacho.Rows.Count != 0) { %>
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
                        <asp:LinkButton ID="btnInfo" CssClass="btn btn-primary" CommandArgument='<%#Eval("NUMERO_PEDIDO")%>' Text="Despachar" OnClick="btnInfo_Click" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

    <% if (gvPedidoListo.Rows.Count != 0) { %>
    <div class="container">
        <div class="row" style="margin-top: 20px; margin-bottom: 20px;">
            <div class="main-center">
                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 text-center">
                        <h1>Historial de Pedidos</h1>
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
                        <h1>No tiene ningún Pedido en su historial</h1>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <% } %>

    <div style="display: flex; justify-content: center; margin-bottom: 70px">
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
                <asp:BoundField DataField="ESTADO_DESPACHO" HeaderText="Estado de Despacho" />
                <asp:BoundField DataField="COMENTARIO" HeaderText="Comentario" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnInfo2" CssClass="btn btn-info" CommandArgument='<%#Eval("NUMERO_PEDIDO")%>' OnClick="btnInfo2_Click" Text="Ver Detalle" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

    <asp:ScriptManager runat="server"></asp:ScriptManager>

    <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel" runat="server"></h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div style="display: flex; justify-content: center; margin-bottom: 70px">
                        <asp:GridView ID="gvDetalle" AutoGenerateColumns="true" runat="server" ForeColor="#333333" GridLines="Vertical">
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
