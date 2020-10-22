using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using System.Data;

namespace CapaNegocio
{
    public class NCategoria
    {
        public static string Insertar(string nombre, string descripcion)
        {
            DCategoria dCategoria = new DCategoria();
            dCategoria.Nombre = nombre;
            dCategoria.Descripcion = descripcion;

            return dCategoria.Insertar(dCategoria); 
        }

        public static string Modificar(int idcategoria,string nombre, string descripcion)
        {
            DCategoria dCategoria = new DCategoria();
            dCategoria.IdCategoria = idcategoria;
            dCategoria.Nombre = nombre;
            dCategoria.Descripcion = descripcion;

            return dCategoria.Modificar(dCategoria);
        }

        public static string Eliminar(int idcategoria)
        {
            DCategoria dCategoria = new DCategoria();
            dCategoria.IdCategoria = idcategoria;
        
            return dCategoria.Eliminar(dCategoria);
        }


        public static DataTable Consultar()
        {
            return new DCategoria().Consultar();
        }

        public static DataTable ConsultarNombre(string textobuscar)
        {
            DCategoria dCategoria = new DCategoria();
            dCategoria.TextoBuscar = textobuscar;

            return dCategoria.ConsultarNombre(dCategoria); 
        }

    }
}
