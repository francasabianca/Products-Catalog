using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio;
using Dominio;

namespace Catalogo
{
    public partial class frmNuevoArticulo : Form
    {
        Articulo articulo = null;

        Helper help = new Helper();
        public frmNuevoArticulo()
        {
            InitializeComponent();
        }
        public frmNuevoArticulo(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
            Text = "Modificar artículo";
        }
        private void frmNuevoArticulo_Load(object sender, EventArgs e)
        {
            MarcaNegocio marca = new MarcaNegocio();
            cboMarca.DataSource = marca.listar();
            cboMarca.ValueMember = "Id";
            cboMarca.DisplayMember = "Descripcion";
            CategoriaNegocio categoria = new CategoriaNegocio();
            cboCategoria.DataSource = categoria.listar();
            cboCategoria.ValueMember = "Id";
            cboCategoria.DisplayMember = "Descripcion";

            if (articulo != null)
            {
                txtCodigo.Text = articulo.Codigo;
                txtNombre.Text = articulo.Nombre;
                txtDescripcion.Text = articulo.Descripcion;
                cboMarca.SelectedValue = articulo.Marca.Id;
                cboCategoria.SelectedValue = articulo.Categoria.Id;
                txtUrlImagen.Text = articulo.urlImagen;
                txtPrecio.Text = articulo.Precio.ToString();
                help.cargarImagen(pcbUrlNuevo, articulo.urlImagen);
            }         
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio aux = new ArticuloNegocio();

            try
            {
                if ((help.validarCampos(txtCodigo.Text, txtNombre.Text, txtDescripcion.Text, txtPrecio.Text)))
                    return;

                if (articulo == null)
                    articulo = new Articulo();              

                articulo.Codigo = txtCodigo.Text;
                articulo.Nombre = txtNombre.Text;
                articulo.Descripcion = txtDescripcion.Text;
                articulo.Marca = (Marca)cboMarca.SelectedItem;
                articulo.Categoria = (Categoria)cboCategoria.SelectedItem;
                articulo.urlImagen = txtUrlImagen.Text;
                articulo.Precio = decimal.Parse(txtPrecio.Text);


                if (articulo.ID != 0)
                {
                    aux.modificarArticulo(articulo);
                    MessageBox.Show("Modificado exitosamente");
                }
                else
                {
                    aux.agregarArticulo(articulo);
                    MessageBox.Show("Agregado exitosamente");
                }
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }           
        }
        private void txtUrlImagen_Leave(object sender, EventArgs e)
        {          
                help.cargarImagen(pcbUrlNuevo, txtUrlImagen.Text);
        }      
    }
}
