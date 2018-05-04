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
//aaa
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
                    DateTime fecha = Convert.ToDateTime("01/01/2000");

                    Service1 s = new Service1();
                    XmlSerializer sr = new XmlSerializer(typeof(Modelo.Producto));
                    StringWriter writer = new StringWriter();
                    sr.Serialize(writer, producto);

                    if (s.ObtenerProducto(writer.ToString()) != null)
                    {
                        producto = s.ObtenerProducto(writer.ToString());

                        txtNombre.Text = producto.NOMBRE_PRODUCTO + "";
                        txtDescripcion.Text = producto.DESCRIPCION_PRODUCTO + "";
                        if (producto.FECHA_VENCIMIENTO_PRODUCTO != null)
                        {
                            fecha = (DateTime) producto.FECHA_VENCIMIENTO_PRODUCTO;
                        }

                        calendarFecha.SelectedDate = fecha;
                        txtPrecio.Text = producto.PRECIO_PRODUCTO + "";
                        txtStock.Text = producto.STOCK_PRODUCTO + "";
                        txtStockCritico.Text = producto.STOCK_CRITICO_PRODUCTO + "";
                        
                        ddlFamilia.SelectedIndex = producto.ID_FAMILIA - 1;
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
            short stock = 0;
            short stockCritico = 0;
            int precio = 0;

            try
            {
                if (txtNombre.Text != string.Empty && txtPrecio.Text != string.Empty &&
                txtStock.Text != string.Empty && txtStockCritico.Text != string.Empty &&
                txtDescripcion.Text != string.Empty)
                {
                    Modelo.Producto producto = new Modelo.Producto();
                    producto.ID_PRODUCTO = MiSesionP.ID_PRODUCTO;
                    producto.NOMBRE_PRODUCTO = txtNombre.Text;
                    producto.ID_FAMILIA = short.Parse(ddlFamilia.SelectedValue);
                    producto.DESCRIPCION_PRODUCTO = txtDescripcion.Text;
                    producto.FECHA_VENCIMIENTO_PRODUCTO = calendarFecha.SelectedDate;

                    if (short.TryParse(txtStock.Text, out stock) &&
                        short.TryParse(txtStockCritico.Text, out stockCritico) &&
                        int.TryParse(txtPrecio.Text, out precio))
                    {
                        producto.STOCK_PRODUCTO = stock;
                        producto.STOCK_CRITICO_PRODUCTO = stockCritico;
                        producto.PRECIO_PRODUCTO = precio;

                        Service1 s = new Service1();
                        XmlSerializer sr = new XmlSerializer(typeof(Modelo.Producto));
                        StringWriter writer = new StringWriter();
                        sr.Serialize(writer, producto);

                        if (s.ModificarProducto(writer.ToString()))
                        {
                            exito.Text = "El producto ha sido modificado con éxito";
                            alerta_exito.Visible = true;
                            alerta.Visible = false;
                        }
                        else
                        {
                            alerta_exito.Visible = false;
                            error.Text = "La modificación de Producto ha fallado";
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

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtDescripcion.Text = string.Empty;
            calendarFecha.SelectedDate = Convert.ToDateTime("01/01/2000");
            txtNombre.Text = string.Empty;
            txtPrecio.Text = string.Empty;
            txtStock.Text = string.Empty;
            txtStockCritico.Text = string.Empty;
            ddlFamilia.SelectedIndex = 0;
        }
    }
}