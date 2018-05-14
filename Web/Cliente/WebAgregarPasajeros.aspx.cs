using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Cliente {
    public partial class WebAgregarPasajeros : System.Web.UI.Page {

        Usuario user = new Usuario();

        void Page_PreInit(object sender,EventArgs e) {
            if (MiSesion != null) {
                if (MiSesion.TIPO_USUARIO != null && MiSesion.ESTADO != null) {
                    if (MiSesion.TIPO_USUARIO.Equals(Tipo_Usuario.Administrador.ToString()) && MiSesion.ESTADO.Equals(Estado_Usuario.Habilitado.ToString())) {
                        MasterPageFile = "~/Administrador/AdminM.Master";
                        user = MiSesion;
                    }
                    else if (MiSesion.TIPO_USUARIO.Equals(Tipo_Usuario.Cliente.ToString()) && MiSesion.ESTADO.Equals(Estado_Usuario.Habilitado.ToString())) {
                        MasterPageFile = "~/Cliente/ClienteM.Master";
                        Modelo.Cliente cli = new Modelo.Cliente();
                        cli.BuscarCliente(MiSesion.ID_USUARIO);
                        SesionCl = cli;
                        user = MiSesion;
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
            if (MiSesion.TIPO_USUARIO.Equals(Tipo_Usuario.Cliente.ToString()) && MiSesion.ESTADO.Equals(Estado_Usuario.Habilitado.ToString())) {
                if (!IsPostBack) {
                    List<Modelo.Cliente> clientes = ClienteCollection.ListaClientes();
                    ddlEmpresa.DataSource = clientes;
                    ddlEmpresa.DataTextField = "NOMBRE_CLIENTE";
                    ddlEmpresa.DataValueField = "RUT_CLIENTE";
                    ddlEmpresa.DataBind();
                }
            }
        }

        //Sesion de Usuario
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
    }
}