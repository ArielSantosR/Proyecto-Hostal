﻿using System;
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
            btnLimpiar.CausesValidation = false;
            btnLimpiar.UseSubmitBehavior = false;

            Service1 service = new Service1();
            string familia = service.ListarFamilia();
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.FamiliaCollection));
            StringReader reader = new StringReader(familia);
            Modelo.FamiliaCollection coleccionFamilia = (Modelo.FamiliaCollection)ser.Deserialize(reader);
            reader.Close();

            string proveedor = service.ListarProveedor();
            XmlSerializer ser2 = new XmlSerializer(typeof(Modelo.ProveedorCollection2));
            StringReader reader2 = new StringReader(proveedor);
            Modelo.ProveedorCollection2 coleccionProveedor = (Modelo.ProveedorCollection2)ser2.Deserialize(reader2);
            reader.Close();

            if (!IsPostBack)
            {

                ddlFamilia.DataSource = coleccionFamilia;
                ddlFamilia.DataValueField = "ID_FAMILIA";
                ddlFamilia.DataTextField = "NOMBRE_FAMILIA";
                ddlFamilia.DataBind();

                ddlRut.DataSource = coleccionProveedor;
                ddlRut.DataValueField = "RUT_PROVEEDOR";
                ddlRut.DataTextField = "RutYNombre";
                ddlRut.DataBind();

                ddlUnidad.Items.Add("Unidad");
                ddlUnidad.Items.Add("Gramos");
                ddlUnidad.Items.Add("100 Gramos");
                ddlUnidad.Items.Add("200 Gramos");
                ddlUnidad.Items.Add("500 Gramos");
                ddlUnidad.Items.Add("Kilos");
                ddlUnidad.Items.Add("1 Kilogramo");
                ddlUnidad.Items.Add("2 Kilogramos");
                ddlUnidad.Items.Add("5 Kilogramos");
                ddlUnidad.Items.Add("Milílitros");
                ddlUnidad.Items.Add("350 Milílitros");
                ddlUnidad.Items.Add("500 Milílitros");
                ddlUnidad.Items.Add("750 Milílitros");
                ddlUnidad.Items.Add("Litros");
                ddlUnidad.Items.Add("1 Litro");
                ddlUnidad.Items.Add("1.5 Litros");
                ddlUnidad.Items.Add("2 Litros");
                ddlUnidad.Items.Add("2.5 Litros");
                ddlUnidad.Items.Add("3 Litros");
                ddlUnidad.Items.Add("Galón");
                ddlUnidad.Items.Add("Saco");

                calendarFecha.SelectedDate = Convert.ToDateTime("01/01/2000");
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
            calendarFecha.SelectedDate = Convert.ToDateTime("01/01/2000"); 
            txtNombre.Text = string.Empty;
            txtPrecio.Text = string.Empty;
            txtStock.Text = string.Empty;
            txtStockCritico.Text = string.Empty;
            ddlUnidad.SelectedIndex = 0;
            ddlRut.SelectedIndex = 0;

            ddlFamilia.SelectedIndex = 0;

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
                    if (calendarFecha.SelectedDate == Convert.ToDateTime("01/01/2000") || calendarFecha.SelectedDate > DateTime.Now)
                    {
                        Modelo.Producto producto = new Modelo.Producto();
                        producto.NOMBRE_PRODUCTO = txtNombre.Text;
                        producto.ID_FAMILIA = short.Parse(ddlFamilia.SelectedValue);
                        producto.RUT_PROVEEDOR = int.Parse(ddlRut.SelectedValue);
                        producto.DESCRIPCION_PRODUCTO = txtDescripcion.Text;
                        producto.FECHA_VENCIMIENTO_PRODUCTO = calendarFecha.SelectedDate;
                        producto.UNIDAD_MEDIDA = ddlUnidad.SelectedValue;

                        if (short.TryParse(txtStock.Text, out stock) &&
                            short.TryParse(txtStockCritico.Text, out stockCritico) &&
                            int.TryParse(txtPrecio.Text, out precio))
                        {
                            if (stock > 0 && stockCritico > 0 && precio > 0)
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
                                    exito.Text = "El producto ha sido agregado con éxito";
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
                                error.Text = "Los números deben ser superiores a 0";
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
                        error.Text = "La fecha de Vencimiento no puede ser menor al día actual";
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
