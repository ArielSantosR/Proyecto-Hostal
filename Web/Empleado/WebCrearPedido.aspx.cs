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
    public partial class WebCrearPedido : System.Web.UI.Page
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
            btnLimpiar.CausesValidation = false;
            btnLimpiar.UseSubmitBehavior = false;

            //Cargando DDL Producto
            Service1 service = new Service1();
            string producto = service.ListarProducto();
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.ProductoCollection));
            StringReader reader = new StringReader(producto);
            Modelo.ProductoCollection coleccionProducto = (Modelo.ProductoCollection)ser.Deserialize(reader);
            reader.Close();

            txtPrecio.ReadOnly = true;

            if (!IsPostBack)
            {
                ddlProducto.DataSource = coleccionProducto;
                ddlProducto.DataTextField = "NombreYPrecio";
                ddlProducto.DataValueField = "ID_PRODUCTO";
                ddlProducto.DataBind();

                ddlRut.Enabled = false;
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                int cantidad = 0;
                //lalala
                if (txtCantidad.Text != string.Empty)
                {
                    if (int.TryParse(txtCantidad.Text, out cantidad))
                    {
                        if (ddlRut.SelectedValue != string.Empty)
                        {
                            Modelo.Empleado empleado = new Modelo.Empleado();
                            empleado.ID_USUARIO = MiSesion.ID_USUARIO;

                            //Si el ID de empleado es encontrado, pasar al siguiente paso
                            Service1 s = new Service1();
                            XmlSerializer sr = new XmlSerializer(typeof(Modelo.Empleado));
                            StringWriter writer = new StringWriter();
                            sr.Serialize(writer, empleado);
                            writer.Close();

                            Modelo.Empleado empleado2 = s.buscarIDE(writer.ToString());

                            if (empleado2 != null)
                            {

                                Modelo.Pedido pedido = new Modelo.Pedido();
                                pedido.FECHA_PEDIDO = DateTime.Now;
                                pedido.ESTADO_PEDIDO = "Pendiente";
                                pedido.RUT_EMPLEADO = empleado2.RUT_EMPLEADO;
                                pedido.RUT_PROVEEDOR = int.Parse(ddlRut.SelectedValue);

                                Modelo.DetallePedido detalle = new Modelo.DetallePedido();
                                detalle.CANTIDAD = cantidad;
                                detalle.ID_PRODUCTO = long.Parse(ddlProducto.SelectedValue);

                                XmlSerializer sr2 = new XmlSerializer(typeof(Modelo.Pedido));
                                StringWriter writer2 = new StringWriter();
                                sr2.Serialize(writer2, pedido);

                                XmlSerializer sr3 = new XmlSerializer(typeof(Modelo.DetallePedido));
                                StringWriter writer3 = new StringWriter();
                                sr3.Serialize(writer3, detalle);

                                if (s.AgregarPedido(writer2.ToString()) && s.AgregarDetallePedido(writer3.ToString()))
                                {
                                    exito.Text = "El Pedido ha sido completado con éxito";
                                    alerta_exito.Visible = true;
                                    alerta.Visible = false;
                                }
                                else
                                {
                                    alerta_exito.Visible = false;
                                    error.Text = "No se ha podido hacer el Pedido";
                                    alerta.Visible = true;
                                }
                            }
                            else
                            {
                                alerta_exito.Visible = false;
                                error.Text = "Error, no se pudo encontrar al Empleado";
                                alerta.Visible = true;
                            }
                        }
                        else
                        {
                            alerta_exito.Visible = false;
                            error.Text = "Error, debe seleccionar un proveedor ";
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
            txtCantidad.Text = string.Empty;
            ddlProducto.SelectedIndex = 0;
            ddlRut.Items.Clear();
            ddlRut.Enabled = false;
            txtPrecio.Text = "";
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

        protected void ddlProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Primero activa el DDL desactivado
            ddlRut.Enabled = true;

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

            XmlSerializer sr2 = new XmlSerializer(typeof(Modelo.Producto));
            StringWriter writer2 = new StringWriter();
            sr.Serialize(writer2, producto2);

            //Ya con los datos, al fin puede hacer una busqueda en Cascada en el DDL

            string proveedor = service.ListarProveedorProducto(writer2.ToString());
            XmlSerializer ser2 = new XmlSerializer(typeof(Modelo.ProveedorCollection2));
            StringReader reader2 = new StringReader(proveedor);
            Modelo.ProveedorCollection2 coleccionProveedor = (Modelo.ProveedorCollection2)ser2.Deserialize(reader2);
            reader2.Close();

            Modelo.ProveedorCollection2 coleccionProveedor2 = coleccionProveedor;
            ddlRut.DataSource = coleccionProveedor;
            ddlRut.DataTextField = "RUT_PROVEEDOR";
            ddlRut.DataValueField = "RUT_PROVEEDOR";
            ddlRut.DataBind();

            //Para funcionar requiere que el update panel tenga el Modo Condicional
            UpdatePanel2.Update();
        }

        protected void txtCantidad_TextChanged(object sender, EventArgs e)
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
    }
}