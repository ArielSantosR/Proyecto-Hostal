<%@ Page Title="" Language="C#" MasterPageFile="~/Proveedor/ProveedorM.Master" AutoEventWireup="true" CodeBehind="WebHistorialPedidos.aspx.cs" Inherits="Web.Proveedor.WebDespachados" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
       <div class="row main">
         <div class="main-login main-center-wide">
             <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 text-center">
                        <h5>Buscar Pedidos Despachados</h5>
                      </div>
             </div>

           <div class="row">
              <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                  <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                      <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="Black" BorderStyle="Solid" CellSpacing="1" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="250px" NextPrevFormat="ShortMonth" Width="330px">
                          <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" Height="8pt" />
                          <DayStyle BackColor="#CCCCCC" />
                          <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" />
                          <OtherMonthDayStyle ForeColor="#999999" />
                          <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                          <TitleStyle BackColor="#333399" BorderStyle="Solid" Font-Bold="True" Font-Size="12pt" ForeColor="White" Height="12pt" />
                          <TodayDayStyle BackColor="#999999" ForeColor="White" />
                      </asp:Calendar>
                  </div>
                  <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                      <asp:Calendar ID="Calendar2" runat="server" BackColor="White" BorderColor="Black" BorderStyle="Solid" CellSpacing="1" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="250px" NextPrevFormat="ShortMonth" Width="330px">
                          <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" Height="8pt" />
                          <DayStyle BackColor="#CCCCCC" />
                          <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" />
                          <OtherMonthDayStyle ForeColor="#999999" />
                          <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                          <TitleStyle BackColor="#333399" BorderStyle="Solid" Font-Bold="True" Font-Size="12pt" ForeColor="White" Height="12pt" />
                          <TodayDayStyle BackColor="#999999" ForeColor="White" />
                      </asp:Calendar>
                  </div>
              </div>
               <div class="row">
                  <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                   <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-success btn-lg btn-block login-button" style="font-size: 15px"/>
                </div>
                </div>
         </div>   
             </div>
       </div>
    </div>

    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
        GridView
                <asp:GridView ID="GridView1" runat="server"></asp:GridView>
            </div>    
</asp:Content>
