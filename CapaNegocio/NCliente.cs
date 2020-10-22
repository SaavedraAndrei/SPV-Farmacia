using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using System.Data;

namespace CapaNegocio
{
    public class NCliente
    {
        public static string Insertar(string nombre, string apellidos, string sexo, DateTime fecha_nac,
            string tipo_doc, string num_doc, string direccion, string telefono, string email)
        {
            DCliente cliente = new DCliente();
            cliente.Nombre = nombre;
            cliente.Apellidos = apellidos;
            cliente.Sexo = sexo;
            cliente.Fecha_Nac = fecha_nac;
            cliente.Tipo_Doc = tipo_doc;
            cliente.Num_Doc = num_doc;
            cliente.Direccion = direccion;
            cliente.Telefono = telefono;
            cliente.Email = email;

            return cliente.Insertar(cliente);
        }

        public static string Modificar(int idcliente, string nombre, string apellidos, string sexo, DateTime fecha_nac,
            string tipo_doc, string num_doc, string direccion, string telefono, string email)
        {
            DCliente cliente = new DCliente();
            cliente.IdCliente = idcliente;
            cliente.Nombre = nombre;
            cliente.Apellidos = apellidos;
            cliente.Sexo = sexo;
            cliente.Fecha_Nac = fecha_nac;
            cliente.Tipo_Doc = tipo_doc;
            cliente.Num_Doc = num_doc;
            cliente.Direccion = direccion;
            cliente.Telefono = telefono;
            cliente.Email = email;

            return cliente.Modificar(cliente);
        }

        public static string Eliminar(int idcliente)
        {
            DCliente cliente = new DCliente();
            cliente.IdCliente = idcliente;

            return cliente.Eliminar(cliente);
        }


        public static DataTable Consultar()
        {
            return new DCliente().Consultar();
        }

        public static DataTable ConsultarApellidos(string textobuscar)
        {
            DCliente cliente = new DCliente();
            cliente.TextoBuscar = textobuscar;

            return cliente.ConsultarApellidos(cliente);
        }

        public static DataTable ConsultarDocumento(string textobuscar)
        {
            DCliente cliente = new DCliente();
            cliente.TextoBuscar = textobuscar;

            return cliente.ConsultarDocumento(cliente);
        }

    }
}
