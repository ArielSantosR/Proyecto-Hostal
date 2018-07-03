<%@ Page Title="" Language="C#" MasterPageFile="~/Empleado/EmpleadoM.Master" AutoEventWireup="true" CodeBehind="WebVerOrdenesFacturar.aspx.cs" Inherits="Web.Empleado.WebVerOrdenesFacturar" Culture="Auto" UICulture="Auto"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function showCalendar() {
            $("#<%=imgCalendar1.ClientID %>").trigger("click");
            $("#<%=imgCalendar2.ClientID %>").trigger("click");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true" EnableScriptLocalization="true"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="upAlerts">
        <ContentTemplate>
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
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="container">
        <div class="row" style="margin-top: 20px; margin-bottom: 70px;">
            <div class="main-center" style="max-width: 1024px;">
                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 text-center">
                        <h1>Lista de Órdenes</h1>
                    </div>
                </div>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <div class="form-group">
                            <label for="name" class="col-sm-12 control-label">Clientes</label>
                            <div class="col-sm-12">
                                <div class="input-group">
                                    <span class="input-group-text"><i class="fa fa-user fa" aria-hidden="true"></i></span>
                                    <asp:DropDownList ID="ddlFiltro" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFiltro_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="form-group">
                    <label for="name" class="col-sm control-label">Fechas Filtro</label>
                    <div class="col-sm-12 input-group-append">
                        <label class="control-label">Desde:&nbsp;</label>
                        <asp:TextBox ID="txtFechaInicio" runat="server" onclick="showCalendar();" onfocusout="showCalendar();" placeholder="Ingrese Fecha Inicial a Filtrar" CssClass="form-control" Width="230px"></asp:TextBox>&nbsp;
                        <asp:ImageButton ID="imgCalendar1" runat="server" ImageUrl="../images/calendar-icon.png" Height="40px" Width="40px" />&nbsp;
                        <ajaxToolkit:CalendarExtender ID="calendarFechaInicio" runat="server" TargetControlID="txtFechaInicio" PopupButtonID="imgCalendar1" Format="dd/MM/yyyy"  />&nbsp;&nbsp;
                        <label class="control-label">Hasta:&nbsp;</label>
                        <asp:TextBox ID="txtFechaFinal" runat="server" onclick="showCalendar();" onfocusout="showCalendar();" placeholder="Ingrese Fecha Final a Filtrar" CssClass="form-control" Width="220px"></asp:TextBox>&nbsp;
                        <asp:ImageButton ID="imgCalendar2" runat="server" ImageUrl="../images/calendar-icon.png" Height="40px" Width="40px" />&nbsp;
                        <ajaxToolkit:CalendarExtender ID="calendarFechaFinal" runat="server" TargetControlID="txtFechaFinal" PopupButtonID="imgCalendar2" Format="dd/MM/yyyy" />
                        <asp:Button CssClass="btn btn-primary" ID="btnFiltrarFecha" runat="server" Text="Filtar por Fecha" OnClick="btnFiltrarFecha_Click" />
                    </div>
                </div>
                <div class="form-group">
                    <asp:Button CssClass="btn btn-primary" ID="Limpiar" runat="server" Text="Limpiar Filtros" OnClick="Limpiar_Click" />
                </div>
                <asp:UpdatePanel runat="server" UpdateMode="Always" ID="upGrid">
                    <ContentTemplate>
                        <% if (gvOrdenes.Rows.Count != 0) { %>
                        <div class="form-group">
                            <div style="display: flex; justify-content: center; margin-bottom: 70px">
                                <asp:GridView ID="gvOrdenes" runat="server" ForeColor="#333333" GridLines="Vertical" CssClass="text-center rounded" AllowPaging="True" PageSize="8" DataKeyNames="Número Orden" OnSelectedIndexChanged="gvOrdenes_SelectedIndexChanged">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:CommandField ShowSelectButton="True" SelectText="Facturar" ControlStyle-CssClass="btn btn-primary"/>
                                    </Columns>
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
                        <% }
                            else { %>
                        <div class="container">
                            <div class="row" style="margin-top: 20px; margin-bottom: 20px;">
                                <div class="main-center">
                                    <div class="row">
                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 text-center">
                                            <h1>No hay ninguna orden asignada para facturar</h1>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <% } %>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
