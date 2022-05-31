using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class ArticuloNegocio
    {
        List<Articulo> lista = new List<Articulo>();
        AccesoDatos datos = new AccesoDatos();
        Helper helper = new Helper();
        public List<Articulo> listarArticulo()
        {
            try
            {
                helper.mapeoArticulo(lista);             
                return lista;
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
        public void agregarArticulo(Articulo nuevo)
        {
            try
            {
                datos.setearConsulta("insert into ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio) values (@Codigo, @Nombre, @Descripcion, @IdMarca, @IdCategoria, @ImagenUrl, @Precio)");
                datos.setearParametros("@Codigo", nuevo.Codigo);
                datos.setearParametros("@Nombre", nuevo.Nombre);
                datos.setearParametros("@Descripcion", nuevo.Descripcion);
                datos.setearParametros("@IdMarca", nuevo.Marca.Id);
                datos.setearParametros("@IdCategoria", nuevo.Categoria.Id);
                datos.setearParametros("@ImagenUrl", nuevo.urlImagen);
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
        public void eliminarArticulo(int id)
        {
            try
            {
                datos.setearConsulta("delete from Articulos where id = @ID");
                datos.setearParametros("@ID", id);
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
        public void modificarArticulo(Articulo modificado)
        {
            try
            {
            datos.setearConsulta("update ARTICULOS set Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, IdMarca = @IdMarca, IdCategoria = @IdCategoria, ImagenUrl = @ImagenUrl, Precio = @Precio where id = @ID");
            datos.setearParametros("@Codigo", modificado.Codigo);
            datos.setearParametros("@Nombre", modificado.Nombre);
            datos.setearParametros("@Descripcion", modificado.Descripcion);
            datos.setearParametros("@IdMarca", modificado.Marca.Id);
            datos.setearParametros("@IdCategoria", modificado.Categoria.Id);
            datos.setearParametros("@ImagenUrl", modificado.urlImagen);
            datos.setearParametros("@Precio", modificado.Precio);
            datos.setearParametros("@ID", modificado.ID);
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
        public List<Articulo> filtrarArticulo(string precio, string marca, string categoria, string filtro)
        {
            try
            {
                string consulta = "select a.id, Codigo, Nombre, a.Descripcion Descripcion,  ImagenUrl, Precio, a.IdMarca, m.Descripcion Marca, a.IdCategoria, c.Descripcion Categoria, m.Id, c.Id from ARTICULOS a, MARCAS m, CATEGORIAS c where a.IdMarca = m.Id and a.IdCategoria = c.Id ";

                consulta = helper.filtrar(precio, marca, categoria, filtro, consulta);
           
                helper.mapeoArticulo(lista, consulta);

                return lista;
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
