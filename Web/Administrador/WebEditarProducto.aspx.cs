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
    public partial class WebEditarProducto : System.Web.UI.Page
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

        public Producto MiSesionP
        {
            get
            {
                if (Session["Producto"] == null)
                {
                    Session["Producto"] = new Producto();
                }
                return (Producto)Session["Producto"];
            }
            set
            {
                Session["Producto"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            error.Text = "";
            exito.Text = "";
            alerta_exito.Visible = false;
            alerta.Visible = false;

            //Cargando DDL Familia
            Service1 service = new Service1();
            string familia = service.ListarFamilia();
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.FamiliaCollection));
            StringReader reader = new StringReader(familia);
            Modelo.FamiliaCollection coleccionFamilia = (Modelo.FamiliaCollection)ser.Deserialize(reader);
            reader.Close();

            if (!IsPostBack)
            {
                ddlFamilia.DataSource = coleccionFamilia;
                ddlFamilia.DataTextField = "NOMBRE_FAMILIA";
                ddlFamilia.DataValueField = "ID_FAMILIA";
                ddlFamilia.DataBind();


                if (MiSesionP.ID_PRODUCTO != 0)
                {
                    Modelo.Producto producto = new Modelo.Producto();
                    producto.ID_PRODUCTO = MiSesionP.ID_PRODUCTO;

                    Service1 s = new Service1();
                    XmlSerializer sr = new XmlSerializer(typeof(Modelo.Producto));
                    StringWriter writer = new StringWriter();
                    sr.Serialize(writer, producto);

                    if (s.ObtenerProducto(writer.ToString()) != null)
                    {
                        producto = s.ObtenerProducto(writer.ToString());

                        txtNombre.Text = producto.NOMBRE_PRODUCTO + "";
                        txtDescripcion.Text = producto.DESCRIPCION_PRODUCTO + "";
                        
                        txtFechaVencimiento.Text = producto.FECHA_VENCIMIENTO_PRODUCTO + "";
                        txtPrecio.Text = producto.PRECIO_PRODUCTO + "";
                        txtStock.Text = producto.STOCK_PRODUCTO + "";
                        txtStockCritico.Text = producto.STOCK_CRITICO_PRODUCTO + "";
                        
                        ddlFamilia.SelectedIndex = producto.ID_FAMILIA;

                       
                    }
                }
                else
                {
                    Response.Write("<script language='javascript'>window.alert('No puede acceder a esta página');window.location='../Hostal/WebLogin.aspx';</script>");
                }
            }
        }

        //falta terminar
        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                int precio = 0;

                if (txtNombre.Text != string.Empty && txtPrecio.Text != string.Empty)
                {
                    if (int.TryParse(txtPrecio.Text, out precio))
                    {
                        Modelo.Producto producto = new Modelo.Producto();
                        producto.NOMBRE_PRODUCTO = txtNombre.Text;
                        producto.PRECIO_PRODUCTO = precio;
                        producto.ID_FAMILIA = short.Parse(ddlFamilia.SelectedValue);
                        producto.FECHA_VENCIMIENTO_PRODUCTO = DateTime.Parse(txtFechaVencimiento.Text);
                        producto.STOCK_CRITICO_PRODUCTO = short.Parse(txtStockCritico.Text);
                        producto.STOCK_PRODUCTO = short.Parse(txtStock.Text);
                        producto.DESCRIPCION_PRODUCTO = txtDescripcion.Text;


                        Service1 s = new Service1();
                        XmlSerializer sr = new XmlSerializer(typeof(Modelo.Producto));
                        StringWriter writer = new StringWriter();
                        sr.Serialize(writer, producto);

                        if (s.ModificarProducto(writer.ToString()))
                        {
                            exito.Text = "El Producto ha sido modificado con éxito";
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
                        error.Text = "Datos Ingresados incorrectamente, verifique que ha ingresado datos correctamente";
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
            txtDescripcion.Text = string.Empty;
            txtFechaVencimiento.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtPrecio.Text = string.Empty;
            txtStock.Text = string.Empty;
            txtStockCritico.Text = string.Empty;
            ddlFamilia.SelectedIndex = 0;
        }
    }
}