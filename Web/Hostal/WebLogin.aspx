 <%@ Page Title="" Language="C#" MasterPageFile="~/Hostal/HostalM.Master" AutoEventWireup="true" CodeBehind="WebLogin.aspx.cs" Inherits="Web.WebLogin2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       
    <div class="alert alert-danger alert-dismissible" id="alerta" runat="server">
  <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
  <strong>Error!</strong> <asp:Literal ID="error" runat="server"></asp:Literal>
</div>

<div style="display: flex; justify-content: center;">
        <asp:Login ID="Login1" runat="server" OnAuthenticate="Login1_Authenticate">
            <LayoutTemplate>
                <div class="container" >
                    <div class="row main">
                      <div class="main-login main-center">
                        <div class="row">
                          <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 text-center">
                            <h1>Iniciar Sesión</h1>
                          </div>
                        </div>
                          <div class="form-group">
                           <asp:Label ID="UserNameLabel" cssclass="col-sm-12 control-label" runat="server" AssociatedControlID="UserName">Nombre de usuario:</asp:Label>
                            <div class="col-sm-12">
                                <div class="input-group">
                                <span class="input-group-text"><i class="fas fa-user fa" aria-hidden="true"></i></span>
                                    <asp:TextBox ID="UserName" CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator enabled="false" Display="Dynamic" ID="UserNameRequired" runat="server" ControlToValidate="UserName" ErrorMessage="El nombre de usuario es obligatorio." ToolTip="El nombre de usuario es obligatorio." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="PasswordLabel" runat="server" cssclass="col-sm-12 control-label" AssociatedControlID="Password">Contraseña:</asp:Label>
                                <div class="col-sm-12">
                                    <div class="input-group">
										<span class="input-group-text"><i class="fas fa-lock fa-lg" aria-hidden="true"></i></span>
                                        <asp:TextBox ID="Password"  CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                                        <asp:RequiredFieldValidator enabled="false" Display="Dynamic" ID="PasswordRequired" runat="server" ControlToValidate="Password" ErrorMessage="La contraseña es obligatoria." ToolTip="La contraseña es obligatoria." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                    </div>
							    </div>
						</div>
                          <div class="row"> 
							<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                <asp:CheckBox ID="RememberMe" runat="server" cssclass="col-sm-12 control-label text-center" Text="Recordármelo la próxima vez." />
							</div>
						</div>
                          <div class="row"> 
                              <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">   
                                    <asp:Button ID="LoginButton" runat="server" CssClass="btn btn-danger btn-lg btn-block login-button" CommandName="Login" Text="Inicio de sesión" ValidationGroup="Login1" />
                              </div>
						</div>
                        <div class="row"> 
                              <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                  <br/> <a href="WebRegistro.aspx" style="color: #FFFFFF; text-align: center" class="col-sm-12 control-label">¿Aún no tienes cuenta? Regístrate Aquí</a>
                              </div>
                        </div> 		     				
                    </div>
                </div>
            </div>        
            </LayoutTemplate>
        </asp:Login>
       </div>
    

</asp:Content>
