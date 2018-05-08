using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Proveedor {
    public partial class WebEditarProveedor : System.Web.UI.Page {

        void Page_PreInit(object sender,EventArgs e) {
            if (MiSesion != null) {
                if (MiSesion.TIPO_USUARIO != null && MiSesion.ESTADO != null) {
                    if (MiSesion.TIPO_USUARIO.Equals(Tipo_Usuario.Administrador.ToString()) && MiSesion.ESTADO.Equals(Estado_Usuario.Habilitado.ToString())) {
                        MasterPageFile = "~/Administrador/AdminM.Master";
                    }
                    else if (MiSesion.TIPO_USUARIO.Equals(Tipo_Usuario.Proveedor.ToString()) && MiSesion.ESTADO.Equals(Estado_Usuario.Habilitado.ToString())) {
                        Modelo.Proveedor pro = new Modelo.Proveedor();
                        pro.BuscarProveedor(MiSesion.ID_USUARIO);
                        SesionPro = pro;
                    }
                    else {
                        Response.Write("<script language='javascript'>window.alert('No Posee los permisos necesarios');window.location='../Hostal/WebLogin.aspx';</script>");
                    }
                }
                else {
                    Response.Write("<script language='javascript'>window.alert('Debe Iniciar Sesión Primero');window.location='../Hostal/WebLogin.aspx';</script>");
                }
            }
        }

        protected void Page_Load(object sender,EventArgs e) {
            error.Text = "";
            //Bloqueo de cambios dependiendo del usuario
            if (MiSesion.TIPO_USUARIO.Equals(Tipo_Usuario.Empleado.ToString()) && MiSesion.ESTADO.Equals(Estado_Usuario.Habilitado.ToString())) {
                txtRut.Enabled = false;
                txtPNombre.Enabled = false;
                txtSNombre.Enabled = false;
                txtUsuario.Enabled = false;
                txtApellidoP.Enabled = false;
                txtApellidoM.Enabled = false;
                ddlEstado.Enabled = false;
            }
            else {
                txtUsuario.Enabled = false;
                txtRut.Enabled = false;
            }

            if (!IsPostBack) {
                ddlEstado.DataSource = Enum.GetValues(typeof(Estado_Usuario));
                ddlEstado.DataBind();

                ddlTipoProveedor.DataSource = TipoProveedorCollection.ListaTiposProveedor();
                ddlTipoProveedor.DataTextField = "NOMBRE_TIPO";
                ddlTipoProveedor.DataValueField = "ID_TIPO_PROVEEDOR";
                ddlTipoProveedor.DataBind();

                alerta.Visible = false;
                //Carga de datos actuales
                txtRut.Text = SesionPro.RUT_PROVEEDOR.ToString() + "-" + SesionPro.DV_PROVEEDOR;
                txtPNombre.Text = SesionPro.PNOMBRE_PROVEEDOR;
                txtSNombre.Text = SesionPro.SNOMBRE_PROVEEDOR;
                if (MiSesion.TIPO_USUARIO.Equals(Tipo_Usuario.Empleado.ToString())) {
                    txtUsuario.Text = MiSesion.NOMBRE_USUARIO;
                }
                else {
                    txtUsuario.Text = SesionEdit.NOMBRE_USUARIO;
                }
                txtApellidoP.Text = SesionPro.APP_PATERNO_PROVEEDOR;
                txtApellidoM.Text = SesionPro.APP_MATERNO_PROVEEDOR;
                ddlTipoProveedor.SelectedValue = SesionPro.ID_TIPO_PROVEEDOR.ToString();
            }
        }

        //Creación de Sesión
        public Usuario MiSesion {
            get {
                if (Session["Usuario"] == null) {
                    Session["Usuario"] = new Usuario();
                }
                return (Usuario)Session["Usuario"];
            }
            set {
                Session["Usuario"] = value;
            }
        }

        //Sesion para editar datos
        public Modelo.Proveedor SesionPro {
            get {
                if (Session["Proveedor"] == null) {
                    Session["Proveedor"] = new Modelo.Proveedor();
                }
                return (Modelo.Proveedor)Session["Proveedor"];
            }
            set {
                Session["Proveedor"] = value;
            }
        }

        public Usuario SesionEdit {
            get {
                if (Session["UsuarioEdit"] == null) {
                    Session["UsuarioEdit"] = new Usuario();
                }
                return (Usuario)Session["UsuarioEdit"];
            }
            set {
                Session["UsuarioEdit"] = value;
            }
        }

        protected void btnActualizar_Click(object sender,EventArgs e) {
            try {
                bool flag = true;
                Usuario usuario = new Modelo.Usuario();
                Modelo.Proveedor proveedor = SesionPro;

                if (MiSesion.TIPO_USUARIO.Equals(Tipo_Usuario.Empleado.ToString()) && MiSesion.ESTADO.Equals(Estado_Usuario.Habilitado.ToString())) {
                    usuario = MiSesion;
                }
                else {
                    usuario = SesionEdit;
                }

                if (!string.IsNullOrEmpty(txtPass.Text) && (!string.IsNullOrEmpty(txtConfirmar.Text))) {
                    if (txtPass.Text.Equals(txtConfirmar.Text)) {
                        string hashed = BCrypt.HashPassword(txtPass.Text,BCrypt.GenerateSalt(12));
                        usuario.PASSWORD = hashed;
                    }
                    else {
                        error.Text = "Las contraseñas no son iguales";
                        flag = false;
                        alerta.Visible = true;
                    }
                }

                if (!txtPNombre.Text.Equals(proveedor.PNOMBRE_PROVEEDOR)) {
                    proveedor.PNOMBRE_PROVEEDOR = txtPNombre.Text;
                }

                if (!txtSNombre.Text.Equals(proveedor.SNOMBRE_PROVEEDOR)) {
                    proveedor.SNOMBRE_PROVEEDOR = txtSNombre.Text;
                }

                if (!txtApellidoP.Text.Equals(proveedor.APP_PATERNO_PROVEEDOR)) {
                    proveedor.APP_PATERNO_PROVEEDOR = txtApellidoP.Text;
                }

                if (!txtApellidoM.Text.Equals(proveedor.APP_MATERNO_PROVEEDOR)) {
                    proveedor.APP_MATERNO_PROVEEDOR = txtApellidoM.Text;
                }

                if (MiSesion.TIPO_USUARIO.Equals(Modelo.Tipo_Usuario.Administrador.ToString())) {
                    usuario.ESTADO = ddlEstado.SelectedValue.ToString();
                }

                if (!ddlTipoProveedor.SelectedValue.Equals(proveedor.ID_TIPO_PROVEEDOR)) {
                    proveedor.ID_TIPO_PROVEEDOR = short.Parse(ddlTipoProveedor.SelectedValue);
                }

                if (flag) {
                    if (usuario.Update() && proveedor.Update()) {
                        if (MiSesion.TIPO_USUARIO.Equals(Tipo_Usuario.Cliente.ToString()) && MiSesion.ESTADO.Equals(Estado_Usuario.Habilitado.ToString())) {
                            Response.Write("<script language='javascript'>window.alert('Se ha actualizado con éxito.');window.location='../Proveedor/WebEmpleado.aspx';</script>");
                        }
                        else {
                            Response.Write("<script language='javascript'>window.alert('Se ha actualizado con éxito.');window.location='../Administrador/WebVerUsuarios.aspx';</script>");
                        }
                    }
                    else {
                        error.Text = "La actualización ha fallado";
                        alerta.Visible = true;
                    }
                }

            }
            catch (Exception ex) {
                error.Text = "Excepcion" + ex.Message;
                alerta.Visible = true;
            }
        }
    }
}