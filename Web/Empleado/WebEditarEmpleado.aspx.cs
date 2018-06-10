using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Empleado {
    public partial class WebEditarEmpleado : System.Web.UI.Page {

        void Page_PreInit(object sender,EventArgs e) {
            if (MiSesion != null) {
                if (MiSesion.TIPO_USUARIO != null && MiSesion.ESTADO != null) {
                    if (MiSesion.TIPO_USUARIO.Equals(Tipo_Usuario.Administrador.ToString()) &&
                    MiSesion.ESTADO.Equals(Estado_Usuario.Habilitado.ToString())) {
                        MasterPageFile = "~/Administrador/AdminM.Master";
                        Modelo.Empleado emp = new Modelo.Empleado();
                        emp.BuscarEmpleado(MiSesion.ID_USUARIO);
                        SesionEmp = emp;
                    }
                    else if (MiSesion.TIPO_USUARIO.Equals(Tipo_Usuario.Empleado.ToString()) &&
                    MiSesion.ESTADO.Equals(Estado_Usuario.Habilitado.ToString())) {
                        Modelo.Empleado emp = new Modelo.Empleado();
                        emp.BuscarEmpleado(MiSesion.ID_USUARIO);
                        SesionEmp = emp;
                    }
                    else
                    {
                        Response.Write("<script language='javascript'>window.alert('No Posee los permisos necesarios');window.location='../Hostal/WebLogin.aspx';</script>");
                    }
                }
                else
                {
                    Response.Write("<script language='javascript'>window.alert('Debe Iniciar Sesión Primero');window.location='../Hostal/WebLogin.aspx';</script>");
                }
            }
        }

        protected void Page_Load(object sender,EventArgs e) {
            try {
                error.Text = "";
                //Bloqueo de cambios dependiendo del usuario
                if (MiSesion != null) {
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

                        alerta.Visible = false;
                        //Carga de datos actuales
                        txtRut.Text = SesionEmp.RUT_EMPLEADO.ToString() + "-" + SesionEmp.DV_EMPLEADO;
                        txtPNombre.Text = SesionEmp.PNOMBRE_EMPLEADO;
                        txtSNombre.Text = SesionEmp.SNOMBRE_EMPLEADO;
                        if (MiSesion.TIPO_USUARIO.Equals(Tipo_Usuario.Empleado.ToString())) {
                            txtUsuario.Text = MiSesion.NOMBRE_USUARIO;
                        }
                        else {
                            txtUsuario.Text = SesionEdit.NOMBRE_USUARIO;
                        }
                        txtApellidoP.Text = SesionEmp.APP_PATERNO_EMPLEADO;
                        txtApellidoM.Text = SesionEmp.APP_MATERNO_EMPLEADO;
                    }
                }
                else {
                    Response.Write("<script language='javascript'>window.alert('Debe Iniciar Sesión Primero');window.location='../Hostal/WebLogin.aspx';</script>");
                }
            }
            catch (Exception) {
                Response.Write("<script language='javascript'>window.alert('Debe Iniciar Sesión Primero');window.location='../Hostal/WebLogin.aspx';</script>");
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

        //Sesiones para editar datos
        public Modelo.Empleado SesionEmp {
            get {
                if (Session["Empleado"] == null) {
                    Session["Empleado"] = new Modelo.Empleado();
                }
                return (Modelo.Empleado)Session["Empleado"];
            }
            set {
                Session["Empleado"] = value;
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
                Modelo.Empleado empleado = SesionEmp;

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

                if (!txtPNombre.Text.Equals(empleado.PNOMBRE_EMPLEADO)) {
                    empleado.PNOMBRE_EMPLEADO = txtPNombre.Text;
                }

                if (!txtSNombre.Text.Equals(empleado.SNOMBRE_EMPLEADO)) {
                    empleado.SNOMBRE_EMPLEADO = txtSNombre.Text;
                }

                if (!txtApellidoP.Text.Equals(empleado.APP_PATERNO_EMPLEADO)) {
                    empleado.APP_PATERNO_EMPLEADO = txtApellidoP.Text;
                }

                if (!txtApellidoM.Text.Equals(empleado.APP_MATERNO_EMPLEADO)) {
                    empleado.APP_MATERNO_EMPLEADO = txtApellidoM.Text;
                }

                if (MiSesion.TIPO_USUARIO.Equals(Modelo.Tipo_Usuario.Administrador.ToString())) {
                    usuario.ESTADO = ddlEstado.SelectedValue.ToString();

                    if (MiSesion.TIPO_USUARIO.Equals(Modelo.Tipo_Usuario.Administrador.ToString()) && usuario.ESTADO.Equals("Deshabilitado"))
                    {
                        error.Text = "No puede deshabilitar a un administrador.";
                        flag = false;
                        alerta.Visible = true;
                    } 
                }

                if (flag) {
                    if (usuario.Update() && empleado.Update()) {
                        if (MiSesion.TIPO_USUARIO.Equals(Tipo_Usuario.Empleado.ToString()) && MiSesion.ESTADO.Equals(Estado_Usuario.Habilitado.ToString())) {
                            Response.Write("<script language='javascript'>window.alert('Se ha actualizado con éxito.');window.location='../Empleado/WebEmpleado.aspx';</script>");
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
            catch (Exception ex)
            {
                error.Text = "Excepción: " + ex.ToString();
                alerta.Visible = true;
            }
        }
    }
}