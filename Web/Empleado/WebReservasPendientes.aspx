<%@ Page Title="" Language="C#" MasterPageFile="~/Empleado/EmpleadoM.Master" AutoEventWireup="true" CodeBehind="WebReservasPendientes.aspx.cs" Inherits="Web.Empleado.WebRegistrarOrden" %>

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

    <% if (gvOrden.Rows.Count != 0) { %>
    <div class="container">
        <div class="row" style="margin-top: 20px; margin-bottom: 20px;">
            <div class="main-center-wide">
                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 text-center">
                        <h1>Reservas Pendientes</h1>
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
                            <asp:BoundField DataField="FECHA_LLEGADA" HeaderText="Fecha de Llegada" />
                            <asp:BoundField DataField="FECHA_SALIDA" HeaderText="Fecha de Salida" />
                            <asp:BoundField DataField="EMPLEADO" HeaderText="Empleado que Acepto" />
                            <asp:BoundField DataField="CLIENTE" HeaderText="Cliente" />
                            <asp:BoundField DataField="ESTADO_ORDEN" HeaderText="Estado Orden" />
                            <asp:BoundField DataField="COMENTARIO" HeaderText="Comentario" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnInfo2" CssClass="btn btn-info" CommandArgument='<%#Eval("NUMERO_ORDEN")%>' OnClick="btnInfo2_Click" Text="Ver Detalle" runat="server" />
                                    <asp:LinkButton ID="btnEditar" CssClass="btn btn-primary" OnClick="btnEditar_Click" CommandArgument='<%#Eval("NUMERO_ORDEN")%>' Text="Aceptar" runat="server" />
                                    <asp:LinkButton ID="btnEliminar" type="button" CssClass="btn btn-danger" OnClick="btnEliminar_Click" CommandArgument='<%#Eval("NUMERO_ORDEN")%>' Text="Rechazar" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
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
                        <h1>No hay reservas pendientes</h1>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <% } %>

    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Comentario</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label for="message-text" class="col-form-label">Para rechazar una Reserva debe especificar las razones</label>
                        <asp:TextBox ID="txtComentario" placeholder="Ingrese un mensaje" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                    <asp:Button ID="btnAgregar" runat="server" Text="Enviar" OnClick="btnModal_Click" CssClass="btn btn-danger btn-block login-button" />
                </div>
            </div>
        </div>
    </div>


    <div class="modal fade" id="exampleModal2" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel2" runat="server"></h5>
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
                                <asp:BoundField DataField="RUT_HUESPED" HeaderText="RUT Huésped" />
                                <asp:BoundField DataField="HUESPED" HeaderText="Nombre Huésped" />
                                <asp:BoundField DataField="HABITACION" HeaderText="Habitación" />
                                <asp:BoundField DataField="PENSION" HeaderText="Pensión" />
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
