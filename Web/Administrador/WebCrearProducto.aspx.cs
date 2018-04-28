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

            Modelo.Producto producto = new Modelo.Producto();
            producto.NOMBRE_PRODUCTO = txtNombre.Text;
            producto.PRECIO_PRODUCTO = int.Parse(txtPrecio.Text);
            producto.ID_FAMILIA = short.Parse(ddlFamilia.SelectedValue);
            producto.STOCK_PRODUCTO = short.Parse(txtStock.Text);
            producto.STOCK_CRITICO_PRODUCTO = short.Parse(txtStockCritico.Text);
            producto.DESCRIPCION_PRODUCTO = txtDescripcion.Text;
            producto.FECHA_VENCIMIENTO_PRODUCTO = DateTime.Parse(txtFechaVencimiento.Text);
            
        }
    }
}
