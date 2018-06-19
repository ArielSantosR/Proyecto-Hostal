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

        public List<DetallePlato> MiSesionM
        {
            get
            {
                if (Session["DetallePlato"] == null)
                {
                    Session["DetallePlato"] = new List<DetallePlato>();
                }
                return (List<DetallePlato>)Session["DetallePlato"];
            }
            set
            {
                Session["DetallePlato"] = value;
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
                //Cargando Tipo de Plato
                Service1 service = new Service1();

                string categoria = service.ListarCategoria();
                XmlSerializer ser2 = new XmlSerializer(typeof(Modelo.CategoriaCollection));
                StringReader reader2 = new StringReader(categoria);
                Modelo.CategoriaCollection coleccionProveedor = (Modelo.CategoriaCollection)ser2.Deserialize(reader2);
                reader2.Close();

                if (!IsPostBack)
                {
                    ddlCategoria.DataSource = coleccionProveedor;
                    ddlCategoria.DataTextField = "NOMBRE_CATEGORIA";
                    ddlCategoria.DataValueField = "ID_CATEGORIA";
                    ddlCategoria.DataBind();

                    ddlCategoria.SelectedIndex = 0;

                    //segun el valor seleccionado en el anterior DDL, hara una busqueda para cargar
                    //todos los datos
                    Modelo.Categoria categoria2 = new Modelo.Categoria();

                    categoria2.ID_CATEGORIA = short.Parse(ddlCategoria.SelectedValue);

                    XmlSerializer sr = new XmlSerializer(typeof(Modelo.Categoria));
                    StringWriter writer = new StringWriter();
                    sr.Serialize(writer, categoria2);

                    string platos = service.ListarPlatoPorCategoria(writer.ToString());
                    XmlSerializer ser = new XmlSerializer(typeof(Modelo.PlatoCollection));
                    StringReader reader = new StringReader(platos);
                    Modelo.PlatoCollection coleccionProducto = (Modelo.PlatoCollection)ser.Deserialize(reader);
                    reader.Close();

                    ddlPlato.DataSource = coleccionProducto;
                    ddlPlato.DataTextField = "NombreYPrecio";
                    ddlPlato.DataValueField = "ID_PLATO";
                    ddlPlato.DataBind();

                    //Para funcionar requiere que el update panel tenga el Modo Condicional

                    MiSesionM = null;
                    btnLimpiar.Enabled = false;
                    btnVer.Enabled = false;
                }
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
                short cantidad = 1;
                if (ddlPlato.SelectedValue != string.Empty)
                {
                    Modelo.DetallePlato detalle = new Modelo.DetallePlato();
                    detalle.CANTIDAD = cantidad;
                    detalle.ID_PLATO = short.Parse(ddlPlato.SelectedValue);
                    Service1 s = new Service1();
                    XmlSerializer sr = new XmlSerializer(typeof(Modelo.Plato));
                    bool existe = false;
                    bool repetido = false;

                    foreach (DetallePlato d in MiSesionM)
                    {
                        if (detalle.ID_PLATO == d.ID_PLATO)
                        {
                            existe = true;
                        }
                        Modelo.Plato p = new Modelo.Plato();
                        p.ID_PLATO = detalle.ID_PLATO;
                        StringWriter writer = new StringWriter();
                        sr.Serialize(writer, p);
                        Modelo.Plato plato2 = s.ObtenerPlato(writer.ToString());
                        Modelo.Plato p2 = new Modelo.Plato();
                        p2.ID_PLATO = d.ID_PLATO;
                        StringWriter writer2 = new StringWriter();
                        sr.Serialize(writer2, p2);
                        Modelo.Plato plato3 = s.ObtenerPlato(writer2.ToString());
                        if (plato2.ID_TIPO_PLATO == plato3.ID_TIPO_PLATO)
                        {
                            repetido = true;
                        }
                    }
                    if (existe)
                    {
                        alerta_exito.Visible = false;
                        error.Text = "Este Plato ya ha sido agregado";
                        alerta.Visible = true;
                    }
                    else if (repetido)
                    {
                        alerta_exito.Visible = false;
                        error.Text = "Solo puede tener una sola categoría de plato a la vez";
                        alerta.Visible = true;
                    }
                    else
                    {
                        exito.Text = "Plato Agregado a la Lista.";
                        alerta_exito.Visible = true;
                        alerta.Visible = false;
                        MiSesionM.Add(detalle);
                        btnLimpiar.Enabled = true;
                        btnVer.Enabled = true;
                        gvDetalle.DataSource = MiSesionM;
                        gvDetalle.DataBind();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#exampleModal2').modal();", true);
                    }
                }
                else
                {
                    alerta_exito.Visible = false;
                    error.Text = "Debe seleccionar un Plato ";
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

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            try
            {
                if (MiSesionM.Count > 0)
                {
                    Modelo.Minuta pension = new Modelo.Minuta();
                    pension.NOMBRE_PENSION = string.Empty;
                    pension.VALOR_PENSION = 0;
                    pension.HABILITADO = "T";

                    Service1 s = new Service1();
                    XmlSerializer sr = new XmlSerializer(typeof(Modelo.Plato));

                    foreach (DetallePlato d in MiSesionM)
                    {
                        Modelo.Plato p = new Modelo.Plato();
                        p.ID_PLATO = d.ID_PLATO;
                        StringWriter writer = new StringWriter();
                        sr.Serialize(writer, p);
                        Modelo.Plato plato = s.ObtenerPlato(writer.ToString());
                        pension.VALOR_PENSION = pension.VALOR_PENSION + (plato.PRECIO_PLATO * d.CANTIDAD);

                        if (pension.NOMBRE_PENSION.Equals(string.Empty))
                        {
                            switch (plato.ID_TIPO_PLATO)
                            {
                                case 1:
                                    pension.NOMBRE_PENSION = "Desayuno";
                                    break;

                                case 2:
                                    pension.NOMBRE_PENSION = "Almuerzo";
                                    break;

                                case 3:
                                    pension.NOMBRE_PENSION = "Cena";
                                    break;
                            }                            
                        }
                        else
                        {
                            switch (plato.ID_TIPO_PLATO)
                            {
                                case 1:
                                    pension.NOMBRE_PENSION = pension.NOMBRE_PENSION + "-Desayuno";
                                    break;

                                case 2:
                                    pension.NOMBRE_PENSION = pension.NOMBRE_PENSION + "-Almuerzo";
                                    break;

                                case 3:
                                    pension.NOMBRE_PENSION = pension.NOMBRE_PENSION + "-Cena";
                                    break;
                            }
                        }
                    }

                    Modelo.Plato p2 = new Modelo.Plato();
                    p2.ID_PLATO = MiSesionM[0].ID_PLATO;
                    StringWriter writer2 = new StringWriter();
                    sr.Serialize(writer2, p2);
                    Modelo.Plato plato2 = s.ObtenerPlato(writer2.ToString());

                    switch (plato2.ID_CATEGORIA)
                    {
                        case 1:
                            pension.NOMBRE_PENSION = pension.NOMBRE_PENSION + " Bronce";
                            break;

                        case 2:
                            pension.NOMBRE_PENSION = pension.NOMBRE_PENSION + " Plata";
                            break;

                        case 3:
                            pension.NOMBRE_PENSION = pension.NOMBRE_PENSION + " Oro";
                            break;
                    }
                    if (pension.VALOR_PENSION > 0)
                    {
                        XmlSerializer sr2 = new XmlSerializer(typeof(Modelo.Pension));
                        StringWriter writer3 = new StringWriter();
                        sr2.Serialize(writer3, pension);
                        writer2.Close();

                        if (s.AgregarMinuta(writer3.ToString()))
                        {

                            bool v_exito = true;

                            foreach (DetallePlato d in MiSesionM)

                            {
                                Modelo.DetallePlato detalle = new Modelo.DetallePlato();
                                detalle.CANTIDAD = d.CANTIDAD;
                                detalle.ID_PLATO = d.ID_PLATO;
                                XmlSerializer sr3 = new XmlSerializer(typeof(Modelo.DetallePlato));
                                StringWriter writer4 = new StringWriter();
                                sr3.Serialize(writer4, detalle);

                                if (!s.AgregarDetalleMinuta(writer4.ToString()))
                                {
                                    v_exito = false;
                                }
                            }
                            if (v_exito)
                            {
                                Response.Write("<script language='javascript'>window.alert('Minuta Creada');window.location='../Empleado/WebVerMinuta.aspx';</script>");
                            }
                            else
                            {
                                alerta_exito.Visible = false;
                                error.Text = "No se ha podido crear la minuta";
                                alerta.Visible = true;
                            }
                        }
                        else
                        {
                            alerta_exito.Visible = false;
                            error.Text = "No se ha podido añadir el detalle de Minuta";
                            alerta.Visible = true;
                        }
                    }
                    else
                    {
                        alerta_exito.Visible = false;
                        error.Text = "Precio no válido";
                        alerta.Visible = true;
                    }
                }
                else
                {
                    alerta_exito.Visible = false;
                    error.Text = "debe ingresar platos para poder hacer una Minuta";
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
        
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                //Lee los valores del LinkButton, primero usa la clase LinkButton para 
                //Transformar los datos de Sender, luego los lee y los asigna a una variable
                LinkButton btn = (LinkButton)(sender);
                short ID_PLATO = short.Parse(btn.CommandArgument);

                foreach (DetallePlato d in MiSesionM.ToList())
                {
                    if (d.ID_PLATO == ID_PLATO)
                    {
                        MiSesionM.Remove(d);
                    }
                }

                if (MiSesionM.Count == 0)
                {
                    btnVer.Enabled = false;
                    btnLimpiar.Enabled = false;
                }

                gvDetalle.DataSource = MiSesionM;
                gvDetalle.DataBind();
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

        protected void ddlCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Service1 service = new Service1();
                //segun el valor seleccionado en el anterior DDL, hara una busqueda para cargar
                //todos los datos
                Modelo.Categoria categoria2 = new Modelo.Categoria();

                categoria2.ID_CATEGORIA = short.Parse(ddlCategoria.SelectedValue);

                XmlSerializer sr = new XmlSerializer(typeof(Modelo.Categoria));
                StringWriter writer = new StringWriter();
                sr.Serialize(writer, categoria2);

                string platos = service.ListarPlatoPorCategoria(writer.ToString());
                XmlSerializer ser = new XmlSerializer(typeof(Modelo.PlatoCollection));
                StringReader reader = new StringReader(platos);
                Modelo.PlatoCollection coleccionPlato = (Modelo.PlatoCollection)ser.Deserialize(reader);
                reader.Close();

                ddlPlato.DataSource = coleccionPlato;
                ddlPlato.DataTextField = "NombreYPrecio";
                ddlPlato.DataValueField = "ID_PLATO";
                ddlPlato.DataBind();

                btnLimpiar.Enabled = false;
                btnVer.Enabled = false;

                //Para funcionar requiere que el update panel tenga el Modo Condicional

                if (MiSesionM.Count > 0)
                {
                    alerta_exito.Visible = false;
                    error.Text = "La minuta solo puede tener una categoría, por lo que los platos seleccionados serán eliminados";
                    alerta.Visible = true;
                }

                MiSesionM.Clear();

                gvDetalle.DataSource = MiSesionM;
                gvDetalle.DataBind();
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
 