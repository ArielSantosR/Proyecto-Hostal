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
    public partial class WebClientesAsignados : System.Web.UI.Page
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
            try
            {
                if (MiSesion != null)
                {
                    error.Text = "";
                    exito.Text = "";
                    alerta_exito.Visible = false;
                    alerta.Visible = false;

                    Service1 s = new Service1();
                    string datos = s.ListarReservaAsignada();
                    XmlSerializer ser = new XmlSerializer(typeof(Modelo.OrdenCompraCollection));
                    StringReader reader = new StringReader(datos);

                    Modelo.OrdenCompraCollection listaOrden = (Modelo.OrdenCompraCollection)ser.Deserialize(reader);
                    reader.Close();
                    gvOrden.DataSource = listaOrden;
                    gvOrden.DataBind();
                }
                else
                {
                    Response.Write("<script language='javascript'>window.alert('Debe Iniciar Sesión Primero');window.location='../Hostal/WebLogin.aspx';</script>");
                }
            }
            catch (Exception)
            {
                Response.Write("<script language='javascript'>window.alert('Debe Iniciar Sesión Primero');window.location='../Hostal/WebLogin.aspx';</script>");
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

        public Modelo.Cliente MiSesionCl
        {
            get
            {
                if (Session["Cliente"] == null)
                {
                    Session["Cliente"] = new Modelo.Cliente();
                }
                return (Modelo.Cliente)Session["Cliente"];
            }
            set
            {
                Session["Cliente"] = value;
            }
        }

        public DetalleHabitacion misesionDH
        {
            get
            {
                if (Session["DetalleHabitacion"] == null)
                {
                    Session["DetalleHabitacion"] = new Modelo.DetalleHabitacion();
                }
                return (Modelo.DetalleHabitacion)Session["DetalleHabitacion"];
            }
            set
            {
                Session["DetalleHabitacion"] = value;
            }

        }

        protected void btnInfo_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)(sender);
                int rut_cliente = int.Parse(btn.CommandArgument);

                Modelo.Cliente cliente = new Modelo.Cliente();
                cliente.RUT_CLIENTE = rut_cliente;

                MiSesionCl = cliente;

                if (MiSesionCl.RUT_CLIENTE != 0)
                {
                    Service1 s = new Service1();
                    XmlSerializer sr = new XmlSerializer(typeof(Modelo.Cliente));
                    StringWriter writer = new StringWriter();
                    sr.Serialize(writer, cliente);

                    if (s.ListaDetalleHabitacionCliente(writer.ToString()) != null)
                    {
                        string datos = s.ListaDetalleHabitacionCliente(writer.ToString());
                        XmlSerializer ser3 = new XmlSerializer(typeof(Modelo.DetalleHabitacionCollection));
                        StringReader reader = new StringReader(datos);

                        Modelo.DetalleHabitacionCollection listaDetalle = (Modelo.DetalleHabitacionCollection)ser3.Deserialize(reader);
                        reader.Close();
                        CargarGrid(listaDetalle);

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#exampleModal2').modal();", true);
                    }
                }
            }
            catch (Exception ex)
            {
                alerta_exito.Visible = false;
                error.Text = "Excepción: " + ex.ToString();
                alerta.Visible = true;
            }
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            Service1 s = new Service1();

            LinkButton btn = (LinkButton)(sender);
            short id_detalle_h = short.Parse(btn.CommandArgument);

            DetalleHabitacion detalle = new DetalleHabitacion();
            detalle.ID_DETALLE_H = id_detalle_h;

            misesionDH = detalle;

            DetalleHabitacion detalle2 = misesionDH;
            XmlSerializer sr2 = new XmlSerializer(typeof(Modelo.DetalleHabitacion));
            StringWriter writer2 = new StringWriter();
            sr2.Serialize(writer2, detalle2);

            string habitacion2 = s.ListarHabitacionDisponible();
            XmlSerializer ser4 = new XmlSerializer(typeof(Modelo.HabitacionCollection));
            StringReader reader4 = new StringReader(habitacion2);
            Modelo.HabitacionCollection coleccionHabitacion = (Modelo.HabitacionCollection)ser4.Deserialize(reader4);
            reader4.Close();

            ddlHabitacion.DataSource = coleccionHabitacion;
            ddlHabitacion.DataTextField = "DatosHabitacion";
            ddlHabitacion.DataValueField = "NUMERO_HABITACION";
            ddlHabitacion.DataBind();

            UpdatePanel2.Update();

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#exampleModal').modal();", true);
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            #region Cambiar Estado de Habitacion
            try
            {
                //1- Cambiando el estado de Habitación a Ocupado
                Habitacion habitacion = new Habitacion();
                habitacion.NUMERO_HABITACION = short.Parse(ddlHabitacion.SelectedValue);

                Service1 s = new Service1();
                XmlSerializer sr = new XmlSerializer(typeof(Modelo.Habitacion));
                StringWriter writer = new StringWriter();
                sr.Serialize(writer, habitacion);

                if (s.ObtenerHabitacion(writer.ToString()) != null)
                {
                    habitacion = s.ObtenerHabitacion(writer.ToString());

                    if (habitacion.ID_TIPO_HABITACION == 1)
                    {
                        habitacion.ESTADO_HABITACION = "Ocupado";
                    }
                    else if (habitacion.ID_TIPO_HABITACION == 2)
                    {
                        if (habitacion.ESTADO_HABITACION.Equals("Disponible"))
                        {
                            habitacion.ESTADO_HABITACION = "Vacante 1";
                        }
                        else
                        {
                            habitacion.ESTADO_HABITACION = "Ocupado";
                        }
                    }
                    else if (habitacion.ID_TIPO_HABITACION == 3)
                    {
                        if (habitacion.ESTADO_HABITACION.Equals("Disponible"))
                        {
                            habitacion.ESTADO_HABITACION = "Vacante 2";
                        }
                        else if (habitacion.ESTADO_HABITACION.Equals("Vacante 2"))
                        {
                            habitacion.ESTADO_HABITACION = "Vacante 1";
                        }
                        else
                        {
                            habitacion.ESTADO_HABITACION = "Ocupado";
                        }
                    }

                    XmlSerializer sr2 = new XmlSerializer(typeof(Modelo.Habitacion));
                    StringWriter writer2 = new StringWriter();
                    sr2.Serialize(writer2, habitacion);

                    if (s.ModificarHabitacion(writer2.ToString()))
                    {
                        
                        DetalleHabitacion detalleHabitacion2 = new DetalleHabitacion();
                        detalleHabitacion2.ID_DETALLE_H = misesionDH.ID_DETALLE_H;

                        XmlSerializer sr6 = new XmlSerializer(typeof(Modelo.DetalleHabitacion));
                        StringWriter writer6 = new StringWriter();
                        sr6.Serialize(writer6, detalleHabitacion2);

                        if (s.obtenerDetalleHabitacion(writer6.ToString()) != null)
                        {
                            detalleHabitacion2 = s.obtenerDetalleHabitacion(writer6.ToString());

                            #region Cambiar Habitación a Ocupado
                            Habitacion habitacion2 = new Habitacion();
                            habitacion2.NUMERO_HABITACION = detalleHabitacion2.NUMERO_HABITACION;

                            XmlSerializer sr5 = new XmlSerializer(typeof(Modelo.Habitacion));
                            StringWriter writer5 = new StringWriter();
                            sr5.Serialize(writer5, habitacion2);

                            if (s.ObtenerHabitacion(writer5.ToString()) != null)
                            {
                                habitacion2 = s.ObtenerHabitacion(writer5.ToString());

                                if (habitacion2.ID_TIPO_HABITACION == 1)
                                {
                                    habitacion2.ESTADO_HABITACION = "Disponible";
                                }
                                else if (habitacion2.ID_TIPO_HABITACION == 2)
                                {
                                    if (habitacion2.ESTADO_HABITACION.Equals("Vacante 1"))
                                    {
                                        habitacion2.ESTADO_HABITACION = "Disponible";
                                    }
                                    else
                                    {
                                        habitacion2.ESTADO_HABITACION = "Vacante 1";
                                    }
                                }
                                else if (habitacion2.ID_TIPO_HABITACION == 3)
                                {
                                    if (habitacion2.ESTADO_HABITACION.Equals("Ocupado"))
                                    {
                                        habitacion2.ESTADO_HABITACION = "Vacante 1";
                                    }
                                    else if (habitacion2.ESTADO_HABITACION.Equals("Vacante 1"))
                                    {
                                        habitacion2.ESTADO_HABITACION = "Vacante 2";
                                    }
                                    else
                                    {
                                        habitacion2.ESTADO_HABITACION = "Disponible";
                                    }
                                }

                                XmlSerializer sr4 = new XmlSerializer(typeof(Modelo.Habitacion));
                                StringWriter writer4 = new StringWriter();
                                sr2.Serialize(writer4, habitacion2);

                                if (s.ModificarHabitacion(writer4.ToString()))
                                {
                                    #region Agregando Detalle de Habitacion
                                    //2- Agregando el detalle de habitación 
                                    DetalleHabitacion detalleHabitacion = new DetalleHabitacion();
                                    //Esta linea consulta si tiene un numero asignado, en caso contrario no hara nada.
                                    if (misesionDH != null)
                                    {
                                        detalleHabitacion = misesionDH;
                                        detalleHabitacion.NUMERO_HABITACION = short.Parse(ddlHabitacion.SelectedValue);

                                        XmlSerializer sr3 = new XmlSerializer(typeof(Modelo.DetalleHabitacion));
                                        StringWriter writer3 = new StringWriter();
                                        sr3.Serialize(writer3, detalleHabitacion);

                                        if (s.EditarDetalleHabitacion(writer3.ToString()))
                                        {
                                            Response.Write("<script language='javascript'>window.alert('Habitación Cambiada');window.location='../Empleado/WebClientesAsignados.aspx';</script>");
                                        }
                                        else
                                        {
                                            alerta_exito.Visible = false;
                                            error.Text = "No se pudo cambiar de habitación";
                                            alerta.Visible = true;
                                        }
                                    }
                                    else
                                    {
                                        Response.Write("<script language='javascript'>window.alert('Vuelva a cargar la orden');window.location='../Hostal/WebAsignarHabitacion.aspx';</script>");
                                    }
                                    #endregion
                                }
                                else
                                {
                                    alerta_exito.Visible = false;
                                    error.Text = "No se pudo modificar la habitación";
                                    alerta.Visible = true;
                                }

                                alerta_exito.Visible = false;
                                error.Text = "No se encontró el detalle de habitación";
                                alerta.Visible = true;
                            }
                            else
                            {
                                alerta_exito.Visible = false;
                                error.Text = "No se pudo encontrar la habitación";
                                alerta.Visible = true;
                            }
                            #endregion
                        }
                        else
                        {
                            alerta_exito.Visible = false;
                            error.Text = "No se pudo encontrar el detalle de habitación";
                            alerta.Visible = true;
                        }
                    }
                    else
                    {
                        alerta_exito.Visible = false;
                        error.Text = "No se pudo modificar la habitación";
                        alerta.Visible = true;
                    }
                }
                else
                {
                    alerta_exito.Visible = false;
                    error.Text = "No se pudo encontrar la habitación";
                    alerta.Visible = true;
                }
            }
            catch (Exception ex)
            {
                alerta_exito.Visible = false;
                error.Text = "Excepción: " + ex.ToString();
                alerta.Visible = true;
            }
            #endregion
        }

        private void CargarGrid (DetalleHabitacionCollection listaDetalle) {
            Huesped huesped;
            Modelo.Cliente cli;
            Pension pension;

            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] {
                new DataColumn("ID_DETALLE_H", typeof(short)),
                new DataColumn("NUMERO_HABITACION", typeof(short)),
                new DataColumn("CLIENTE",typeof(string)),
                new DataColumn("HUESPED",typeof(string)),
                new DataColumn("PENSION",typeof(string)),
                new DataColumn("FechaLlegada",typeof(string)),
                new DataColumn("FechaSalida",typeof(string))
            });

            foreach (DetalleHabitacion item in listaDetalle) {
                huesped = new Huesped();
                huesped.RUT_HUESPED = item.RUT_HUESPED;
                huesped.BuscarHuesped();

                cli = new Modelo.Cliente();
                cli.RUT_CLIENTE = item.RUT_CLIENTE;
                cli.BuscarCliente();

                pension = new Pension();
                pension.ID_PENSION = item.ID_PENSION;
                pension.BuscarPension();

                dt.Rows.Add(item.ID_DETALLE_H,item.NUMERO_HABITACION,cli.NOMBRE_CLIENTE,huesped.PNOMBRE_HUESPED + " " + huesped.APP_PATERNO_HUESPED + " " + huesped.APP_MATERNO_HUESPED,pension.NOMBRE_PENSION,item.FECHA_LLEGADA.ToShortDateString(),item.FECHA_SALIDA.ToShortDateString());
            }

            gvDetalle.DataSource = dt;
            gvDetalle.DataBind();
        }
    }
}