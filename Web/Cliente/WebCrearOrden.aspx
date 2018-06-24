﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Cliente/ClienteM.Master" AutoEventWireup="true" CodeBehind="WebCrearOrden.aspx.cs" Inherits="Web.Cliente.WebCrearOrden" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="../js/scriptRut.js"></script>
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
            <div class="main-login main-center">
                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 text-center">
                        <h5><strong>Crear Reserva</strong></h5>
                    </div>
                </div>

                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="form-group" runat="server" id="divClientes">
                            <label for="name" class="col-sm-12 control-label">Empresa</label>
                            <div class="col-sm-12">
                                <div class="input-group">
                                    <span class="input-group-text"><i class="fa fa-user fa" aria-hidden="true"></i></span>
                                    <asp:DropDownList ID="ddlClientes" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlClientes_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="name" class="col-sm-12 control-label">RUT Huésped </label>
                            <div class="col-sm-12">
                                <div class="input-group">
                                    <span class="input-group-text"><i class="fa fa-user fa" aria-hidden="true"></i></span>
                                    <asp:DropDownList ID="ddlRut" CssClass="form-control" runat="server" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="name" class="col-sm-12 control-label">Categoría Habitación </label>
                            <div class="col-sm-12">
                                <div class="input-group">
                                    <span class="input-group-text"><i class="fa fa-user fa" aria-hidden="true"></i></span>
                                    <asp:DropDownList ID="ddlCategoria" CssClass="form-control" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="name" class="col-sm-12 control-label">Minuta</label>
                            <div class="col-sm-12">
                                <div class="input-group">
                                    <span class="input-group-text"><i class="fa fa-user fa" aria-hidden="true"></i></span>
                                    <asp:DropDownList ID="ddlPension" CssClass="form-control" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="form-group">
                    <label for="name" class="col-sm-12 control-label">Fecha de llegada</label>
                    &nbsp;<div class="col-sm-12">
                        <div class="input-group">
                            <span class="input-group-text"><i class="fas fa-calendar-alt" aria-hidden="true"></i></span>
                            <asp:Calendar ID="calendarFecha" runat="server" BackColor="White" BorderColor="Black" BorderStyle="Solid" CellSpacing="1" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="250px" NextPrevFormat="ShortMonth" Width="330px">
                                <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" Height="8pt" />
                                <DayStyle BackColor="#CCCCCC" />
                                <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" />
                                <OtherMonthDayStyle ForeColor="#999999" />
                                <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                                <TitleStyle BackColor="#333399" BorderStyle="Solid" Font-Bold="True" Font-Size="12pt" ForeColor="White" Height="12pt" />
                                <TodayDayStyle BackColor="#CCCCCC" />
                            </asp:Calendar>
                        </div>
                    </div>
                </div>

                <div class="form-group">
                    <label for="name" class="col-sm-12 control-label">Fecha de Salida</label>
                    &nbsp;<div class="col-sm-12">
                        <div class="input-group">
                            <span class="input-group-text"><i class="fas fa-calendar-alt" aria-hidden="true"></i></span>
                            <asp:Calendar ID="calendarSalida" runat="server" BackColor="White" BorderColor="Black" BorderStyle="Solid" CellSpacing="1" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="250px" NextPrevFormat="ShortMonth" Width="330px">
                                <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" Height="8pt" />
                                <DayStyle BackColor="#CCCCCC" />
                                <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" />
                                <OtherMonthDayStyle ForeColor="#999999" />
                                <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                                <TitleStyle BackColor="#333399" BorderStyle="Solid" Font-Bold="True" Font-Size="12pt" ForeColor="White" Height="12pt" />
                                <TodayDayStyle BackColor="#CCCCCC" />
                            </asp:Calendar>
                        </div>
                    </div>
                </div>


                <div class="row">
                    <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4">
                        <br />
                        <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="btn btn-primary btn-lg btn-block login-button" OnClick="btnAgregar_Click" />
                    </div>
                    <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4">
                        <br />
                        <asp:Button ID="btnVer" runat="server" Text="Lista" CssClass="btn btn-info btn-lg btn-block login-button" OnClick="btnVer_Click" />
                    </div>
                    <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4">
                        <br />
                        <asp:Button ID="btnReservar" runat="server" Text="Reservar" CssClass="btn btn-secondary btn-lg btn-block login-button" OnClick="btnReservar_Click" />
                    </div>
                </div>
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
                            <div style="display: flex; justify-content: center; margin-top: 35px;">
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
                                        <asp:BoundField DataField="NOMBRE_HUESPED" HeaderText="Nombre Huesped" />
                                        <asp:BoundField DataField="PENSION" HeaderText="Pensión" />
                                        <asp:BoundField DataField="CATEGORIA_HAB" HeaderText="Categoría Habitacion" />
                                        <asp:BoundField DataField="RUT_ELIMINAR" Visible="false" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEliminar" OnClick="btnEliminar_Click" CssClass="btn btn-danger" CommandArgument='<%#Eval("RUT_ELIMINAR")%>' Text="Eliminar" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
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
        </div>
    </div>
</asp:Content>
