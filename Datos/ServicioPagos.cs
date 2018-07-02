using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos {
    public class ServicioPagos {

        private HostalEntities ent = new HostalEntities();

        public bool AgregarFactura(FACTURA factura) {
            ent.FACTURA.Add(factura);
            ent.SaveChanges();
            return true;
        }

        public List<DETALLE_FACTURA> ListarDetalleFacturas() {
            return ent.DETALLE_FACTURA.ToList();
        }

        public List<FACTURA> ListarFacturas() {
            return ent.FACTURA.ToList();
        }

        public BOLETA BuscarBoleta (int iD_BOLETA) {
            BOLETA b = ent.BOLETA.FirstOrDefault(x => x.ID_BOLETA == iD_BOLETA);
            return b;
        }

        public List<BOLETA> ListarBoletas() {
            return ent.BOLETA.ToList();
        }

        public FACTURA BuscarFactura (int iD_FACTURA) {
            FACTURA f = ent.FACTURA.FirstOrDefault(x => x.ID_FACTURA == iD_FACTURA);
            return f;
        }

        public List<DETALLE_BOLETA> ListarDetalleBoletas() {
            return ent.DETALLE_BOLETA.ToList();
        }

        public bool AgregarDetalleFactura(DETALLE_FACTURA detalle) {
            ent.DETALLE_FACTURA.Add(detalle);
            ent.SaveChanges();
            return true;
        }

        public bool AgregarBoleta(BOLETA boleta) {
            ent.BOLETA.Add(boleta);
            ent.SaveChanges();
            return true;
        }

        public bool AgregarDetalleBoleta(DETALLE_BOLETA detalle) {
            ent.DETALLE_BOLETA.Add(detalle);
            ent.SaveChanges();
            return true;
        }
    }
}
