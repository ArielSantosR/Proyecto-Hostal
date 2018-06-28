using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo {
    [Serializable]
    public class BoletaCollection : List<Boleta>{

        public static List<Boleta> ListarBoletas() {
            ServicioPagos serv = new ServicioPagos();
            return GenerarLista(serv.ListarBoletas());
        }

        private static List<Boleta> GenerarLista(List<BOLETA> listDatos) {
            List<Boleta> list = new List<Boleta>();
            Boleta boleta;
            foreach (BOLETA dato in listDatos) {
                boleta = new Boleta();
                boleta.FECHA_EMISION_BOLETA = dato.FECHA_EMISION_BOLETA;
                boleta.ID_BOLETA = dato.ID_BOLETA;
                boleta.RUT_EMPLEADO = dato.RUT_EMPLEADO;
                boleta.RUT_HUESPED = dato.RUT_HUESPED;
                boleta.VALOR_DESC_BOLETA = dato.VALOR_DESC_BOLETA;
                boleta.VALOR_TOTAL_BOLETA = dato.VALOR_TOTAL_BOLETA;

                list.Add(boleta);
            }
            return list;
        }
    }
}
