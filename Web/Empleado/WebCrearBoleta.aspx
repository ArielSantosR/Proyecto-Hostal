<%@ Page Title="" Language="C#" MasterPageFile="~/Empleado/EmpleadoM.Master" AutoEventWireup="true" CodeBehind="WebCrearBoleta.aspx.cs" Inherits="Web.Empleado.WebCrearBoleta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="../js/scriptRut.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="upAlert" runat="server">
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
        <div class="row main">
            <div class="main-login main-center-wide">
                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 text-center">
                        <h5><strong>Boleta</strong></h5>
                    </div>
                </div>
                <div class="border-top my-3"></div>
                <div class="form-group">
                    <label for="name" class="col-sm control-label"><strong>Cliente</strong></label>
                    <div class="col-sm-12 input-group-lg">
                        <div class="input-group-text col-sm-12" style="background-color: #009edf; border: none; color: #FFF">
                            <label class="control-label">R.U.T:</label>&nbsp;
                            <asp:TextBox ID="txtRut" runat="server" CssClass="form-control" oninput="checkRut(this)" Width="230px" required="required" OnTextChanged="txtRut_TextChanged"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            <label class="control-label">Nombre:</label>&nbsp;
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" required="required"></asp:TextBox>
                        </div>
                    </div>
                    <div class="border-top my-3"></div>
                    <div class="form-group">
                        <label for="name" class="col-sm control-label"><strong>Detalles</strong></label>
                        <div style="display: flex; justify-content: center; margin-bottom: 20px">
                            <asp:UpdatePanel ID="upGrid" ChildrenAsTriggers="true" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="gvDetalle" AutoGenerateColumns="False" runat="server" ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True" EmptyDataText="No se encontraron registros." CssClass="align-content-center">
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
                                            <asp:TemplateField HeaderText="CANTIDAD">
                                                <ItemTemplate>
                                                    <asp:UpdatePanel ID="upCantidad" ChildrenAsTriggers="true" UpdateMode="Conditional" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtCantidad" runat="server" Text='<%# Bind("Cantidad") %>' TextMode="Number" AutoPostBack="true" OnTextChanged="txtCantidad_TextChanged"></asp:TextBox>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Descripcion" HeaderText="DESCRIPCIÓN" />
                                            <asp:TemplateField HeaderText="DESCUENTO">
                                                <ItemTemplate>
                                                    <asp:UpdatePanel ID="upDescuento" ChildrenAsTriggers="true" UpdateMode="Conditional" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtDescuentoG" runat="server" Text='<%# Bind("Descuento") %>' TextMode="Number" Enabled="false" AutoPostBack="true" OnTextChanged="txtDescuento_TextChanged"></asp:TextBox>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="VALOR UNITARIO">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtValorUniG" runat="server" Text='<%# Bind("ValorUni") %>' TextMode="Number" Enabled="false"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TOTAL">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtValorTotal" runat="server" Text='<%# Bind("Total") %>' TextMode="Number" Enabled="false"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="border-top my-3"></div>
                    <div class="form-group">
                        <label for="name" class="col-sm control-label"><strong>Datos de Pago</strong></label>
                        <asp:UpdatePanel ID="upPagos" runat="server">
                            <ContentTemplate>
                                <div class="input-group-text col-sm-12" style="background-color: #009edf; border: none; color: #FFF">
                                    <label class="control-label">Descuento:</label>&nbsp;
                                    <asp:TextBox ID="txtDescuento" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                    <label class="control-label">Total:</label>&nbsp;
                                    <asp:TextBox ID="txtTotal" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="input-group-text col-sm-12" style="background-color: #009edf; border: none; color: #FFF">
                            <label class="control-label">Medio de Pago:</label>&nbsp;
                        <asp:RadioButton ID="rbEfectivo" runat="server" Text="&nbsp;EFECTIVO" GroupName="FormasPago" CssClass="form-control" BackColor="#009edf" BorderStyle="None" ForeColor="White" />
                            <asp:RadioButton ID="rbCredito" runat="server" Text="&nbsp;CRÉDITO" GroupName="FormasPago" CssClass="form-control" BackColor="#009edf" BorderStyle="None" ForeColor="White" />
                            <asp:RadioButton ID="rbDebito" runat="server" Text="&nbsp;DÉBITO" GroupName="FormasPago" CssClass="form-control" BackColor="#009edf" BorderStyle="None" ForeColor="White" />
                        </div>
                    </div>
                    <div class="border-top my-3"></div>
                    <div class="row">
                        <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4">
                            <asp:Button ID="btnAgregar" runat="server" Text="Agregar Servicios" CssClass="btn btn-primary btn-lg btn-block login-button" OnClick="btnAgregar_Click" />
                        </div>
                        <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4">
                            <asp:Button ID="btnBoleta" runat="server" Text="Generar Boleta" CssClass="btn btn-success btn-block btn-lg login-button" OnClick="btnBoleta_Click" />
                        </div>
                    </div>
                </div>

                <div class="modal fade" id="exampleModal2" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog modal-lg" role="document" style="max-width: 1024px">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="exampleModal2Label" style="color: black">Agregar Servicios</h5>
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
                                        <asp:Button ID="btnAgregarPension" runat="server" Text="Agregar" CssClass="btn btn-primary" OnClick="btnAgregarPension_Click" />
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
    </div>
</asp:Content>
