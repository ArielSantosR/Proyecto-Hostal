<%@ Page Title="" Language="C#" MasterPageFile="~/Cliente/ClienteM.Master" AutoEventWireup="true" CodeBehind="WebVerHistorial.aspx.cs" Inherits="Web.Cliente.WebVerHistorial" %>

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

    <div class="container">
        <div class="row" style="margin-top: 20px; margin-bottom: 20px;">
            <div class="main-center">
                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 text-center">
                        <h1>Historial de Reserva</h1>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <% if (gvOrdenAsignada.Rows.Count == 0) { %>
    <div class="container">
        <div class="row" style="margin-top: 20px; margin-bottom: 20px;">
            <div class="main-center-wide">
                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 text-center">
                        <h1>No tiene reservas asignadas</h1>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <% } else { %>

    <div class="container">
        <div class="row" style="margin-top: 20px; margin-bottom: 20px;">
            <div class="main-center-wide">
                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 text-center">
                        <h1>Reservas Asignadas</h1>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div style="display: flex; justify-content: center; margin-bottom: 70px">
        <asp:GridView ID="gvOrdenAsignada" AutoGenerateColumns="false" runat="server" ForeColor="#333333" GridLines="Vertical">
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
                <asp:BoundField DataField="NUMERO_ORDEN" HeaderText="Número de Reserva" />
                <asp:BoundField DataField="CANTIDAD_HUESPEDES" HeaderText="Cantidad de Huéspedes" />
                <asp:BoundField DataField="FechaLlegada" HeaderText="Fecha de Llegada" />
                <asp:BoundField DataField="FechaSalida" HeaderText="Fecha de Salida" />
                <asp:BoundField DataField="RUT_EMPLEADO" HeaderText="RUT Empleado" />
                <asp:BoundField DataField="RUT_CLIENTE" HeaderText="RUT Cliente" />
                <asp:BoundField DataField="ESTADO_ORDEN" HeaderText="Estado Orden" />
                <asp:BoundField DataField="COMENTARIO" HeaderText="Comentario" />
                <asp:BoundField DataField="MONTO_TOTAL" HeaderText="Monto Total" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnInfo3" CssClass="btn btn-info" CommandArgument='<%#Eval("RUT_CLIENTE")%>' OnClick="btnInfo3_Click" Text="Ver Detalle" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <% } %>

    <% if (gvOrdenPendiente.Rows.Count == 0) { %>
    <div class="container">
        <div class="row" style="margin-top: 20px; margin-bottom: 20px;">
            <div class="main-center-wide">
                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 text-center">
                        <h1>No tiene reservas pendientes</h1>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <% } else { %>
    <div class="container">
        <div class="row" style="margin-top: 20px; margin-bottom: 20px;">
            <div class="main-center-wide">
                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 text-center">
                        <h1>Reservas Pendientes</h1>
                    </div>
                </div>
            </div>
        </div>
    </div>
                <div style="display: flex; justify-content: center; margin-bottom: 70px">
                    <asp:GridView ID="gvOrdenPendiente" AutoGenerateColumns="false" runat="server" ForeColor="#333333" GridLines="Vertical">
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
                            <asp:BoundField DataField="NUMERO_ORDEN" HeaderText="Número de Reserva" />
                            <asp:BoundField DataField="CANTIDAD_HUESPEDES" HeaderText="Cantidad de Huéspedes" />
                            <asp:BoundField DataField="FechaLlegada" HeaderText="Fecha de Llegada" />
                            <asp:BoundField DataField="FechaSalida" HeaderText="Fecha de Salida" />
                            <asp:BoundField DataField="RUT_EMPLEADO" HeaderText="RUT Empleado" />
                            <asp:BoundField DataField="RUT_CLIENTE" HeaderText="RUT Cliente" />
                            <asp:BoundField DataField="ESTADO_ORDEN" HeaderText="Estado Orden" />
                            <asp:BoundField DataField="COMENTARIO" HeaderText="Comentario" />
                            <asp:BoundField DataField="MONTO_TOTAL" HeaderText="Monto Total" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnInfo2" CssClass="btn btn-info" CommandArgument='<%#Eval("NUMERO_ORDEN")%>' OnClick="btnInfo2_Click" Text="Detalles" runat="server" />
                                    <asp:LinkButton ID="btnEliminar" CssClass="btn btn-danger" CommandArgument='<%#Eval("NUMERO_ORDEN")%>' OnClick="btnCancelar_Click" Text="Cancelar" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
    <% } %>

    <% if (gvOrden.Rows.Count == 0) { %>
    <div class="container">
        <div class="row" style="margin-top: 20px; margin-bottom: 20px;">
            <div class="main-center-wide">
                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 text-center">
                        <h1>No hay Reservas Finalizadas</h1>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <% } else { %>
    <div class="container">
        <div class="row" style="margin-top: 20px; margin-bottom: 20px;">
            <div class="main-center-wide">
                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 text-center">
                        <h1>Reservas Finalizadas</h1>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div style="display: flex; justify-content: center; margin-bottom: 70px">
        <asp:GridView ID="gvOrden" AutoGenerateColumns="false" runat="server" ForeColor="#333333" GridLines="Vertical">
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
                <asp:BoundField DataField="NUMERO_ORDEN" HeaderText="Número de Reserva" />
                <asp:BoundField DataField="CANTIDAD_HUESPEDES" HeaderText="Cantidad de Huéspedes" />
                <asp:BoundField DataField="FechaLlegada" HeaderText="Fecha de Llegada" />
                <asp:BoundField DataField="FechaSalida" HeaderText="Fecha de Salida" />
                <asp:BoundField DataField="RUT_EMPLEADO" HeaderText="RUT Empleado" />
                <asp:BoundField DataField="RUT_CLIENTE" HeaderText="RUT Cliente" />
                <asp:BoundField DataField="ESTADO_ORDEN" HeaderText="Estado Orden" />
                <asp:BoundField DataField="COMENTARIO" HeaderText="Comentario" />
                <asp:BoundField DataField="MONTO_TOTAL" HeaderText="Monto Total" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnInfo2" CssClass="btn btn-info" CommandArgument='<%#Eval("NUMERO_ORDEN")%>' OnClick="btnInfo2_Click" Text="Ver Detalle" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <% } %>

    <asp:ScriptManager runat="server"></asp:ScriptManager>

    <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Detalle Pedido</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div style="display: flex; justify-content: center; margin-bottom: 70px">
                        <asp:GridView ID="gvDetalle" AutoGenerateColumns="false" runat="server" ForeColor="#333333" GridLines="Vertical">
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
                                <asp:BoundField DataField="ID_DETALLE" HeaderText="ID Detalle" />
                                <asp:BoundField DataField="NUMERO_ORDEN" HeaderText="Número de Orden" />
                                <asp:BoundField DataField="RUT_HUESPED" HeaderText="RUT Huésped" />
                                <asp:BoundField DataField="ID_CATEGORIA_HABITACION" HeaderText="ID Categoría" />
                                <asp:BoundField DataField="ID_PENSION" HeaderText="ID Pensión" />
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

    <div class="modal fade" id="exampleModal2" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel2">Habitación Asignada</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div style="display: flex; justify-content: center; margin-bottom: 70px">
                        <asp:GridView ID="gvDetalle2" AutoGenerateColumns="false" runat="server" ForeColor="#333333" GridLines="Vertical">
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
                                <asp:BoundField DataField="ID_DETALLE_H" HeaderText="ID Detalle" />
                                <asp:BoundField DataField="NUMERO_HABITACION" HeaderText="Número de Habitación" />
                                <asp:BoundField DataField="RUT_CLIENTE" HeaderText="RUT Cliente" />
                                <asp:BoundField DataField="RUT_HUESPED" HeaderText="RUT Huésped" />
                                <asp:BoundField DataField="ID_PENSION" HeaderText="ID Pensión" />
                                <asp:BoundField DataField="FechaLlegada" HeaderText="Fecha de Llegada" />
                                <asp:BoundField DataField="FechaSalida" HeaderText="Fecha de Salida" />
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
