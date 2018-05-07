using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfNegocio
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IService1
    {
        //CRUD Usuario
        [OperationContract]
        bool Login(string user);

        [OperationContract]
        Usuario GetUsuario(string user);

        [OperationContract]
        bool ExisteUsuario(string user);

        [OperationContract]
        bool RegistroUsuario(string user);

        [OperationContract]
        bool EliminarUsuario(string user);

        [OperationContract]
        bool ModificarUsuario(string user);

        [OperationContract]
        string ListarUsuarios();
        //Huesped
        [OperationContract]
        bool ExisteHuesped(string huesped);

        [OperationContract]
        bool RegistroHuesped(string huesped);

        [OperationContract]
        bool EliminarHuesped(string huesped);

        [OperationContract]
        bool ModificarHuesped(string huesped);

        [OperationContract]
        string ListarHuesped();

        //CRUD Cliente
        [OperationContract]
        bool ExisteRutC(string cliente);

        [OperationContract]
        bool RegistroCliente(string cliente);
        //CRUD Empleado
        [OperationContract]
        bool ExisteRutE(string empleado);

        [OperationContract]
        Empleado buscarIDE (string empleado);

        [OperationContract]
        bool RegistroEmpleado(string empleado);
        //CRUD Proveedor
        [OperationContract]
        bool ExisteRutP(string proveedor);

        [OperationContract]
        bool RegistroProveedor(string proveedor);

        [OperationContract]
        string ListarProveedor();

        //CRUD Habitacion
        [OperationContract]
        bool AgregarHabitacion(string habitacion);

        [OperationContract]
        bool ExisteHabitacion(string habitacion);

        [OperationContract]
        string ListarHabitacion();

        [OperationContract]
        Habitacion ObtenerHabitacion(string habitacion);

        [OperationContract]
        bool ModificarHabitacion(string habitacion);

        [OperationContract]
        bool EliminarHabitacion(string habitacion);

        //CRUD Producto
        [OperationContract]
        bool AgregarProducto(string producto);

        [OperationContract]
        bool ModificarProducto(string producto);

        [OperationContract]
        Producto ObtenerProducto(string producto);

        [OperationContract]
        bool ExisteProducto(string producto);

        [OperationContract]
        string ListarProducto();

        [OperationContract]
        bool EliminarProducto(string producto);

        [OperationContract]
        string ListarProveedorProducto(string producto);

        //CRUD Plato
        [OperationContract]
        bool AgregarPlato(string plato);

        [OperationContract]
        bool ExistePlato(string plato);

        [OperationContract]
        Plato ObtenerPlato(string plato);

        [OperationContract]
        bool ModificarPlato(string plato);

        [OperationContract]
        string ListarPlato();

        [OperationContract]
        bool EliminarPlato(string plato);

        //CRUD tipo plato
        [OperationContract]
        bool AgregarTipoPlato(string tipoPlato);

        [OperationContract]
        bool EliminarTipoPlato(string tipoPlato);

        [OperationContract]
        bool ModificarTipoPlato(string tipoPlato);

        [OperationContract]
        bool ExisteTipoPlato(string tipoPlato);

        [OperationContract]
        TipoPlato ObtenerTipoPlato(string tipoPlato);

        //CRUD TIPO PROVEEDOR
        [OperationContract]
        bool AgregarTipoProveedor(string tipoProveedor);

        [OperationContract]
        bool EliminarTipoProveedor(string tipoProveedor);

        [OperationContract]
        bool ModificarTipoProveedor(string tipoProveedor);

        [OperationContract]
        bool ExisteTipoProveedor(string tipoProveedor);

        [OperationContract]
        TipoProveedor ObtenerTipoProveedor(string tipoProveedor);

        //CRUD Pedido
        [OperationContract]
        bool AgregarPedido(string pedido);

        [OperationContract]
        string ListarPedidoAdmin();


        //DDL
        [OperationContract]
        string ListarTipoProveedor();

        [OperationContract]
        string ListarPais();

        [OperationContract]
        string ListarComuna();

        [OperationContract]
        string ListarRegion();

        [OperationContract]
        string ListarTipoHabitacion();

        [OperationContract]
        string ListarTipoPlato();

        [OperationContract]
        string ListarFamilia();
    }
}
