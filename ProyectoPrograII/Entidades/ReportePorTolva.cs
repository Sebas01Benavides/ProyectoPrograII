using Microsoft.Data.SqlClient;
using ProyectoPrograII.DatosAcceso;
using ProyectoPrograII.Interfaces;
using ProyectoPrograII.Servicios;
using System;
using System.Data;

namespace ProyectoPrograII.Entidades
{
    internal class ReportePorTolva : IReporte
    {
        private string numeroTolva;

        public ReportePorTolva(string numeroTolva)
        {
            this.numeroTolva = numeroTolva;
        }

        public void GenerarReporte()
        {
            Console.Clear();
            Console.WriteLine("=======================================================");
            Utilidades.EscribirConColor("REPORTE POR TOLVA", ConsoleColor.DarkCyan);
            Console.WriteLine("\n=======================================================");

            SqlConnection con = Conexion.GetConexion();

            // buscar la tolva
            string query = @"SELECT identificador_tolva, promedio_brix, destino, fecha_clasificacion
                            FROM ClasificacionesFinales
                            WHERE identificador_tolva = @tolva";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@tolva", numeroTolva);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                string tolva = dr["identificador_tolva"].ToString();
                decimal brix = Convert.ToDecimal(dr["promedio_brix"]);
                string destino = dr["destino"].ToString();
                DateTime fecha = Convert.ToDateTime(dr["fecha_clasificacion"]);

                Console.WriteLine($"\nTolva: {tolva}");
                Console.WriteLine($"Promedio Brix: {brix:F2}°Brix");
                Console.WriteLine($"Destino: {destino}");
                Console.WriteLine($"Fecha: {fecha:dd/MM/yyyy HH:mm:ss}");
            }
            else
            {
                Console.WriteLine($"\nNo se encontro la tolva: {numeroTolva}");
            }

            dr.Close();
            con.Close();

            Console.WriteLine("\n=======================================================");
        }
    }
}
