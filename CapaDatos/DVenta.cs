using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class DVenta
    {
        private int _idVenta;
        private int _idCliente;
        private int _idTrabajador;
        private DateTime _Fecha;
        private string _Tipo_Comprobante;
        private string _Serie;
        private string _Correlativo;
        private decimal _Igv;

        public int IdVenta { get => _idVenta; set => _idVenta = value; }
        public int IdCliente { get => _idCliente; set => _idCliente = value; }
        public int IdTrabajador { get => _idTrabajador; set => _idTrabajador = value; }
        public DateTime Fecha { get => _Fecha; set => _Fecha = value; }
        public string Tipo_Comprobante { get => _Tipo_Comprobante; set => _Tipo_Comprobante = value; }
        public string Serie { get => _Serie; set => _Serie = value; }
        public string Correlativo { get => _Correlativo; set => _Correlativo = value; }
        public decimal Igv { get => _Igv; set => _Igv = value; }

        public DVenta()
        {

        }

        public DVenta(int idventa, int idcliente, int idtrabajador,DateTime fecha, string tipo_comprobante
            , string serie, string correlativo, decimal igv)
        {
            this.IdVenta = idventa;
            this.IdCliente = idcliente;
            this.IdTrabajador = idtrabajador;
            this.Fecha = fecha;
            this.Tipo_Comprobante = tipo_comprobante;
            this.Serie = serie;
            this.Correlativo = correlativo;
            this.Igv = igv;
        }

        public string DisminuirStock(int iddetalle_ingreso, int cantidad)
        {
            string rpta = string.Empty;
            SqlConnection SqlCn = new SqlConnection();

            try
            {
                SqlCn.ConnectionString = Conexion.Cn;
                SqlCn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = SqlCn;
                cmd.CommandText = "SP_DISMINUIR_STOCK";
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paridDetalle_Ingreso = new SqlParameter();
                paridDetalle_Ingreso.ParameterName = "@iddetalle_ingreso";
                paridDetalle_Ingreso.SqlDbType = SqlDbType.Int;
                paridDetalle_Ingreso.Value = iddetalle_ingreso;
                cmd.Parameters.Add(paridDetalle_Ingreso);

                SqlParameter parCantidad = new SqlParameter();
                parCantidad.ParameterName = "@cantidad";
                parCantidad.SqlDbType = SqlDbType.Int;
                parCantidad.Value = cantidad;
                cmd.Parameters.Add(parCantidad);



                rpta = cmd.ExecuteNonQuery() == 1 ? "OK" : "No se actualizó el registro";
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            finally
            {
                if (SqlCn.State == ConnectionState.Open)
                {
                    SqlCn.Close();
                }
            }

            return rpta;

        }


        public string Insertar(DVenta venta, List<DDetalle_Venta> detalle)
        {
            string rpta = string.Empty;
            SqlConnection SqlCn = new SqlConnection();

            try
            {
                SqlCn.ConnectionString = Conexion.Cn;
                SqlCn.Open();

                SqlTransaction SqlTra = SqlCn.BeginTransaction();


                SqlCommand cmd = new SqlCommand();
                cmd.Connection = SqlCn;
                cmd.Transaction = SqlTra;
                cmd.CommandText = "SP_A_TABLA_VENTA";
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paridVenta = new SqlParameter();
                paridVenta.ParameterName = "@idventa";
                paridVenta.SqlDbType = SqlDbType.Int;
                paridVenta.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(paridVenta);

                SqlParameter paridCliente = new SqlParameter();
                paridCliente.ParameterName = "@idcliente";
                paridCliente.SqlDbType = SqlDbType.Int;
                paridCliente.Value = venta.IdCliente;
                cmd.Parameters.Add(paridCliente);


                SqlParameter parIdTrabajador = new SqlParameter();
                parIdTrabajador.ParameterName = "@idtrabajador";
                parIdTrabajador.SqlDbType = SqlDbType.Int;
                parIdTrabajador.Value = venta.IdTrabajador  ;
                cmd.Parameters.Add(parIdTrabajador);

                SqlParameter parFecha = new SqlParameter();
                parFecha.ParameterName = "@fecha";
                parFecha.SqlDbType = SqlDbType.Date;
                parFecha.Value = venta.Fecha;
                cmd.Parameters.Add(parFecha);


                SqlParameter parTipo_Comprobante = new SqlParameter();
                parTipo_Comprobante.ParameterName = "@tipo_comprobante";
                parTipo_Comprobante.SqlDbType = SqlDbType.VarChar;
                parFecha.Size = 20;
                parTipo_Comprobante.Value = venta.Tipo_Comprobante;
                cmd.Parameters.Add(parTipo_Comprobante);

                SqlParameter parSerie = new SqlParameter();
                parSerie.ParameterName = "@serie";
                parSerie.SqlDbType = SqlDbType.VarChar;
                parSerie.Size = 4;
                parSerie.Value = venta.Serie;
                cmd.Parameters.Add(parSerie);

                SqlParameter parCorrelativo = new SqlParameter();
                parCorrelativo.ParameterName = "@correlativo";
                parCorrelativo.SqlDbType = SqlDbType.VarChar;
                parCorrelativo.Size = 7;
                parCorrelativo.Value = venta.Correlativo;
                cmd.Parameters.Add(parCorrelativo);

                SqlParameter parIgv = new SqlParameter();
                parIgv.ParameterName = "@igv";
                parIgv.SqlDbType = SqlDbType.Decimal;
                parIgv.Value = venta.Igv;
                cmd.Parameters.Add(parIgv);


                rpta = cmd.ExecuteNonQuery() == 1 ? "OK" : "No se ingresó el registro";

                if (rpta.Equals("OK"))
                {
                    //obtener el código del ingreso generado
                    this.IdVenta = Convert.ToInt32(cmd.Parameters["@idventa"].Value);
                    foreach (DDetalle_Venta det in detalle)
                    {
                        det.IdVenta = this.IdVenta;
                        //llamar a insertar
                        rpta = det.Insertar(det, ref SqlCn, ref SqlTra);
                        if (!rpta.Equals("OK"))
                        {
                            break;
                        }
                        else
                        {
                            rpta = DisminuirStock(det.IdDetalle_Ingreso, det.Cantidad);
                            if (!rpta.Equals("OK"))
                            {
                                break;  
                            }
                        }
                    }
                }

                if (rpta.Equals("OK"))
                {
                    SqlTra.Commit();
                }
                else
                {
                    SqlTra.Rollback();
                }




            }
            catch (Exception ex)
            {
                rpta = ex.Message;

            }
            finally
            {
                if (SqlCn.State == ConnectionState.Open)
                {
                    SqlCn.Close();
                }
            }

            return rpta;
        }

        public string Eliminar(DVenta venta)
        {
            string rpta = string.Empty;
            SqlConnection SqlCn = new SqlConnection();

            try
            {
                SqlCn.ConnectionString = Conexion.Cn;
                SqlCn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = SqlCn;
                cmd.CommandText = "SP_E_TABLA_VENTA";
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paridVenta = new SqlParameter();
                paridVenta.ParameterName = "@idventa";
                paridVenta.SqlDbType = SqlDbType.Int;
                paridVenta.Value = venta.IdVenta;
                cmd.Parameters.Add(paridVenta);


                rpta = cmd.ExecuteNonQuery() == 1 ? "OK" : "OK";
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            finally
            {
                if (SqlCn.State == ConnectionState.Open)
                {
                    SqlCn.Close();
                }
            }

            return rpta;

        }

        public DataTable Consultar()
        {

            DataTable DtResultado = new DataTable("tblVenta");
            SqlConnection SqlCn = new SqlConnection();

            try
            {
                SqlCn.ConnectionString = Conexion.Cn;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = SqlCn;
                cmd.CommandText = "SP_C_TABLA_VENTA";
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter Dat = new SqlDataAdapter(cmd);
                Dat.Fill(DtResultado);

            }
            catch (Exception ex)
            {

                DtResultado = null;
            }

            return DtResultado;

        }

        //MÉTODO CONSULTAR POR NOMBRE
        public DataTable ConsultarFechas(string TextoBuscar1, string TextoBuscar2)
        {
            DataTable DtResultado = new DataTable("tblVenta");
            SqlConnection SqlCn = new SqlConnection();

            try
            {
                SqlCn.ConnectionString = Conexion.Cn;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = SqlCn;
                cmd.CommandText = "SP_C_FECHA_TABLA_VENTA";
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parTextoBuscar1 = new SqlParameter();
                parTextoBuscar1.ParameterName = "@textobuscar1";
                parTextoBuscar1.SqlDbType = SqlDbType.Date;
                parTextoBuscar1.Size = 40;
                parTextoBuscar1.Value = TextoBuscar1;
                cmd.Parameters.Add(parTextoBuscar1);

                SqlParameter parTextoBuscar2 = new SqlParameter();
                parTextoBuscar2.ParameterName = "@textobuscar2";
                parTextoBuscar2.SqlDbType = SqlDbType.Date;
                parTextoBuscar2.Size = 40;
                parTextoBuscar2.Value = TextoBuscar2;
                cmd.Parameters.Add(parTextoBuscar2);

                SqlDataAdapter SqlDat = new SqlDataAdapter(cmd);
                SqlDat.Fill(DtResultado);



            }
            catch (Exception ex)
            {
                DtResultado = null;

            }

            return DtResultado;
        }


        public DataTable ConsultarDetalle_Venta(string TextoBuscar)
        {
            DataTable DtResultado = new DataTable("tblDetalle_Venta");
            SqlConnection SqlCn = new SqlConnection();

            try
            {
                SqlCn.ConnectionString = Conexion.Cn;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = SqlCn;
                cmd.CommandText = "SP_C_TABLA_DETALLE_VENTA";
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parTextoBuscar = new SqlParameter();
                parTextoBuscar.ParameterName = "@textobuscar";
                parTextoBuscar.SqlDbType = SqlDbType.Int;
                parTextoBuscar.Value = TextoBuscar;
                cmd.Parameters.Add(parTextoBuscar);



                SqlDataAdapter SqlDat = new SqlDataAdapter(cmd);
                SqlDat.Fill(DtResultado);



            }
            catch (Exception ex)
            {
                DtResultado = null;

            }

            return DtResultado;
        }

        public DataTable ConsultarArticulo_Venta_Nombre(string TextoBuscar)
        {
            DataTable DtResultado = new DataTable("tblArticulo");
            SqlConnection SqlCn = new SqlConnection();

            try
            {
                SqlCn.ConnectionString = Conexion.Cn;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = SqlCn;
                cmd.CommandText = "SP_C_ARTICULO_NOMBRE_TABLA_VENTA";
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parTextoBuscar = new SqlParameter();
                parTextoBuscar.ParameterName = "@textobuscar";
                parTextoBuscar.SqlDbType = SqlDbType.VarChar;
                parTextoBuscar.Size = 50;
                parTextoBuscar.Value = TextoBuscar;
                cmd.Parameters.Add(parTextoBuscar);



                SqlDataAdapter SqlDat = new SqlDataAdapter(cmd);
                SqlDat.Fill(DtResultado);



            }
            catch (Exception ex)
            {
                DtResultado = null;

            }

            return DtResultado;
        }


        public DataTable ConsultarArticulo_Venta_Codigo(string TextoBuscar)
        {
            DataTable DtResultado = new DataTable("tblArticulo");
            SqlConnection SqlCn = new SqlConnection();

            try
            {
                SqlCn.ConnectionString = Conexion.Cn;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = SqlCn;
                cmd.CommandText = "SP_C_ARTICULO_CODIGO_TABLA_VENTA";
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parTextoBuscar = new SqlParameter();
                parTextoBuscar.ParameterName = "@textobuscar";
                parTextoBuscar.SqlDbType = SqlDbType.VarChar;
                parTextoBuscar.Size = 50;
                parTextoBuscar.Value = TextoBuscar;
                cmd.Parameters.Add(parTextoBuscar);



                SqlDataAdapter SqlDat = new SqlDataAdapter(cmd);
                SqlDat.Fill(DtResultado);



            }
            catch (Exception ex)
            {
                DtResultado = null;

            }

            return DtResultado;
        }
    }
}
