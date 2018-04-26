﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador/AdminM.Master" AutoEventWireup="true" CodeBehind="WebVerHabitacion.aspx.cs" Inherits="Web.Administrador.WebHabitaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="display: flex; justify-content: center; margin-bottom: 20px">
        <asp:GridView ID="gvHabitacion" AutoGenerateColumns="false" runat="server" ForeColor="#333333" GridLines="None">      
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
                                 <asp:BoundField DataField="NUMERO_HABITACION" HeaderText="ID_USUARIO" />
                                <asp:BoundField DataField="PRECIO_HABITACION" HeaderText="PRECIO_HABITACION" />
                                <asp:BoundField DataField="ESTADO_HABITACION" HeaderText="ESTADO_HABITACION" />
                                <asp:BoundField DataField="ID_TIPO_HABITACION" HeaderText="ID_TIPO_HABITACION" />
                                <asp:BoundField DataField="RUT_CLIENTE" HeaderText="RUT_CLIENTE" />
                                 <asp:CommandField ShowEditButton="true" UpdateText="Editar" CancelText="Cancel" ItemStyle-CssClass="btn btn-success" > <ItemStyle CssClass="btn btn-success"></ItemStyle> </asp:CommandField>
                                 <asp:CommandField ShowDeleteButton="true" DeleteText="Eliminar" CancelText="Cancel"  ItemStyle-CssClass="btn btn-danger"   > <ItemStyle CssClass="margin-right: 10px; btn btn-danger"></ItemStyle></asp:CommandField>
                            </Columns>
                        </asp:GridView>
        </div>    
</asp:Content>
