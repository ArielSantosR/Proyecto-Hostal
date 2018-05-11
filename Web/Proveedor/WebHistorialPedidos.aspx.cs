﻿using Modelo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;
using WcfNegocio;

namespace Web.Proveedor
{
    public partial class WebDespachados : System.Web.UI.Page
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
                    else if (MiSesion.TIPO_USUARIO.Equals("Proveedor") &&
                    MiSesion.ESTADO.Equals("Habilitado"))
                    {
                        MasterPageFile = "~/Proveedor/ProveedorM.Master";
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

            Modelo.Proveedor proveedor = new Modelo.Proveedor();

            if (MiSesion != null)
            {
                if (MiSesion.TIPO_USUARIO.Equals("Proveedor"))
                {
                    proveedor.ID_USUARIO = MiSesion.ID_USUARIO;

                    //Si el ID de empleado es encontrado, pasar al siguiente paso
                    Service1 s = new Service1();
                    XmlSerializer sr = new XmlSerializer(typeof(Modelo.Proveedor));
                    StringWriter writer = new StringWriter();
                    sr.Serialize(writer, proveedor);
                    writer.Close();

                    Modelo.Proveedor proveedor2 = s.buscarIDP(writer.ToString());
                    XmlSerializer sr2 = new XmlSerializer(typeof(Modelo.Proveedor));
                    StringWriter writer2 = new StringWriter();
                    sr2.Serialize(writer2, proveedor2);
                    writer2.Close();

                    string datos = service.ListarHistorialProveedor(writer2.ToString());
                    XmlSerializer ser3 = new XmlSerializer(typeof(Modelo.PedidoCollection));
                    StringReader reader = new StringReader(datos);

                    Modelo.PedidoCollection listaPedido = (Modelo.PedidoCollection)ser3.Deserialize(reader);
                    reader.Close();
                    gvPedidoListo.DataSource = listaPedido;
                    gvPedidoListo.DataBind();


                    string datos2 = service.ListarPedidoDespacho(writer2.ToString());
                    XmlSerializer ser4 = new XmlSerializer(typeof(Modelo.PedidoCollection));
                    StringReader reader2 = new StringReader(datos2);

                    Modelo.PedidoCollection listaPedido2 = (Modelo.PedidoCollection)ser4.Deserialize(reader2);
                    reader.Close();
                    gvPedidoDespacho.DataSource = listaPedido2;
                    gvPedidoDespacho.DataBind();
                }
                //Else si es el administrador deberia decir algo supongo

            }
            else
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

        protected void btnInfo_Click(object sender, EventArgs e)
        {
            //Lee los valores del LinkButton, primero usa la clase LinkButton para 
            //Transformar los datos de Sender, luego los lee y los asigna a una variable
            LinkButton btn = (LinkButton)(sender);
            short numero_pedido = short.Parse(btn.CommandArgument);

            Pedido pedido = new Pedido();
            pedido.NUMERO_PEDIDO = numero_pedido;

            Service1 s = new Service1();
            XmlSerializer sr = new XmlSerializer(typeof(Modelo.Pedido));
            StringWriter writer = new StringWriter();
            sr.Serialize(writer, pedido);

            if (s.ObtenerPedido(writer.ToString()) == null)
            {
                alerta_exito.Visible = false;
                error.Text = "No se ha encontrado el Pedido";
                alerta.Visible = true;
            }
            else
            {
                Pedido pedido2 = s.ObtenerPedido(writer.ToString());
                pedido2.ESTADO_DESPACHO = "Despachado";

                XmlSerializer sr2 = new XmlSerializer(typeof(Modelo.Pedido));
                StringWriter writer2 = new StringWriter();
                sr2.Serialize(writer2, pedido2);

                if (s.EditarEstadoPedido(writer2.ToString()))
                {
                    exito.Text = "Despacho Aceptado. Por favor realice el despacho con la orden correspondiente.";
                    alerta_exito.Visible = true;
                    alerta.Visible = false;

                    Modelo.Proveedor proveedor = new Modelo.Proveedor();
                    proveedor.ID_USUARIO = MiSesion.ID_USUARIO;

                    XmlSerializer sr3 = new XmlSerializer(typeof(Modelo.Proveedor));
                    StringWriter writer3 = new StringWriter();
                    sr3.Serialize(writer3, proveedor);
                    writer.Close();

                    Modelo.Proveedor proveedor2 = s.buscarIDP(writer3.ToString());
                    XmlSerializer sr4 = new XmlSerializer(typeof(Modelo.Proveedor));
                    StringWriter writer4 = new StringWriter();
                    sr4.Serialize(writer4, proveedor2);
                    writer4.Close();

                    string datos = s.ListarPedidoDespacho(writer4.ToString());
                    XmlSerializer ser5 = new XmlSerializer(typeof(Modelo.PedidoCollection));
                    StringReader reader5 = new StringReader(datos);

                    Modelo.PedidoCollection listaPedido = (Modelo.PedidoCollection)ser5.Deserialize(reader5);
                    reader5.Close();
                    gvPedidoDespacho.DataSource = listaPedido;
                    gvPedidoDespacho.DataBind();
                }
                else
                {
                    alerta_exito.Visible = false;
                    error.Text = "La modificación de Estado Pedido ha fallado";
                    alerta.Visible = true;
                }
            }
        } 
    }
}