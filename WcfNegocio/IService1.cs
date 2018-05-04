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
        //CRUD Cliente
        [OperationContract]
        bool ExisteRutC(string cliente);

        [OperationContract]
        bool RegistroCliente(string cliente);
        //CRUD Empleado
        [OperationContract]
        bool ExisteRutE(string empleado);

        [OperationContract]
        bool RegistroEmpleado(string empleado);
        //CRUD Proveedor
        [OperationContract]
        bool ExisteRutP(string proveedor);

        [OperationContract]
        bool RegistroProveedor(string proveedor);

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
