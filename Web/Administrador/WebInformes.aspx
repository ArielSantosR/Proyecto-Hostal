<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador/AdminM.Master" AutoEventWireup="true" CodeBehind="WebInformes.aspx.cs" Inherits="Web.Administrador.WebInformes" %>

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
        <div class="row main">
            <div class="main-login main-center-wide">
                <div class="row" style="margin-top: 20px; margin-bottom: 20px;">
                    <div class="col-md-6">
                        <asp:Button runat="server" Text="Informes de Clientes" CssClass="btn btn-primary active  btn-lg btn-block login-button" Style="margin: auto;" data-toggle="collapse" data-target="#informeCliente" OnClientClick="return false" />
                        <div id="informeCliente" class="collapse" style="margin-top: 10px;">
                            <asp:Button runat="server" Text="Informe Ordenes de Clientes" ID="btnClienteOrd" CssClass="btn btn-primary btn-lg btn-block login-button" Style="font-size: 15px;" OnClick="btnClienteOrd_Click" />
                            <asp:Button runat="server" Text="Informe Clientes Frecuentes" ID="btnClienteFrec" CssClass="btn btn-primary btn-lg btn-block login-button" Style="font-size: 15px;" OnClick="btnClienteFrec_Click" />
                            <asp:Button runat="server" Text="Informe Top 10 de Clientes" ID="btnClienteTop" CssClass="btn btn-primary btn-lg btn-block login-button" Style="font-size: 15px;" OnClick="btnClienteTop_Click" />
                        </div>
                    </div>
                    <div class="col-md-6">
                        <asp:Button runat="server" Text="Informes de Habitaciones" CssClass="btn btn-primary btn-lg btn-block active" Style="margin: auto;" data-toggle="collapse" data-target="#informeHabitacion" OnClientClick="return false" />
                        <div id="informeHabitacion" class="collapse" style="margin-top: 10px;">
                            <asp:Button runat="server" Text="Dias de Uso de Habitaciones" ID="btnDiasUso" CssClass="btn btn-primary btn-lg btn-block login-button" Style="font-size: 15px" OnClick="btnDiasUso_Click" />
                            <asp:Button runat="server" Text="Cantidad de Usos de Habitaciones" ID="btnCantidadUsos" CssClass="btn btn-primary btn-lg btn-block login-button" Style="font-size: 15px" OnClick="btnCantidadUsos_Click" />
                            <asp:Button runat="server" Text="Top 10 Usos de Habitaciones" ID="btnCantidadUsosTop" CssClass="btn btn-primary btn-lg btn-block login-button" Style="font-size: 15px" OnClick="btnCantidadUsosTop_Click" />
                            <asp:Button runat="server" Text="Habitaciones Preferidas" ID="btnHabitacionPref" CssClass="btn btn-primary btn-lg btn-block login-button" Style="font-size: 15px" OnClick="btnHabitacionPref_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="modal fade" id="modalPrev" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg" role="document" style="max-width:1024px">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModal2Label">Detalle Informe</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <% if (gvDetalle.Rows.Count == 0) { %>
                        <div class="container">
                            <div class="row" style="margin-top: 20px; margin-bottom: 20px;">
                                <div class="main-center">
                                    <div class="row">
                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 text-center">
                                            <h1>No hay Datos</h1>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <% }
                            else { %>
                        <div style="display: flex; justify-content: center; margin-top: 35px;">
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
                        <% } %>
                    </div>
                    <div class="modal-footer">
                        <% if (gvDetalle.Rows.Count == 0) { %>
                            <button type="button" class="btn btn-success" data-dismiss="modal" disabled="disabled">Excel</button>
                            <button type="button" class="btn btn-primary" data-dismiss="modal" disabled="disabled">Word</button>
                            <button type="button" class="btn btn-danger" data-dismiss="modal" disabled="disabled">PDF</button>
                        <%}else {%>
                            <asp:Button ID="btnExcel" runat="server" Text="Excel" CssClass="btn btn-success" OnClick="btnExcel_Click"/>
                            <asp:Button ID="btnWord" runat="server" Text="Word" CssClass="btn btn-primary" OnClick="btnWord_Click"/>
                            <asp:Button ID="btnPdf" runat="server" Text="PDF" CssClass="btn btn-danger" OnClick="btnPdf_Click"/>
                        <%} %>
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
