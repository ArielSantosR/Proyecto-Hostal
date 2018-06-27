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
            txtPrecio.Attributes.Add("readonly", "Readonly");

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
                            Response.Write("<script language='javascript'>window.alert('Para hacer una Reserva debe registrar huéspedes primero');window.location='../Cliente/WebAgregarPasajeros.aspx';</script>");
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
                            ddlCategoria.DataTextField = "NombreYPrecio";
                            ddlCategoria.DataValueField = "ID_CATEGORIA_HABITACION";
                            ddlCategoria.DataBind();
                            ddlCategoria.Items.Insert(0,new ListItem("Seleccione Categoria de Habitación...","0"));

                            ddlTipo.DataSource = TipoHabitacionCollection.ListarTipos();
                            ddlTipo.DataTextField = "NombreYPrecio";
                            ddlTipo.DataValueField = "ID_TIPO_HABITACION";
                            ddlTipo.DataBind();
                            ddlTipo.Items.Insert(0,new ListItem("Seleccione Tipo de Habitación...","0"));

                            ddlPension.DataSource = coleccionMinuta;
                            ddlPension.DataTextField = "NombreYPrecio";
                            ddlPension.DataValueField = "ID_PENSION";
                            ddlPension.DataBind();
                            ddlPension.Items.Insert(0,new ListItem("Seleccione Pensión...","0"));

                            MiSesionO = null;
                            btnVer.Enabled = false;
                            btnReservar.Enabled = false;

                            txtPrecio.Text = "0";
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
                            ddlTipo.Enabled = false;

                            ddlClientes.DataSource = ClienteCollection.ListaClientes();
                            ddlClientes.DataTextField = "NOMBRE_CLIENTE";
                            ddlClientes.DataValueField = "RUT_CLIENTE";
                            ddlClientes.DataBind();
                            ddlClientes.Items.Insert(0,new ListItem("Seleccione Empresa...","0"));
                            ddlRut.Items.Insert(0, new ListItem("Seleccione Huesped...", "0"));
                            ddlCategoria.Items.Insert(0,new ListItem("Seleccione Categoria de Habitación...","0"));
                            ddlPension.Items.Insert(0,new ListItem("Seleccione Pensión...","0"));
                            ddlTipo.Items.Insert(0,new ListItem("Seleccione Tipo de Habitación...","0"));

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
                if (ddlRut.SelectedIndex > 0 && ddlCategoria.SelectedIndex > 0 && ddlPension.SelectedIndex > 0)
                {
                    Modelo.DetalleOrden detalle = new Modelo.DetalleOrden();
                    detalle.RUT_HUESPED = int.Parse(ddlRut.SelectedValue);
                    detalle.ID_PENSION = short.Parse(ddlPension.SelectedValue);
                    detalle.ID_CATEGORIA_HABITACION = short.Parse(ddlCategoria.SelectedValue);
                    detalle.ID_TIPO_HABITACION = short.Parse(ddlTipo.SelectedValue);

                    CategoriaHabitacion cat = new CategoriaHabitacion();
                    cat.ID_CATEGORIA_HABITACION = detalle.ID_CATEGORIA_HABITACION;
                    cat.BuscarCategoria();

                    TipoHabitacion tipo = new TipoHabitacion();
                    tipo.ID_TIPO_HABITACION = detalle.ID_TIPO_HABITACION;
                    tipo.BuscarTipo();

                    Minuta min = new Minuta();
                    min.ID_PENSION = detalle.ID_PENSION;
                    min.BuscarMinuta();

                    detalle.VALOR_HABITACION = tipo.PRECIO_TIPO + cat.PRECIO_CATEGORIA;
                    detalle.VALOR_MINUTA = min.VALOR_PENSION;
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

                    #region Precio Total
                    Service1 s = new Service1();
                    XmlSerializer ser = new XmlSerializer(typeof(Modelo.MinutaCollection));
                    string minuta = s.ListarMinuta();
                    StringReader reader = new StringReader(minuta);
                    MinutaCollection coleccionMinuta = (MinutaCollection)ser.Deserialize(reader);

                    XmlSerializer ser2 = new XmlSerializer(typeof(Modelo.CategoriaHabitacionCollection));
                    string categoria = s.ListarCategoriaHabitacion();
                    StringReader reader2 = new StringReader(categoria);
                    CategoriaHabitacionCollection coleccionCategoria = (CategoriaHabitacionCollection)ser2.Deserialize(reader2);

                    List<TipoHabitacion> coleccionTipos = TipoHabitacionCollection.ListarTipos();

                    if (MiSesionO.Count > 0)
                    {
                        int precio = 0;

                        foreach (DetalleOrden o in MiSesionO)
                        {
                            List<Modelo.Minuta> minutas = (from consulta in coleccionMinuta
                                                           where consulta.ID_PENSION == o.ID_PENSION
                                                           select consulta).ToList();

                            List<Modelo.CategoriaHabitacion> categorias = (from consulta in coleccionCategoria
                                                                           where consulta.ID_CATEGORIA_HABITACION == o.ID_CATEGORIA_HABITACION
                                                                           select consulta).ToList();

                            List<Modelo.TipoHabitacion> tipos = (from consulta in coleccionTipos
                                                                 where consulta.ID_TIPO_HABITACION == o.ID_TIPO_HABITACION
                                                                 select consulta).ToList();

                            precio = precio + minutas[0].VALOR_PENSION + categorias[0].PRECIO_CATEGORIA + tipos[0].PRECIO_TIPO;
                        }

                        txtPrecio.Text = precio + "";
                    }
                    #endregion
                }
                else
                {
                    alerta_exito.Visible = false;
                    error.Text = "Para crear una reserva debe seleccionar todos los campos";
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
                int RUT_HUESPED = int.Parse(btn.CommandArgument);

                foreach (DetalleOrden o in MiSesionO.ToList())
                {
                    if (o.RUT_HUESPED == RUT_HUESPED)
                    {
                        MiSesionO.Remove(o);
                    }
                }
                CargarGridView(MiSesionO);

                #region Precio Total
                Service1 s = new Service1();
                XmlSerializer ser = new XmlSerializer(typeof(Modelo.MinutaCollection));
                string minuta = s.ListarMinuta();
                StringReader reader = new StringReader(minuta);
                MinutaCollection coleccionMinuta = (MinutaCollection)ser.Deserialize(reader);

                XmlSerializer ser2 = new XmlSerializer(typeof(Modelo.CategoriaHabitacionCollection));
                string categoria = s.ListarCategoriaHabitacion();
                StringReader reader2 = new StringReader(categoria);
                CategoriaHabitacionCollection coleccionCategoria = (CategoriaHabitacionCollection)ser2.Deserialize(reader2);

                List<TipoHabitacion> coleccionTipos = TipoHabitacionCollection.ListarTipos();

                if (MiSesionO.Count > 0)
                {
                    int precio = 0;

                    foreach (DetalleOrden o in MiSesionO)
                    {
                        List<Modelo.Minuta> minutas = (from consulta in coleccionMinuta
                                                       where consulta.ID_PENSION == o.ID_PENSION
                                                       select consulta).ToList();

                        List<Modelo.CategoriaHabitacion> categorias = (from consulta in coleccionCategoria
                                                                       where consulta.ID_CATEGORIA_HABITACION == o.ID_CATEGORIA_HABITACION
                                                                       select consulta).ToList();
                        List<Modelo.TipoHabitacion> tipos = (from consulta in coleccionTipos
                                                             where consulta.ID_TIPO_HABITACION == o.ID_TIPO_HABITACION
                                                             select consulta).ToList();

                        precio = precio + minutas[0].VALOR_PENSION + categorias[0].PRECIO_CATEGORIA + tipos[0].PRECIO_TIPO;
                    }

                    txtPrecio.Text = precio + "";
                }
                #endregion

                if (MiSesionO.Count == 0)
                {
                    btnVer.Enabled = false;
                    btnReservar.Enabled = false;
                    txtPrecio.Text = "0";
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
            try {
                DateTime fechaLlegada = DateTime.Parse(txtFechaInicio.Text);
                DateTime fechaSalida = DateTime.Parse(txtFechaFinal.Text);
                if (MiSesion.TIPO_USUARIO.Equals(Tipo_Usuario.Cliente.ToString())) {
                    if (MiSesionO.Count > 0) {
                        if (DateTime.Compare(fechaLlegada,DateTime.Today) >= 0 && DateTime.Compare(fechaSalida,fechaLlegada) >= 0) {
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
                                orden.FECHA_LLEGADA = fechaLlegada;
                                orden.FECHA_SALIDA = fechaSalida;
                                orden.RUT_CLIENTE = cliente2.RUT_CLIENTE;
                                orden.ESTADO_ORDEN = "Pendiente";

                                #region Precio Total con Días

                                var date = orden.FECHA_SALIDA - orden.FECHA_LLEGADA;
                                int dias = date.Days;

                                XmlSerializer ser = new XmlSerializer(typeof(Modelo.MinutaCollection));
                                string minuta = s.ListarMinuta();
                                StringReader reader = new StringReader(minuta);
                                MinutaCollection coleccionMinuta = (MinutaCollection)ser.Deserialize(reader);

                                XmlSerializer ser2 = new XmlSerializer(typeof(Modelo.CategoriaHabitacionCollection));
                                string categoria = s.ListarCategoriaHabitacion();
                                StringReader reader2 = new StringReader(categoria);
                                CategoriaHabitacionCollection coleccionCategoria = (CategoriaHabitacionCollection)ser2.Deserialize(reader2);

                                List<TipoHabitacion> coleccionTipos = TipoHabitacionCollection.ListarTipos();

                                int precio = 0;

                                if (MiSesionO.Count > 0) {


                                    foreach (DetalleOrden o in MiSesionO) {
                                        List<Modelo.Minuta> minutas = (from consulta in coleccionMinuta
                                                                       where consulta.ID_PENSION == o.ID_PENSION
                                                                       select consulta).ToList();

                                        List<Modelo.CategoriaHabitacion> categorias = (from consulta in coleccionCategoria
                                                                                       where consulta.ID_CATEGORIA_HABITACION == o.ID_CATEGORIA_HABITACION
                                                                                       select consulta).ToList();
                                        List<Modelo.TipoHabitacion> tipos = (from consulta in coleccionTipos
                                                                             where consulta.ID_TIPO_HABITACION == o.ID_TIPO_HABITACION
                                                                             select consulta).ToList();


                                        precio = precio + ((minutas[0].VALOR_PENSION + categorias[0].PRECIO_CATEGORIA + tipos[0].PRECIO_TIPO) * dias);
                                    }

                                    txtPrecio.Text = precio + "";
                                }
                                #endregion

                                orden.MONTO_TOTAL = precio;

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
                                        detalle.ID_TIPO_HABITACION = o.ID_TIPO_HABITACION;
                                        detalle.VALOR_HABITACION = o.VALOR_HABITACION;
                                        detalle.VALOR_MINUTA = o.VALOR_MINUTA;
                                        detalle.ESTADO = "Pendiente";
                                        

                                        XmlSerializer sr3 = new XmlSerializer(typeof(Modelo.DetalleOrden));
                                        StringWriter writer3 = new StringWriter();
                                        sr3.Serialize(writer3,detalle);

                                        if (!s.AgregarDetalleReserva(writer3.ToString())) {
                                            v_exito = false;
                                        }
                                    }
                                    if (v_exito) {
                                        Response.Write("<script language='javascript'>window.alert('Reserva Realizada, El Precio Total de su estadía será de: $" + precio + ". Espere que su solicitud sea aceptada');window.location='../Cliente/WebVerHistorial.aspx';</script>");
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
                            if (DateTime.Compare(fechaLlegada,DateTime.Today) >= 0 && DateTime.Compare(fechaSalida,fechaLlegada) >= 0) {
                                Modelo.Cliente cliente = new Modelo.Cliente();
                                cliente.RUT_CLIENTE = int.Parse(ddlClientes.SelectedValue.ToString());
                                Service1 s = new Service1();

                                cliente.BuscarCliente();

                                if (cliente != null) {
                                    Modelo.OrdenCompra orden = new Modelo.OrdenCompra();
                                    orden.CANTIDAD_HUESPEDES = MiSesionO.Count;
                                    orden.FECHA_LLEGADA = fechaLlegada;
                                    orden.FECHA_SALIDA = fechaSalida;
                                    orden.RUT_CLIENTE = cliente.RUT_CLIENTE;
                                    orden.ESTADO_ORDEN = Estado_Orden.Pendiente.ToString();

                                    #region Precio Total con Días

                                    var date = orden.FECHA_SALIDA - orden.FECHA_LLEGADA;
                                    int dias = date.Days;

                                    XmlSerializer ser = new XmlSerializer(typeof(Modelo.MinutaCollection));
                                    string minuta = s.ListarMinuta();
                                    StringReader reader = new StringReader(minuta);
                                    MinutaCollection coleccionMinuta = (MinutaCollection)ser.Deserialize(reader);

                                    XmlSerializer ser2 = new XmlSerializer(typeof(Modelo.CategoriaHabitacionCollection));
                                    string categoria = s.ListarCategoriaHabitacion();
                                    StringReader reader2 = new StringReader(categoria);
                                    CategoriaHabitacionCollection coleccionCategoria = (CategoriaHabitacionCollection)ser2.Deserialize(reader2);

                                    List<TipoHabitacion> coleccionTipos = TipoHabitacionCollection.ListarTipos();

                                    int precio = 0;

                                    if (MiSesionO.Count > 0) {


                                        foreach (DetalleOrden o in MiSesionO) {
                                            List<Modelo.Minuta> minutas = (from consulta in coleccionMinuta
                                                                           where consulta.ID_PENSION == o.ID_PENSION
                                                                           select consulta).ToList();

                                            List<Modelo.CategoriaHabitacion> categorias = (from consulta in coleccionCategoria
                                                                                           where consulta.ID_CATEGORIA_HABITACION == o.ID_CATEGORIA_HABITACION
                                                                                           select consulta).ToList();
                                            List<Modelo.TipoHabitacion> tipos = (from consulta in coleccionTipos
                                                                                 where consulta.ID_TIPO_HABITACION == o.ID_TIPO_HABITACION
                                                                                 select consulta).ToList();

                                            precio = precio + ((minutas[0].VALOR_PENSION + categorias[0].PRECIO_CATEGORIA + tipos[0].PRECIO_TIPO) * dias);
                                        }

                                        txtPrecio.Text = precio + "";
                                    }
                                    #endregion

                                    orden.MONTO_TOTAL = precio;

                                    XmlSerializer sr2 = new XmlSerializer(typeof(Modelo.OrdenCompra));
                                    StringWriter writer2 = new StringWriter();
                                    sr2.Serialize(writer2,orden);

                                    if (s.AgregarReserva(writer2.ToString()))
                                    {
                                        bool v_exito = true;

                                        foreach (DetalleOrden o in MiSesionO)
                                        {
                                            Modelo.DetalleOrden detalle = new Modelo.DetalleOrden();
                                            detalle.RUT_HUESPED = o.RUT_HUESPED;
                                            detalle.ID_PENSION = o.ID_PENSION;
                                            detalle.ID_CATEGORIA_HABITACION = o.ID_CATEGORIA_HABITACION;
                                            detalle.ID_TIPO_HABITACION = o.ID_TIPO_HABITACION;
                                            detalle.VALOR_HABITACION = o.VALOR_HABITACION;
                                            detalle.VALOR_MINUTA = o.VALOR_MINUTA;
                                            detalle.ESTADO = "Pendiente";

                                            XmlSerializer sr3 = new XmlSerializer(typeof(Modelo.DetalleOrden));
                                            StringWriter writer3 = new StringWriter();
                                            sr3.Serialize(writer3, detalle);

                                            if (!s.AgregarDetalleReserva(writer3.ToString()))
                                            {
                                                v_exito = false;
                                            }
                                        }
                                        if (v_exito)
                                        {
                                            Response.Write("<script language='javascript'>window.alert('Reserva Realizada, El Precio Total de su estadía será de: $" + precio + ". Espere que su solicitud sea aceptada');window.location='../Cliente/WebVerHistorial.aspx';</script>");
                                        }
                                        else
                                        {
                                            alerta_exito.Visible = false;
                                            error.Text = "No se ha podido realizar la reserva";
                                            alerta.Visible = true;
                                        }
                                    }
                                    else
                                    {
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
            TipoHabitacion tipo;

            //Creacion DataTable
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] {
                new DataColumn("RUT_HUESPED", typeof(string)),
                new DataColumn("NOMBRE_HUESPED", typeof(string)),
                new DataColumn("PENSION",typeof(string)),
                new DataColumn("VALOR_PENSION",typeof(string)),
                new DataColumn("CATEGORIA_HAB",typeof(string)),
                new DataColumn("VALOR_HAB",typeof(string)),
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

                tipo = new TipoHabitacion();
                tipo.ID_TIPO_HABITACION = d.ID_TIPO_HABITACION;
                tipo.BuscarTipo();

                dt.Rows.Add(huesped.RUT_HUESPED + "-" + huesped.DV_HUESPED,huesped.PNOMBRE_HUESPED + " " + huesped.APP_PATERNO_HUESPED + " " + huesped.APP_MATERNO_HUESPED,pension.NOMBRE_PENSION,"$" + pension.VALOR_PENSION,tipo.NOMBRE_TIPO_HABITACION + "-" + cat.NOMBRE_CATEGORIA,"$" + d.VALOR_HABITACION,d.RUT_HUESPED);
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
            ddlCategoria.DataTextField = "NombreYPrecio";
            ddlCategoria.DataValueField = "ID_CATEGORIA_HABITACION";
            ddlCategoria.DataBind();
            ddlCategoria.Items.Insert(0,new ListItem("Seleccione Categoria de Habitación...","0"));

            ddlPension.DataSource = coleccionMinuta;
            ddlPension.DataTextField = "NombreYPrecio";
            ddlPension.DataValueField = "ID_PENSION";
            ddlPension.DataBind();
            ddlPension.Items.Insert(0,new ListItem("Seleccione Pensión...","0"));

            ddlTipo.DataSource = TipoHabitacionCollection.ListarTipos();
            ddlTipo.DataTextField = "NombreYPrecio";
            ddlTipo.DataValueField = "ID_TIPO_HABITACION";
            ddlTipo.DataBind();
            ddlTipo.Items.Insert(0,new ListItem("Seleccione Tipo de Habitación...","0"));
        }

        protected void ddlClientes_SelectedIndexChanged(object sender,EventArgs e) {
            CargarDDL();
            ddlCategoria.Enabled = true;
            ddlPension.Enabled = true;
            ddlRut.Enabled = true;
            ddlTipo.Enabled = true;
        }
    }
}