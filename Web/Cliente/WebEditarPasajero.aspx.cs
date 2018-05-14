using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Cliente {
    public partial class WebEditarPasajero : System.Web.UI.Page {

        void Page_PreInit(object sender,EventArgs e) {
            if (MiSesion != null) {
                if (MiSesion.TIPO_USUARIO != null && MiSesion.ESTADO != null) {
                    if (MiSesion.TIPO_USUARIO.Equals(Tipo_Usuario.Administrador.ToString()) && MiSesion.ESTADO.Equals(Estado_Usuario.Habilitado.ToString())) {
                        MasterPageFile = "~/Administrador/AdminM.Master";
                    }
                    else if (MiSesion.TIPO_USUARIO.Equals(Tipo_Usuario.Cliente.ToString()) && MiSesion.ESTADO.Equals(Estado_Usuario.Habilitado.ToString())) {
                        MasterPageFile = "~/Cliente/ClienteM.Master";
                        Modelo.Cliente cli = new Modelo.Cliente();
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
            txtRutPasajero.Enabled = false;
            error.Text = string.Empty;
            alerta.Visible = false;
            if (!IsPostBack) {
                txtRutPasajero.Text = SesionEdit.RUT_HUESPED + "-" + SesionEdit.DV_HUESPED;
                txtPNombre.Text = SesionEdit.PNOMBRE_HUESPED;
                txtSNombre.Text = SesionEdit.SNOMBRE_HUESPED;
                txtAPaterno.Text = SesionEdit.APP_PATERNO_HUESPED;
                txtAMaterno.Text = SesionEdit.APP_MATERNO_HUESPED;
                txtNumeroTelefono.Text = SesionEdit.TELEFONO_HUESPED.ToString();
            }
        }

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

        //Sesion Cliente
        public Modelo.Cliente SesionCl {
            get {
                if (Session["Cliente"] == null) {
                    Session["Cliente"] = new Modelo.Cliente();
                }
                return (Modelo.Cliente)Session["Cliente"];
            }
            set {
                Session["Cliente"] = value;
            }
        }

        //Sesion Editar
        public Huesped SesionEdit {
            get {
                if (Session["Huesped"] == null) {
                    Session["Huesped"] = new Modelo.Huesped();
                }
                return (Huesped)Session["Huesped"];
            }

            set {
                Session["Huesped"] = value;
            }
        }

        protected void btnEditar_Click(object sender,EventArgs e) {
            try {
                Huesped huesped = new Huesped();
                huesped = SesionEdit;
                long num = long.Parse(txtNumeroTelefono.Text);

                if (!huesped.PNOMBRE_HUESPED.Equals(txtPNombre.Text)) {
                    huesped.PNOMBRE_HUESPED = txtPNombre.Text;
                }
                if (!txtSNombre.Text.Equals(huesped.SNOMBRE_HUESPED)) {
                    huesped.SNOMBRE_HUESPED = txtSNombre.Text;
                }
                if (!huesped.APP_PATERNO_HUESPED.Equals(txtAPaterno.Text)) {
                    huesped.APP_PATERNO_HUESPED = txtAPaterno.Text;
                }
                if (!huesped.APP_MATERNO_HUESPED.Equals(txtAMaterno.Text)) {
                    huesped.APP_MATERNO_HUESPED = txtAMaterno.Text;
                }
                if (huesped.TELEFONO_HUESPED != num) {
                    huesped.TELEFONO_HUESPED = num;
                }
                if (huesped.Update()) {
                    Response.Write("<script language='javascript'>window.alert('Se ha actualizado con éxito.');window.location='../Cliente/WebAgregarPasajeros.aspx';</script>");
                }
                else {
                    error.Text = "Actualizacion a fallado";
                    alerta.Visible = true;
                }
            }catch(Exception ex) {
                error.Text = "Excepción: " + ex.Message;
                alerta.Visible = true;
            }
            
        }
    }
}