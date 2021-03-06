﻿using System;
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

        public List<PLATO> ListarPlato()
        {
            return ent.PLATO.ToList();
        }

        public bool AgregarTipoPlato(TIPO_PLATO p)
        {
            ent.TIPO_PLATO.Add(p);
            ent.SaveChanges();
            return true;
        }
        public bool AgregarCategoria(CATEGORIA c)
        {
            ent.CATEGORIA.Add(c);
            ent.SaveChanges();
            return true;
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

        public PLATO BuscarPlato (short iD_PLATO) {
            PLATO p = ent.PLATO.FirstOrDefault(x => x.ID_PLATO == iD_PLATO);
            return p;
        }

        public bool ExisteTipoPlato(TIPO_PLATO tipoPlato)
        {
            TIPO_PLATO tp = ent.TIPO_PLATO.FirstOrDefault(objeto =>
            objeto.NOMBRE_TIPO_PLATO.Equals(tipoPlato.NOMBRE_TIPO_PLATO));
            if (tp != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ExisteCategoria(CATEGORIA categoria)
        {
            CATEGORIA c = ent.CATEGORIA.FirstOrDefault(objeto =>
            objeto.NOMBRE_CATEGORIA.Equals(categoria.NOMBRE_CATEGORIA));
            if (c != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ExisteTipoPlatoID(TIPO_PLATO tipoPlato)
        {
            TIPO_PLATO tp = ent.TIPO_PLATO.FirstOrDefault(objeto =>
            objeto.ID_TIPO_PLATO.Equals(tipoPlato.ID_TIPO_PLATO));
            if (tp != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ExisteCategoriaID(CATEGORIA categoria)
        {
            CATEGORIA c = ent.CATEGORIA.FirstOrDefault(objeto =>
            objeto.ID_CATEGORIA.Equals(categoria.ID_CATEGORIA));
            if (c != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ExistePlatoID(PLATO plato)
        {
            PLATO p = ent.PLATO.FirstOrDefault(objeto =>
            objeto.ID_PLATO.Equals(plato.ID_PLATO));
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

        public TIPO_PLATO obtenerTipoPlato(TIPO_PLATO tipoPlato)
        {

            TIPO_PLATO tp = ent.TIPO_PLATO.FirstOrDefault(objeto =>
            objeto.ID_TIPO_PLATO.Equals(tipoPlato.ID_TIPO_PLATO));

            if (tp != null)
            {
                return tp;
            }
            else
            {
                return null;
            }
        }
        public CATEGORIA obtenerCategoria(CATEGORIA categoria)
        {

            CATEGORIA c = ent.CATEGORIA.FirstOrDefault(objeto =>
            objeto.ID_CATEGORIA.Equals(categoria.ID_CATEGORIA));

            if (c != null)
            {
                return c;
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

        public bool EditarTipoPlato(TIPO_PLATO tipoPlato)
        {
            TIPO_PLATO tp = ent.TIPO_PLATO.FirstOrDefault(objeto =>
            objeto.ID_TIPO_PLATO.Equals(tipoPlato.ID_TIPO_PLATO));

            if (tp != null)
            {
                tp.ID_TIPO_PLATO = tipoPlato.ID_TIPO_PLATO;
                tp.NOMBRE_TIPO_PLATO = tipoPlato.NOMBRE_TIPO_PLATO;

                ent.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool EditarCategoria(CATEGORIA categoria)
        {
            CATEGORIA c = ent.CATEGORIA.FirstOrDefault(objeto =>
            objeto.ID_CATEGORIA.Equals(categoria.ID_CATEGORIA));

            if (c != null)
            {
                c.ID_CATEGORIA = categoria.ID_CATEGORIA;
                c.NOMBRE_CATEGORIA = categoria.NOMBRE_CATEGORIA;

                ent.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool EliminarPlato(PLATO plato)
        {
            PLATO p = ent.PLATO.FirstOrDefault(objeto =>
            objeto.ID_PLATO.Equals(plato.ID_PLATO));

            if (p != null)
            {
                ent.PLATO.Remove(p);
                ent.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool EliminarTipoPlato(TIPO_PLATO tipoPlato)
        {
            TIPO_PLATO tp = ent.TIPO_PLATO.FirstOrDefault(objeto =>
            objeto.ID_TIPO_PLATO.Equals(tipoPlato.ID_TIPO_PLATO));

            if (tp != null)
            {
                ent.TIPO_PLATO.Remove(tp);
                ent.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool EliminarCategoria(CATEGORIA categoria)
        {
            CATEGORIA c = ent.CATEGORIA.FirstOrDefault(objeto =>
            objeto.ID_CATEGORIA.Equals(categoria.ID_CATEGORIA));

            if (c != null)
            {
                ent.CATEGORIA.Remove(c);
                ent.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<PLATO> listaPlatoCategoria(CATEGORIA categoria)
        {
            var lista = (from consulta in ent.PLATO
                         where consulta.ID_CATEGORIA == categoria.ID_CATEGORIA
                         orderby consulta.ID_TIPO_PLATO
                         select consulta).ToList();
            return lista;
        }
    }
}
