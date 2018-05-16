<%@ Page Title="" Language="C#" MasterPageFile="~/Cliente/ClienteM.Master" AutoEventWireup="true" CodeBehind="WebCrearOrden.aspx.cs" Inherits="Web.Cliente.WebCrearOrden" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="../js/scriptRut.js"></script>
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

  <div class="container">
    <div class="row main">
      <div class="main-login main-center">
            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 text-center">
                    <h5><strong>Crear Pedido</strong></h5>
                </div>
            </div>
            
              <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
          <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <div class="form-group">
              <label for="name" class="col-sm-12 control-label">RUT Proveedor </label>
                <div class="col-sm-12">
                  <div class="input-group">
                  <span class="input-group-text"><i class="fa fa-user fa" aria-hidden="true"></i></span>
                      <asp:DropDownList ID="ddlRut" CssClass="form-control" runat="server" AutoPostBack="true" >
                      </asp:DropDownList>
                </div>
              </div>
            </div>    
                <div class="form-group">
              <label for="name" class="col-sm-12 control-label">Producto </label>
                <div class="col-sm-12">
                  <div class="input-group">
                  <span class="input-group-text"><i class="fa fa-user fa" aria-hidden="true"></i></span>
                      <asp:DropDownList ID="ddlProducto" CssClass="form-control" runat="server">
                      </asp:DropDownList>
                </div>
              </div>
            </div>          
                </ContentTemplate>
            </asp:UpdatePanel>

          <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
              <ContentTemplate>
          <div class="form-group">
              <label for="name" class="col-sm-12 control-label">Cantidad</label>
                <div class="col-sm-12">
                  <div class="input-group">
                  <span class="input-group-text"><i class="fa fa-user fa" aria-hidden="true"></i></span>
                  <asp:TextBox ID="txtCantidad" placeholder="Ingrese cantidad de Productos" TextMode="Number" runat="server" CssClass="form-control" required="required" min="1" AutoPostBack="true"></asp:TextBox>
                </div>
              </div>
            </div>

          <div class="form-group">
              <label for="name" class="col-sm-12 control-label">Precio Total</label>
                <div class="col-sm-12">
                  <div class="input-group">
                  <span class="input-group-text"><i class="fas fa-money-bill-alt" aria-hidden="true"></i></span>
                  <asp:TextBox ID="txtPrecio" placeholder="Ingrese cantidad de Productos para ver Total" runat="server" CssClass="form-control" required="required"></asp:TextBox>
                </div>
              </div>
            </div>
                  </ContentTemplate>
              </asp:UpdatePanel>

          <div class="row"> 
                  <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                      <br/> <asp:Button ID="btnRegistrar" runat="server" Text="Agregar Huésped"  CssClass="btn btn-primary btn-lg btn-block login-button"  />
                  </div>
                  <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                      <br/> <asp:Button ID="btnLimpiar" runat="server" Text="Realizar Reserva"  CssClass="btn btn-secondary btn-lg btn-block login-button"/>
                  </div>
                </div>   
          </div>
        <div class="row">
            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12"> 
                <div style="display: flex; justify-content: center; margin-top: 35px;">
        <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Conditional" runat="server">
              <ContentTemplate>
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
                                    <asp:BoundField DataField="ID_PRODUCTO" HeaderText="ID Producto" />
                                    <asp:BoundField DataField="CANTIDAD" HeaderText="Cantidad" />                    
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEliminar" CssClass="btn btn-danger" CommandArgument='<%#Eval("ID_PRODUCTO")%>'  text="Eliminar" runat="server"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                  </ContentTemplate>
              </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
      </div>  
    
    
     <div class="container">
    <div class="row main">
      <div class="main-login main-center">
            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 text-center">
                    <h5>Crear Orden</h5>
                </div>
            </div> 
            <div class="form-group">
              <label for="name" class="col-sm-12 control-label">Nombre Pasajero</label>
                <div class="col-sm-12">
                  <div class="input-group">
                  <span class="input-group-text"><i class="fa fa-user fa" aria-hidden="true"></i></span>
                  <asp:TextBox ID="txtNombrePasajero" placeholder="Ingrese Nombre del Pasajero" runat="server" CssClass="form-control" required="required"></asp:TextBox>
                </div>
              </div>
            </div>
            <div class="form-group">
              <label for="rut" class="col-sm-12 control-label">Rut Pasajero</label>
                <div class="col-sm-12">
                  <div class="input-group">
                    <span class="input-group-text"><i class="fa fa-user-secret" aria-hidden="true"></i></span>
                    <asp:TextBox ID="txtRutPasajero" placeholder="Ingrese Rut sin puntos ni guión" runat="server" oninput="checkRut(this)" required="required" CssClass="form-control"></asp:TextBox>
                </div>
              </div>
            </div>      
            <div class="form-group">
              <label for="email" class="col-sm-12 control-label">Correo Electrónico</label>
              <div class="col-sm-12">
                <div class="input-group">
                  <span class="input-group-text"><i class="fa fa-envelope fa" aria-hidden="true"></i></span>
                  <asp:TextBox ID="txtEmailPasajero" placeholder="Ingrese Correo Electrónico" TextMode="Email" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
              </div>
            </div>  
              <div class="form-group">
                <label for="phone" class="col-sm-12 control-label">Teléfono</label>
                  <div class="col-sm-12">
                    <div class="input-group">
                      <span class="input-group-text"><i class="fa fa-phone fa-lg" aria-hidden="true"></i></span>
                      <asp:TextBox ID="txtTelefonoPasajero" placeholder="Ingrese Teléfono del Pasajero" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
                    </div>
                  </div>
                </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="form-group">
                  <label for="country" class="col-sm-12 control-label">Menú</label>
                    <div class="col-sm-12">
                      <div class="input-group">
                        <span class="input-group-text"><i class="fas fa-utensils" aria-hidden="true"></i></span>
                          <asp:DropDownList ID="ddlMenu" CssClass="form-control" runat="server" AutoPostBack="True"> </asp:DropDownList>
                      </div>
                    </div>
                  </div>
              
                </ContentTemplate>
            </asp:UpdatePanel>
              <div class="form-group">
                  <label for="direction" class="col-sm-12 control-label">Dirección</label>
                    <div class="col-sm-12">
                      <div class="input-group">
                        <span class="input-group-text"><i class="fa fa-home fa-lg" aria-hidden="true"></i></span>
                        <asp:TextBox ID="txtDireccion" placeholder="Ingrese su dirección" runat="server" CssClass="form-control" required="required"></asp:TextBox>
                      </div>
                    </div>
                  </div>
                <div class="row"> 
                  <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                      <br/> <asp:Button ID="btnAgregar" runat="server" Text="Agregar Pasajero"  CssClass="btn btn-primary btn-lg btn-block login-button" />
                  </div>
                  
                </div> 
          </div>
        <div class="form-group">
               <div class="row">
                   <label for="direction" class="col-sm-12 control-label">Resumen Orden</label>
                   GRIDVIEW
                   <asp:GridView ID="GridView1" runat="server"></asp:GridView>
               </div>  
                <div class="row">
                    <asp:Button ID="btnCrearOrden" runat="server" Text="Crear Orden" CssClass="btn btn-warning btn-lg btn-block login-button" />
                    </div>
         </div>    
                   
            </div>
          </div>
</asp:Content>
