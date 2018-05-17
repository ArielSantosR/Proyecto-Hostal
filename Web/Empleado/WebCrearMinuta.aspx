<%@ Page Title="" Language="C#" MasterPageFile="~/Empleado/EmpleadoM.Master" AutoEventWireup="true" CodeBehind="WebCrearMinuta.aspx.cs" Inherits="Web.Empleado.WebCrearMinuta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                    <h5>Crear Minuta</h5>         
                </div>
            </div>
            
              <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
          <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                 <div class="form-group">
              <label for="name" class="col-sm-12 control-label">Nombre Minuta</label>
                <div class="col-sm-12">
                  <div class="input-group">
                  <span class="input-group-text"><i class="fa fa-user fa" aria-hidden="true"></i></span>
                      <asp:TextBox ID="txtNombreMinuta" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
              </div>
            </div>  
                <div class="form-group">
              <label for="name" class="col-sm-12 control-label">Desayuno</label>
                <div class="col-sm-12">
                  <div class="input-group">
                  <span class="input-group-text"><i class="fa fa-user fa" aria-hidden="true"></i></span>
                      <asp:DropDownList ID="ddlDesayuno" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProducto_SelectedIndexChanged">
                      </asp:DropDownList>
                </div>
              </div>
            </div>  
                <div class="form-group">
              <label for="name" class="col-sm-12 control-label">Almuerzo </label>
                <div class="col-sm-12">
                  <div class="input-group">
                  <span class="input-group-text"><i class="fa fa-user fa" aria-hidden="true"></i></span>
                      <asp:DropDownList ID="ddlAlmuerzo" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProducto_SelectedIndexChanged">
                      </asp:DropDownList>
                </div>
              </div>
            </div>
                <div class="form-group">
              <label for="name" class="col-sm-12 control-label">Cena</label>
                <div class="col-sm-12">
                  <div class="input-group">
                  <span class="input-group-text"><i class="fa fa-user fa" aria-hidden="true"></i></span>
                      <asp:DropDownList ID="ddlCena" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProducto_SelectedIndexChanged">
                      </asp:DropDownList>
                </div>
              </div>
            </div>  
                      
                </ContentTemplate>
            </asp:UpdatePanel>

          <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
              <ContentTemplate>

          <div class="form-group">
              <label for="name" class="col-sm-12 control-label">Precio Total</label>
                <div class="col-sm-12">
                  <div class="input-group">
                  <span class="input-group-text"><i class="fas fa-money-bill-alt" aria-hidden="true"></i></span>
                  <asp:TextBox ID="txtPrecio" placeholder="Ingrese un plato" runat="server" CssClass="form-control" required="required"></asp:TextBox>
                </div>
              </div>
            </div>
                  </ContentTemplate>
              </asp:UpdatePanel>

          <div class="row"> 
                  <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                      <br/> <asp:Button ID="btnRegistrar" runat="server" Text="Agregar a la Lista"  CssClass="btn btn-primary btn-lg btn-block login-button" OnClick="btnAgregar_Click" />
                  </div>
                  <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                      <br/> <asp:Button ID="btnLimpiar" runat="server" Text="Crear Minuta"  CssClass="btn btn-warning btn-lg btn-block login-button" OnClick="btnCrear_Click"/>
                  </div>
                </div>   
          </div>
        <div class="row">
            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12"> 
                <div style="display: flex; justify-content: center; margin-top: 35px;">
        <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
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
                                        <asp:LinkButton ID="btnEliminar" <!--OnClick="btnEliminar_Click"--> CssClass="btn btn-danger" CommandArgument='<%#Eval("ID_PRODUCTO")%>'  text="Eliminar" runat="server"/>
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
</asp:Content>
