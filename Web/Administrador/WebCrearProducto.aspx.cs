using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using WcfNegocio;
using System.Xml.Serialization;
using System.IO;

namespace Web.Administrador
{
    public partial class WebCrearProducto : System.Web.UI.Page
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

            Service1 service = new Service1();
            string familia = service.ListarFamilia();
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.FamiliaCollection));
            StringReader reader = new StringReader(familia);
            Modelo.FamiliaCollection coleccionFamilia = (Modelo.FamiliaCollection)ser.Deserialize(reader);
            reader.Close();


            if (!IsPostBack)
            {

                ddlFamilia.DataSource = coleccionFamilia;
                ddlFamilia.DataValueField = "ID_FAMILIA";
                ddlFamilia.DataTextField = "NOMBRE_FAMILIA";
                ddlFamilia.DataBind();

            }

        }

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

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNombre.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtFechaVencimiento.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtPrecio.Text = string.Empty;
            txtStock.Text = string.Empty;
            txtStockCritico.Text = string.Empty;

            ddlFamilia.SelectedIndex = 0;

        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            short stock = 0;
            short stockCritico = 0;
            int precio = 0;
            DateTime fecha;

            try
            {
                if (txtNombre.Text != string.Empty && txtPrecio.Text != string.Empty &&
                txtStock.Text != string.Empty && txtStockCritico.Text != string.Empty &&
                txtDescripcion.Text != string.Empty)
                {
                    Modelo.Producto producto = new Modelo.Producto();
                    producto.NOMBRE_PRODUCTO = txtNombre.Text;
                    producto.ID_FAMILIA = short.Parse(ddlFamilia.SelectedValue);
                    producto.DESCRIPCION_PRODUCTO = txtDescripcion.Text;

                    if (DateTime.TryParse(txtFechaVencimiento.Text, out fecha))
                    {
                        producto.FECHA_VENCIMIENTO_PRODUCTO = fecha;
                    }
                    else
                    {
                        producto.FECHA_VENCIMIENTO_PRODUCTO = DateTime.Parse("01-01-1900");
                    }

                    if (short.TryParse(txtStock.Text, out stock) && 
                        short.TryParse(txtStockCritico.Text, out stockCritico)  && 
                        int.TryParse(txtPrecio.Text, out precio))
                    {
                        producto.STOCK_PRODUCTO = stock;
                        producto.STOCK_CRITICO_PRODUCTO = stockCritico;
                        producto.PRECIO_PRODUCTO = precio;

                        Service1 s = new Service1();
                        XmlSerializer sr = new XmlSerializer(typeof(Modelo.Producto));
                        StringWriter writer = new StringWriter();
                        sr.Serialize(writer, producto);

                        if (s.AgregarProducto(writer.ToString()))
                        {
                            exito.Text = "El producto ha sido agregado con éxito con éxito";
                            alerta_exito.Visible = true;
                            alerta.Visible = false;
                        }
                        else
                        {
                            alerta_exito.Visible = false;
                            error.Text = "El ingreso de Producto ha fallado";
                            alerta.Visible = true;
                        }
                    }
                    else
                    {
                        alerta_exito.Visible = false;
                        error.Text = "Verifique el Ingreso numérico";
                        alerta.Visible = true;
                    }
                }
                else
                {
                    alerta_exito.Visible = false;
                    error.Text = "Debe rellenar todos los campos requeridos";
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
    }
}
