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
    public partial class frmCatalogo : Form
    {
        private List<Articulo> listaArticulos = new List<Articulo>(); 
        
        private Articulo articulo = new Articulo();

        private Helper helper = new Helper();

        private bool filtroSeleccionado = false;
        public frmCatalogo()
        {
            InitializeComponent();
        }               
        private void frmCatalogo_Load(object sender, EventArgs e)
        {
            try
            {
                helper.cargarCatalogo(dgvArticulos);
                helper.ocultarColumnas(dgvArticulos, "UrlImagen", "ID");
                helper.cargarImagen(pcbUrlImagen);
                helper.cargarItems(cboPrecio, cboMarca, cboCategoria);          
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }      
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmNuevoArticulo aux = new frmNuevoArticulo();
            aux.ShowDialog();
            helper.cargarCatalogo(dgvArticulos);
        }
        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow != null)
            {
                articulo = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                frmNuevoArticulo aux = new frmNuevoArticulo(articulo);
                aux.ShowDialog();
                helper.cargarCatalogo(dgvArticulos);
            }
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Desea eliminar el item seleecionado?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (respuesta == DialogResult.Yes)
            {
                articulo = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                ArticuloNegocio aux = new ArticuloNegocio();
                aux.eliminarArticulo(articulo.ID);
                helper.cargarCatalogo(dgvArticulos);
            }                          
        }
        private void btnDetalle_Click(object sender, EventArgs e)
        {
            articulo = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Codigo: " + articulo.Codigo);
            sb.AppendLine("Nombre: " + articulo.Nombre);
            sb.AppendLine("Descripción: " + articulo.Descripcion);
            sb.AppendLine("Marca: " + articulo.Marca.Descripcion);
            sb.AppendLine("Categoria :" + articulo.Categoria.Descripcion);          
            sb.AppendLine("Precio: " + articulo.Precio.ToString());

            MessageBox.Show(sb.ToString(), "Detalles del artículo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio articulo = new ArticuloNegocio();

            if ((helper.validarFiltro(cboPrecio, txtFiltro.Text)))
                return;

            string filtro = txtFiltro.Text;
            string precio = cboPrecio.SelectedItem.ToString();
            string marca = cboMarca.SelectedItem.ToString();
            string categoria = cboCategoria.SelectedItem.ToString();

            helper.validarBusqueda(ref precio, ref marca, ref categoria);

            listaArticulos = articulo.filtrarArticulo(precio, marca, categoria, filtro);

            helper.cargarCatalogo(dgvArticulos, listaArticulos);
        }
        private void btnFiltroAvanzado_Click(object sender, EventArgs e)
        {
            filtroSeleccionado = true;
            helper.habilitarDeshabilitarBotones(btnFiltroAvanzado, btnFiltroRapido, btnBuscar, txtFiltro, cboPrecio, cboMarca, cboCategoria, filtroSeleccionado);
        }
        private void btnFiltroRapido_Click(object sender, EventArgs e)
        {
            filtroSeleccionado = false;
            helper.habilitarDeshabilitarBotones(btnFiltroRapido, btnFiltroAvanzado, btnBuscar, txtFiltro, cboPrecio, cboMarca, cboCategoria, filtroSeleccionado);
        }
        private void cboPrecio_DropDownClosed(object sender, EventArgs e)
        {
            if (cboPrecio.SelectedIndex == -1)
                cboPrecio.Items.Clear();
            if (cboMarca.SelectedIndex == -1)
                cboMarca.Items.Clear();
            if (cboCategoria.SelectedIndex == -1)
                cboCategoria.Items.Clear();
        }
        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            if (filtroSeleccionado == false)
            {
            helper.filtrarRapido(dgvArticulos, txtFiltro.Text);
            helper.ocultarColumnas(dgvArticulos, "UrlImagen", "ID");
            }
        }
       
        private void cboPrecio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPrecio.SelectedIndex == 0)
                txtFiltro.Enabled = false;
            else
                txtFiltro.Enabled = true;
        }
        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow != null)
            {
                articulo = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                helper.cargarImagen(pcbUrlImagen, articulo.urlImagen);
            }
        }
        
    }
}
