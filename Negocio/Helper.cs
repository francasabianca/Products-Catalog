using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class Helper
    {
        AccesoDatos datos = new AccesoDatos();
        
        public void mapeoArticulo(List<Articulo> lista)
        {
            datos.setearConsulta("select a.id, Codigo, Nombre, a.Descripcion Descripcion,  ImagenUrl, Precio, a.IdMarca, m.Descripcion Marca, a.IdCategoria, c.Descripcion Categoria, m.Id, c.Id from ARTICULOS a, MARCAS m, CATEGORIAS c where a.IdMarca = m.Id and a.IdCategoria = c.Id");
            datos.ejectuarLectura();

            while (datos.Lector.Read())
            {
                Articulo aux = new Articulo();
                aux.ID = (int)datos.Lector["id"];
                aux.Codigo = (string)datos.Lector["Codigo"];
                aux.Nombre = (string)datos.Lector["Nombre"];
                aux.Descripcion = (string)datos.Lector["Descripcion"];
                if (!(datos.Lector["ImagenUrl"] is DBNull))
                    aux.urlImagen = (string)datos.Lector["ImagenUrl"];
                aux.Precio = (decimal)datos.Lector["Precio"];
                aux.Marca = new Marca();
                aux.Marca.Id = (int)datos.Lector["IdMarca"];
                aux.Marca.Descripcion = (string)datos.Lector["Marca"];
                aux.Categoria = new Categoria();
                aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];

                lista.Add(aux);
            }

        }
        public void mapeoArticulo(List<Articulo> lista, string consulta)
        {
            datos.setearConsulta(consulta);
            datos.ejectuarLectura();

            while (datos.Lector.Read())
            {
                Articulo aux = new Articulo();
                aux.ID = (int)datos.Lector["id"];
                aux.Codigo = (string)datos.Lector["Codigo"];
                aux.Nombre = (string)datos.Lector["Nombre"];
                aux.Descripcion = (string)datos.Lector["Descripcion"];
                if (!(datos.Lector["ImagenUrl"] is DBNull))
                    aux.urlImagen = (string)datos.Lector["ImagenUrl"];
                aux.Precio = (decimal)datos.Lector["Precio"];
                aux.Marca = new Marca();
                aux.Marca.Id = (int)datos.Lector["IdMarca"];
                aux.Marca.Descripcion = (string)datos.Lector["Marca"];
                aux.Categoria = new Categoria();
                aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];

                lista.Add(aux);
            }
        }
        public string filtrar(string precio, string marca, string categoria, string filtro, string consulta)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(consulta);

            if (marca != null)
            {
                sb.Append(" and m.Descripcion = '" + marca + "'");               
            }
            if (categoria != null)
            {
                sb.Append(" and c.Descripcion = '" + categoria + "'");               
            }
            if (precio != null)
            {
                if (precio == "Es mayor a")
                {
                    sb.Append(" and Precio > " + filtro);
                }
                else
                {
                    sb.Append(" and Precio < " + filtro);
                }
            }
            consulta = sb.ToString();

            return consulta;
        }
    }
}
