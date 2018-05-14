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

namespace Web.Empleado
{
    public partial class WebCrearDetalle : System.Web.UI.Page
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
                    else if (MiSesion.TIPO_USUARIO.Equals("Empleado") &&
                    MiSesion.ESTADO.Equals("Habilitado"))
                    {
                        MasterPageFile = "~/Empleado/EmpleadoM.Master";
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

            try
            {
                //Cargando DDL Rut
                Service1 service = new Service1();

                string proveedor = service.ListarProveedor();
                XmlSerializer ser2 = new XmlSerializer(typeof(Modelo.ProveedorCollection2));
                StringReader reader2 = new StringReader(proveedor);
                Modelo.ProveedorCollection2 coleccionProveedor = (Modelo.ProveedorCollection2)ser2.Deserialize(reader2);
                reader2.Close();

                txtPrecio.ReadOnly = true;

                if (!IsPostBack)
                {
                    ddlRut.DataSource = coleccionProveedor;
                    ddlRut.DataTextField = "RutYNombre";
                    ddlRut.DataValueField = "RUT_PROVEEDOR";
                    ddlRut.DataBind();

                    //segun el valor seleccionado en el anterior DDL, hara una busqueda para cargar
                    //todos los datos
                    Modelo.Proveedor proveedor2 = new Modelo.Proveedor();

                    proveedor2.RUT_PROVEEDOR = MiSesionPedido.RUT_PROVEEDOR;

                    XmlSerializer sr = new XmlSerializer(typeof(Modelo.Proveedor));
                    StringWriter writer = new StringWriter();
                    sr.Serialize(writer, proveedor2);

                    //Ya con los datos, al fin puede hacer una busqueda en Cascada en el DDL

                    string productos = service.ListarProductosProveedor(writer.ToString());
                    XmlSerializer ser = new XmlSerializer(typeof(Modelo.ProductoCollection));
                    StringReader reader = new StringReader(productos);
                    Modelo.ProductoCollection coleccionProducto = (Modelo.ProductoCollection)ser.Deserialize(reader);
                    reader.Close();

                    ddlProducto.DataSource = coleccionProducto;
                    ddlProducto.DataTextField = "NombreYPrecio";
                    ddlProducto.DataValueField = "ID_PRODUCTO";
                    ddlProducto.DataBind();

                    //Para funcionar requiere que el update panel tenga el Modo Condicional
                    UpdatePanel2.Update();

                    Pedido pedido = new Pedido();
                    pedido = MiSesionPedido;

                    XmlSerializer sr3 = new XmlSerializer(typeof(Modelo.Pedido));
                    StringWriter writer3 = new StringWriter();
                    sr3.Serialize(writer3, pedido);

                    if (service.ListarDetallePedido(writer3.ToString()) != null)
                    {
                        string datos = service.ListarDetallePedido(writer3.ToString());
                        XmlSerializer ser4 = new XmlSerializer(typeof(Modelo.DetallePedidoCollection));
                        StringReader reader4 = new StringReader(datos);

                        Modelo.DetallePedidoCollection listaDetalle = (Modelo.DetallePedidoCollection)ser4.Deserialize(reader4);
                        reader.Close();
                        gvDetalle.DataSource = listaDetalle;
                        gvDetalle.DataBind();

                        MiSesionD = listaDetalle;
                    }
                }
                ddlRut.SelectedValue = MiSesionPedido.RUT_PROVEEDOR + "";
                ddlRut.Attributes.Add("disabled", "disabled");

                
            }
            catch (Exception ex)
            {
                alerta_exito.Visible = false;
                error.Text = "Excepción: " + ex.ToString();
                alerta.Visible = true;
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

        public Pedido MiSesionPedido
        {
            get
            {
                if (Session["Pedido"] == null)
                {
                    Session["Pedido"] = new Pedido();
                }
                return (Pedido)Session["Pedido"];
            }
            set
            {
                Session["Pedido"] = value;
            }
        }

        public List<DetallePedido> MiSesionD
        {
            get
            {
                if (Session["ListaDetalle"] == null)
                {
                    Session["ListaDetalle"] = new List<DetallePedido>();
                }
                return (List<DetallePedido>)Session["ListaDetalle"];
            }
            set
            {
                Session["ListaDetalle"] = value;
            }
        }

        protected void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int cantidad = 0;

                if (int.TryParse(txtCantidad.Text, out cantidad))
                {
                    Service1 service = new Service1();
                    //segun el valor seleccionado en el anterior DDL, hara una busqueda para cargar
                    //todos los datos
                    Modelo.Producto producto = new Modelo.Producto();
                    producto.ID_PRODUCTO = long.Parse(ddlProducto.SelectedValue);

                    XmlSerializer sr = new XmlSerializer(typeof(Modelo.Producto));
                    StringWriter writer = new StringWriter();
                    sr.Serialize(writer, producto);

                    //Una vez encuentra sus datos los carga en una segunda variable
                    Producto producto2 = service.ObtenerProducto(writer.ToString());

                    txtPrecio.Text = "$" + producto2.PRECIO_PRODUCTO * cantidad;
                }
                else
                {
                    txtCantidad.Text = "";
                    txtPrecio.Text = "";
                }

                UpdatePanel1.Update();
            }
            catch (Exception ex)
            {
                alerta_exito.Visible = false;
                error.Text = "Excepción: " + ex.ToString();
                alerta.Visible = true;
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                int cantidad = 0;

                if (txtCantidad.Text != string.Empty)
                {
                    if (int.TryParse(txtCantidad.Text, out cantidad))
                    {
                        if (cantidad > 0)
                        {
                            if (ddlProducto.SelectedValue != string.Empty)
                            {
                                Modelo.DetallePedido detalle = new Modelo.DetallePedido();
                                detalle.CANTIDAD = cantidad;
                                detalle.ID_PRODUCTO = long.Parse(ddlProducto.SelectedValue);
                                detalle.NUMERO_PEDIDO = MiSesionPedido.NUMERO_PEDIDO;

                                bool existe = false;

                                foreach (DetallePedido d in MiSesionD)
                                {
                                    if (detalle.ID_PRODUCTO == d.ID_PRODUCTO)
                                    {
                                        existe = true;
                                    }
                                }
                                if (existe)
                                {
                                    alerta_exito.Visible = false;
                                    error.Text = "Este Producto ya ha sido agregado";
                                    alerta.Visible = true;
                                }
                                else
                                {
                                    Service1 s = new Service1();
                                    XmlSerializer sr3 = new XmlSerializer(typeof(Modelo.DetallePedido));
                                    StringWriter writer3 = new StringWriter();
                                    sr3.Serialize(writer3, detalle);

                                    if (s.AgregarDetallePedido(writer3.ToString()))
                                    {
                                        Response.Write("<script language='javascript'>window.alert('Ha agregado un producto a Pedido');window.location='../Empleado/WebHistorialPedidos.aspx';</script>");
                                    }
                                    else
                                    {
                                        alerta_exito.Visible = false;
                                        error.Text = "Este Producto ya ha sido agregado";
                                        alerta.Visible = true;
                                    }
                                }
                            }
                            else
                            {
                                alerta_exito.Visible = false;
                                error.Text = "Error, debe seleccionar un producto ";
                                alerta.Visible = true;
                            }
                        }
                        else
                        {
                            alerta_exito.Visible = false;
                            error.Text = "Debe Ingresar una cantidad superior a 0";
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
            catch (Exception ex)
            {
                alerta_exito.Visible = false;
                error.Text = "Excepción: " + ex.ToString();
                alerta.Visible = true;
            }
        }
    }
}