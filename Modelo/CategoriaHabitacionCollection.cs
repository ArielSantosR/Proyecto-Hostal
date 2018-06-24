using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    [Serializable]
    public class CategoriaHabitacionCollection : List<CategoriaHabitacion> {

        public static List<CategoriaHabitacion> ListarCategorias() {
            ServicioCategoria serv = new ServicioCategoria();
            return GenerarLista(serv.ListarCategoriaHabitacion());
        }

        private static List<CategoriaHabitacion> GenerarLista(List<CATEGORIA_HABITACION> listDatos) {
            List<CategoriaHabitacion> list = new List<CategoriaHabitacion>();
            CategoriaHabitacion categoria;
            foreach (CATEGORIA_HABITACION dato in listDatos) {
                categoria = new CategoriaHabitacion();
                categoria.ID_CATEGORIA_HABITACION = dato.ID_CATEGORIA_HABITACION;
                categoria.NOMBRE_CATEGORIA = dato.NOMBRE_CATEGORIA;
                categoria.PRECIO_CATEGORIA = dato.PRECIO_CATEGORIA;

                list.Add(categoria);
            }
            return list;
        }
    }
}
