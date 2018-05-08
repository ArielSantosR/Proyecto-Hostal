using Modelo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;
using WcfNegocio;

namespace Web.Cliente {
    public partial class WebEditarCliente : System.Web.UI.Page {

        private Modelo.RegionCollection coleccionRegion;
        private Modelo.ComunaCollection coleccionComuna;

        void Page_PreInit(object sender,EventArgs e) {
            if (MiSesion != null) {
                if (MiSesion.TIPO_USUARIO != null && MiSesion.ESTADO != null) {
                    if (MiSesion.TIPO_USUARIO.Equals(Tipo_Usuario.Administrador.ToString()) &&
                    MiSesion.ESTADO.Equals(Estado_Usuario.Habilitado.ToString())) {
                        MasterPageFile = "~/Administrador/AdminM.Master";
                    }
                    else if (MiSesion.TIPO_USUARIO.Equals(Tipo_Usuario.Cliente.ToString()) &&
                    MiSesion.ESTADO.Equals(Estado_Usuario.Habilitado.ToString())) {
                        Modelo.Cliente cli = new Modelo.Cliente();
                        cli.BuscarCliente(MiSesion.ID_USUARIO);
                        SesionCl = cli;
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

            //Cargando DDL Pais
            Service1 service = new Service1();
            string paises = service.ListarPais();
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.PaisCollection));
            StringReader reader = new StringReader(paises);
            Modelo.PaisCollection coleccionPais = (Modelo.PaisCollection)ser.Deserialize(reader);
            reader.Close();

            //Cargando DDL Regiones
            string regiones = service.ListarRegion();
            XmlSerializer ser1 = new XmlSerializer(typeof(Modelo.RegionCollection));
            StringReader reader1 = new StringReader(regiones);
            coleccionRegion = (Modelo.RegionCollection)ser1.Deserialize(reader1);
            reader1.Close();

            //Cargando DDL Comunas
            string comunas = service.ListarComuna();
            XmlSerializer ser2 = new XmlSerializer(typeof(Modelo.ComunaCollection));
            StringReader reader2 = new StringReader(comunas);
            coleccionComuna = (Modelo.ComunaCollection)ser2.Deserialize(reader2);
            reader2.Close();

            //Bloqueo de cambios dependiendo del usuario
            if (MiSesion.TIPO_USUARIO.Equals(Tipo_Usuario.Cliente.ToString()) && MiSesion.ESTADO.Equals(Estado_Usuario.Habilitado.ToString())) {
                txtNombre.Enabled = false;
                txtRut.Enabled = false;
                ddlEstado.Enabled = false;
            }
            else {
                txtNombre.Enabled = false;
                txtRut.Enabled = false;
            }

            if (!IsPostBack) {

                ddlEstado.DataSource = Enum.GetValues(typeof(Estado_Usuario));
                ddlEstado.DataBind();

                alerta.Visible = false;
                ddlPais.DataSource = coleccionPais;
                ddlPais.DataTextField = "NOMBRE_PAIS";
                ddlPais.DataValueField = "ID_PAIS";
                ddlPais.DataBind();

                ddlRegion.DataSource = coleccionRegion.Where(x => x.Id_Pais == int.Parse(ddlPais.SelectedValue));
                ddlRegion.DataTextField = "Nombre";
                ddlRegion.DataValueField = "Id_Region";
                ddlRegion.DataBind();

                ddlComuna.DataSource = coleccionComuna.Where(x => x.Id_Region == int.Parse(ddlRegion.SelectedValue));
                ddlComuna.DataTextField = "Nombre";
                ddlComuna.DataValueField = "Id_Comuna";
                ddlComuna.DataBind();


                Comuna com = new Comuna();

                var listaC = coleccionComuna.Where(x => x.Id_Comuna == SesionCl.ID_COMUNA).ToList();
                com.Id_Comuna = listaC[0].Id_Comuna;
                com.Id_Region = listaC[0].Id_Region;
                com.Nombre = listaC[0].Nombre;

                Region reg = new Region();

                var listaR = coleccionRegion.Where(x => x.Id_Region == com.Id_Region).ToList();
                reg.Id_Pais = listaR[0].Id_Pais;
                reg.Id_Region = listaR[0].Id_Region;
                reg.Nombre = listaR[0].Nombre;

                Pais pais = new Pais();

                var listaP = coleccionPais.Where(x => x.ID_PAIS == reg.Id_Pais).ToList();
                pais.ID_PAIS = listaP[0].ID_PAIS;
                pais.NOMBRE_PAIS = listaP[0].NOMBRE_PAIS;

                //Carga de datos actuales
                txtRut.Text = SesionCl.RUT_CLIENTE.ToString() + "-" + SesionCl.DV_CLIENTE;
                txtDireccion.Text = SesionCl.DIRECCION_CLIENTE;
                txtEmail.Text = SesionCl.CORREO_CLIENTE;
                if (MiSesion.TIPO_USUARIO.Equals(Tipo_Usuario.Cliente.ToString()) &&
                        MiSesion.ESTADO.Equals(Estado_Usuario.Habilitado.ToString())) {
                    txtNombre.Text = MiSesion.NOMBRE_USUARIO;
                }
                else {
                    txtNombre.Text = SesionEdit.NOMBRE_USUARIO;
                }

                txtTelefono.Text = SesionCl.TELEFONO_CLIENTE.ToString();
                txtNombreC.Text = SesionCl.NOMBRE_CLIENTE;
                ddlComuna.SelectedValue = com.Id_Comuna.ToString();
                ddlRegion.SelectedValue = reg.Id_Region.ToString();
                ddlPais.SelectedValue = pais.ID_PAIS.ToString();
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

        protected void btnEditar_Click(object sender,EventArgs e) {
            try {
                bool flag = true;
                Usuario usuario = new Modelo.Usuario();
                Modelo.Cliente cliente = SesionCl;

                if (MiSesion.TIPO_USUARIO.Equals(Tipo_Usuario.Cliente.ToString()) && MiSesion.ESTADO.Equals(Estado_Usuario.Habilitado.ToString())) {
                    usuario = MiSesion;
                }
                else {
                    usuario = SesionEdit;
                }

                if (!string.IsNullOrEmpty(txtPassword.Text) && (!string.IsNullOrEmpty(txtConfirmar.Text))) {
                    if (txtPassword.Text.Equals(txtConfirmar.Text)) {
                        string hashed = BCrypt.HashPassword(txtPassword.Text,BCrypt.GenerateSalt(12));
                        usuario.PASSWORD = hashed;
                    }
                    else {
                        error.Text = "Las contraseñas no son iguales";
                        flag = false;
                        alerta.Visible = true;
                    }
                }

                if (!txtNombreC.Text.Equals(cliente.NOMBRE_CLIENTE)) {
                    cliente.NOMBRE_CLIENTE = txtNombreC.Text;
                }

                if (!txtDireccion.Text.Equals(cliente.DIRECCION_CLIENTE)) {
                    cliente.DIRECCION_CLIENTE = txtDireccion.Text;
                    cliente.ID_COMUNA = short.Parse(ddlComuna.SelectedValue);
                }

                if (!txtEmail.Text.Equals(cliente.CORREO_CLIENTE)) {
                    cliente.CORREO_CLIENTE = txtEmail.Text;
                }

                if (!txtTelefono.Text.Equals(cliente.TELEFONO_CLIENTE.ToString())) {
                    cliente.TELEFONO_CLIENTE = long.Parse(txtTelefono.Text);
                }

                if (MiSesion.TIPO_USUARIO.Equals(Modelo.Tipo_Usuario.Administrador.ToString())) {
                    usuario.ESTADO = ddlEstado.SelectedValue.ToString();
                }

                if (flag) {
                    if (usuario.Update() && cliente.Update()) {
                        if (MiSesion.TIPO_USUARIO.Equals(Tipo_Usuario.Cliente.ToString()) && MiSesion.ESTADO.Equals(Estado_Usuario.Habilitado.ToString())) {
                            Response.Write("<script language='javascript'>window.alert('Se ha actualizado con éxito.');window.location='../Cliente/WebCliente.aspx';</script>");
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
            catch (Exception) {
                error.Text = "Excepcion";
                alerta.Visible = true;
            }
        }

        protected void ddlPais_SelectedIndexChanged(object sender,EventArgs e) {
            ddlRegion.DataSource = coleccionRegion.Where(x => x.Id_Pais == int.Parse(ddlPais.SelectedValue));
            ddlRegion.DataTextField = "Nombre";
            ddlRegion.DataValueField = "Id_Region";
            ddlRegion.DataBind();
        }

        protected void ddlRegion_SelectedIndexChanged(object sender,EventArgs e) {
            ddlComuna.DataSource = coleccionComuna.Where(x => x.Id_Region == int.Parse(ddlRegion.SelectedValue));
            ddlComuna.DataTextField = "Nombre";
            ddlComuna.DataValueField = "Id_Comuna";
            ddlComuna.DataBind();
            ddlComuna.Enabled = true;
        }
    }
}