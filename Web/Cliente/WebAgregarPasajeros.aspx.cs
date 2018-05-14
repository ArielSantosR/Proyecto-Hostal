using Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Cliente {
    public partial class WebAgregarPasajeros : System.Web.UI.Page {

        private Usuario user = new Usuario();
        private List<Huesped> pasajeros;

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
            error.Text = "";
            exito.Text = "";
            alerta_exito.Visible = false;
            alerta.Visible = false;
            if (MiSesion.TIPO_USUARIO.Equals(Tipo_Usuario.Administrador.ToString()) && MiSesion.ESTADO.Equals(Estado_Usuario.Habilitado.ToString())) {
                if (!IsPostBack) {
                    List<Modelo.Cliente> clientes = ClienteCollection.ListaClientes();
                    ddlEmpresa.DataSource = clientes;
                    ddlEmpresa.DataTextField = "NOMBRE_CLIENTE";
                    ddlEmpresa.DataValueField = "RUT_CLIENTE";
                    ddlEmpresa.DataBind();   
                }
                pasajeros = HuespedCollection.ListaHuesped().Where(x => x.RUT_CLIENTE == int.Parse(ddlEmpresa.SelectedValue)).ToList<Huesped>();
                if (pasajeros.Count != 0) {
                    divGrid.Visible = true;
                    CargarGridView(pasajeros);
                }
                else {
                    divGrid.Visible = false;
                }
            }
            else {
                divEmpresas.Visible = false;
                pasajeros = HuespedCollection.ListaHuesped().Where(x => x.RUT_CLIENTE == SesionCl.RUT_CLIENTE).ToList<Huesped>();
                if (pasajeros.Count != 0) {
                    divGrid.Visible = true;
                    CargarGridView(pasajeros);
                }
                else {
                    divGrid.Visible = false;
                }
            }
        }

        private void CargarGridView(List<Huesped> pasajeros) {

            //Creacion DataTable
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[6] {
                new DataColumn("Rut", typeof(string)),
                new DataColumn("Primer Nombre", typeof(string)),
                new DataColumn("Segundo Nombre",typeof(string)),
                new DataColumn("Apellido Paterno",typeof(string)),
                new DataColumn("Apellido Materno",typeof(string)),
                new DataColumn("Teléfono",typeof(long))
            });

            //Carga de datos en DataTable
            foreach (Modelo.Huesped h in pasajeros) {
                dt.Rows.Add(h.RUT_HUESPED + "-" + h.DV_HUESPED,h.PNOMBRE_HUESPED,h.SNOMBRE_HUESPED,h.APP_PATERNO_HUESPED,h.APP_MATERNO_HUESPED,h.TELEFONO_HUESPED);
            }

            //Carga de GriedView
            gvPasajeros.DataSource = dt;
            gvPasajeros.DataBind();
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

        protected void btnAgregar_Click(object sender,EventArgs e) {
            try {
                if (txtRutPasajero.Text != string.Empty && txtPNombre.Text != string.Empty && txtAPaterno.Text != string.Empty && txtAMaterno.Text != string.Empty) {
                    int telefono = 0;

                    if (txtNumeroTelefono.Text == string.Empty) {
                        
                        Huesped huesped = new Huesped();

                        huesped.RUT_HUESPED = int.Parse(txtRutPasajero.Text.Substring(0,txtRutPasajero.Text.Length - 2));
                        huesped.DV_HUESPED = txtRutPasajero.Text.Substring(txtRutPasajero.Text.Length - 1);

                        if (MiSesion.TIPO_USUARIO.Equals(Tipo_Usuario.Cliente.ToString()) && MiSesion.ESTADO.Equals(Estado_Usuario.Habilitado.ToString())) {
                            huesped.RUT_CLIENTE = SesionCl.RUT_CLIENTE;
                        }
                        else {
                            huesped.RUT_CLIENTE = int.Parse(ddlEmpresa.SelectedValue);
                        }


                        huesped.APP_MATERNO_HUESPED = txtAMaterno.Text;
                        huesped.APP_PATERNO_HUESPED = txtAPaterno.Text;
                        huesped.PNOMBRE_HUESPED = txtPNombre.Text;
                        huesped.SNOMBRE_HUESPED = txtSNombre.Text;
                        huesped.TELEFONO_HUESPED = telefono;
                        huesped.REGISTRADO = Registrado_Huesped.N.ToString();


                        if (!huesped.ExisteRut()) {
                            if (huesped.Crear()) {
                                exito.Text = "El registro ha se ha realizado con exito";
                                alerta_exito.Visible = true;
                                Limpiar();
                                if (MiSesion.TIPO_USUARIO.Equals(Tipo_Usuario.Administrador.ToString()) && MiSesion.ESTADO.Equals(Estado_Usuario.Habilitado.ToString())) {
                                    pasajeros = HuespedCollection.ListaHuesped().Where(x => x.RUT_CLIENTE == int.Parse(ddlEmpresa.SelectedValue)).ToList<Huesped>();
                                    if (pasajeros.Count != 0) {
                                        divGrid.Visible = true;
                                        CargarGridView(pasajeros);
                                    }
                                    else {
                                        divGrid.Visible = false;
                                    }
                                }
                                else {
                                    pasajeros = HuespedCollection.ListaHuesped().Where(x => x.RUT_CLIENTE == SesionCl.RUT_CLIENTE).ToList<Huesped>();
                                    if (pasajeros.Count != 0) {
                                        divGrid.Visible = true;
                                        CargarGridView(pasajeros);
                                    }
                                    else {
                                        divGrid.Visible = false;
                                    }
                                }

                            }
                            else {
                                error.Text = "El registro ha fallado";
                                alerta.Visible = true;
                            }
                        }
                        else {
                            error.Text = "El Rut ya existe";
                            alerta.Visible = true;
                        }
                    }
                    else {
                        if (int.TryParse(txtNumeroTelefono.Text,out telefono)) {
                            Huesped huesped = new Huesped();

                            huesped.RUT_HUESPED = int.Parse(txtRutPasajero.Text.Substring(0,txtRutPasajero.Text.Length - 2));
                            huesped.DV_HUESPED = txtRutPasajero.Text.Substring(txtRutPasajero.Text.Length - 1);

                            if (MiSesion.TIPO_USUARIO.Equals(Tipo_Usuario.Cliente.ToString()) && MiSesion.ESTADO.Equals(Estado_Usuario.Habilitado.ToString())) {
                                huesped.RUT_CLIENTE = SesionCl.RUT_CLIENTE;
                            }
                            else {
                                huesped.RUT_CLIENTE = int.Parse(ddlEmpresa.SelectedValue);
                            }


                            huesped.APP_MATERNO_HUESPED = txtAMaterno.Text;
                            huesped.APP_PATERNO_HUESPED = txtAPaterno.Text;
                            huesped.PNOMBRE_HUESPED = txtPNombre.Text;
                            huesped.SNOMBRE_HUESPED = txtSNombre.Text;
                            huesped.TELEFONO_HUESPED = telefono;
                            huesped.REGISTRADO = Registrado_Huesped.N.ToString();

                            if (!huesped.ExisteRut()) {
                                if (huesped.Crear()) {
                                    exito.Text = "El registro se ha realizado con exito";
                                    alerta_exito.Visible = true;
                                    Limpiar();
                                    if (MiSesion.TIPO_USUARIO.Equals(Tipo_Usuario.Administrador.ToString()) && MiSesion.ESTADO.Equals(Estado_Usuario.Habilitado.ToString())) {
                                        pasajeros = HuespedCollection.ListaHuesped().Where(x => x.RUT_CLIENTE == int.Parse(ddlEmpresa.SelectedValue)).ToList<Huesped>();
                                        if (pasajeros.Count != 0) {
                                            divGrid.Visible = true;
                                            CargarGridView(pasajeros);
                                        }
                                        else {
                                            divGrid.Visible = false;
                                        }
                                    }
                                    else {
                                        pasajeros = HuespedCollection.ListaHuesped().Where(x => x.RUT_CLIENTE == SesionCl.RUT_CLIENTE).ToList<Huesped>();
                                        if (pasajeros.Count != 0) {
                                            divGrid.Visible = true;
                                            CargarGridView(pasajeros);
                                        }
                                        else {
                                            divGrid.Visible = false;
                                        }
                                    }
                                }
                                else {
                                    error.Text = "El registro ha fallado";
                                    alerta.Visible = true;
                                }
                            }
                            else {
                                error.Text = "El Rut ya existe";
                                alerta.Visible = true;
                            }
                        }
                        else {
                            error.Text = "Verifique que ha ingresado el teléfono correctamente";
                            alerta.Visible = true;
                        }
                    }
                }
                else {
                    error.Text = "Verifique que ha ingresados todos los datos requeridos del Pasajero";
                    alerta.Visible = true;
                }

            }
            catch (Exception ex) {
                error.Text = "Excepción: " + ex.Message;
                alerta.Visible = true;
            }

        }

        private void Limpiar() {
            txtAMaterno.Text = string.Empty;
            txtAPaterno.Text = string.Empty;
            txtNumeroTelefono.Text = string.Empty;
            txtPNombre.Text = string.Empty;
            txtRutPasajero.Text = string.Empty;
            txtSNombre.Text = string.Empty;
        }

        protected void gvPasajeros_RowDeleting(object sender,GridViewDeleteEventArgs e) {
            Huesped huesped = new Huesped();
            huesped.RUT_HUESPED = int.Parse(e.Keys["Rut"].ToString().Substring(0,e.Keys["Rut"].ToString().Length - 2));
            if (huesped.Delete()) {
                pasajeros = HuespedCollection.ListaHuesped().Where(x => x.RUT_CLIENTE == int.Parse(ddlEmpresa.SelectedValue)).ToList<Huesped>();
                if (pasajeros.Count != 0) {
                    divGrid.Visible = true;
                    CargarGridView(pasajeros);
                }
                else {
                    divGrid.Visible = false;
                }
                exito.Text = "Se ha eliminado con exito";
                alerta_exito.Visible = true;

            }else {
                error.Text = "La eliminación ha fallado";
                alerta.Visible = true;
            }
        }

        protected void gvPasajeros_RowEditing(object sender,GridViewEditEventArgs e) {
            Huesped huesped = new Huesped();
            huesped.RUT_HUESPED = int.Parse(gvPasajeros.DataKeys[e.NewEditIndex].Value.ToString().Substring(0,gvPasajeros.DataKeys[e.NewEditIndex].Value.ToString().Length - 2));
            huesped.BuscarHuesped();
            SesionEdit = huesped;
            Response.Redirect("../Cliente/WebEditarPasajero.aspx");
        }
    }
}