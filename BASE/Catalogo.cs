using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BASE  
{
    public  class Catalogo
    {
        public int Id { get; set; }

        public string DescripcionCatalogo { get; set; }


        //sobreescribimos el metodo ToString para que nos muestre la descripcion de la categoria
        public override string ToString()
        {
            return DescripcionCatalogo;
        }   
    }
}
