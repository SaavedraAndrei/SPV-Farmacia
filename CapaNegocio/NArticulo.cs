using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using System.Data;

namespace CapaNegocio
{
    public class NArticulo
    {
        public static string Insertar(string codigo, string nombre,string descripcion, byte[] imagen, int idcategoria, int idpresentacion)
        {
            DArticulo dArticulo= new DArticulo();
            dArticulo.Codigo = codigo;
            dArticulo.Nombre = nombre;
            dArticulo.Descripcion = descripcion;
            dArticulo.Imagen = imagen;
            dArticulo.IdCategoria =idcategoria;
            dArticulo.IdPresentacion = idpresentacion;

            return dArticulo.Insertar(dArticulo);
        }

        public static string Modificar(int idarticulo, string codigo, string nombre, 
            string descripcion, byte[] imagen, int idcategoria, int idpresentacion)
        {
            DArticulo dArticulo = new DArticulo();
            dArticulo.IdArticulo = idarticulo;
            dArticulo.Codigo = codigo;
            dArticulo.Nombre = nombre;
            dArticulo.Descripcion = descripcion;
            dArticulo.Imagen = imagen;
            dArticulo.IdCategoria = idcategoria;
            dArticulo.IdPresentacion = idpresentacion;

            return dArticulo.Modificar(dArticulo);
        }

        public static string Eliminar(int idarticulo)
        {
            DArticulo dArticulo= new DArticulo();
            dArticulo.IdArticulo = idarticulo;

            return dArticulo.Eliminar(dArticulo);
        }


        public static DataTable Consultar()
        {
            return new DArticulo().Consultar();
        }

        public static DataTable ConsultarNombre(string textobuscar)
        {
            DArticulo dArticulo = new DArticulo();
            dArticulo.TextoBuscar = textobuscar;

            return dArticulo.ConsultarNombre(dArticulo);
        }


    }

}
