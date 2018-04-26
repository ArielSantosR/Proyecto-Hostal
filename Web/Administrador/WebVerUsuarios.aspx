<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador/AdminM.Master" AutoEventWireup="true" CodeBehind="WebVerUsuarios.aspx.cs" Inherits="Web.Administrador.WebUsuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="display: flex; justify-content: center; margin-bottom: 20px">
        <asp:GridView ID="gvUsuario" runat="server" ForeColor="#333333" GridLines="None" DataKeyNames="Tipo" OnRowEditing="gvUsuario_RowEditing" >
             
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
