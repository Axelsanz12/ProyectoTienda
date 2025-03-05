using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BASE;
using Negocio;

namespace Presentacion
{
    public partial class Form1 : Form
    {

        private List<Articulos> listaArticulos;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            listaArticulos = negocio.listar();
            dgvArticulos.DataSource = listaArticulos;
            OcultarColumnas();
            cargarImagen(listaArticulos[0].UrlImagen);
        }


        public void OcultarColumnas()
        {
            dgvArticulos.Columns["UrlImagen"].Visible = false;


        }
        private void cargarImagen(string imagen)
        {
            try
            {
                pxbArticulo.Load(imagen);

            }
            catch (Exception ex)
            {
                // si la imagen no se puede cargar o no tiene muestra esta.

                pxbArticulo.Load("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTOwRConBYl2t6L8QMOAQqa5FDmPB_bg7EnGA&s");
            }

        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            // lo que generamos aca es que recorra la base de datos segun el articulo seleccionado y muestre la imagen
            if (dgvArticulos.CurrentRow != null)
            {
                Articulos seleccionado = (Articulos)dgvArticulos.CurrentRow.DataBoundItem;
                cargarImagen(seleccionado.UrlImagen);

            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            AltaArticulo alta = new AltaArticulo();
            alta.ShowDialog();
            cargar();


        }

        public void cargar()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                listaArticulos = negocio.listar();
                dgvArticulos.DataSource = listaArticulos;
                cargarImagen(listaArticulos[0].UrlImagen);
                OcultarColumnas();


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

        }


        private void eliminar (bool logico = false)
        {

            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulos seleccionado;
            try
            {
                DialogResult respuesta = MessageBox.Show("¿desea eliminarlo?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (respuesta == DialogResult.Yes)
                {
                    seleccionado = (Articulos)dgvArticulos.CurrentRow.DataBoundItem;
                    negocio.Eliminar(seleccionado.id);

                    cargar();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            eliminar(true);
        }

        private void BtnModificar_Click(object sender, EventArgs e)
        {
            Articulos seleccionado;
            seleccionado =(Articulos)dgvArticulos.CurrentRow.DataBoundItem;

            AltaArticulo modificar = new AltaArticulo(seleccionado);
            modificar.ShowDialog();
            cargar();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            List<Articulos> listaFiltrada;
            string filtro = txtBuscar.Text;

            if(filtro != "")
            {
                listaFiltrada = listaArticulos.FindAll(x => x.Nombre.ToLower().Contains(filtro.ToLower()) || x.Tipo.DescripcionCatalogo.ToLower().Contains(filtro.ToLower()));

            }
            else
            {
                listaFiltrada = listaArticulos;
            }

            dgvArticulos.DataSource = null;
            dgvArticulos .DataSource = listaFiltrada;
            OcultarColumnas();


        }

        private void btnDetalle_Click(object sender, EventArgs e)
        {
            string filtro = txtDetalle.Text; // tenemos lo que busca el usuario

            if (!string.IsNullOrEmpty(filtro)) // verificamos si el filtro esta vacio txt
            {
                Articulos articulo = listaArticulos.FirstOrDefault(a =>
                    a.Codigo.Equals(filtro, StringComparison.OrdinalIgnoreCase) ||
                    a.Nombre.Equals(filtro, StringComparison.OrdinalIgnoreCase)  //ignoramos mayúsculas y minúsculas.
                );

                if (articulo != null)
                {
                    MostrarDetalles(articulo); // Llamar a la función para mostrar los detalles
                }
                else
                {
                    MessageBox.Show("Artículo no encontrado,Intenten copiando todo el Nombre del articulo O CODIGO.", "Error" );
                }
            }
            else
            {
                MessageBox.Show("Por favor, ingresa un filtro de búsqueda.", "Advertencia");
            }
        }

        private void MostrarDetalles(Articulos articulo)
        
        {
            AltaArticulo modificar = new AltaArticulo(articulo);
            modificar.Text = "Detalle del Artículo"; // Cambia el Text para identificarlo
            modificar.ShowDialog();
            cargar();
        }

        
    }
}
