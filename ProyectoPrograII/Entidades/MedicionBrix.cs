using Microsoft.Data.SqlClient;
using ProyectoPrograII.DatosAcceso;
using ProyectoPrograII.Entidades;
using ProyectoPrograII.Interfaces;
using ProyectoPrograII.Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoPrograII.Entidades
{
    internal class MedicionBrix : IClasificacion
    {
        private string identificadorTolva;
        private decimal[] muestrasBrix;
        private decimal promedioBrix;
        private string destinoFinal;

        public MedicionBrix()
        {
            identificadorTolva = string.Empty;
            muestrasBrix = new decimal[5];
            promedioBrix = 0;
            destinoFinal = string.Empty;
        }


        public string IdentificadorTolva
        {
            get => identificadorTolva;
            set => identificadorTolva = value;
        }

        public decimal[] MuestrasBrix
        {
            get => muestrasBrix;
            set => muestrasBrix = value;
        }

        public decimal PromedioBrix
        {
            get => promedioBrix;
            set => promedioBrix = value;
        }

        public string DestinoFinal
        {
            get => destinoFinal;
            set => destinoFinal = value;
        }

        public decimal CalcularPromedioBrix(decimal[] muestras)
        {
            if (muestras == null || muestras.Length != 5)
            {
                throw new ArgumentException("Debe proporcionar exactamente 5 muestras.");
            }

            decimal suma = 0;
            foreach (decimal muestra in muestras)
            {
                if (muestra < 0)
                {
                    throw new ArgumentException("Los valores de Brix no pueden ser negativos.");
                }
                suma += muestra;
            }

            return Math.Round(suma / 5, 2);
        }

        public string ClasificarDestino(decimal promedioBrix)
        {
            string destino = "Sin clasificar";

            try
            {
                using (SqlConnection con = Conexion.GetConexion())
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    string query = @"SELECT destino 
                                    FROM ParametrosBrix 
                                    WHERE @promedioBrix >= rangoMinimo 
                                    AND @promedioBrix <= rangoMaximo";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@promedioBrix", promedioBrix);

                        object resultado = cmd.ExecuteScalar();
                        if (resultado != null)
                        {
                            destino = resultado.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al clasificar destino: {ex.Message}");
            }

            return destino;
        }

        public bool ProcesarMedicionCompleta(string IdentificadorTolva, decimal[] muestras)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(IdentificadorTolva))
                {
                    throw new ArgumentException("El identificador de tolva no puede estar vacío.");
                }

                this.IdentificadorTolva = IdentificadorTolva;
                this.MuestrasBrix = muestras;
                this.PromedioBrix = CalcularPromedioBrix(muestras);
                this.DestinoFinal = ClasificarDestino(this.PromedioBrix);

                bool medicionesGuardadas = GuardarMediciones();
                bool clasificacionGuardada = GuardarClasificacion();

                return medicionesGuardadas && clasificacionGuardada;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al procesar medición: {ex.Message}");
                return false;
            }
        }

        private bool GuardarMediciones()
        {
            try
            {
                using (SqlConnection con = Conexion.GetConexion())
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    string query = @"INSERT INTO MedicionesBrix (numero_muestra, grados_brix, fecha_medicion) 
                                    VALUES (@numero_muestra, @grados_brix, GETDATE())";

                    for (int i = 0; i < muestrasBrix.Length; i++)
                    {
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@numero_muestra", i + 1);
                            cmd.Parameters.AddWithValue("@grados_brix", muestrasBrix[i]);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar mediciones: {ex.Message}");
                return false;
            }
        }

        private bool GuardarClasificacion()
        {
            try
            {
                using (SqlConnection con = Conexion.GetConexion())
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    string query = @"INSERT INTO ClasificacionesFinales 
                                    (identificador_tolva, promedio_brix, destino, fecha_clasificacion) 
                                    VALUES (@identificador_tolva, @promedio_brix, @destino, GETDATE())";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@identificador_tolva", this.IdentificadorTolva);
                        cmd.Parameters.AddWithValue("@promedio_brix", this.PromedioBrix);
                        cmd.Parameters.AddWithValue("@destino", this.DestinoFinal);

                        int filasAfectadas = cmd.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar clasificación: {ex.Message}");
                return false;
            }
        }


        public void MostrarResultados()
        {
            Console.WriteLine("\n--------------------------------------------------------");
            Utilidades.EscribirConColor("-          RESULTADOS DE MEDICIÓN BRIX                   -", ConsoleColor.DarkCyan);
            Console.WriteLine("\n-----------------------------------------------------------");
            Console.WriteLine($"  Número de Serie Tolva: {IdentificadorTolva}");
            Console.WriteLine($"  Fecha: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
            Console.WriteLine($"  Promedio Brix: {PromedioBrix:F2}°Brix");
            Utilidades.EscribirConColor($"  Destino: {DestinoFinal}", ConsoleColor.Cyan);
            Console.WriteLine("\n----------------------------------------------------------\n");
        }


       
    }
}
