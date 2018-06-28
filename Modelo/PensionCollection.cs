using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    [Serializable]
    public class PensionCollection : List<Pension>
    {
        public static List<Pension> ListarPensiones() {
            ServicioMinuta serv = new ServicioMinuta();
            return GenerarLista(serv.ListarMinuta());
        }

        private static List<Pension> GenerarLista(List<PENSION> listDatos) {
            List<Pension> list = new List<Pension>();
            Pension pension;
            foreach (PENSION dato in listDatos) {
                pension = new Pension();

                pension.ID_PENSION = dato.ID_PENSION;
                pension.HABILITADO = dato.HABILITADO;
                pension.NOMBRE_PENSION = dato.NOMBRE_PENSION;
                pension.VALOR_PENSION = dato.VALOR_PENSION;
                
                list.Add(pension);
            }
            return list;
        }
    }
}
