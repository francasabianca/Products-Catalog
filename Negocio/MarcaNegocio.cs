using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class MarcaNegocio
    {
        List<Marca> lista = new List<Marca>();
        AccesoDatos datos = new AccesoDatos();
        public List<Marca> listar()
        {
            datos.setearConsulta("select Id, Descripcion from MARCAS");
            datos.ejectuarLectura();

            try
            {
                while (datos.Lector.Read())
                {
                    Marca marca = new Marca();
                    marca.Id = (int)datos.Lector["Id"];
                    marca.Descripcion = (string)datos.Lector["Descripcion"];
                    lista.Add(marca);
                }
                return lista;
            }
            catch (Exception)
            {
                throw;
            }  
        }
    }
}
