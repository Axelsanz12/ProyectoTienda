using BASE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Negocio
{
    public class MarcaNegocio
    {
            public List<Marcas> listar()
            {

                List<Marcas> lista = new List<Marcas>();
                AcessoDatos datos = new AcessoDatos();

                try
                {

                    datos.setearConsulta("select Id, Descripcion from MARCAS");
                    datos.ejecutarLectura();
                    while (datos.Lector.Read())
                    {
                        Marcas aux = new Marcas();
                        aux.Id = (int)datos.Lector["Id"];
                        aux.DescripcionMarca = (string)datos.Lector["Descripcion"];
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
