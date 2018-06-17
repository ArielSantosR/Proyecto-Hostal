<%@ Page Title="" Language="C#" MasterPageFile="~/Empleado/EmpleadoM.Master" AutoEventWireup="true" CodeBehind="WebVerMinuta.aspx.cs" Inherits="Web.Empleado.WebVerMinuta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="alert alert-danger alert-dismissible" id="alerta" runat="server">
        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
        <strong>Error!</strong>
        <asp:Literal ID="error" runat="server"></asp:Literal>
    </div>

    <div class="alert alert-success alert-dismissible" id="alerta_exito" runat="server">
        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
        <strong>Éxito!</strong>
        <asp:Literal ID="exito" runat="server"></asp:Literal>
    </div>


    <div style="display: flex; justify-content: center; margin-bottom: 20px; margin-top: 50px">
        <asp:GridView ID="gvMinuta" AutoGenerateColumns="false" runat="server" ForeColor="#333333" GridLines="Vertical" AllowPaging="True" PageSize="8" OnPageIndexChanging="gvMinuta_PageIndexChanging" >
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
                <asp:BoundField DataField="ID_PENSION" HeaderText="Número de Minuta" />
                <asp:BoundField DataField="NOMBRE_PENSION" HeaderText="Nombre de Minuta" />
                <asp:BoundField DataField="VALOR_PENSION" HeaderText="Precio de Minuta" />
                <asp:TemplateField>
                     <ItemTemplate>
                   <asp:LinkButton ID="btnEliminar" OnClick="btnEliminar_Click" CssClass="btn btn-danger" CommandArgument='<%#Eval("ID_PENSION")%>' Text="Eliminar" runat="server" />
                       <asp:LinkButton ID="btnInfo" OnClick="btnInfo_Click" CssClass="btn btn-info" CommandArgument='<%#Eval("ID_PENSION")%>' Text="Ver Detalle" runat="server" />
                        </ItemTemplate>
               </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

    <asp:ScriptManager runat="server"></asp:ScriptManager>

    
    <div class="modal fade" id="exampleModal2" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModal2Label">Detalle Minuta</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div style="display: flex; justify-content: center; margin-bottom: 70px">
                        <asp:GridView ID="gvDetalleMinuta" AutoGenerateColumns="true" runat="server" ForeColor="#333333" GridLines="Vertical" AllowPaging="True" PageSize="8" OnPageIndexChanging="gvDetalleMinuta_PageIndexChanging">
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
                            
                        </asp:GridView>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
