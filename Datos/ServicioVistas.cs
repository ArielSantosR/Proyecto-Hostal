using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos {
    public class ServicioVistas {

        private HostalEntities ent = new HostalEntities();

        public List<V_CLIENTE_FRECUENTE> ListarClienteFrecuente() {
            var lista = ent.V_CLIENTE_FRECUENTE.ToList();
            return lista;
        }

        public List<V_CLIENTE_ORDENES> ListarOrdenesClientes() {
            var lista = (from consulta in ent.V_CLIENTE_ORDENES
                         select consulta).ToList();
            return lista;
        }

        public  List<V_CLIENTE_ORDENES_TOP_10> ListarTopClientes() {
            var lista = (from consulta in ent.V_CLIENTE_ORDENES_TOP_10
                         select consulta).ToList();
            return lista;
        }

        public List<V_DIAS_USOS> ListarDiasUsoHabitaciones() {
            var lista = (from consulta in ent.V_DIAS_USOS
                         select consulta).ToList();
            return lista;
        }

        public List<V_HABITACION_PREFERIDA> ListarHabitacionesFrecuentes() {
            var lista = (from consulta in ent.V_HABITACION_PREFERIDA
                         select consulta).ToList();
            return lista;
        }

        public List<V_HABITACION_USOS> ListarUsosHabitaciones() {
            var lista = (from consulta in ent.V_HABITACION_USOS
                         select consulta).ToList();
            return lista;
        }

        public List<V_HABITACION_USOS_TOP_10> ListarTopUsoHabitaciones() {
            var lista = (from consulta in ent.V_HABITACION_USOS_TOP_10
                         select consulta).ToList();
            return lista;
        }
    }
}
