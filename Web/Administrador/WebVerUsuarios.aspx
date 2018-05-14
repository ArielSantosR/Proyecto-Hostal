<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador/AdminM.Master" AutoEventWireup="true" CodeBehind="WebVerUsuarios.aspx.cs" Inherits="Web.Administrador.WebUsuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <% if (gvUsuario.Rows.Count != 0) { %>
    <div class="container">
        <div class="row" style="margin-top: 20px; margin-bottom: 20px;">
            <div class="main-center">
	            <div class="row">
	                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 text-center">
		                <h1>Lista de Usuarios</h1>
	                </div>
                </div>
            </div>
        </div>
    </div>
    <% } else { %>
    <div class="container">
        <div class="row" style="margin-top: 20px; margin-bottom: 20px;">
            <div class="main-center">
	            <div class="row">
	                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 text-center">
		                <h1>No hay ningún usuario registrado</h1>
	                </div>
                </div>
            </div>
        </div>
    </div>
    <% } %>
    
    <div style="display: flex; justify-content: center; margin-bottom: 20px">
        <asp:GridView ID="gvUsuario" runat="server" ForeColor="#333333" GridLines="Vertical" DataKeyNames="Tipo,ID" OnRowEditing="gvUsuario_RowEditing" >
             
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
                <asp:CommandField ShowEditButton="true" UpdateText="Editar" CancelText="Cancel" ItemStyle-CssClass="btn btn-success" > <ItemStyle CssClass="btn btn-success"></ItemStyle> </asp:CommandField>
            </Columns>
        </asp:GridView>    
    </div>


</asp:Content>
