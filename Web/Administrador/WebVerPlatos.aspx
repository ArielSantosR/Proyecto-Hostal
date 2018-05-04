<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador/AdminM.Master" AutoEventWireup="true" CodeBehind="WebVerPlatos.aspx.cs" Inherits="Web.WebVerPlatos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="display: flex; justify-content: center; margin-bottom: 20px">
        <asp:GridView ID="gvPlato" AutoGenerateColumns="false" runat="server" ForeColor="#333333" GridLines="None">      
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
                                 <asp:BoundField DataField="ID_PLATO" HeaderText="ID Plato" />
                                <asp:BoundField DataField="NOMBRE_PLATO" HeaderText="Nombre" />
                                <asp:BoundField DataField="PRECIO_PLATO" HeaderText="Precio" />
                                <asp:BoundField DataField="ID_CATEGORIA" HeaderText="ID Categoría" />
                                <asp:BoundField DataField="ID_TIPO_PLATO" HeaderText="ID Tipo Plato" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEditar" onclick="btnEditar_Click" CssClass="btn btn-success" CommandArgument='<%#Eval("ID_PLATO")%>'  text="Editar" runat="server"/>
                                        <asp:LinkButton ID="btnEliminar" onclick="btnEliminar_Click" CssClass="btn btn-danger" CommandArgument='<%#Eval("ID_PLATO")%>'  text="Eliminar" runat="server"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
        </div>    
</asp:Content>
