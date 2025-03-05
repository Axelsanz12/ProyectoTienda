    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using BASE; 
    using Negocio;
    using System.Configuration;
    using System.IO;
    using static System.Net.Mime.MediaTypeNames;

namespace Presentacion
    {
  
    public partial class AltaArticulo : Form
        {
         private OpenFileDialog Archivo = null;

        private Articulos articulos = null;

        public object ConfigurationManager { get; private set; }

        public AltaArticulo()
            {
                InitializeComponent();
            }


        public AltaArticulo(Articulos arti)
        {

            InitializeComponent();
            this.articulos = arti;
            Text = "MODIFICAR PRODUCTO";
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
            {
                Close();
            }

            private void btnAceptar_Click(object sender, EventArgs e)
            {
                ArticuloNegocio negocio = new ArticuloNegocio();

            try
            {
                if (string.IsNullOrWhiteSpace(txtCodigo.Text) ||
                 string.IsNullOrWhiteSpace(txtNombre.Text) ||
                 string.IsNullOrWhiteSpace(txtDescripcion.Text) ||
                 string.IsNullOrWhiteSpace(txtImagen.Text) ||
                 cboCategoria.SelectedItem == null ||
                 cboMarca.SelectedItem == null)
                {
                    MessageBox.Show("todos los campos son obligatorios", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // detenemos la ejecucion
                }



                if (articulos == null)

                    articulos = new Articulos();

                articulos.Codigo = (txtCodigo.Text);
                articulos.Nombre = txtNombre.Text;
                articulos.Descripcion = txtDescripcion.Text;
                articulos.Tipo = (Catalogo)cboCategoria.SelectedItem;
                articulos.UrlImagen = txtImagen.Text;
                articulos.marca = (Marcas)cboMarca.SelectedItem;
                articulos.Precio = Convert.ToDecimal(txtPrecio.Text);

                if (articulos.id != 0)
                {
                 
                    negocio.Modicar(articulos);
                    MessageBox.Show("modificado correctamente");
                }
                else
                {
                    negocio.agregar(articulos);

                    MessageBox.Show("Agregado exitosamente");

                }
              

                Close();    

                }
                catch (Exception)
                {

                    throw;
                }

            }

            private void txtImagen_TextChanged(object sender, EventArgs e)
            {
                cargarimagen(txtImagen.Text);
            }

            private void cargarimagen(string imagen)
            {
                try
                {
                    pbxArticulo.Load(imagen);
                }
                catch (Exception ex)
                {
                    pbxArticulo.Load("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTOwRConBYl2t6L8QMOAQqa5FDmPB_bg7EnGA&s");
                }
            }

            private void AltaArticulo_Load(object sender, EventArgs e)
            {
                catalogoNegocio negocio = new catalogoNegocio();
                MarcaNegocio marcas = new MarcaNegocio();

                try
                {
                    cboMarca.DataSource = marcas.listar();
                    cboMarca.DisplayMember = "DescripcionMarca";  // Esto asegura que se muestre la descripción
                    cboMarca.ValueMember = "Id";

                     cboCategoria.DataSource = negocio.listar();
                     cboCategoria.DisplayMember = "DescripcionCatalogo";  
                     cboCategoria.ValueMember = "Id";


                if (articulos != null)
                {
                    txtCodigo.Text = articulos.Codigo;
                    txtNombre.Text = articulos.Nombre;
                    txtDescripcion.Text = articulos.Descripcion;
                    txtImagen.Text = articulos.UrlImagen;
                    cargarimagen(articulos.UrlImagen);

                    cboCategoria.SelectedValue = articulos.Tipo.Id;
                    cboMarca.SelectedValue = articulos.marca.Id;


                    txtPrecio.Text = articulos.Precio > 0 ? articulos.Precio.ToString("0.00") : "0.00";


                }
                if (this.Text.Contains("Detalle")) 
                {
                    btnAceptar.Visible = false;
                    BtnCancelar.Visible = false;
                }
            

            }
            catch (Exception ex)
                {

                    MessageBox.Show(ex.ToString()); 
                }
            }

        private void BtnAgregarImagen_Click(object sender, EventArgs e)
        {
            Archivo = new OpenFileDialog();
            
            Archivo.Filter = "jpg|*.jpg;|png|*.png";
            Archivo.ShowDialog();
            if(Archivo.ShowDialog() == DialogResult.OK)
            {
                txtImagen.Text = Archivo.FileName;  
                cargarimagen(Archivo.FileName); 
            }
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {

            if(!char.IsNumber(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != '.')
            {
                e.Handled = true;
             
            }
        }

        private void txtImagen_Leave(object sender, EventArgs e)
        {

            cargarimagen(txtImagen.Text);


        }
    }
    }


    