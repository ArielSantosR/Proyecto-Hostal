using Modelo;
using System;
using System.Collections.Generic;
using System.Data;
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
            try
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
            catch (Exception)
            {
                Response.Write("<script language='javascript'>window.alert('Debe Iniciar Sesión Primero');window.location='../Hostal/WebLogin.aspx';</script>");
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
            btnVer.CausesValidation = false;
            btnVer.UseSubmitBehavior = false;

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

                    ddlRut.SelectedIndex = 0;

                    //segun el valor seleccionado en el anterior DDL, hara una busqueda para cargar
                    //todos los datos
                    Modelo.Proveedor proveedor2 = new Modelo.Proveedor();

                    proveedor2.RUT_PROVEEDOR = int.Parse(ddlRut.SelectedValue);

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

                    MiSesionD = null;
                    btnLimpiar.Enabled = false;
                    btnVer.Enabled = false;
                }
                CargarTabla(MiSesionD);
                
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
                                    exito.Text = "Pedido Agregado a la Lista.";
                                    alerta_exito.Visible = true;
                                    alerta.Visible = false;
                                    MiSesionD.Add(detalle);
                                    btnLimpiar.Enabled = true;
                                    btnVer.Enabled = true;
                                    txtCantidad.Text = "";
                                    txtPrecio.Text = "";
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#exampleModal2').modal();", true);
                                }

                                
                                CargarTabla(MiSesionD);
                                
                                
                            }
                            else
                            {
                                alerta_exito.Visible = false;
                                error.Text = "Debe seleccionar un producto ";
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
                error.Text = "Excepción: " + ex.Message;
                alerta.Visible = true;
            }
        }

        private void CargarTabla(List<DetallePedido> lista)
        {
            Producto producto = new Producto();
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] {
                new DataColumn("Código", typeof(long)),
                new DataColumn("Nombre", typeof(string)),
                new DataColumn("Descripción",typeof(string)),
                new DataColumn("Unidad Medida",typeof(string)),
                new DataColumn("Cantidad",typeof(int))
            });
            foreach (DetallePedido item in lista)
            {
                producto = new Producto();
                producto.ID_PRODUCTO = item.ID_PRODUCTO;
                producto.Read();

                dt.Rows.Add(producto.ID_PRODUCTO, producto.NOMBRE_PRODUCTO, producto.DESCRIPCION_PRODUCTO, producto.UNIDAD_MEDIDA, item.CANTIDAD);
            }

            gvDetalle.DataSource = dt;
            gvDetalle.DataBind();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            try
            {
                if (MiSesionD.Count > 0)
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
                        pedido.ESTADO_DESPACHO = "Pendiente";

                        XmlSerializer sr2 = new XmlSerializer(typeof(Modelo.Pedido));
                        StringWriter writer2 = new StringWriter();
                        sr2.Serialize(writer2, pedido);

                        if (s.AgregarPedido(writer2.ToString()))
                        {
                            bool v_exito = true;

                            foreach (DetallePedido d in MiSesionD)
                            {
                                Modelo.DetallePedido detalle = new Modelo.DetallePedido();
                                detalle.CANTIDAD = d.CANTIDAD;
                                detalle.ID_PRODUCTO = d.ID_PRODUCTO;

                                XmlSerializer sr3 = new XmlSerializer(typeof(Modelo.DetallePedido));
                                StringWriter writer3 = new StringWriter();
                                sr3.Serialize(writer3, detalle);

                                if (!s.AgregarDetallePedido(writer3.ToString()))
                                {
                                    v_exito = false;
                                }
                            }

                            if (v_exito)
                            {
                                if (MiSesion.TIPO_USUARIO.Equals("Empleado"))
                                {
                                    Response.Write("<script language='javascript'>window.alert('Pedido Realizado, el administrador debe confirmar su envío');window.location='../Empleado/WebHistorialPedidos.aspx';</script>");
                                }
                                else
                                {
                                    Response.Write("<script language='javascript'>window.alert('Pedido Realizado, debe confirmar su envío al Proveedor');window.location='../Administrador/WebVerPedido.aspx';</script>");
                                }
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
                            error.Text = "No se ha podido hacer el Pedido";
                            alerta.Visible = true;
                        }
                    }
                    else
                    {
                        alerta_exito.Visible = false;
                        error.Text = "no se pudo encontrar al Empleado";
                        alerta.Visible = true;
                    }
                }
                else
                {
                    alerta_exito.Visible = false;
                    error.Text = "debe ingresar objetos a la lista para Registrar una Orden";
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

        protected void ddlProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Service1 service = new Service1();
                //segun el valor seleccionado en el anterior DDL, hara una busqueda para cargar
                //todos los datos
                Modelo.Proveedor proveedor2 = new Modelo.Proveedor();
                proveedor2.RUT_PROVEEDOR = int.Parse(ddlRut.SelectedValue);

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
                txtCantidad.Text = "";
                txtPrecio.Text = "";
                UpdatePanel1.Update();
                UpdatePanel2.Update();

                btnLimpiar.Enabled = false;
                btnVer.Enabled = false;
                MiSesionD.Clear();

                CargarTabla(MiSesionD);
            }
            catch (Exception ex)
            {
                alerta_exito.Visible = false;
                error.Text = "Excepción: " + ex.ToString();
                alerta.Visible = true;
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

        protected void gvDetalle_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {

                long ID_PRODUCTO = long.Parse(e.Keys["Código"].ToString());

                foreach (DetallePedido d in MiSesionD.ToList())
                {
                    if (d.ID_PRODUCTO == ID_PRODUCTO)
                    {
                        MiSesionD.Remove(d);
                    }
                }

                if (MiSesionD.Count == 0)
                {
                    btnLimpiar.Enabled = false;
                    btnVer.Enabled = false;
                }

                CargarTabla(MiSesionD);
            }
            catch (Exception ex)
            {
                alerta_exito.Visible = false;
                error.Text = "Excepción: " + ex.ToString();
                alerta.Visible = true;
            }
        }

        protected void btnVer_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#exampleModal2').modal();", true);
        }
    }
}