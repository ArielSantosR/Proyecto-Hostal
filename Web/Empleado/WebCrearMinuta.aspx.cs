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
    public partial class WebCrearMinuta : System.Web.UI.Page
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
        protected void Page_Load(object sender, EventArgs e)
        {
            error.Text = "";
            exito.Text = "";
            alerta_exito.Visible = false;
            alerta.Visible = false;

            try
            {
                //Cargando DDL Plato
                Service1 service = new Service1();

                string plato = service.ListarPlato();
                XmlSerializer ser2 = new XmlSerializer(typeof(Modelo.PlatoCollection));
                StringReader reader2 = new StringReader(plato);
                Modelo.PlatoCollection coleccionPlato = (Modelo.PlatoCollection)ser2.Deserialize(reader2);
                reader2.Close();

                txtPrecio.ReadOnly = true;

                if (!IsPostBack)
                {
                    ddlDesayuno.DataSource = coleccionPlato;
                    ddlDesayuno.DataTextField = "NombreYPrecio";
                    ddlDesayuno.DataValueField = "ID_PLATO";
                    ddlDesayuno.DataBind();

                    ddlDesayuno.SelectedIndex = 0;

                    ddlAlmuerzo.DataSource = coleccionPlato;
                    ddlAlmuerzo.DataTextField = "NombreYPrecio";
                    ddlAlmuerzo.DataValueField = "ID_PLATO";
                    ddlAlmuerzo.DataBind();

                    ddlAlmuerzo.SelectedIndex = 0;

                    ddlCena.DataSource = coleccionPlato;
                    ddlCena.DataTextField = "NombreYPrecio";
                    ddlCena.DataValueField = "ID_PLATO";
                    ddlCena.DataBind();

                    ddlCena.SelectedIndex = 0;
                    //segun el valor seleccionado en el anterior DDL, hara una busqueda para cargar
                    //todos los datos
                    Modelo.Plato plato2 = new Modelo.Plato();

                    plato2.ID_PLATO = short.Parse(ddlDesayuno.SelectedValue);

                    XmlSerializer sr = new XmlSerializer(typeof(Modelo.Plato));
                    StringWriter writer = new StringWriter();
                    sr.Serialize(writer, plato2);

                    //Ya con los datos, al fin puede hacer una busqueda en Cascada en el DDL
                    
                   // string platos = service.ListarPlato(writer.ToString());
                    XmlSerializer ser = new XmlSerializer(typeof(Modelo.Plato));
                    //StringReader reader = new StringReader(productos);
                    //Modelo.ProductoCollection coleccionProducto = (Modelo.ProductoCollection)ser.Deserialize(reader);
                    //reader.Close();

                    /*ddlProducto.DataSource = coleccionProducto;
                    ddlProducto.DataTextField = "NombreYPrecio";
                    ddlProducto.DataValueField = "ID_PRODUCTO";
                    ddlProducto.DataBind();*/

                    //Para funcionar requiere que el update panel tenga el Modo Condicional
                    UpdatePanel2.Update();

                    MiSesionD = null;
                }
                gvDetalle.DataSource = MiSesionD;
                gvDetalle.DataBind();

                UpdatePanel3.Update();
            }
            catch (Exception ex)
            {
                alerta_exito.Visible = false;
                error.Text = "Excepción: " + ex.ToString();
                alerta.Visible = true;
            }

        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnCrear_Click(object sender, EventArgs e)
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

                        Modelo.Minuta minuta = new Modelo.Minuta();
                        minuta.NOMBRE_PENSION = txtNombreMinuta.Text;
                        minuta.NUMERO_HABITACION = 0;
                        minuta.VALOR_PENSION = int.Parse(txtPrecio.Text);
                        
                        

                        XmlSerializer sr2 = new XmlSerializer(typeof(Modelo.Minuta));
                        StringWriter writer2 = new StringWriter();
                        sr2.Serialize(writer2, minuta);

                        if (s.AgregarPlato(writer2.ToString()))
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
                        error.Text = "Error, no se pudo encontrar al Empleado";
                        alerta.Visible = true;
                    }
                }
                else
                {
                    alerta_exito.Visible = false;
                    error.Text = "Error, debe ingresar objetos a la lista para Registrar una Orden";
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

        protected void ddlProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
           /* try
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

                MiSesionD.Clear();

                gvDetalle.DataSource = MiSesionD;
                gvDetalle.DataBind();

                UpdatePanel3.Update();
            }
            catch (Exception ex)
            {
                alerta_exito.Visible = false;
                error.Text = "Excepción: " + ex.ToString();
                alerta.Visible = true;
            }
            */
        }

        protected void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            /*try
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
            */
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
        //////
        /*
          protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                //Lee los valores del LinkButton, primero usa la clase LinkButton para 
                //Transformar los datos de Sender, luego los lee y los asigna a una variable
                LinkButton btn = (LinkButton)(sender);
                long ID_PRODUCTO = long.Parse(btn.CommandArgument);

                foreach (DetallePedido d in MiSesionD.ToList())
                {
                    if (d.ID_PRODUCTO == ID_PRODUCTO)
                    {
                        MiSesionD.Remove(d);
                    }
                }

                gvDetalle.DataSource = MiSesionD;
                gvDetalle.DataBind();

                UpdatePanel3.Update();
            }
            catch (Exception ex)
            {
                alerta_exito.Visible = false;
                error.Text = "Excepción: " + ex.ToString();
                alerta.Visible = true;
            }
        }
         */






    }
}
 