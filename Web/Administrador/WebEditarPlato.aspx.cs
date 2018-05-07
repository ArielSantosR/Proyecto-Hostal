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

namespace Web.Administrador
{
    public partial class WebEditarPlato : System.Web.UI.Page
    {
        void Page_PreInit(object sender, EventArgs e)
        {
            if (MiSesion != null)
            {
                if (MiSesion.TIPO_USUARIO != null && MiSesion.ESTADO != null)
                {
                    if (MiSesion.TIPO_USUARIO.Equals("Administrador") &&
                    MiSesion.ESTADO.Equals("Habilitado"))
                    {
                        MasterPageFile = "~/Administrador/AdminM.Master";
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

        protected void Page_Load(object sender, EventArgs e)
        {
            error.Text = "";
            exito.Text = "";
            alerta_exito.Visible = false;
            alerta.Visible = false;
            btnLimpiar.CausesValidation = false;
            btnLimpiar.UseSubmitBehavior = false;

            //Cargando DDL Categoria
            Service1 service = new Service1();
            string categoria = service.ListarCategoria();
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.CategoriaCollection));
            StringReader reader = new StringReader(categoria);
            Modelo.CategoriaCollection coleccionCategoria = (Modelo.CategoriaCollection)ser.Deserialize(reader);
            reader.Close();

            //Cargando DDL Tipo Plato
            string tipo_plato = service.ListarTipoPlato();
            XmlSerializer ser2 = new XmlSerializer(typeof(Modelo.TipoPlatoCollection));
            StringReader reader2 = new StringReader(tipo_plato);
            Modelo.TipoPlatoCollection coleccionTipoPlato = (Modelo.TipoPlatoCollection)ser2.Deserialize(reader2);
            reader.Close();

            if (!IsPostBack)
            {
                ddlCategoria.DataSource = coleccionCategoria;
                ddlCategoria.DataTextField = "NOMBRE_CATEGORIA";
                ddlCategoria.DataValueField = "ID_CATEGORIA";
                ddlCategoria.DataBind();

                ddlTipo.DataSource = coleccionTipoPlato;
                ddlTipo.DataTextField = "NOMBRE_TIPO_PLATO";
                ddlTipo.DataValueField = "ID_TIPO_PLATO";
                ddlTipo.DataBind();

                if (MiSesionPlato.ID_PLATO != 0)
                {
                    Modelo.Plato plato = new Modelo.Plato();
                    plato.ID_PLATO = MiSesionPlato.ID_PLATO;

                    Service1 s = new Service1();
                    XmlSerializer sr = new XmlSerializer(typeof(Modelo.Plato));
                    StringWriter writer = new StringWriter();
                    sr.Serialize(writer, plato);

                    if (s.ObtenerPlato(writer.ToString()) != null)
                    {
                        plato = s.ObtenerPlato(writer.ToString());

                        txtNombre.Text = plato.NOMBRE_PLATO;
                        txtPrecio.Text = plato.PRECIO_PLATO + "";
                        ddlCategoria.SelectedIndex = plato.ID_CATEGORIA - 1;
                        ddlTipo.SelectedIndex = plato.ID_TIPO_PLATO - 1;
                    }
                }
                else
                {
                    Response.Write("<script language='javascript'>window.alert('No puede acceder a esta página');window.location='../Hostal/WebLogin.aspx';</script>");
                }

            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                int precio = 0;

                if (txtNombre.Text != string.Empty && txtPrecio.Text != string.Empty)
                {
                    if (int.TryParse(txtPrecio.Text, out precio))
                    {
                        Modelo.Plato plato = new Modelo.Plato();
                        plato.ID_PLATO = MiSesionPlato.ID_PLATO;
                        plato.NOMBRE_PLATO = txtNombre.Text;
                        plato.PRECIO_PLATO = precio;
                        plato.ID_CATEGORIA = short.Parse(ddlCategoria.SelectedValue);
                        plato.ID_TIPO_PLATO = short.Parse(ddlTipo.SelectedValue);

                        Service1 s = new Service1();
                        XmlSerializer sr = new XmlSerializer(typeof(Modelo.Plato));
                        StringWriter writer = new StringWriter();
                        sr.Serialize(writer, plato);

                        if (s.ModificarPlato(writer.ToString()))
                        {
                            exito.Text = "Plato modificado con éxito";
                            alerta_exito.Visible = true;
                            alerta.Visible = false;
                        }
                        else
                        {
                            alerta_exito.Visible = false;
                            error.Text = "No se ha podido modificar";
                            alerta.Visible = true;
                        }

                    }
                    else
                    {
                        alerta_exito.Visible = false;
                        error.Text = "Datos Ingresados incorrectamente, verifique que ha ingresado números correctamente";
                        alerta.Visible = true;
                    }
                }
                else
                {
                    alerta_exito.Visible = false;
                    error.Text = "Debe llenar todos los datos";
                    alerta.Visible = true;
                }

            }
            catch (Exception)
            {
                alerta_exito.Visible = false;
                error.Text = "Excepcion";
                alerta.Visible = true;
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNombre.Text = string.Empty;
            txtPrecio.Text = string.Empty;
            ddlCategoria.SelectedIndex = 0;
            ddlTipo.SelectedIndex = 0;
        }

        //Creación de Sesión
        public Usuario MiSesion
        {
            get
            {
                if (Session["Usuario"] == null)
                {
                    Session["Usuario"] = new Usuario();
                }
                return (Usuario)Session["Usuario"];
            }
            set
            {
                Session["Usuario"] = value;
            }
        }

        public Plato MiSesionPlato
        {
            get
            {
                if (Session["Plato"] == null)
                {
                    Session["Plato"] = new Plato();
                }
                return (Plato)Session["Plato"];
            }
            set
            {
                Session["Plato"] = value;
            }
        }
    }
}