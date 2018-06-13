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

                string tipoPlato = service.ListarTipoPlato();
                XmlSerializer ser2 = new XmlSerializer(typeof(Modelo.TipoPlatoCollection));
                StringReader reader2 = new StringReader(tipoPlato);
                Modelo.TipoPlatoCollection coleccionProveedor = (Modelo.TipoPlatoCollection)ser2.Deserialize(reader2);
                reader2.Close();

                txtPrecio.ReadOnly = true;

                if (!IsPostBack)
                {
                    ddlTipo.DataSource = coleccionProveedor;
                    ddlTipo.DataTextField = "NOMBRE_TIPO_PLATO";
                    ddlTipo.DataValueField = "ID_TIPO_PLATO";
                    ddlTipo.DataBind();

                    ddlTipo.SelectedIndex = 0;

                    //segun el valor seleccionado en el anterior DDL, hara una busqueda para cargar
                    //todos los datos
                    Modelo.TipoPlato tipo_plato = new Modelo.TipoPlato();

                    tipo_plato.ID_TIPO_PLATO = short.Parse(ddlTipo.SelectedValue);

                    XmlSerializer sr = new XmlSerializer(typeof(Modelo.TipoPlato));
                    StringWriter writer = new StringWriter();
                    sr.Serialize(writer, tipo_plato);

                    string platos = service.ListarPlatoPorTipo(writer.ToString());
                    XmlSerializer ser = new XmlSerializer(typeof(Modelo.PlatoCollection));
                    StringReader reader = new StringReader(platos);
                    Modelo.PlatoCollection coleccionProducto = (Modelo.PlatoCollection)ser.Deserialize(reader);
                    reader.Close();

                    ddlPlato.DataSource = coleccionProducto;
                    ddlPlato.DataTextField = "NombreYPrecio";
                    ddlPlato.DataValueField = "ID_PLATO";
                    ddlPlato.DataBind();

                    //Para funcionar requiere que el update panel tenga el Modo Condicional
                    UpdatePanel2.Update();

                   // MiSesionM = null;
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
                short cantidad = 0;

                if (txtCantidad.Text != string.Empty)
                {
                    if (short.TryParse(txtCantidad.Text, out cantidad))
                    {
                        if (cantidad > 0)
                        {
                            if (ddlPlato.SelectedValue != string.Empty)
                            {
                                Modelo.DetallePlato detalle = new Modelo.DetallePlato();
                                detalle.CANTIDAD = cantidad;
                                detalle.ID_PLATO = short.Parse(ddlPlato.SelectedValue);

                                bool existe = false;

                                foreach (DetallePlato d in MiSesionM)
                                {
                                    if (detalle.ID_PLATO == d.ID_PLATO)
                                    {
                                        existe = true;
                                    }
                                }
                                if (existe)
                                {
                                    alerta_exito.Visible = false;
                                    error.Text = "Este Plato ya ha sido agregado";
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
                                    txtCantidad.Text = "";
                                    txtPrecio.Text = "";
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

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            try
            {
                if (MiSesionM.Count > 0)
                {
                    if (txtNombreMinuta.Text != string.Empty)
                    {
                        Modelo.Pension pension = new Modelo.Pension();
                        pension.NOMBRE_PENSION = txtNombreMinuta.Text;
                        pension.VALOR_PENSION = 0;

                        Service1 s = new Service1();
                        XmlSerializer sr = new XmlSerializer(typeof(Modelo.Plato));
                        

                        //Desayuno VIP
                        foreach (DetallePlato d in MiSesionM)
                        {
                            Modelo.Plato p = new Modelo.Plato();
                            p.ID_PLATO = d.ID_PLATO;
                            StringWriter writer = new StringWriter();
                            sr.Serialize(writer, p);
                            Modelo.Plato plato2 = s.ObtenerPlato(writer.ToString());
                            pension.VALOR_PENSION = pension.VALOR_PENSION + (plato2.PRECIO_PLATO * d.CANTIDAD);
                        }

                        if (pension.VALOR_PENSION > 0)
                        {
                            XmlSerializer sr2 = new XmlSerializer(typeof(Modelo.Pension));
                            StringWriter writer2 = new StringWriter();
                            sr2.Serialize(writer2, pension);
                            writer2.Close();
                            
                            if (s.AgregarMinuta(writer2.ToString()))
                            {
                                bool v_exito = true;

                                foreach (DetallePlato d in MiSesionM)
                                {
                                    Modelo.DetallePlato detalle = new Modelo.DetallePlato();
                                    detalle.CANTIDAD = d.CANTIDAD;
                                    detalle.ID_PLATO = d.ID_PLATO;

                                    XmlSerializer sr3 = new XmlSerializer(typeof(Modelo.DetallePlato));
                                    StringWriter writer3 = new StringWriter();
                                    sr3.Serialize(writer3, detalle);

                                    if (!s.AgregarDetalleMinuta(writer3.ToString()))
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
                        error.Text = "Debe ponerle un nombre a la minuta";
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
                    Modelo.Plato plato = new Modelo.Plato();
                    plato.ID_PLATO = short.Parse(ddlPlato.SelectedValue);

                    XmlSerializer sr = new XmlSerializer(typeof(Modelo.Plato));
                    StringWriter writer = new StringWriter();
                    sr.Serialize(writer, plato);

                    //Una vez encuentra sus datos los carga en una segunda variable
                    Plato plato2 = service.ObtenerPlato(writer.ToString());

                    txtPrecio.Text = "$" + plato2.PRECIO_PLATO * cantidad;
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

        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Service1 service = new Service1();
                //segun el valor seleccionado en el anterior DDL, hara una busqueda para cargar
                //todos los datos
                Modelo.TipoPlato tipo_plato = new Modelo.TipoPlato();

                tipo_plato.ID_TIPO_PLATO = short.Parse(ddlTipo.SelectedValue);

                XmlSerializer sr = new XmlSerializer(typeof(Modelo.TipoPlato));
                StringWriter writer = new StringWriter();
                sr.Serialize(writer, tipo_plato);

                string platos = service.ListarPlatoPorTipo(writer.ToString());
                XmlSerializer ser = new XmlSerializer(typeof(Modelo.PlatoCollection));
                StringReader reader = new StringReader(platos);
                Modelo.PlatoCollection coleccionProducto = (Modelo.PlatoCollection)ser.Deserialize(reader);
                reader.Close();

                ddlPlato.DataSource = coleccionProducto;
                ddlPlato.DataTextField = "NombreYPrecio";
                ddlPlato.DataValueField = "ID_PLATO";
                ddlPlato.DataBind();

                //Para funcionar requiere que el update panel tenga el Modo Condicional
                txtCantidad.Text = "";
                txtPrecio.Text = "";
                UpdatePanel1.Update();
                UpdatePanel2.Update();

                btnLimpiar.Enabled = false;
                btnVer.Enabled = false;
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
 