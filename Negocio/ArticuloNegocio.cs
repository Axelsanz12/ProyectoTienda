using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using BASE; 

namespace Negocio
{
    public class ArticuloNegocio
    {
        public List<Articulos> listar()
        {
            List<Articulos> lista = new List<Articulos>();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            try
            {
                conexion.ConnectionString = "server=.\\SQLEXPRESS; database=CATALOGO_DB; integrated security=true";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "select p.Id , Codigo , Nombre ,P.Descripcion , ImagenUrl ,e.Id as IdCategoria, e.Descripcion as Tipo, M.Id as IdMarca, M.Descripcion as Marca ,P.Precio from ARTICULOS P ,CATEGORIAS E, MARCAS M where e.Id = p.IdCategoria and m.Id = p.IdMarca";
;
                comando.Connection = conexion;

                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Articulos aux = new Articulos();
                    aux.id = lector.GetInt32(0);
                    aux.Codigo = (string)lector["Codigo"];
                    aux.Nombre = (string)lector["Nombre"];
                    aux.Descripcion = (string)lector["Descripcion"];
                    aux.Precio = Convert.ToDecimal(lector["Precio"]);


                    aux.UrlImagen = (string)lector["ImagenUrl"];

                    aux.Tipo = new Catalogo();
                    aux.Tipo.Id = (int)lector["IdCategoria"]; 
                    aux.Tipo.DescripcionCatalogo = (string)lector["Tipo"]; 

                    aux.marca = new Marcas();
                    aux.marca.Id = (int)lector["IdMarca"]; 
                    aux.marca.DescripcionMarca = (string)lector["Marca"]; 




                    lista.Add(aux);
                }

                conexion.Close();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
           
            public void agregar (Articulos nuevo)
        {   
            AcessoDatos datos  = new AcessoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO ARTICULOS (Codigo, Nombre, Descripcion, IdCategoria ,ImagenUrl,Idmarca,Precio) VALUES (@Codigo, @Nombre, @Descripcion, @IdCategoria , @UrlImagen ,@IdMarca ,@Precio)");
                datos.setearParametros("@codigo", nuevo.Codigo);
                datos.setearParametros("@Nombre", nuevo.Nombre);
                datos.setearParametros("@descripcion", nuevo.Descripcion);
                datos.setearParametros("@IdCategoria",nuevo.Tipo.Id);
                datos.setearParametros("@IdMarca", nuevo.marca.Id);
                datos.setearParametros("@UrlImagen", nuevo.UrlImagen);  
                datos.setearParametros("@Precio", nuevo.Precio);


                datos.ejecutarAccion();


            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
                

        }

      public void Eliminar (int id)
        {
            AcessoDatos datos = new AcessoDatos (); 
            datos.setearConsulta("delete from ARTICULOS where Id = @id");
            datos.setearParametros("@id", id);
            datos.ejecutarAccion();
        }

        public void Modicar (Articulos Celu)
        {
            AcessoDatos datos = new AcessoDatos();
            try
            {
                datos.setearConsulta("update ARTICULOS set Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, IdCategoria = @IdCategoria, ImagenUrl = @UrlImagen, IdMarca = @IdMarca, Precio = @Precio where Id = @Id");
                datos.setearParametros("@codigo", Celu.Codigo);
                datos.setearParametros("@Nombre", Celu.Nombre);
                datos.setearParametros("@descripcion", Celu.Descripcion);
                datos.setearParametros("@idCategoria", Celu.Tipo.Id);
                datos.setearParametros("@UrlImagen", Celu.UrlImagen);
                datos.setearParametros("@IdMarca", Celu.marca.Id);  
                datos.setearParametros("@Precio", Celu.Precio);
                datos.setearParametros("@Id", Celu.id); 

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            finally
            {
                datos.cerrarConexion();
            }

        }




    }
}






