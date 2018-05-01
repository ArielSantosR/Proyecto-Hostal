<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador/AdminM.Master" AutoEventWireup="true" CodeBehind="WebVerProductos.aspx.cs" Inherits="Web.Administrador.WebVerProductos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="display: flex; justify-content: center; margin-bottom: 20px">
        <asp:GridView ID="gvProducto" AutoGenerateColumns="false" runat="server" ForeColor="#333333" GridLines="None">      
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
                                 <asp:BoundField DataField="ID_PRODUCTO" HeaderText="ID_PRODUCTO" />
                                <asp:BoundField DataField="NOMBRE_PRODUCTO" HeaderText="NOMBRE_PRODUCTO" />
                                <asp:BoundField DataField="STOCK_PRODUCTO" HeaderText="STOCK_PRODUCTO" />
                                <asp:BoundField DataField="PRECIO_PRODUCTO" HeaderText="PRECIO_PRODUCTO" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEditar"  CssClass="btn btn-success" text="Editar" runat="server"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
        </div>    
</asp:Content>
