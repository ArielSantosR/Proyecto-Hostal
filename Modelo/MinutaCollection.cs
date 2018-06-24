using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    [Serializable]
    public class MinutaCollection : List<Minuta> {

        public static List<Pension> ListarMinutas() {
            ServicioMinuta serv = new ServicioMinuta();
            return GenerarLista(serv.ListarMinuta());
        }

        private static List<Pension> GenerarLista(List<PENSION> listDatos) {
            List<Pension> list = new List<Pension>();
            Pension pension;
            foreach (PENSION dato in listDatos) {
                pension = new Pension();
                pension.ID_PENSION = dato.ID_PENSION;
                pension.NOMBRE_PENSION = dato.NOMBRE_PENSION;
                pension.VALOR_PENSION = dato.VALOR_PENSION;
                pension.HABILITADO = dato.HABILITADO;

                list.Add(pension);
            }
            return list.Where(x => x.HABILITADO == "T").ToList();
        }
    }
}
