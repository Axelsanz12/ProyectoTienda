using BASE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Negocio
{
    public class catalogoNegocio
    {
        public List<Catalogo> listar()
        {

            List<Catalogo> lista = new List<Catalogo>();
            AcessoDatos datos = new AcessoDatos();

            try
            {

                datos.setearConsulta("select Id, Descripcion from CATEGORIAS");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Catalogo aux = new Catalogo();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.DescripcionCatalogo = (string)datos.Lector["Descripcion"];
                    lista.Add(aux);
                }
                return lista;


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
