using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Negocio;
using Dominio;

namespace Catalogo
{
    public class Helper
    {
        private List<Articulo> listaArticulos = new List<Articulo>();
        public void cargarCatalogo(DataGridView aux)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            listaArticulos = negocio.listarArticulo();
            aux.DataSource = listaArticulos;
        }
        public void cargarCatalogo(DataGridView aux, List<Articulo> lista)
        {
            aux.DataSource = lista;
        }
        public void cargarItems(ComboBox aux, ComboBox aux1, ComboBox aux2)
        {
            MarcaNegocio marca = new MarcaNegocio();
            CategoriaNegocio categoria = new CategoriaNegocio();           

            aux.Items.Add("PRECIO");
            aux.Items.Add("Es mayor a");
            aux.Items.Add("Es menor a");
            aux.SelectedIndex = 0;

            aux1.Items.Add("MARCAS");
            aux1.Items.Add("Samsung");
            aux1.Items.Add("Apple");
            aux1.Items.Add("Sony");
            aux1.Items.Add("Huawei");
            aux1.Items.Add("Motorola");
            aux1.SelectedIndex = 0;

            aux2.Items.Add("CATEGORIAS");
            aux2.Items.Add("Celulares");
            aux2.Items.Add("Televisores");
            aux2.Items.Add("Media");
            aux2.Items.Add("Audio");
            aux2.SelectedIndex = 0;
        }

        public void habilitarDeshabilitarBotones(Button activo, Button inactivo, Button buscar, TextBox filtro, ComboBox aux, ComboBox aux1, ComboBox aux2, bool a)
        {
            activo.BackColor = Color.DarkGray;
            inactivo.BackColor = Color.LightGray;
            
            
            if (a == true)
            {
                buscar.Enabled = true;
                aux.Enabled = true;
                aux1.Enabled = true;
                aux2.Enabled = true;

                if (aux.SelectedIndex == 0)
                    filtro.Enabled = false;

                else
                    filtro.Enabled = true;
            }
            else
            {
                filtro.Enabled = true;
                buscar.Enabled = false;
                aux.Enabled = false;
                aux1.Enabled = false;
                aux2.Enabled = false;
            }
        }             
        public void ocultarColumnas (DataGridView aux, string columna1, string columna2)
        {              
            aux.Columns[columna1].Visible = false;
            aux.Columns[columna2].Visible = false;               
        }
        public void cargarImagen(PictureBox imagen)
        {
            if(listaArticulos[0].urlImagen != null)
            try
            {
                imagen.Load(listaArticulos[0].urlImagen);
            }
            catch (Exception)
            {
                imagen.Load("https://th.bing.com/th/id/OIP.qH3-RRsFjbcg2ch2Rly4UAHaER?pid=ImgDet&rs=1");
            }
            else
                imagen.Load("https://th.bing.com/th/id/OIP.qH3-RRsFjbcg2ch2Rly4UAHaER?pid=ImgDet&rs=1");

        }
        public void cargarImagen(PictureBox imagen, string url)
        {
            if (url != null)
                try
                {
                    imagen.Load(url);
                }
                catch (Exception)
                {
                    imagen.Load("https://th.bing.com/th/id/OIP.qH3-RRsFjbcg2ch2Rly4UAHaER?pid=ImgDet&rs=1");
                }
            else
                imagen.Load("https://th.bing.com/th/id/OIP.qH3-RRsFjbcg2ch2Rly4UAHaER?pid=ImgDet&rs=1");
        }
        public void filtrarRapido(DataGridView aux, string filtro)
        {
            List<Articulo> listaFiltrada;          

            if (filtro != null)
            {
                listaFiltrada = listaArticulos.FindAll(x => x.Nombre.ToUpper().Contains(filtro.ToUpper()) || x.Descripcion.ToUpper().Contains(filtro.ToUpper()) || x.Marca.Descripcion.ToUpper().Contains(filtro.ToUpper()) || x.Categoria.Descripcion.ToUpper().Contains(filtro.ToUpper()));
            }
            else
            {
                listaFiltrada = listaArticulos;
            }
            aux.DataSource = null;
            aux.DataSource = listaFiltrada;
        }
        public bool soloNumeros(string texto)
        {
            foreach (char caracter in texto)
            {
                if (!(char.IsNumber(caracter)))
                {
                    return false;
                }
            }
            return true;
        }
        public bool soloNumeros(string texto, char a)
        {
            foreach (char caracter in texto)
            {
                if (!(char.IsNumber(caracter)) && !(caracter == a))
                {
                    return false;
                }
            }
            return true;
        }
        public bool validarFiltro(ComboBox Precio, string filtro)
        {

            if (Precio.SelectedIndex > 0 && string.IsNullOrEmpty(filtro) || soloNumeros(filtro) == false)
            {
                MessageBox.Show("Ingrese un valor numérico en el campo de texto", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }            
            return false;
        }
        public void validarBusqueda(ref string precio, ref string marca, ref string categoria)
        {
            if (precio == "PRECIO")
                precio = null;
            if (marca == "MARCAS")
                marca = null;
            if (categoria == "CATEGORIAS")
                categoria = null;
        }
        public bool validarCampos(string codigo, string nombre, string descripcion, string precio)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("");

            if (string.IsNullOrEmpty(codigo))          
                sb.AppendLine(". Código");         
            if (string.IsNullOrEmpty(nombre))           
                sb.AppendLine(". Nombre");          
            if (string.IsNullOrEmpty(descripcion))        
                sb.AppendLine(". Descripción");          
            if (string.IsNullOrEmpty(precio) || soloNumeros(precio, ',') == false)
                sb.AppendLine(". Precio");
            if (sb.Length > 2)
            {                
                MessageBox.Show("Se deben corregir / rellenar los siguientes campos: " + sb.ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            return false;
        }
    }
}
