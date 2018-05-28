<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador/AdminM.Master" AutoEventWireup="true" CodeBehind="WebVerHabitacion.aspx.cs" Inherits="Web.Administrador.WebHabitaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="display: flex; justify-content: center; margin-bottom: 20px">
        <asp:GridView ID="gvHabitacion" AutoGenerateColumns="false" runat="server" ForeColor="#333333" GridLines="Vertical" AllowPaging="True" PageSize="15">      
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
                                 <asp:BoundField DataField="NUMERO_HABITACION" HeaderText="Número de Habitación" />
                                <asp:BoundField DataField="ESTADO_HABITACION" HeaderText="Estado" />
                                <asp:BoundField DataField="ID_TIPO_HABITACION" HeaderText="ID Tipo Habitación" />
                                <asp:BoundField DataField="RUT_CLIENTE" HeaderText="RUT Cliente" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEditar" onclick="btnEditar_Click" CssClass="btn btn-success" CommandArgument='<%#Eval("NUMERO_HABITACION")%>'  text="Editar" runat="server"/>
                                        <asp:LinkButton ID="btnEliminar" onclick="btnEliminar_Click" CssClass="btn btn-danger" CommandArgument='<%#Eval("NUMERO_HABITACION")%>'  text="Eliminar" runat="server"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
        </div>    
</asp:Content>
