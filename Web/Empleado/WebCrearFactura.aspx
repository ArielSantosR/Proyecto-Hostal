<%@ Page Title="" Language="C#" MasterPageFile="~/Empleado/EmpleadoM.Master" AutoEventWireup="true" CodeBehind="WebCrearFactura.aspx.cs" Inherits="Web.Empleado.WebCrearFactura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>
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
                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 text-center">
                        <h5><strong>Factura</strong></h5>
                    </div>
                </div>
                <div class="border-top my-3"></div>
                <div class="form-group">
                    <label for="name" class="col-sm control-label"><strong>Cliente</strong></label>
                    <div class="col-sm-12 input-group-lg">
                        <div class="input-group-text col-sm-12" style="background-color: #009edf; border: none; color: #FFF">
                            <label class="control-label">R.U.T:</label>&nbsp;
                            <asp:TextBox ID="txtRut" runat="server" CssClass="form-control" Width="230px" ></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            <asp:RequiredFieldValidator ID="rfvRut" runat="server" ControlToValidate="txtRut" ErrorMessage="Campo Requerido"></asp:RequiredFieldValidator>
                            <label class="control-label">Nombre:</label>&nbsp;
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txtNombre" ErrorMessage="Campo Requerido"></asp:RequiredFieldValidator>
                        </div>
                        <div class="input-group-text col-sm-12" style="background-color: #009edf; border: none; color: #FFF">
                            <label class="control-label">Dirección:</label>&nbsp;
                            <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDireccion" runat="server" ControlToValidate="txtDireccion" ErrorMessage="Campo Requerido"></asp:RequiredFieldValidator>
                        </div>
                        <div class="input-group-text col-sm-12" style="background-color: #009edf; border: none; color: #FFF">
                            <label class="control-label">País:</label>&nbsp;
                            <asp:UpdatePanel runat="server" ID="upPais">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlPais" CssClass="form-control" runat="server" AutoPostBack="true" Width="230px" OnSelectedIndexChanged="ddlPais_SelectedIndexChanged"></asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            &nbsp;&nbsp;&nbsp;<label class="control-label">Region:</label>&nbsp;
                            <asp:UpdatePanel runat="server" ID="upRegion">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlRegion" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged"></asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            &nbsp;&nbsp;&nbsp;<label class="control-label">Comuna:</label>&nbsp;
                            <asp:UpdatePanel runat="server" ID="upComuna">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlComuna" CssClass="form-control" runat="server" AutoPostBack="true" Width="230px"></asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="input-group-text col-sm-12" style="background-color: #009edf; border: none; color: #FFF">
                            <label class="control-label">N° Orden:</label>&nbsp;
                            <asp:TextBox ID="txtOrden" runat="server" CssClass="form-control" Width="230px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            <label class="control-label">Giro:</label>&nbsp;
                            <asp:UpdatePanel runat="server" ID="upGiro">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlGiro" CssClass="form-control" runat="server" AutoPostBack="true" Width="230px"></asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:RequiredFieldValidator ID="rfvGiro" runat="server" ControlToValidate="ddlGiro" ErrorMessage="Campo Requerido"></asp:RequiredFieldValidator>
                            &nbsp;&nbsp;&nbsp;<label class="control-label">Teléfono:</label>&nbsp;
                            <asp:TextBox ID="txtTelefono" TextMode="Number" runat="server" CssClass="form-control" Width="230px"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="border-top my-3"></div>
                <div class="form-group">
                    <label for="name" class="col-sm control-label"><strong>Detalles</strong></label>
                    <div style="display: flex; justify-content: center; margin-bottom: 20px">
                        <asp:GridView ID="gvDetalle" AutoGenerateColumns="false" runat="server" ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="true" EmptyDataText="No se encontraron registros." CssClass="align-content-center">
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
                                <asp:BoundField DataField="Cantidad" HeaderText="CANTIDAD" />
                                <asp:BoundField DataField="Descripcion" HeaderText="DESCRIPCIÓN" />
                                <asp:BoundField DataField="Descuento" HeaderText="DESCUENTO" />
                                <asp:BoundField DataField="ValorUni" HeaderText="VALOR UNITARIO" />
                                <asp:BoundField DataField="Total" HeaderText="TOTAL" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div class="border-top my-3"></div>
                <div class="form-group">
                    <label for="name" class="col-sm control-label"><strong>Datos de Pago</strong></label>
                    <div class="input-group-text col-sm-12" style="background-color: #009edf; border: none; color: #FFF">
                        <label class="control-label">Neto:</label>&nbsp;
                        <asp:TextBox ID="txtNeto" runat="server" CssClass="form-control" Width="230px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                        <label class="control-label">IVA(19%):</label>&nbsp;
                        <asp:TextBox ID="txtIva" runat="server" CssClass="form-control"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                        <label class="control-label">Descuento:</label>&nbsp;
                        <asp:TextBox ID="txtDescuento" runat="server" CssClass="form-control"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                        <label class="control-label">Total:</label>&nbsp;
                        <asp:TextBox ID="txtTotal" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="input-group-text col-sm-12" style="background-color: #009edf; border: none; color: #FFF">
                        <label class="control-label">Medio de Pago:</label>&nbsp;
                        <asp:RadioButton ID="rbEfectivo" runat="server" Text="&nbsp;EFECTIVO" GroupName="FormasPago" CssClass="form-control" BackColor="#009edf" BorderStyle="None" ForeColor="White"/>
                        <asp:RadioButton ID="rbCredito" runat="server" Text="&nbsp;CRÉDITO" GroupName="FormasPago" CssClass="form-control" BackColor="#009edf" BorderStyle="None" ForeColor="White"/>
                        <asp:RadioButton ID="rbDebito" runat="server" Text="&nbsp;DÉBITO" GroupName="FormasPago" CssClass="form-control" BackColor="#009edf" BorderStyle="None" ForeColor="White"/>
                    </div>
                </div>
                <div class="border-top my-3"></div>
                <div class="row">
                    <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4">
                        <asp:Button ID="btnAgregar" runat="server" Text="Agregar Servicios" CssClass="btn btn-primary btn-lg btn-block login-button" OnClick="btnAgregar_Click" />
                    </div>
                    <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4">
                        <asp:Button ID="btnFacturar" runat="server" Text="Facturar" CssClass="btn btn-success btn-block btn-lg login-button" OnClick="btnFacturar_Click" />
                    </div>
                </div>
            </div>

            <div class="modal fade" id="exampleModal2" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-lg" role="document" style="max-width: 1024px">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="exampleModal2Label">Agregar Servicios</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="form-group">
                                <div class="col-sm-12">
                                    <label for="name" class="col-sm control-label"><strong>Pensiones</strong></label>
                                    <asp:DropDownList ID="ddlPension" runat="server" CssClass="form-control"></asp:DropDownList>
                                    <br />
                                    <asp:Button ID="btnAgregarPension" runat="server" Text="Agregar" CssClass="btn btn-primary" />
                                </div>

                                <div class="col-sm-12">
                                    <label for="name" class="col-sm control-label"><strong>Estadías</strong></label>
                                    <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-control"></asp:DropDownList>
                                    <br />
                                    <asp:DropDownList ID="ddlTipo" runat="server" CssClass="form-control"></asp:DropDownList>
                                    <br />
                                    <asp:Button ID="btnAgregarEstadia" runat="server" Text="Agregar" CssClass="btn btn-primary" />
                                </div>
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
