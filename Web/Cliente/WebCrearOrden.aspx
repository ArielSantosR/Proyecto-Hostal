<%@ Page Title="" Language="C#" MasterPageFile="~/Cliente/ClienteM.Master" AutoEventWireup="true" CodeBehind="WebCrearOrden.aspx.cs" Inherits="Web.Cliente.WebCrearOrden" Culture="Auto" UICulture="Auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="../js/scriptRut.js"></script>
    <script type="text/javascript">
        function showCalendar() {
            $("#<%=imgCalendar1.ClientID %>").trigger("click");
            $("#<%=imgCalendar2.ClientID %>").trigger("click");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true" EnableScriptLocalization="true"></asp:ScriptManager>
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
            <div class="main-login main-center" style="max-width: 1024px;">
                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 text-center">
                        <h5><strong>Crear Reserva</strong></h5>
                    </div>
                </div>
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
                        <div class="border-top my-3"></div>
                        <label for="name" class="col-sm control-label"><strong>Huésped</strong></label>
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
                            <label for="name" class="col-sm-12 control-label">Categoría de Habitación </label>
                            <div class="col-sm-12">
                                <div class="input-group">
                                    <span class="input-group-text"><i class="fa fa-user fa" aria-hidden="true"></i></span>
                                    <asp:DropDownList ID="ddlCategoria" CssClass="form-control" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="name" class="col-sm-12 control-label">Tipo de Habitación</label>
                            <div class="col-sm-12">
                                <div class="input-group">
                                    <span class="input-group-text"><i class="fa fa-user fa" aria-hidden="true"></i></span>
                                    <asp:DropDownList ID="ddlTipo" CssClass="form-control" runat="server">
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
                <div class="row">
                    <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4">
                        <br />
                        <asp:Button ID="btnAgregar" runat="server" Text="Agregar Huésped" CssClass="btn btn-primary btn-lg btn-block login-button" OnClick="btnAgregar_Click" />
                    </div>
                    <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4">
                        <br />
                        <asp:Button ID="btnVer" runat="server" Text="Lista de Huéspedes" CssClass="btn btn-info btn-lg btn-block login-button" OnClick="btnVer_Click" />
                    </div>
                </div>
                <div class="border-top my-3"></div>
                <label for="name" class="col-sm control-label"><strong>Datos de la Reserva</strong></label>
                <div class="form-group">
                    <label for="name" class="col-sm-12 control-label">Fechas</label>
                    <div class="col-sm-12 input-group-append">
                        <label class="control-label">Desde:&nbsp;</label>
                        <asp:TextBox ID="txtFechaInicio" runat="server" onclick="showCalendar();" onfocusout="showCalendar();" placeholder="Ingrese Fecha de Llegada" CssClass="form-control" Width="230px"></asp:TextBox>&nbsp;
                        <asp:ImageButton ID="imgCalendar1" runat="server" ImageUrl="../images/calendar-icon.png" Height="40px" Width="40px" />&nbsp;
                        <ajaxToolkit:CalendarExtender ID="calendarFechaInicio" runat="server" TargetControlID="txtFechaInicio" PopupButtonID="imgCalendar1" Format="dd/MM/yyyy" />
                        &nbsp;&nbsp;
                        <label class="control-label">Hasta:&nbsp;</label>
                        <asp:TextBox ID="txtFechaFinal" runat="server" onclick="showCalendar();" onfocusout="showCalendar();" placeholder="Ingrese Fecha de Salida" CssClass="form-control" Width="220px"></asp:TextBox>&nbsp;
                        <asp:ImageButton ID="imgCalendar2" runat="server" ImageUrl="../images/calendar-icon.png" Height="40px" Width="40px" />&nbsp;
                        <ajaxToolkit:CalendarExtender ID="calendarFechaFinal" runat="server" TargetControlID="txtFechaFinal" PopupButtonID="imgCalendar2" Format="dd/MM/yyyy" />
                    </div>
                </div>

                <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="form-group">
                            <label for="name" class="col-sm-12 control-label">Valor Diario</label>
                            <div class="col-sm-12">
                                <div class="input-group">
                                    <span class="input-group-text"><i class="fa fa-user fa" aria-hidden="true"></i></span>
                                    <asp:TextBox ID="txtPrecio" placeholder="Precio Total" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>


                <div class="row">
                    <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4">
                        <br />
                        <asp:Button ID="btnReservar" runat="server" Text="Reservar" CssClass="btn btn-secondary btn-lg btn-block login-button" OnClick="btnReservar_Click" />
                    </div>
                </div>
            </div>

            <div class="modal fade" id="exampleModal2" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-lg" role="document" style="max-width: 1024px">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="exampleModal2Label">Detalle Pedido</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div style="display: flex; justify-content: center; margin-top: 35px;">
                                <asp:GridView ID="gvDetalle" AutoGenerateColumns="false" runat="server" ForeColor="#333333" GridLines="Vertical" CssClass="text-center">
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
                                        <asp:BoundField DataField="NOMBRE_HUESPED" HeaderText="Nombre Huésped" />
                                        <asp:BoundField DataField="PENSION" HeaderText="Pensión" />
                                        <asp:BoundField DataField="VALOR_PENSION" HeaderText="Valor Pensión" />
                                        <asp:BoundField DataField="CATEGORIA_HAB" HeaderText="Habitación" />
                                        <asp:BoundField DataField="VALOR_HAB" HeaderText="Valor Habitación" />
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
