using Microsoft.Data.SqlClient;
using ProyectoPrograII.DatosAcceso;
using ProyectoPrograII.Interfaces;
using ProyectoPrograII.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoPrograII.Entidades
{
    internal class ReportePorFecha : IReporte
    {
        private DateTime fecha;  // Solo una fecha

        public ReportePorFecha(DateTime fecha)  // Solo recibe una fecha
        {
            this.fecha = fecha;
        }

        public void GenerarReporte()
        {
            Console.Clear();
            Console.WriteLine("=======================================================");
            Utilidades.EscribirConColor("REPORTE POR FECHA", ConsoleColor.DarkCyan);
            Console.WriteLine("\n=======================================================");
            Console.WriteLine($"Fecha: {fecha:dd/MM/yyyy}");  // Solo muestra una fecha
            Console.WriteLine("=======================================================\n");

            SqlConnection con = Conexion.GetConexion();

            string query = @"SELECT identificador_tolva, promedio_brix, destino, fecha_clasificacion
                            FROM ClasificacionesFinales
                            WHERE CAST(fecha_clasificacion AS DATE) = @fecha
                            ORDER BY fecha_clasificacion DESC";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@fecha", fecha.Date);  // Solo una fecha

            SqlDataReader dr = cmd.ExecuteReader();

            int total = 0;
            int exportacion = 0;
            int jugo = 0;
            int descarte = 0;

            Console.WriteLine("--- CLASIFICACIONES ---\n");

            while (dr.Read())
            {
                total++;
                string tolva = dr["identificador_tolva"].ToString();
                decimal brix = Convert.ToDecimal(dr["promedio_brix"]);
                string destino = dr["destino"].ToString();
                DateTime fechaClasif = Convert.ToDateTime(dr["fecha_clasificacion"]);

                // contar por destino
                if (destino == "Exportacion") exportacion++;
                if (destino == "Jugo") jugo++;
                if (destino == "Descarte") descarte++;

                Console.WriteLine($"Tolva: {tolva}");
                Console.WriteLine($"Brix: {brix:F2}°");
                Console.WriteLine($"Destino: {destino}");
                Console.WriteLine($"Fecha: {fechaClasif:dd/MM/yyyy HH:mm}");
                Console.WriteLine("-------------------------------------------------------");
            }

            dr.Close();
            con.Close();

            // mostrar totales
            Console.WriteLine("\n=======================================================");
            Console.WriteLine($"Total procesadas: {total}");
            Console.WriteLine($"Exportacion: {exportacion}");
            Console.WriteLine($"Jugo: {jugo}");
            Console.WriteLine($"Descarte: {descarte}");
            Console.WriteLine("=======================================================");
        }
    }
}