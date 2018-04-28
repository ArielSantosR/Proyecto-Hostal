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
    public partial class WebAgregarPlato : System.Web.UI.Page
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
            }

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
                        plato.NOMBRE_PLATO = txtNombre.Text;
                        plato.PRECIO_PLATO = precio;
                        plato.ID_CATEGORIA = short.Parse(ddlCategoria.SelectedValue);
                        plato.ID_TIPO_PLATO = short.Parse(ddlTipo.SelectedValue);

                        Service1 s = new Service1();
                        XmlSerializer sr = new XmlSerializer(typeof(Modelo.Plato));
                        StringWriter writer = new StringWriter();
                        sr.Serialize(writer, plato);

                        if (!s.ExistePlato(writer.ToString()))
                        {
                            if (s.AgregarPlato(writer.ToString()))
                            {
                                exito.Text = "El plato ha sido agregado con éxito";
                                alerta_exito.Visible = true;
                                alerta.Visible = false;
                            }
                            else
                            {
                                alerta_exito.Visible = false;
                                error.Text = "No se ha podido agregar";
                                alerta.Visible = true;
                            }
                        }
                        else
                        {
                            alerta_exito.Visible = false;
                            error.Text = "Datos Ingresados incorrectamente, verifique que ha ingresado numeros correctamente";
                            alerta.Visible = true;
                        }

                    }
                    else
                    {
                        alerta_exito.Visible = false;
                        error.Text = "Datos Ingresados incorrectamente, verifique que ha ingresado todos los campos";
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
    }
}