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
                    <h5><strong>Crear Reserva</strong></h5>
                </div>
            </div>
            
              <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
          <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <div class="form-group">
              <label for="name" class="col-sm-12 control-label">RUT Huésped </label>
                <div class="col-sm-12">
                  <div class="input-group">
                  <span class="input-group-text"><i class="fa fa-user fa" aria-hidden="true"></i></span>
                      <asp:DropDownList ID="ddlRut" CssClass="form-control" runat="server" AutoPostBack="true" >
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
                  <TodayDayStyle BackColor="#CCCCCC"/>
              </asp:Calendar>
                </div>
              </div>
            </div>

          <asp:Calendar>

          </asp:Calendar>

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
                  <TodayDayStyle BackColor="#CCCCCC"/>
              </asp:Calendar>
                </div>
              </div>
            </div>


          <div class="row"> 
                  <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                      <br/> <asp:Button ID="btnRegistrar" runat="server" OnClick="btnRegistrar_Click" Text="Agregar Huésped"  CssClass="btn btn-primary btn-lg btn-block login-button"  />
                  </div>
                  <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                      <br/> <asp:Button ID="btnLimpiar" runat="server" OnClick="btnLimpiar_Click" Text="Realizar Reserva"  CssClass="btn btn-secondary btn-lg btn-block login-button"/>
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
                                    <asp:BoundField DataField="RUT_HUESPED" HeaderText="RUT Huésped" />
                                    <asp:BoundField DataField="ID_PENSION" HeaderText="ID Pensión" />     
                                    <asp:BoundField DataField="ID_CATEGORIA_HABITACION" HeaderText="ID Categoría Habitación" />                
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEliminar" OnClick="btnEliminar_Click" CssClass="btn btn-danger" CommandArgument='<%#Eval("RUT_HUESPED")%>'  text="Eliminar" runat="server"/>
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
