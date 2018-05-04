using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class ServicioPlato
    {
        HostalEntities ent = new HostalEntities();

        public List<TIPO_PLATO> ListarTipoPlato()
        {
            return ent.TIPO_PLATO.ToList<TIPO_PLATO>();
        }

        public List<CATEGORIA> ListarCategoria()
        {
            return ent.CATEGORIA.ToList();
        }

        public bool AgregarPlato(PLATO p)
        {
            ent.PLATO.Add(p);
            ent.SaveChanges();
            return true;
        }

        public bool ExistePlato(PLATO plato)
        {
            PLATO p = ent.PLATO.FirstOrDefault(objeto =>
            objeto.NOMBRE_PLATO.Equals(plato.NOMBRE_PLATO));
            if (p != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public PLATO obtenerPlato(PLATO plato)
        {

            PLATO p = ent.PLATO.FirstOrDefault(objeto =>
            objeto.ID_PLATO.Equals(plato.ID_PLATO));

            if (p != null)
            {
                return p;
            }
            else
            {
                return null;
            }
        }

        public bool EditarPlato(PLATO plato)
        {
            PLATO p = ent.PLATO.FirstOrDefault(objeto =>
            objeto.ID_PLATO.Equals(plato.ID_PLATO));

            if (p != null)
            {
                p.NOMBRE_PLATO = plato.NOMBRE_PLATO;
                p.PRECIO_PLATO = plato.PRECIO_PLATO;
                p.ID_TIPO_PLATO = plato.ID_TIPO_PLATO;
                p.ID_PLATO = plato.ID_PLATO;
                p.ID_CATEGORIA = plato.ID_CATEGORIA;

                ent.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
