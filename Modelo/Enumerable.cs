using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo {
    public enum Tipo_Usuario {
        Administrador,
        Cliente,
        Proveedor,
        Empleado
    }

    public enum Estado_Usuario {
        Habilitado,
        Deshabilitado
    }
}
