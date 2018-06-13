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

namespace Web
{
    public partial class WebRegistro2 : System.Web.UI.Page
    {
        Modelo.RegionCollection coleccionRegion;
        Modelo.ComunaCollection coleccionComuna;

        protected void Page_Load(object sender, EventArgs e)
        {
            btnLimpiar.CausesValidation = false;
            btnLimpiar.UseSubmitBehavior = false;

            try
            {
                error.Text = "";
                //ddlComuna.Enabled = false;
                //ddlRegion.Enabled = false;
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

                List<Giro> giros = GiroCollection.ListaGiro();

                if (!IsPostBack)
                {
                    alerta.Visible = false;
                    ddlPais.DataSource = coleccionPais;
                    ddlPais.DataTextField = "NOMBRE_PAIS";
                    ddlPais.DataValueField = "ID_PAIS";
                    ddlPais.DataBind();
                    ddlPais.Items.Insert(0,new ListItem("Seleccione País...","0"));

                    ddlComuna.Enabled = false;
                    ddlComuna.Items.Insert(0,new ListItem("Seleccione Comuna...","0"));

                    ddlRegion.Enabled = false;
                    ddlRegion.Items.Insert(0,new ListItem("Seleccione Región...","0"));

                    ddlGiro.DataSource = giros;
                    ddlGiro.DataTextField = "NOMBRE_GIRO";
                    ddlGiro.DataValueField = "ID_GIRO";
                    ddlGiro.DataBind();
                    ddlGiro.Items.Insert(0,new ListItem("Seleccione Giro...","0"));
                }
            }
            catch (Exception ex)
            {
                error.Text = "Excepción: " + ex.ToString();
                alerta.Visible = true;
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNombre.Text != string.Empty && txtPassword.Text != string.Empty && txtConfirmar.Text != string.Empty)
                {
                    Modelo.Usuario usuario = new Modelo.Usuario();
                    usuario.NOMBRE_USUARIO = txtNombre.Text;
                    string hashed = BCrypt.HashPassword(txtPassword.Text, BCrypt.GenerateSalt(12));
                    usuario.PASSWORD = hashed;
                    usuario.ESTADO = Estado_Usuario.Habilitado.ToString();
                    usuario.TIPO_USUARIO = Tipo_Usuario.Cliente.ToString();

                    Service1 s = new Service1();
                    XmlSerializer sr = new XmlSerializer(typeof(Modelo.Usuario));
                    StringWriter writer = new StringWriter();
                    sr.Serialize(writer, usuario);
                    if (ddlGiro.SelectedIndex != 0) {
                        if (ddlComuna.SelectedIndex != 0) {
                            if (txtPassword.Text.Equals(txtConfirmar.Text)) {
                                if (!s.ExisteUsuario(writer.ToString())) {
                                    if (txtRut.Text != string.Empty && txtNombreC.Text != string.Empty && txtDireccion.Text != string.Empty) {
                                        int telefono = 0;

                                        if (txtTelefono.Text == string.Empty) {

                                            Modelo.Cliente cliente = new Modelo.Cliente();
                                            Modelo.Proveedor proveedor = new Modelo.Proveedor();
                                            Modelo.Empleado empleado = new Modelo.Empleado();

                                            cliente.RUT_CLIENTE = int.Parse(txtRut.Text.Substring(0,txtRut.Text.Length - 2));
                                            proveedor.RUT_PROVEEDOR = int.Parse(txtRut.Text.Substring(0,txtRut.Text.Length - 2));
                                            empleado.RUT_EMPLEADO = int.Parse(txtRut.Text.Substring(0,txtRut.Text.Length - 2));

                                            cliente.ID_COMUNA = short.Parse(ddlComuna.SelectedValue);
                                            cliente.DV_CLIENTE = txtRut.Text.Substring(txtRut.Text.Length - 1);
                                            cliente.DIRECCION_CLIENTE = txtDireccion.Text;
                                            cliente.NOMBRE_CLIENTE = txtNombreC.Text;
                                            if (txtEmail.Text == string.Empty) {
                                                cliente.CORREO_CLIENTE = "";
                                            }
                                            else {
                                                cliente.CORREO_CLIENTE = txtEmail.Text;
                                            }

                                            cliente.TELEFONO_CLIENTE = telefono;
                                            cliente.ID_GIRO = short.Parse(ddlGiro.SelectedValue);

                                            XmlSerializer sr2 = new XmlSerializer(typeof(Modelo.Cliente));
                                            StringWriter writer2 = new StringWriter();
                                            sr2.Serialize(writer2,cliente);
                                            writer2.Close();

                                            XmlSerializer sr3 = new XmlSerializer(typeof(Modelo.Proveedor));
                                            StringWriter writer3 = new StringWriter();
                                            sr3.Serialize(writer3,proveedor);
                                            writer3.Close();

                                            XmlSerializer sr4 = new XmlSerializer(typeof(Modelo.Empleado));
                                            StringWriter writer4 = new StringWriter();
                                            sr4.Serialize(writer4,empleado);
                                            writer4.Close();

                                            if (!s.ExisteRutC(writer2.ToString()) && !s.ExisteRutP(writer3.ToString()) && !s.ExisteRutE(writer4.ToString())) {
                                                if (s.RegistroUsuario(writer.ToString()) && s.RegistroCliente(writer2.ToString())) {
                                                    Response.Write("<script language='javascript'>window.alert('Se ha registrado con éxito. pruebe iniciar sesión');window.location='../Hostal/WebLogin.aspx';</script>");
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
                                            if (int.TryParse(txtTelefono.Text,out telefono)) {
                                                Modelo.Cliente cliente = new Modelo.Cliente();
                                                Modelo.Proveedor proveedor = new Modelo.Proveedor();
                                                Modelo.Empleado empleado = new Modelo.Empleado();

                                                cliente.RUT_CLIENTE = int.Parse(txtRut.Text.Substring(0,txtRut.Text.Length - 2));
                                                proveedor.RUT_PROVEEDOR = int.Parse(txtRut.Text.Substring(0,txtRut.Text.Length - 2));
                                                empleado.RUT_EMPLEADO = int.Parse(txtRut.Text.Substring(0,txtRut.Text.Length - 2));

                                                cliente.ID_COMUNA = short.Parse(ddlComuna.SelectedValue);
                                                cliente.DV_CLIENTE = txtRut.Text.Substring(txtRut.Text.Length - 1);
                                                cliente.DIRECCION_CLIENTE = txtDireccion.Text;
                                                cliente.NOMBRE_CLIENTE = txtNombreC.Text;
                                                if (txtEmail.Text == string.Empty) {
                                                    cliente.CORREO_CLIENTE = "";
                                                }
                                                else {
                                                    cliente.CORREO_CLIENTE = txtEmail.Text;
                                                }

                                                cliente.TELEFONO_CLIENTE = telefono;
                                                cliente.ID_GIRO = short.Parse(ddlGiro.SelectedValue);

                                                XmlSerializer sr2 = new XmlSerializer(typeof(Modelo.Cliente));
                                                StringWriter writer2 = new StringWriter();
                                                sr2.Serialize(writer2,cliente);
                                                writer2.Close();

                                                XmlSerializer sr3 = new XmlSerializer(typeof(Modelo.Proveedor));
                                                StringWriter writer3 = new StringWriter();
                                                sr3.Serialize(writer3,proveedor);
                                                writer3.Close();

                                                XmlSerializer sr4 = new XmlSerializer(typeof(Modelo.Empleado));
                                                StringWriter writer4 = new StringWriter();
                                                sr4.Serialize(writer4,empleado);
                                                writer4.Close();

                                                if (!s.ExisteRutC(writer2.ToString()) && !s.ExisteRutP(writer3.ToString()) && !s.ExisteRutE(writer4.ToString())) {
                                                    if (s.RegistroUsuario(writer.ToString()) && s.RegistroCliente(writer2.ToString())) {
                                                        //Datos Usuario
                                                        txtNombre.Text = string.Empty;
                                                        txtPassword.Text = string.Empty;
                                                        txtConfirmar.Text = string.Empty;

                                                        //Datos Cliente
                                                        txtRut.Text = string.Empty;
                                                        txtNombreC.Text = string.Empty;
                                                        txtDireccion.Text = string.Empty;
                                                        txtEmail.Text = string.Empty;
                                                        txtTelefono.Text = string.Empty;
                                                        ddlPais.SelectedIndex = 0;
                                                        ddlRegion.SelectedIndex = 0;
                                                        ddlComuna.SelectedIndex = 0;
                                                        ddlGiro.SelectedIndex = 0;

                                                        Response.Write("<script language='javascript'>window.alert('Se ha registrado con éxito. pruebe iniciar sesión');window.location='../Hostal/WebLogin.aspx';</script>");
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
                                        error.Text = "Verifique que ha ingresados todos los datos requeridos de Cliente";
                                        alerta.Visible = true;
                                    }
                                }
                                else {
                                    error.Text = "El Nombre de usuario ya ha sido utilizado. Intente con otro";
                                    alerta.Visible = true;
                                }
                            }
                            else {
                                error.Text = "Las Contraseñas no coinciden";
                                alerta.Visible = true;
                            }
                        }
                        else {
                            error.Text = "Verifique que selecciono correctamente la comuna, región o país";
                            alerta.Visible = true;
                        }
                    }else {
                        error.Text = "Seleccione un giro valido";
                        alerta.Visible = true;
                    }
                }
                else
                {
                    error.Text = "Verifique que todos los datos de usuario hayan sido ingresados";
                    alerta.Visible = true;
                }
            }
            catch (Exception ex)
            {
                error.Text = "Excepción: " + ex.ToString();
                alerta.Visible = true;
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            //Datos Usuario
            txtNombre.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtConfirmar.Text = string.Empty;

            //Datos Cliente
            txtRut.Text = string.Empty;
            txtNombreC.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            ddlPais.SelectedIndex = 0;
            ddlRegion.SelectedIndex = 0;
            ddlComuna.SelectedIndex = 0;
        }

        protected void ddlPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlRegion.DataSource = coleccionRegion.Where(x => x.Id_Pais == int.Parse(ddlPais.SelectedValue));
            ddlRegion.DataTextField = "Nombre";
            ddlRegion.DataValueField = "Id_Region";
            ddlRegion.DataBind();
            ddlRegion.Items.Insert(0,new ListItem("Seleccione Región...","0"));

            ddlRegion.Enabled = true;
        }

        protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlComuna.DataSource = coleccionComuna.Where(x => x.Id_Region == int.Parse(ddlRegion.SelectedValue));
            ddlComuna.DataTextField = "Nombre";
            ddlComuna.DataValueField = "Id_Comuna";
            ddlComuna.DataBind();
            ddlComuna.Items.Insert(0,new ListItem("Seleccione Comuna...","0"));

            ddlComuna.Enabled = true;

        }
    }
}