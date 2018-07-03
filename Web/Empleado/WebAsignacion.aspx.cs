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
    public partial class WebAsignacion : System.Web.UI.Page
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
                error.Text = "";
                exito.Text = "";
                alerta_exito.Visible = false;
                alerta.Visible = false;

                if (MiSesionOrden.NUMERO_ORDEN != 0)
                {
                    OrdenCompra orden = MiSesionOrden;

                    Service1 s = new Service1();
                    XmlSerializer sr = new XmlSerializer(typeof(Modelo.OrdenCompra));
                    StringWriter writer = new StringWriter();
                    sr.Serialize(writer, orden);

                    if (s.ListaHuespedesNoAsignados(writer.ToString()) != null)
                    {
                        string datos = s.ListaHuespedesNoAsignados(writer.ToString());
                        XmlSerializer ser3 = new XmlSerializer(typeof(Modelo.DetalleOrdenCollection));
                        StringReader reader = new StringReader(datos);

                        Modelo.DetalleOrdenCollection listaDetalle = (Modelo.DetalleOrdenCollection)ser3.Deserialize(reader);
                        reader.Close();

                        MiSesionOrden = s.ObtenerReserva(writer.ToString());

                        if (listaDetalle.Count > 0)
                        {
                            CargarGridPendiente(listaDetalle);

                            string datos2 = s.ListaHuespedesAsignados(writer.ToString());

                            XmlSerializer ser4 = new XmlSerializer(typeof(Modelo.DetalleOrdenCollection));
                            StringReader reader2 = new StringReader(datos2);

                            Modelo.DetalleOrdenCollection listaDetalle2 = (Modelo.DetalleOrdenCollection)ser4.Deserialize(reader2);
                            reader.Close();

                            CargarGridAceptado(listaDetalle2);
                            
                        }
                        else
                        {
                            //Cambia el estado de la orden de compra a Asignado
                            MiSesionOrden.ESTADO_ORDEN = "Asignado";
                            OrdenCompra orden2 = MiSesionOrden;
                            XmlSerializer ser4 = new XmlSerializer(typeof(Modelo.OrdenCompra));
                            StringWriter writer4 = new StringWriter();
                            ser4.Serialize(writer4, orden2);

                            if (s.EditarEstadoReserva(writer4.ToString()))
                            {
                                Response.Write("<script language='javascript'>window.alert('La reserva ha sido asignada con éxito');window.location='../Empleado/WebAsignarHabitacion.aspx';</script>");
                            }
                            else
                            {
                                Response.Write("<script language='javascript'>window.alert('Ha ocurrido un Error');window.location='../Hostal/WebLogin.aspx';</script>");
                            }
                        }
                    }
                }
                else
                {
                    Response.Write("<script language='javascript'>window.alert('Necesita asignar una orden de Habitación');window.location='../Hostal/WebLogin.aspx';</script>");
                }
            }
            catch (Exception)
            {
                Response.Write("<script language='javascript'>window.alert('Debe Iniciar Sesión Primero');window.location='../Hostal/WebLogin.aspx';</script>");
            }
        }

        private void CargarGridAceptado (DetalleOrdenCollection listaDetalle2) {
            Huesped huesped;
            Pension pension;
            Categoria cat;
            TipoHabitacion tipo;

            //Creacion DataTable
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] {
                new DataColumn("ID_DETALLE", typeof(int)),
                new DataColumn("RUT_HUESPED", typeof(string)),
                new DataColumn("HUESPED",typeof(string)),
                new DataColumn("HABITACION",typeof(string)),
                new DataColumn("PENSION",typeof(string)),
                new DataColumn("ESTADO",typeof(string))
            });

            //Carga de datos en DataTable
            foreach (DetalleOrden d in listaDetalle2) {
                huesped = new Huesped();
                huesped.RUT_HUESPED = d.RUT_HUESPED;
                huesped.BuscarHuesped();

                cat = new Categoria();
                cat.ID_CATEGORIA = d.ID_CATEGORIA_HABITACION;
                cat.BuscarCategoria();

                tipo = new TipoHabitacion();
                tipo.ID_TIPO_HABITACION = d.ID_TIPO_HABITACION;
                tipo.BuscarTipo();

                pension = new Pension();
                pension.ID_PENSION = d.ID_PENSION;
                pension.BuscarPension();

                dt.Rows.Add(d.ID_DETALLE,huesped.RUT_HUESPED + "-" + huesped.DV_HUESPED,huesped.PNOMBRE_HUESPED + " " + huesped.APP_PATERNO_HUESPED + " " + huesped.APP_MATERNO_HUESPED,tipo.NOMBRE_TIPO_HABITACION + "-" + cat.NOMBRE_CATEGORIA,pension.NOMBRE_PENSION,d.ESTADO);
            }

            //Carga de GriedView
            gvAceptado.DataSource = dt;
            gvAceptado.DataBind();
        }

        private void CargarGridPendiente (DetalleOrdenCollection listaDetalle) {
            Huesped huesped;
            Pension pension;
            Categoria cat;
            TipoHabitacion tipo;

            //Creacion DataTable
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] {
                new DataColumn("ID_DETALLE", typeof(int)),
                new DataColumn("RUT_HUESPED", typeof(string)),
                new DataColumn("HUESPED",typeof(string)),
                new DataColumn("HABITACION",typeof(string)),
                new DataColumn("PENSION",typeof(string)),
                new DataColumn("ESTADO",typeof(string))
            });

            //Carga de datos en DataTable
            foreach (DetalleOrden d in listaDetalle) {
                huesped = new Huesped();
                huesped.RUT_HUESPED = d.RUT_HUESPED;
                huesped.BuscarHuesped();

                cat = new Categoria();
                cat.ID_CATEGORIA = d.ID_CATEGORIA_HABITACION;
                cat.BuscarCategoria();

                tipo = new TipoHabitacion();
                tipo.ID_TIPO_HABITACION = d.ID_TIPO_HABITACION;
                tipo.BuscarTipo();

                pension = new Pension();
                pension.ID_PENSION = d.ID_PENSION;
                pension.BuscarPension();

                dt.Rows.Add(d.ID_DETALLE,huesped.RUT_HUESPED + "-" + huesped.DV_HUESPED,huesped.PNOMBRE_HUESPED + " " + huesped.APP_PATERNO_HUESPED + " " + huesped.APP_MATERNO_HUESPED,tipo.NOMBRE_TIPO_HABITACION + "-" + cat.NOMBRE_CATEGORIA,pension.NOMBRE_PENSION,d.ESTADO);
            }

            //Carga de GriedView
            gvDetalle.DataSource = dt;
            gvDetalle.DataBind();
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

        public OrdenCompra MiSesionOrden
        {
            get
            {
                if (Session["OrdenCompra"] == null)
                {
                    Session["OrdenCompra"] = new OrdenCompra();
                }
                return (OrdenCompra)Session["OrdenCompra"];
            }
            set
            {
                Session["OrdenCompra"] = value;
            }
        }

        public DetalleOrden MiSesionDetalleO
        {
            get
            {
                if (Session["DetalleOrden"] == null)
                {
                    Session["DetalleOrden"] = new DetalleOrden();
                }
                return (DetalleOrden)Session["DetalleOrden"];
            }
            set
            {
                Session["DetalleOrden"] = value;
            }
        }

        //Al Presionar Asignar
        protected void btnInfo2_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)(sender);
                short ID_DETALLE = short.Parse(btn.CommandArgument);

                DetalleOrden detalle = new DetalleOrden();
                detalle.ID_DETALLE = ID_DETALLE;

                MiSesionDetalleO = detalle;

                if (MiSesionDetalleO.ID_DETALLE != 0)
                {
                    Service1 s = new Service1();
                    XmlSerializer sr = new XmlSerializer(typeof(Modelo.DetalleOrden));
                    StringWriter writer = new StringWriter();
                    sr.Serialize(writer, detalle);

                    if (s.ObtenerDetalleReserva(writer.ToString()) != null)
                    {
                        MiSesionDetalleO = s.ObtenerDetalleReserva(writer.ToString());

                        DetalleOrden detalle2 = MiSesionDetalleO;
                        XmlSerializer sr2 = new XmlSerializer(typeof(Modelo.DetalleOrden));
                        StringWriter writer2 = new StringWriter();
                        sr.Serialize(writer2, detalle2);

                        if (s.ListarHabitacionDisponibleCategoria(writer2.ToString()) != null)
                        {
                            string habitacion = s.ListarHabitacionDisponibleCategoria(writer2.ToString());
                            XmlSerializer ser3 = new XmlSerializer(typeof(Modelo.HabitacionCollection));
                            StringReader reader3 = new StringReader(habitacion);
                            Modelo.HabitacionCollection coleccionHabitacion = (Modelo.HabitacionCollection)ser3.Deserialize(reader3);
                            reader3.Close();

                            if (coleccionHabitacion.Count == 0)
                            {
                                string habitacion2 = s.ListarHabitacionDisponible();
                                XmlSerializer ser4 = new XmlSerializer(typeof(Modelo.HabitacionCollection));
                                StringReader reader4 = new StringReader(habitacion2);
                                coleccionHabitacion = (Modelo.HabitacionCollection)ser4.Deserialize(reader4);
                                reader3.Close();

                                if (coleccionHabitacion.Count == 0)
                                {
                                    Response.Write("<script language=javascript>alert('No hay ninguna habitación disponible. tendrá que rechazar la reserva')</script>");
                                }
                                else
                                {
                                    Response.Write("<script language=javascript>alert('No quedan habitaciones vacantes de esa categoría, escoja otra categoría')</script>");
                                }
                            }

                            ddlHabitacion.DataSource = coleccionHabitacion;
                            ddlHabitacion.DataTextField = "DatosHabitacion";
                            ddlHabitacion.DataValueField = "NUMERO_HABITACION";
                            ddlHabitacion.DataBind();
                        }
                       
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#exampleModal2').modal();", true);
                    }
                }
            }
            catch (Exception)
            {
                Response.Write("<script language='javascript'>window.alert('Debe Iniciar Sesión Primero');window.location='../Hostal/WebLogin.aspx';</script>");
            }
        }

        //Al Rechazar
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            DetalleOrden detalleOrden = MiSesionDetalleO;
            detalleOrden.ESTADO = "No Asignado";

            Service1 s = new Service1();
            XmlSerializer sr5 = new XmlSerializer(typeof(Modelo.DetalleOrden));
            StringWriter writer5 = new StringWriter();
            sr5.Serialize(writer5, detalleOrden);

            if (s.EditarEstadoDetalleReserva(writer5.ToString()))
            {
                Response.Write("<script language='javascript'>window.alert('Huésped No Asignado');window.location='../Empleado/WebAsignacion.aspx';</script>");
            }
            else
            {
                alerta_exito.Visible = false;
                error.Text = "No se pudo editar el detalle de la Reserva";
                alerta.Visible = true;
            }
        }

        //Al Asignar la habitación finalmente
        protected void btnAgregar_Click(object sender, EventArgs e)
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
                    else if(habitacion.ID_TIPO_HABITACION == 3)
                    {
                        if (habitacion.ESTADO_HABITACION.Equals("Disponible"))
                        {
                            habitacion.ESTADO_HABITACION = "Vacante 2";
                        }
                        else if(habitacion.ESTADO_HABITACION.Equals("Vacante 2"))
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
                        #region Agregando Detalle de Habitacion
                        //2- Agregando el detalle de habitación 
                        DetalleHabitacion detalleHabitacion = new DetalleHabitacion();
                        //Esta linea consulta si tiene un numero asignado, en caso contrario no hara nada.
                        if (MiSesionOrden.RUT_CLIENTE != 0)
                        {
                            detalleHabitacion.RUT_CLIENTE = MiSesionOrden.RUT_CLIENTE;
                            detalleHabitacion.NUMERO_HABITACION = short.Parse(ddlHabitacion.SelectedValue);
                            detalleHabitacion.RUT_HUESPED = MiSesionDetalleO.RUT_HUESPED;
                            detalleHabitacion.FECHA_LLEGADA = MiSesionOrden.FECHA_LLEGADA;
                            detalleHabitacion.FECHA_SALIDA = MiSesionOrden.FECHA_SALIDA;
                            detalleHabitacion.ID_PENSION = MiSesionDetalleO.ID_PENSION;

                            XmlSerializer sr3 = new XmlSerializer(typeof(Modelo.DetalleHabitacion));
                            StringWriter writer3 = new StringWriter();
                            sr3.Serialize(writer3, detalleHabitacion);

                            if (s.AgregarDetalleHabitacion(writer3.ToString()))
                            {
                                    #region Detalle Orden
                                    //4- Modificando el estado del detalle de orden a Asignado
                                    DetalleOrden detalleOrden = MiSesionDetalleO;
                                    detalleOrden.ESTADO = "Asignado";

                                    XmlSerializer sr5 = new XmlSerializer(typeof(Modelo.DetalleOrden));
                                    StringWriter writer5 = new StringWriter();
                                    sr5.Serialize(writer5, detalleOrden);

                                    if (s.EditarEstadoDetalleReserva(writer5.ToString()))
                                    {
                                        Response.Write("<script language='javascript'>window.alert('Huésped Asignado');window.location='../Empleado/WebAsignacion.aspx';</script>");
                                    }
                                    else
                                    {
                                        alerta_exito.Visible = false;
                                        error.Text = "No se pudo editar el detalle de la Reserva";
                                        alerta.Visible = true;
                                    }
                                    #endregion
                            }
                            else
                            {
                                alerta_exito.Visible = false;
                                error.Text = "No se pudo agregar el detalle de habitación";
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

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            Service1 s = new Service1();

            DetalleOrden detalle2 = MiSesionDetalleO;
            XmlSerializer sr2 = new XmlSerializer(typeof(Modelo.DetalleOrden));
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

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#exampleModal2').modal();", true);
        }

        protected void btnRemover_Click(object sender, EventArgs e)
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
                        habitacion.ESTADO_HABITACION = "Disponible";
                    }
                    else if (habitacion.ID_TIPO_HABITACION == 2)
                    {
                        if (habitacion.ESTADO_HABITACION.Equals("Vacante 1"))
                        {
                            habitacion.ESTADO_HABITACION = "Disponible";
                        }
                        else
                        {
                            habitacion.ESTADO_HABITACION = "Vacante 1";
                        }
                    }
                    else if (habitacion.ID_TIPO_HABITACION == 3)
                    {
                        if (habitacion.ESTADO_HABITACION.Equals("Ocupado"))
                        {
                            habitacion.ESTADO_HABITACION = "Vacante 1";
                        }
                        else if (habitacion.ESTADO_HABITACION.Equals("Vacante 1"))
                        {
                            habitacion.ESTADO_HABITACION = "Vacante 2";
                        }
                        else
                        {
                            habitacion.ESTADO_HABITACION = "Disponible";
                        }
                    }

                    XmlSerializer sr2 = new XmlSerializer(typeof(Modelo.Habitacion));
                    StringWriter writer2 = new StringWriter();
                    sr2.Serialize(writer2, habitacion);

                    if (s.ModificarHabitacion(writer2.ToString()))
                    {
                        #region Eliminando Detalle de Habitacion
                        //2- Agregando el detalle de habitación 
                        DetalleHabitacion detalleHabitacion = new DetalleHabitacion();
                        //Esta linea consulta si tiene un numero asignado, en caso contrario no hara nada.
                        if (MiSesionOrden.RUT_CLIENTE != 0)
                        {
                            detalleHabitacion.RUT_CLIENTE = MiSesionOrden.RUT_CLIENTE;

                            XmlSerializer sr3 = new XmlSerializer(typeof(Modelo.DetalleHabitacion));
                            StringWriter writer3 = new StringWriter();
                            sr3.Serialize(writer3, detalleHabitacion);

                            if (s.EliminarDetalleHabitacion(writer3.ToString()))
                            {
                                #region Detalle Pasajeros
                                //3- Agregando el detalle de pasajeros de la habitación junto con la fecha de entrada y salida
                                DetallePasajeros detallePasajeros = new DetallePasajeros();
                                detallePasajeros.RUT_HUESPED = MiSesionDetalleO.RUT_HUESPED;
                                detallePasajeros.FECHA_LLEGADA = MiSesionOrden.FECHA_LLEGADA;
                                detallePasajeros.FECHA_SALIDA = MiSesionOrden.FECHA_SALIDA;
                                detallePasajeros.ID_PENSION = MiSesionDetalleO.ID_PENSION;
                                detallePasajeros.NUMERO_HABITACION = short.Parse(ddlHabitacion.SelectedValue);

                                XmlSerializer sr4 = new XmlSerializer(typeof(Modelo.DetallePasajeros));
                                StringWriter writer4 = new StringWriter();
                                sr4.Serialize(writer4, detallePasajeros);
                                /*
                                if (s.AgregarDetallePasajeros(writer4.ToString()))
                                {
                                    #region Detalle Orden
                                    //4- Modificando el estado del detalle de orden a Asignado
                                    DetalleOrden detalleOrden = MiSesionDetalleO;
                                    detalleOrden.ESTADO = "Pendiente";

                                    XmlSerializer sr5 = new XmlSerializer(typeof(Modelo.DetalleOrden));
                                    StringWriter writer5 = new StringWriter();
                                    sr5.Serialize(writer5, detalleOrden);

                                    if (s.EditarEstadoDetalleReserva(writer5.ToString()))
                                    {
                                        Response.Write("<script language='javascript'>window.alert('Huésped Pendiente');window.location='../Empleado/WebAsignacion.aspx';</script>");
                                    }
                                    else
                                    {
                                        alerta_exito.Visible = false;
                                        error.Text = "No se pudo editar el detalle de la Reserva";
                                        alerta.Visible = true;
                                    }
                                    #endregion
                                }
                                else
                                {
                                    alerta_exito.Visible = false;
                                    error.Text = "No se pudo agregar el detalle de pasajeros";
                                    alerta.Visible = true;
                                }
                                */
                                #endregion
                            }
                            else
                            {
                                alerta_exito.Visible = false;
                                error.Text = "No se pudo agregar el detalle de habitación";
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
    }
}