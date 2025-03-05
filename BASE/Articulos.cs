using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BASE
{
    public class Articulos
    {

        public int id { get; set; }

        public string Codigo { get; set; }

        public string Nombre  { get; set; }

        public string Descripcion { get; set;}

        public decimal Precio { get; set; } // tuve problemas al usar el float entonces utilice decimal ya que en la db me aparece money

        public  string UrlImagen { get; set; }

        public Catalogo Tipo { get; set; }

        public Marcas marca { get; set; }   
        



    }
}
