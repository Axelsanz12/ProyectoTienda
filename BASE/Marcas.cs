using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BASE
{
    public class Marcas
    {
        public int Id {get; set; }

        public string  DescripcionMarca { get; set; }

        override public string ToString()
        {
            return DescripcionMarca;
        }   
    }
}
