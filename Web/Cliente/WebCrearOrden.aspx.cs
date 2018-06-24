﻿using Modelo;
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

namespace Web.Cliente
{
    public partial class WebCrearOrden : System.Web.UI.Page
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
                    else if (MiSesion.TIPO_USUARIO.Equals("Cliente") &&
                    MiSesion.ESTADO.Equals("Habilitado"))
                    {
                        MasterPageFile = "~/Cliente/ClienteM.Master";
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

            Service1 service = new Service1();

            Modelo.Cliente cliente = new Modelo.Cliente();

            try
            {
                if (MiSesion != null)
                {
                    if (MiSesion.TIPO_USUARIO.Equals(Tipo_Usuario.Cliente.ToString()))
                    {
                        divClientes.Visible = false;
                        cliente.ID_USUARIO = MiSesion.ID_USUARIO;

                        //Si el ID de Cliente es encontrado, pasar al siguiente paso
                        Service1 s = new Service1();
                        XmlSerializer sr = new XmlSerializer(typeof(Modelo.Cliente));
                        StringWriter writer = new StringWriter();
                        sr.Serialize(writer, cliente);
                        writer.Close();

                        Modelo.Cliente cliente2 = s.buscarIDC(writer.ToString());
                        XmlSerializer sr2 = new XmlSerializer(typeof(Modelo.Cliente));
                        StringWriter writer2 = new StringWriter();
                        sr2.Serialize(writer2, cliente2);
                        writer2.Close();

                        string datos = service.ListarHuespedService(writer2.ToString());
                        XmlSerializer ser3 = new XmlSerializer(typeof(Modelo.HuespedCollection));
                        StringReader reader = new StringReader(datos);
                        Modelo.HuespedCollection coleccionHuesped = (Modelo.HuespedCollection)ser3.Deserialize(reader);

                        if (coleccionHuesped.Count == 0)
                        {
                            Response.Write("<script language='javascript'>window.alert('Para hacer una Reserva debe registrar huéspedes primero');window.location='../Hostal/WebAgregarPasajeros.aspx';</script>");
                        }

                        string categoria_habitacion = service.ListarCategoriaHabitacion();
                        XmlSerializer ser1 = new XmlSerializer(typeof(Modelo.CategoriaHabitacionCollection));
                        StringReader reader1 = new StringReader(categoria_habitacion);
                        Modelo.CategoriaHabitacionCollection coleccionCategoria = (Modelo.CategoriaHabitacionCollection)ser1.Deserialize(reader1);
                        reader.Close();

                        string minuta = service.ListarMinuta();
                        XmlSerializer ser2 = new XmlSerializer(typeof(Modelo.MinutaCollection));
                        StringReader reader2 = new StringReader(minuta);
                        Modelo.MinutaCollection coleccionMinuta = (Modelo.MinutaCollection)ser2.Deserialize(reader2);
                        reader.Close();

                        if (!IsPostBack)
                        {
                            ddlRut.DataSource = coleccionHuesped;
                            ddlRut.DataValueField = "RUT_HUESPED";
                            ddlRut.DataTextField = "RutYNombre";
                            ddlRut.DataBind();
                            ddlRut.Items.Insert(0,new ListItem("Seleccione Huesped...","0"));


                            ddlCategoria.DataSource = coleccionCategoria;
                            ddlCategoria.DataTextField = "NOMBRE_CATEGORIA";
                            ddlCategoria.DataValueField = "ID_CATEGORIA_HABITACION";
                            ddlCategoria.DataBind();
                            ddlCategoria.Items.Insert(0,new ListItem("Seleccione Categoria de Habitación...","0"));

                            ddlPension.DataSource = coleccionMinuta;
                            ddlPension.DataTextField = "NombreYPrecio";
                            ddlPension.DataValueField = "ID_PENSION";
                            ddlPension.DataBind();
                            ddlPension.Items.Insert(0,new ListItem("Seleccione Pensión...","0"));

                            MiSesionO = null;
                            btnVer.Enabled = false;
                            btnReservar.Enabled = false;
                        }
                        CargarGridView(MiSesionO);
                    }
                    else
                    {
                        divClientes.Visible = true;

                        if (!IsPostBack) {
                            ddlCategoria.Enabled = false;
                            ddlPension.Enabled = false;
                            ddlRut.Enabled = false;

                            ddlClientes.DataSource = ClienteCollection.ListaClientes();
                            ddlClientes.DataTextField = "NOMBRE_CLIENTE";
                            ddlClientes.DataValueField = "RUT_CLIENTE";
                            ddlClientes.DataBind();
                            ddlClientes.Items.Insert(0,new ListItem("Seleccione Empresa...","0"));

                            ddlRut.Items.Insert(0,new ListItem("Seleccione Huesped...","0"));
                            ddlCategoria.Items.Insert(0,new ListItem("Seleccione Categoria de Habitación...","0"));
                            ddlPension.Items.Insert(0,new ListItem("Seleccione Pensión...","0"));

                            MiSesionO = null;
                            btnVer.Enabled = false;
                            btnReservar.Enabled = false;
                        }
                        CargarGridView(MiSesionO);
                    }
                }
                else
                {
                    Response.Write("<script language='javascript'>window.alert('Debe Iniciar Sesión Primero');window.location='../Hostal/WebLogin.aspx';</script>");
                }
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
            try
            {
                Modelo.DetalleOrden detalle = new Modelo.DetalleOrden();
                detalle.RUT_HUESPED = int.Parse(ddlRut.SelectedValue);
                detalle.ID_PENSION = short.Parse(ddlPension.SelectedValue);
                detalle.ID_CATEGORIA_HABITACION = short.Parse(ddlCategoria.SelectedValue);
                bool existe = false;

                foreach (DetalleOrden o in MiSesionO)
                {
                    if (detalle.RUT_HUESPED == o.RUT_HUESPED)
                    {
                        existe = true;
                    }
                }
                if (existe)
                {
                    alerta_exito.Visible = false;
                    error.Text = "Este Huésped ya ha sido agregado";
                    alerta.Visible = true;
                }
                else
                {
                    exito.Text = "Huésped Agregado a Reserva.";
                    alerta_exito.Visible = true;
                    alerta.Visible = false;
                    MiSesionO.Add(detalle);
                    btnVer.Enabled = true;
                    btnReservar.Enabled = true;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#exampleModal2').modal();", true);
                }
                CargarGridView(MiSesionO);
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
                int RUT_HUESPED = int.Parse(btn.CommandArgument);

                foreach (DetalleOrden o in MiSesionO.ToList())
                {
                    if (o.RUT_HUESPED == RUT_HUESPED)
                    {
                        MiSesionO.Remove(o);
                    }
                }
                CargarGridView(MiSesionO);

                if (MiSesionO.Count == 0)
                {
                    btnVer.Enabled = false;
                    btnReservar.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                alerta_exito.Visible = false;
                error.Text = "Excepción: " + ex.ToString();
                alerta.Visible = true;
            }
        }

        protected void btnReservar_Click(object sender, EventArgs e)
        {
            try
            {
                if (MiSesion.TIPO_USUARIO.Equals(Tipo_Usuario.Cliente.ToString())) {
                    if (MiSesionO.Count > 0) {
                        if (calendarFecha.SelectedDate >= DateTime.Today && calendarSalida.SelectedDate >= calendarFecha.SelectedDate) {
                            Modelo.Cliente cliente = new Modelo.Cliente();
                            cliente.ID_USUARIO = MiSesion.ID_USUARIO;

                            //Si el ID de Cliente es encontrado, pasar al siguiente paso
                            Service1 s = new Service1();
                            XmlSerializer sr = new XmlSerializer(typeof(Modelo.Cliente));
                            StringWriter writer = new StringWriter();
                            sr.Serialize(writer,cliente);
                            writer.Close();

                            Modelo.Cliente cliente2 = s.buscarIDC(writer.ToString());

                            if (cliente2 != null) {
                                Modelo.OrdenCompra orden = new Modelo.OrdenCompra();
                                orden.CANTIDAD_HUESPEDES = MiSesionO.Count;
                                orden.FECHA_LLEGADA = calendarFecha.SelectedDate;
                                orden.FECHA_SALIDA = calendarSalida.SelectedDate;
                                orden.RUT_CLIENTE = cliente2.RUT_CLIENTE;
                                orden.ESTADO_ORDEN = "Pendiente";

                                XmlSerializer sr2 = new XmlSerializer(typeof(Modelo.OrdenCompra));
                                StringWriter writer2 = new StringWriter();
                                sr2.Serialize(writer2,orden);

                                if (s.AgregarReserva(writer2.ToString())) {
                                    bool v_exito = true;

                                    foreach (DetalleOrden o in MiSesionO) {
                                        Modelo.DetalleOrden detalle = new Modelo.DetalleOrden();
                                        detalle.RUT_HUESPED = o.RUT_HUESPED;
                                        detalle.ID_PENSION = o.ID_PENSION;
                                        detalle.ID_CATEGORIA_HABITACION = o.ID_CATEGORIA_HABITACION;
                                        detalle.ESTADO = "Pendiente";

                                        XmlSerializer sr3 = new XmlSerializer(typeof(Modelo.DetalleOrden));
                                        StringWriter writer3 = new StringWriter();
                                        sr3.Serialize(writer3,detalle);

                                        if (!s.AgregarDetalleReserva(writer3.ToString())) {
                                            v_exito = false;
                                        }
                                    }
                                    if (v_exito) {
                                        Response.Write("<script language='javascript'>window.alert('Reserva Realizada. Espere que su solicitud sea aceptada');window.location='../Cliente/WebVerHistorial.aspx';</script>");
                                    }
                                    else {
                                        alerta_exito.Visible = false;
                                        error.Text = "No se ha podido realizar la reserva";
                                        alerta.Visible = true;
                                    }
                                }
                                else {
                                    alerta_exito.Visible = false;
                                    error.Text = "No se ha podido realizar la reserva";
                                    alerta.Visible = true;
                                }
                            }
                            else {
                                alerta_exito.Visible = false;
                                error.Text = "no se pudo encontrar al Cliente";
                                alerta.Visible = true;
                            }
                        }
                        else {
                            alerta_exito.Visible = false;
                            error.Text = "debe ingresar una fecha válida, igual a la fecha actual o superior";
                            alerta.Visible = true;
                        }
                    }
                    else {
                        alerta_exito.Visible = false;
                        error.Text = "debe ingresar a los huéspedes para poder realizar una Reserva";
                        alerta.Visible = true;
                    }
                }else {
                    if (ddlClientes.SelectedIndex != 0){
                        if (MiSesionO.Count > 0) {
                            if (calendarFecha.SelectedDate >= DateTime.Today && calendarSalida.SelectedDate >= calendarFecha.SelectedDate) {
                                Modelo.Cliente cliente = new Modelo.Cliente();
                                cliente.RUT_CLIENTE = int.Parse(ddlClientes.SelectedValue.ToString());
                                Service1 s = new Service1();

                                cliente.BuscarCliente();

                                if (cliente != null) {
                                    Modelo.OrdenCompra orden = new Modelo.OrdenCompra();
                                    orden.CANTIDAD_HUESPEDES = MiSesionO.Count;
                                    orden.FECHA_LLEGADA = calendarFecha.SelectedDate;
                                    orden.FECHA_SALIDA = calendarSalida.SelectedDate;
                                    orden.RUT_CLIENTE = cliente.RUT_CLIENTE;
                                    orden.ESTADO_ORDEN = Estado_Orden.Pendiente.ToString();

                                    XmlSerializer sr2 = new XmlSerializer(typeof(Modelo.OrdenCompra));
                                    StringWriter writer2 = new StringWriter();
                                    sr2.Serialize(writer2,orden);

                                    if (s.AgregarReserva(writer2.ToString())) {
                                        bool v_exito = true;

                                        foreach (DetalleOrden o in MiSesionO) {
                                            Modelo.DetalleOrden detalle = new Modelo.DetalleOrden();
                                            detalle.RUT_HUESPED = o.RUT_HUESPED;
                                            detalle.ID_PENSION = o.ID_PENSION;
                                            detalle.ID_CATEGORIA_HABITACION = o.ID_CATEGORIA_HABITACION;
                                            detalle.ESTADO = "Pendiente";

                                            XmlSerializer sr3 = new XmlSerializer(typeof(Modelo.DetalleOrden));
                                            StringWriter writer3 = new StringWriter();
                                            sr3.Serialize(writer3,detalle);

                                            if (!s.AgregarDetalleReserva(writer3.ToString())) {
                                                v_exito = false;
                                            }
                                        }
                                        if (v_exito) {
                                            Response.Write("<script language='javascript'>window.alert('Reserva Realizada. Espere que su solicitud sea aceptada');window.location='../Cliente/WebVerHistorial.aspx';</script>");
                                        }
                                        else {
                                            alerta_exito.Visible = false;
                                            error.Text = "No se ha podido realizar la reserva";
                                            alerta.Visible = true;
                                        }
                                    }
                                    else {
                                        alerta_exito.Visible = false;
                                        error.Text = "No se ha podido realizar la reserva";
                                        alerta.Visible = true;
                                    }
                                }
                                else {
                                    alerta_exito.Visible = false;
                                    error.Text = "no se pudo encontrar al Cliente";
                                    alerta.Visible = true;
                                }
                            }
                            else {
                                alerta_exito.Visible = false;
                                error.Text = "debe ingresar una fecha válida, igual a la fecha actual o superior";
                                alerta.Visible = true;
                            }
                        }
                        else {
                            alerta_exito.Visible = false;
                            error.Text = "debe ingresar a los huéspedes para poder realizar una Reserva";
                            alerta.Visible = true;
                        }
                    }
                    else {
                        alerta_exito.Visible = false;
                        error.Text = "Debe seleccionar una empresa valida";
                        alerta.Visible = true;
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

        public List<DetalleOrden> MiSesionO
        {
            get
            {
                if (Session["DetalleOrden"] == null)
                {
                    Session["DetalleOrden"] = new List<DetalleOrden>();
                }
                return (List<DetalleOrden>)Session["DetalleOrden"];
            }
            set
            {
                Session["DetalleOrden"] = value;
            }
        }

        protected void btnVer_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#exampleModal2').modal();", true);
        }

        private void CargarGridView(List<DetalleOrden> detalle) {
            Huesped huesped;
            Pension pension;
            CategoriaHabitacion cat;

            //Creacion DataTable
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] {
                new DataColumn("RUT_HUESPED", typeof(string)),
                new DataColumn("NOMBRE_HUESPED", typeof(string)),
                new DataColumn("PENSION",typeof(string)),
                new DataColumn("CATEGORIA_HAB",typeof(string)),
                new DataColumn("RUT_ELIMINAR",typeof(int))
            });

            //Carga de datos en DataTable
            foreach (Modelo.DetalleOrden d in detalle) {
                huesped = new Huesped();
                huesped.RUT_HUESPED = d.RUT_HUESPED;
                huesped.BuscarHuesped();

                pension = new Pension();
                pension.ID_PENSION = d.ID_PENSION;
                pension.BuscarPension();

                cat = new CategoriaHabitacion();
                cat.ID_CATEGORIA_HABITACION = d.ID_CATEGORIA_HABITACION;
                cat.BuscarCategoria();

                dt.Rows.Add(huesped.RUT_HUESPED + "-" + huesped.DV_HUESPED,huesped.PNOMBRE_HUESPED + " " + huesped.APP_PATERNO_HUESPED + " " + huesped.APP_MATERNO_HUESPED,pension.NOMBRE_PENSION,cat.NOMBRE_CATEGORIA,d.RUT_HUESPED);
            }

            //Carga de GriedView
            gvDetalle.DataSource = dt;
            gvDetalle.DataBind();
        }

        private void CargarDDL() {
            Service1 service = new Service1();
            Modelo.Cliente cliente = new Modelo.Cliente();
            cliente.RUT_CLIENTE = int.Parse(ddlClientes.SelectedValue.ToString());
            
            List<Huesped> coleccionHuesped = HuespedCollection.ListaHuesped(cliente.RUT_CLIENTE);

            if (coleccionHuesped.Count == 0) {
                Response.Write("<script language='javascript'>window.alert('Para hacer una Reserva debe registrar huéspedes primero');window.location='../Cliente/WebAgregarPasajeros.aspx';</script>");
            }
            
            List<CategoriaHabitacion> coleccionCategoria = CategoriaHabitacionCollection.ListarCategorias();
            
            List<Pension> coleccionMinuta = MinutaCollection.ListarMinutas();
            

            ddlRut.DataSource = coleccionHuesped;
            ddlRut.DataValueField = "RUT_HUESPED";
            ddlRut.DataTextField = "RutYNombre";
            ddlRut.DataBind();
            ddlRut.Items.Insert(0,new ListItem("Seleccione Huesped...","0"));

            ddlCategoria.DataSource = coleccionCategoria;
            ddlCategoria.DataTextField = "NOMBRE_CATEGORIA";
            ddlCategoria.DataValueField = "ID_CATEGORIA_HABITACION";
            ddlCategoria.DataBind();
            ddlCategoria.Items.Insert(0,new ListItem("Seleccione Categoria de Habitación...","0"));

            ddlPension.DataSource = coleccionMinuta;
            ddlPension.DataTextField = "NombreYPrecio";
            ddlPension.DataValueField = "ID_PENSION";
            ddlPension.DataBind();
            ddlPension.Items.Insert(0,new ListItem("Seleccione Pensión...","0"));

        }

        protected void ddlClientes_SelectedIndexChanged(object sender,EventArgs e) {
            CargarDDL();
            ddlCategoria.Enabled = true;
            ddlPension.Enabled = true;
            ddlRut.Enabled = true;
        }
    }
}