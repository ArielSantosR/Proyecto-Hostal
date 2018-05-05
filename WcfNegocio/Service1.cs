﻿using Datos;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Web;
using System.Web.Hosting;

namespace WcfNegocio
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código y en el archivo de configuración a la vez.
    public class Service1 : IService1
    {
        ServicioLogin servicioLogin = new ServicioLogin();
        /// <summary>
        /// Login de usuario.
        /// </summary>
        /// <param name="user">El parámetro es un string que contiene usuario y contraseña</param>
        /// <returns bool></returns>
        //CRUD Usuario
        public bool Login(string user)
        {
            //El Xml Lee los datos pasados del login Web y procede a leerlos con reader
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Usuario));
            StringReader reader = new StringReader(user);
            Modelo.Usuario usuario = (Modelo.Usuario)ser.Deserialize(reader);

            //Agregado Datos y Modelo a las clases para saber de donde proceden
            Datos.USUARIO uDatos = new Datos.USUARIO();
            uDatos.NOMBRE_USUARIO = usuario.NOMBRE_USUARIO;
            uDatos.PASSWORD = usuario.PASSWORD;
            ServicioLogin servicio = new ServicioLogin();
            return servicio.Login(uDatos);
        }

        //Registro Usuario
        public Usuario GetUsuario(string user)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Usuario));
            StringReader reader = new StringReader(user);
            Modelo.Usuario usuario = (Modelo.Usuario)ser.Deserialize(reader);

            Datos.USUARIO uDatos = new Datos.USUARIO();
            uDatos.NOMBRE_USUARIO = usuario.NOMBRE_USUARIO;
            uDatos.PASSWORD = usuario.PASSWORD;

            ServicioUsuario serv = new ServicioUsuario();
            Datos.USUARIO uDatos2 = serv.EncontrarUsuario(uDatos);
            usuario.NOMBRE_USUARIO = uDatos2.NOMBRE_USUARIO;
            usuario.PASSWORD = uDatos2.PASSWORD;
            usuario.TIPO_USUARIO = uDatos2.TIPO_USUARIO;
            usuario.ESTADO = uDatos2.ESTADO;

            return usuario;
        }

        public bool ExisteUsuario(string user)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Usuario));
            StringReader reader = new StringReader(user);
            Modelo.Usuario usuario = (Modelo.Usuario)ser.Deserialize(reader);
            ServicioUsuario serv = new ServicioUsuario();
            Datos.USUARIO uDatos = new Datos.USUARIO();
            uDatos.NOMBRE_USUARIO = usuario.NOMBRE_USUARIO;
            if (!serv.ExisteUsuario(uDatos))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool RegistroUsuario(string user)
        {
            //Datos Usuario
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Usuario));
            StringReader reader = new StringReader(user);
            Modelo.Usuario usuario = (Modelo.Usuario)ser.Deserialize(reader);
            ServicioUsuario serv = new ServicioUsuario();
            Datos.USUARIO uDatos = new Datos.USUARIO();
            uDatos.NOMBRE_USUARIO = usuario.NOMBRE_USUARIO;
            uDatos.PASSWORD = usuario.PASSWORD;
            uDatos.TIPO_USUARIO = usuario.TIPO_USUARIO;
            uDatos.ESTADO = usuario.ESTADO;

            return serv.RegistrarUsuario(uDatos);
        }

        public bool EliminarUsuario(string user)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Usuario));
            StringReader reader = new StringReader(user);
            Modelo.Usuario usuario = (Modelo.Usuario)ser.Deserialize(reader);
            ServicioUsuario serv = new ServicioUsuario();
            Datos.USUARIO uDatos = new Datos.USUARIO();
            uDatos.ID_USUARIO = usuario.ID_USUARIO;

            return serv.RegistrarUsuario(uDatos);
        }

        public string ListarUsuarios()
        {
            ServicioUsuario servicio = new ServicioUsuario();
            List<Datos.USUARIO> usuario = servicio.ListarUsuarios();
            Modelo.UsuarioCollection listaUsuario = new Modelo.UsuarioCollection();

            foreach (Datos.USUARIO user in usuario)
            {
                Modelo.Usuario u = new Modelo.Usuario();
                u.ID_USUARIO = user.ID_USUARIO;
                u.NOMBRE_USUARIO = user.NOMBRE_USUARIO;
                u.PASSWORD = user.PASSWORD;
                u.TIPO_USUARIO = user.TIPO_USUARIO;
                u.ESTADO = user.ESTADO;

                listaUsuario.Add(u);
            }

            XmlSerializer ser = new XmlSerializer(typeof(Modelo.UsuarioCollection));
            StringWriter writer = new StringWriter();
            ser.Serialize(writer, listaUsuario);
            writer.Close();
            return writer.ToString();
        }

        public bool ModificarUsuario(string user)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Usuario));
            StringReader reader = new StringReader(user);
            Modelo.Usuario u = (Modelo.Usuario)ser.Deserialize(reader);
            Datos.ServicioUsuario servicio = new Datos.ServicioUsuario();
            Datos.USUARIO usuario = new Datos.USUARIO();
            //Datos de Usuario
            usuario.ID_USUARIO = u.ID_USUARIO;
            usuario.NOMBRE_USUARIO = u.NOMBRE_USUARIO;
            usuario.PASSWORD = u.PASSWORD;
            usuario.TIPO_USUARIO = u.TIPO_USUARIO;
            usuario.ESTADO = u.ESTADO;

            if (servicio.ModificarUsuario(usuario))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //CRUD Cliente
        public bool ExisteRutC(string cliente)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Cliente));
            StringReader reader = new StringReader(cliente);
            Modelo.Cliente c = (Modelo.Cliente)ser.Deserialize(reader);
            ServicioCliente serv = new ServicioCliente();
            Datos.CLIENTE cDatos = new Datos.CLIENTE();
            cDatos.RUT_CLIENTE = c.RUT_CLIENTE;

            if (!serv.ExisteRut(cDatos))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool RegistroCliente(string cliente)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Cliente));
            StringReader reader = new StringReader(cliente);
            Modelo.Cliente c = (Modelo.Cliente)ser.Deserialize(reader);
            ServicioCliente servicio = new ServicioCliente();

            Datos.CLIENTE cDatos = new Datos.CLIENTE();
            //Datos Cliente
            cDatos.RUT_CLIENTE = c.RUT_CLIENTE;
            cDatos.DV_CLIENTE = c.DV_CLIENTE;
            cDatos.DIRECCION_CLIENTE = c.DIRECCION_CLIENTE;
            cDatos.CORREO_CLIENTE = c.CORREO_CLIENTE;
            cDatos.TELEFONO_CLIENTE = c.TELEFONO_CLIENTE;
            cDatos.ID_COMUNA = c.ID_COMUNA;
            cDatos.ID_USUARIO = 0;
            cDatos.NOMBRE_CLIENTE = c.NOMBRE_CLIENTE;

            return servicio.AgregarCliente(cDatos);
        }



        //CRUD Empleado
        public bool ExisteRutE(string empleado)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Empleado));
            StringReader reader = new StringReader(empleado);
            Modelo.Empleado e = (Modelo.Empleado)ser.Deserialize(reader);
            ServicioEmpleado serv = new ServicioEmpleado();
            Datos.EMPLEADO eDatos = new Datos.EMPLEADO();
            eDatos.RUT_EMPLEADO = e.RUT_EMPLEADO;

            if (!serv.ExisteRut(eDatos))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool RegistroEmpleado(string empleado)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Empleado));
            StringReader reader = new StringReader(empleado);
            Modelo.Empleado e = (Modelo.Empleado)ser.Deserialize(reader);
            ServicioEmpleado servicio = new ServicioEmpleado();

            Datos.EMPLEADO eDatos = new Datos.EMPLEADO();
            //Datos Empleado
            eDatos.RUT_EMPLEADO = e.RUT_EMPLEADO;
            eDatos.DV_EMPLEADO = e.DV_EMPLEADO;
            eDatos.PNOMBRE_EMPLEADO = e.PNOMBRE_EMPLEADO;
            eDatos.SNOMBRE_EMPLEADO = e.SNOMBRE_EMPLEADO;
            eDatos.APP_PATERNO_EMPLEADO = e.APP_PATERNO_EMPLEADO;
            eDatos.APP_MATERNO_EMPLEADO = e.APP_MATERNO_EMPLEADO;
            eDatos.ID_USUARIO = 0;

            return servicio.AgregarEmpleado(eDatos);
        }

        //CRUD Proveedor
        public bool ExisteRutP(string proveedor)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Proveedor));
            StringReader reader = new StringReader(proveedor);
            Modelo.Proveedor p = (Modelo.Proveedor)ser.Deserialize(reader);
            ServicioProveedor serv = new ServicioProveedor();
            Datos.PROVEEDOR pDatos = new Datos.PROVEEDOR();
            pDatos.RUT_PROVEEDOR = p.RUT_PROVEEDOR;

            if (!serv.ExisteRut(pDatos))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool RegistroProveedor(string proveedor)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Proveedor));
            StringReader reader = new StringReader(proveedor);
            Modelo.Proveedor p = (Modelo.Proveedor)ser.Deserialize(reader);
            ServicioProveedor servicio = new ServicioProveedor();

            Datos.PROVEEDOR pDatos = new Datos.PROVEEDOR();
            //Datos Proveedor
            pDatos.RUT_PROVEEDOR = p.RUT_PROVEEDOR;
            pDatos.DV_PROVEEDOR = p.DV_PROVEEDOR;
            pDatos.PNOMBRE_PROVEEDOR = p.PNOMBRE_PROVEEDOR;
            pDatos.SNOMBRE_PROVEEDOR = p.SNOMBRE_PROVEEDOR;
            pDatos.APP_PATERNO_PROVEEDOR = p.APP_PATERNO_PROVEEDOR;
            pDatos.APP_MATERNO_PROVEEDOR = p.APP_MATERNO_PROVEEDOR;
            pDatos.ID_TIPO_PROVEEDOR = p.ID_TIPO_PROVEEDOR;
            pDatos.ID_USUARIO = 0;

            return servicio.AgregarProveedor(pDatos);
        }

        public string ListarProveedor()
        {
            ServicioProveedor servicio = new ServicioProveedor();
            List<Datos.PROVEEDOR> proveedor = servicio.ListarProveedor();
            Modelo.ProveedorCollection2 listaProveedor = new Modelo.ProveedorCollection2();

            foreach (Datos.PROVEEDOR p in proveedor)
            {
                Modelo.Proveedor pModelo = new Modelo.Proveedor();
                pModelo.RUT_PROVEEDOR = p.RUT_PROVEEDOR;

                listaProveedor.Add(pModelo);
            }

            XmlSerializer ser = new XmlSerializer(typeof(Modelo.ProveedorCollection2));
            StringWriter writer = new StringWriter();
            ser.Serialize(writer, listaProveedor);
            writer.Close();
            return writer.ToString();
        }

        //Listar Datos
        public string ListarTipoProveedor()
        {
            ServicioProveedor servicio = new ServicioProveedor();
            List<Datos.TIPO_PROVEEDOR> tipo = servicio.ListarTipoProveedor();
            Modelo.TipoProveedorCollection listaTipoProveedor = new Modelo.TipoProveedorCollection();
            foreach (Datos.TIPO_PROVEEDOR t in tipo)
            {
                Modelo.TipoProveedor tModelo = new Modelo.TipoProveedor();
                tModelo.ID_TIPO_PROVEEDOR = t.ID_TIPO_PROVEEDOR;
                tModelo.NOMBRE_TIPO = t.NOMBRE_TIPO;
                listaTipoProveedor.Add(tModelo);
            }
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.TipoProveedorCollection));
            StringWriter writer = new StringWriter();
            ser.Serialize(writer, listaTipoProveedor);
            return writer.ToString();
        }

        //CRUD Habitacion
        public bool AgregarHabitacion(string habitacion)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Habitacion));
            StringReader reader = new StringReader(habitacion);
            Modelo.Habitacion h = (Modelo.Habitacion)ser.Deserialize(reader);
            ServicioHabitacion servicio = new ServicioHabitacion();

            Datos.HABITACION hDatos = new Datos.HABITACION();
            //Datos Proveedor
            hDatos.NUMERO_HABITACION = h.NUMERO_HABITACION;
            hDatos.PRECIO_HABITACION = h.PRECIO_HABITACION;
            hDatos.ESTADO_HABITACION = h.ESTADO_HABITACION;
            hDatos.ID_TIPO_HABITACION = h.ID_TIPO_HABITACION;

            return servicio.AgregarHabitacion(hDatos);
        }

        public string ListarHabitacion()
        {
            ServicioHabitacion servicio = new ServicioHabitacion();
            List<Datos.HABITACION> habitacion = servicio.listarHabitacion();
            Modelo.HabitacionCollection listaHabitacion = new Modelo.HabitacionCollection();

            foreach (Datos.HABITACION h in habitacion) {
                Modelo.Habitacion hModelo = new Modelo.Habitacion();
                hModelo.NUMERO_HABITACION = h.NUMERO_HABITACION;
                hModelo.PRECIO_HABITACION = h.PRECIO_HABITACION;
                hModelo.ESTADO_HABITACION = h.ESTADO_HABITACION;
                hModelo.ID_TIPO_HABITACION = h.ID_TIPO_HABITACION;
                hModelo.RUT_CLIENTE = h.RUT_CLIENTE;

                listaHabitacion.Add(hModelo);
            }

            XmlSerializer ser = new XmlSerializer(typeof(Modelo.HabitacionCollection));
            StringWriter writer = new StringWriter();
            ser.Serialize(writer,listaHabitacion);
            writer.Close();
            return writer.ToString();
        }

        public bool ExisteHabitacion(string habitacion)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Habitacion));
            StringReader reader = new StringReader(habitacion);
            Modelo.Habitacion h = (Modelo.Habitacion)ser.Deserialize(reader);
            ServicioHabitacion serv = new ServicioHabitacion();
            Datos.HABITACION hDatos = new Datos.HABITACION();
            hDatos.NUMERO_HABITACION = h.NUMERO_HABITACION;

            if (!serv.ExisteHabitacion(hDatos))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public Habitacion ObtenerHabitacion(string habitacion)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Habitacion));
            StringReader reader = new StringReader(habitacion);
            Modelo.Habitacion h = (Modelo.Habitacion)ser.Deserialize(reader);
            ServicioHabitacion serv = new ServicioHabitacion();
            Datos.HABITACION hDatos = new Datos.HABITACION();
            hDatos.NUMERO_HABITACION = h.NUMERO_HABITACION;

            if (!serv.ExisteHabitacion(hDatos))
            {
                return null;
            }
            else
            {
                Datos.HABITACION hDatos2 = serv.obtenerHabitacion(hDatos);
                h.NUMERO_HABITACION = hDatos2.NUMERO_HABITACION;
                h.PRECIO_HABITACION = hDatos2.PRECIO_HABITACION;
                h.ESTADO_HABITACION = hDatos2.ESTADO_HABITACION;
                h.RUT_CLIENTE = hDatos2.RUT_CLIENTE;
                h.ID_TIPO_HABITACION = hDatos2.ID_TIPO_HABITACION;

                return h;
            }
        }

        public bool ModificarHabitacion(string habitacion)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Habitacion));
            StringReader reader = new StringReader(habitacion);
            Modelo.Habitacion h = (Modelo.Habitacion)ser.Deserialize(reader);
            ServicioHabitacion serv = new ServicioHabitacion();
            Datos.HABITACION hDatos = new Datos.HABITACION();

            hDatos.NUMERO_HABITACION = h.NUMERO_HABITACION;
            hDatos.PRECIO_HABITACION = h.PRECIO_HABITACION;
            hDatos.ID_TIPO_HABITACION = h.ID_TIPO_HABITACION;
            hDatos.ESTADO_HABITACION = h.ESTADO_HABITACION;
            hDatos.RUT_CLIENTE = h.RUT_CLIENTE;

            return serv.EditarHabitacion(hDatos);
        }

        public bool EliminarHabitacion(string habitacion)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Habitacion));
            StringReader reader = new StringReader(habitacion);
            Modelo.Habitacion h = (Modelo.Habitacion)ser.Deserialize(reader);
            ServicioHabitacion serv = new ServicioHabitacion();
            Datos.HABITACION hDatos = new Datos.HABITACION();

            hDatos.NUMERO_HABITACION = h.NUMERO_HABITACION;

            return serv.EliminarHabitacion(hDatos);
        }

        //CRUD Producto
        public bool AgregarProducto(string producto)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Producto));
            StringReader reader = new StringReader(producto);
            Modelo.Producto p = (Modelo.Producto)ser.Deserialize(reader);
            ServicioProducto servicio = new ServicioProducto();

            Datos.PRODUCTO pDatos = new Datos.PRODUCTO();
            //Datos Producto
            pDatos.DESCRIPCION_PRODUCTO = p.DESCRIPCION_PRODUCTO;
            pDatos.ID_PRODUCTO = p.ID_PRODUCTO;
            pDatos.NOMBRE_PRODUCTO = p.NOMBRE_PRODUCTO;
            pDatos.FECHA_VENCIMIENTO_PRODUCTO = p.FECHA_VENCIMIENTO_PRODUCTO;
            pDatos.STOCK_PRODUCTO = p.STOCK_PRODUCTO;
            pDatos.STOCK_CRITICO_PRODUCTO = p.STOCK_CRITICO_PRODUCTO;
            pDatos.PRECIO_PRODUCTO = p.PRECIO_PRODUCTO;
            pDatos.ID_FAMILIA = p.ID_FAMILIA;
            pDatos.RUT_PROVEEDOR = p.RUT_PROVEEDOR;
            pDatos.ID_PRODUCTO_SEQ = p.ID_PRODUCTO_SEQ;

            return servicio.AgregarProducto(pDatos);
        }
        public bool ModificarProducto(string producto)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Producto));
            StringReader reader = new StringReader(producto);
            Modelo.Producto p = (Modelo.Producto)ser.Deserialize(reader);
            ServicioProducto serv = new ServicioProducto();
            Datos.PRODUCTO pDatos = new Datos.PRODUCTO();

            pDatos.DESCRIPCION_PRODUCTO = p.DESCRIPCION_PRODUCTO;
            pDatos.FECHA_VENCIMIENTO_PRODUCTO = p.FECHA_VENCIMIENTO_PRODUCTO;
            pDatos.ID_FAMILIA = p.ID_FAMILIA;
            pDatos.ID_PRODUCTO = p.ID_PRODUCTO;
            pDatos.NOMBRE_PRODUCTO = p.NOMBRE_PRODUCTO;
            pDatos.PRECIO_PRODUCTO = p.PRECIO_PRODUCTO;
            pDatos.STOCK_CRITICO_PRODUCTO = p.STOCK_CRITICO_PRODUCTO;
            pDatos.STOCK_PRODUCTO = p.STOCK_PRODUCTO;

            return serv.EditarProducto(pDatos); 
        }

        public bool ExisteProducto(string producto)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Producto));
            StringReader reader = new StringReader(producto);
            Modelo.Producto p = (Modelo.Producto)ser.Deserialize(reader);
            ServicioProducto serv = new ServicioProducto();
            Datos.PRODUCTO pDatos = new Datos.PRODUCTO();
            pDatos.ID_PRODUCTO = p.ID_PRODUCTO;

            if (!serv.ExisteProducto(pDatos))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public Producto ObtenerProducto(string producto)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Producto));
            StringReader reader = new StringReader(producto);
            Modelo.Producto p = (Modelo.Producto)ser.Deserialize(reader);
            ServicioProducto serv = new ServicioProducto();
            Datos.PRODUCTO pDatos = new Datos.PRODUCTO();
            pDatos.ID_PRODUCTO = p.ID_PRODUCTO;

            if (!serv.ExisteProducto(pDatos))
            {
                return null;
            }
            else
            {
                Datos.PRODUCTO pDatos2 = serv.obtenerProducto(pDatos);
                p.FECHA_VENCIMIENTO_PRODUCTO = pDatos2.FECHA_VENCIMIENTO_PRODUCTO;
                p.DESCRIPCION_PRODUCTO = pDatos2.DESCRIPCION_PRODUCTO;
                p.ID_FAMILIA = pDatos2.ID_FAMILIA;
                p.ID_PRODUCTO = pDatos2.ID_PRODUCTO;
                p.NOMBRE_PRODUCTO = pDatos2.NOMBRE_PRODUCTO;
                p.PRECIO_PRODUCTO = pDatos2.PRECIO_PRODUCTO;
                p.STOCK_CRITICO_PRODUCTO = pDatos2.STOCK_CRITICO_PRODUCTO;
                p.STOCK_PRODUCTO = pDatos2.STOCK_PRODUCTO;

                return p;
            }
        }

        public string ListarProducto()
        {
            ServicioProducto servicio = new ServicioProducto();
            List<Datos.PRODUCTO> producto = servicio.ListarProducto();
            Modelo.ProductoCollection listaProducto = new Modelo.ProductoCollection();

            foreach (Datos.PRODUCTO p in producto)
            {
                Modelo.Producto pModelo = new Modelo.Producto();
                pModelo.ID_PRODUCTO = p.ID_PRODUCTO;
                pModelo.NOMBRE_PRODUCTO = p.NOMBRE_PRODUCTO;
                pModelo.PRECIO_PRODUCTO = p.PRECIO_PRODUCTO;
                pModelo.STOCK_CRITICO_PRODUCTO = p.STOCK_CRITICO_PRODUCTO;
                pModelo.STOCK_PRODUCTO = p.STOCK_PRODUCTO;
                pModelo.ID_FAMILIA = p.ID_FAMILIA;
                pModelo.FECHA_VENCIMIENTO_PRODUCTO = p.FECHA_VENCIMIENTO_PRODUCTO;
                pModelo.DESCRIPCION_PRODUCTO = p.DESCRIPCION_PRODUCTO;

                listaProducto.Add(pModelo);
            }

            XmlSerializer ser = new XmlSerializer(typeof(Modelo.ProductoCollection));
            StringWriter writer = new StringWriter();
            ser.Serialize(writer, listaProducto);
            writer.Close();
            return writer.ToString();
        }

        public bool EliminarProducto(string producto)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Producto));
            StringReader reader = new StringReader(producto);
            Modelo.Producto p = (Modelo.Producto)ser.Deserialize(reader);
            ServicioProducto serv = new ServicioProducto();
            Datos.PRODUCTO pDatos = new Datos.PRODUCTO();

            pDatos.ID_PRODUCTO = p.ID_PRODUCTO;

            return serv.EliminarProducto(pDatos);
        }


        //CRUD Plato
        public bool AgregarPlato(string plato)
        {
            //Datos Plato
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Plato));
            StringReader reader = new StringReader(plato);
            Modelo.Plato p = (Modelo.Plato)ser.Deserialize(reader);
            ServicioPlato serv = new ServicioPlato();
            Datos.PLATO pDatos = new Datos.PLATO();
            pDatos.ID_PLATO = p.ID_PLATO;
            pDatos.NOMBRE_PLATO = p.NOMBRE_PLATO;
            pDatos.PRECIO_PLATO = p.PRECIO_PLATO;
            pDatos.ID_CATEGORIA = p.ID_CATEGORIA;
            pDatos.ID_TIPO_PLATO = p.ID_TIPO_PLATO;

            return serv.AgregarPlato(pDatos);
        }

        public bool ExistePlato(string plato)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Plato));
            StringReader reader = new StringReader(plato);
            Modelo.Plato p = (Modelo.Plato)ser.Deserialize(reader);
            ServicioPlato serv = new ServicioPlato();
            Datos.PLATO pDatos = new Datos.PLATO();
            pDatos.NOMBRE_PLATO = p.NOMBRE_PLATO;

            if (!serv.ExistePlato(pDatos))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
       

        public Plato ObtenerPlato(string plato)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Plato));
            StringReader reader = new StringReader(plato);
            Modelo.Plato p = (Modelo.Plato)ser.Deserialize(reader);
            ServicioPlato serv = new ServicioPlato();
            Datos.PLATO pDatos = new Datos.PLATO();
            pDatos.ID_PLATO = p.ID_PLATO;

            if (!serv.ExistePlatoID(pDatos))
            {
                return null;
            }
            else
            {
                Datos.PLATO pDatos2 = serv.obtenerPlato(pDatos);

                p.ID_PLATO = pDatos2.ID_PLATO;
                p.NOMBRE_PLATO = pDatos2.NOMBRE_PLATO;
                p.PRECIO_PLATO = pDatos2.PRECIO_PLATO;
                p.ID_CATEGORIA = pDatos2.ID_CATEGORIA;
                p.ID_TIPO_PLATO = pDatos2.ID_TIPO_PLATO;

                return p;
            }
        }

        public bool ModificarPlato(string plato)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Plato));
            StringReader reader = new StringReader(plato);
            Modelo.Plato p = (Modelo.Plato)ser.Deserialize(reader);
            ServicioPlato serv = new ServicioPlato();
            Datos.PLATO pDatos = new Datos.PLATO();

            pDatos.ID_TIPO_PLATO = p.ID_TIPO_PLATO;
            pDatos.ID_PLATO = p.ID_PLATO;
            pDatos.NOMBRE_PLATO = p.NOMBRE_PLATO;
            pDatos.PRECIO_PLATO = p.PRECIO_PLATO;
            pDatos.ID_CATEGORIA = p.ID_CATEGORIA;

            return serv.EditarPlato(pDatos);
        }

        public string ListarPlato()
        {
            ServicioPlato servicio = new ServicioPlato();
            List<Datos.PLATO> plato = servicio.ListarPlato();
            Modelo.PlatoCollection listaPlato = new Modelo.PlatoCollection();

            foreach (Datos.PLATO p in plato)
            {
                Modelo.Plato pModelo = new Modelo.Plato();
                pModelo.ID_PLATO = p.ID_PLATO;
                pModelo.NOMBRE_PLATO = p.NOMBRE_PLATO;
                pModelo.PRECIO_PLATO = p.PRECIO_PLATO;
                pModelo.ID_CATEGORIA = p.ID_CATEGORIA;
                pModelo.ID_TIPO_PLATO = p.ID_TIPO_PLATO;

                listaPlato.Add(pModelo);
            }

            XmlSerializer ser = new XmlSerializer(typeof(Modelo.PlatoCollection));
            StringWriter writer = new StringWriter();
            ser.Serialize(writer, listaPlato);
            writer.Close();
            return writer.ToString();
        }

        public bool EliminarPlato(string plato)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Plato));
            StringReader reader = new StringReader(plato);
            Modelo.Plato p = (Modelo.Plato)ser.Deserialize(reader);
            ServicioPlato serv = new ServicioPlato();
            Datos.PLATO pDatos = new Datos.PLATO();

            pDatos.ID_PLATO = p.ID_PLATO;
     
            return serv.EliminarPlato(pDatos);
        }

        //TIPO PLATO
        public bool AgregarTipoPlato(string tipoPlato)
        {
            //Datos tipo Plato
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.TipoPlato));
            StringReader reader = new StringReader(tipoPlato);
            Modelo.TipoPlato p = (Modelo.TipoPlato)ser.Deserialize(reader);
            ServicioPlato serv = new ServicioPlato();
            Datos.TIPO_PLATO tpDatos = new Datos.TIPO_PLATO();
            tpDatos.ID_TIPO_PLATO = p.ID_TIPO_PLATO;
            tpDatos.NOMBRE_TIPO_PLATO = p.NOMBRE_TIPO_PLATO;

            return serv.AgregarTipoPlato(tpDatos);
        }

        public bool ExisteTipoPlato(string tipoPlato)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.TipoPlato));
            StringReader reader = new StringReader(tipoPlato);
            Modelo.TipoPlato tp = (Modelo.TipoPlato)ser.Deserialize(reader);
            ServicioPlato serv = new ServicioPlato();
            Datos.TIPO_PLATO tpDatos = new Datos.TIPO_PLATO();
            tpDatos.NOMBRE_TIPO_PLATO = tp.NOMBRE_TIPO_PLATO;

            if (!serv.ExisteTipoPlato(tpDatos))
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public TipoPlato ObtenerTipoPlato(string tipoPlato)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.TipoPlato));
            StringReader reader = new StringReader(tipoPlato);
            Modelo.TipoPlato p = (Modelo.TipoPlato)ser.Deserialize(reader);
            ServicioPlato serv = new ServicioPlato();
            Datos.TIPO_PLATO tpDatos = new Datos.TIPO_PLATO();
            tpDatos.ID_TIPO_PLATO = p.ID_TIPO_PLATO;

            if (!serv.ExisteTipoPlatoID(tpDatos))
            {
                return null;
            }
            else
            {
                Datos.TIPO_PLATO pDatos2 = serv.obtenerTipoPlato(tpDatos);

                p.ID_TIPO_PLATO = pDatos2.ID_TIPO_PLATO;
                p.NOMBRE_TIPO_PLATO = pDatos2.NOMBRE_TIPO_PLATO;

                return p;
            }
        }

        public bool ModificarTipoPlato(string tipoPlato)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.Plato));
            StringReader reader = new StringReader(tipoPlato);
            Modelo.TipoPlato p = (Modelo.TipoPlato)ser.Deserialize(reader);
            ServicioPlato serv = new ServicioPlato();
            Datos.TIPO_PLATO tpDatos = new Datos.TIPO_PLATO();

            tpDatos.ID_TIPO_PLATO = p.ID_TIPO_PLATO;
            tpDatos.NOMBRE_TIPO_PLATO = p.NOMBRE_TIPO_PLATO;

            return serv.EditarTipoPlato(tpDatos);
        }

        public bool EliminarTipoPlato(string tipoPlato)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.TipoPlato));
            StringReader reader = new StringReader(tipoPlato);
            Modelo.TipoPlato tp = (Modelo.TipoPlato)ser.Deserialize(reader);
            ServicioPlato serv = new ServicioPlato();
            Datos.TIPO_PLATO tpDatos = new Datos.TIPO_PLATO();

            tpDatos.ID_TIPO_PLATO = tp.ID_TIPO_PLATO;

            return serv.EliminarTipoPlato(tpDatos);
        }

        //DDL
        public string ListarPais()
        {
            ServicioDireccion servicio = new ServicioDireccion();
            List<Datos.PAIS> pais = servicio.ListarPaises();
            Modelo.PaisCollection listaPais = new Modelo.PaisCollection();
            foreach (Datos.PAIS p in pais)
            {
                Modelo.Pais pModelo = new Modelo.Pais();
                pModelo.ID_PAIS = p.ID_PAIS;
                pModelo.NOMBRE_PAIS = p.NOMBRE_PAIS;
                listaPais.Add(pModelo);
            }
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.PaisCollection));
            StringWriter writer = new StringWriter();
            ser.Serialize(writer, listaPais);
            return writer.ToString();
        }

        public string ListarRegion()
        {
            ServicioDireccion servicio = new ServicioDireccion();
            List<Datos.REGION> region = servicio.ListarRegion();
            Modelo.RegionCollection listaRegion = new Modelo.RegionCollection();
            foreach (Datos.REGION r in region)
            {
                Modelo.Region rModelo = new Modelo.Region();
                rModelo.Id_Pais = r.ID_PAIS;
                rModelo.Id_Region = r.ID_REGION;
                rModelo.Nombre = r.NOMBRE_REGION;
                listaRegion.Add(rModelo);
            }
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.RegionCollection));
            StringWriter writer = new StringWriter();
            ser.Serialize(writer, listaRegion);
            return writer.ToString();
        }
        
        public string ListarComuna()
        {
            ServicioDireccion servicio = new ServicioDireccion();
            List<Datos.COMUNA> comuna = servicio.ListarComuna();
            Modelo.ComunaCollection listaComuna = new Modelo.ComunaCollection();
            foreach (Datos.COMUNA c in comuna)
            {
                Modelo.Comuna cModelo = new Modelo.Comuna();
                cModelo.Id_Region = c.ID_REGION;
                cModelo.Id_Comuna = c.ID_COMUNA;
                cModelo.Nombre = c.NOMBRE_COMUNA;
                listaComuna.Add(cModelo);
            }
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.ComunaCollection));
            StringWriter writer = new StringWriter();
            ser.Serialize(writer, listaComuna);
            return writer.ToString();
        }

        public string ListarTipoHabitacion()
        {
            ServicioHabitacion servicio = new ServicioHabitacion();
            List<Datos.TIPO_HABITACION> tipo_habitacion = servicio.ListarTipoHabitacion();
            Modelo.TipoHabitacionCollection listaTipoHabitacion = new Modelo.TipoHabitacionCollection();
            foreach (Datos.TIPO_HABITACION t in tipo_habitacion)
            {
                Modelo.TipoHabitacion tModelo = new Modelo.TipoHabitacion();
                tModelo.ID_TIPO_HABITACION = t.ID_TIPO_HABITACION;
                tModelo.NOMBRE_TIPO_HABITACION = t.NOMBRE_TIPO_HABITACION;
                tModelo.CANTIDAD_PASAJERO = t.CANTIDAD_PASAJERO;
                listaTipoHabitacion.Add(tModelo);
            }
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.TipoHabitacionCollection));
            StringWriter writer = new StringWriter();
            ser.Serialize(writer, listaTipoHabitacion);
            return writer.ToString();
        }

        public string ListarCategoria()
        {
            ServicioPlato servicio = new ServicioPlato();
            List<Datos.CATEGORIA> categoria = servicio.ListarCategoria();
            Modelo.CategoriaCollection listaCategoria = new Modelo.CategoriaCollection();
            foreach (Datos.CATEGORIA c in categoria)
            {
                Modelo.Categoria cModelo = new Modelo.Categoria();
                cModelo.ID_CATEGORIA = c.ID_CATEGORIA;
                cModelo.NOMBRE_CATEGORIA = c.NOMBRE_CATEGORIA;
                listaCategoria.Add(cModelo);
            }
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.CategoriaCollection));
            StringWriter writer = new StringWriter();
            ser.Serialize(writer, listaCategoria);
            return writer.ToString();
        }

        public string ListarTipoPlato()
        {
            ServicioPlato servicio = new ServicioPlato();
            List<Datos.TIPO_PLATO> TipoPlato = servicio.ListarTipoPlato();
            Modelo.TipoPlatoCollection listaPlatos = new Modelo.TipoPlatoCollection();
            foreach (Datos.TIPO_PLATO c in TipoPlato)
            {
                Modelo.TipoPlato cModelo = new Modelo.TipoPlato();
                cModelo.ID_TIPO_PLATO = c.ID_TIPO_PLATO;
                cModelo.NOMBRE_TIPO_PLATO = c.NOMBRE_TIPO_PLATO;
                listaPlatos.Add(cModelo);
            }
            XmlSerializer ser = new XmlSerializer(typeof(Modelo.TipoPlatoCollection));
            StringWriter writer = new StringWriter();
            ser.Serialize(writer, listaPlatos);
            return writer.ToString();
        }

        public string ListarFamilia()
        {
            ServicioProducto servicio = new ServicioProducto();
            List<Datos.FAMILIA> familia = servicio.ListarFamilia();
            Modelo.FamiliaCollection listaFamilia = new Modelo.FamiliaCollection();

            foreach (Datos.FAMILIA f in familia)
            {
                Modelo.Familia fModelo = new Modelo.Familia();
                fModelo.ID_FAMILIA = f.ID_FAMILIA;
                fModelo.NOMBRE_FAMILIA = f.NOMBRE_FAMILIA;
                
                listaFamilia.Add(fModelo);
            }

            XmlSerializer ser = new XmlSerializer(typeof(Modelo.FamiliaCollection));
            StringWriter writer = new StringWriter();
            ser.Serialize(writer, listaFamilia);
            writer.Close();
            return writer.ToString();
        }
    }
}
