using Microsoft.Data.SqlClient;
using ProyectoPrograII.DatosAcceso;
using ProyectoPrograII.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoPrograII.Servicios
{
    internal class Utilidades
    {


        
    // Método para escribir texto con color
    public static void EscribirConColor(string texto, ConsoleColor color = ConsoleColor.Cyan)
        {
            ConsoleColor colorOriginal = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(texto);
            Console.ForegroundColor = colorOriginal;
        }


        public static int menu_inicio()
        {
            Console.WriteLine("----------------------------------------------------------");
            Utilidades.EscribirConColor("\n---------------------CONTROL TOLVAS--------------------", ConsoleColor.DarkCyan);
            Console.WriteLine();
            Console.WriteLine("\n----- 1.Gestion de producción (Calculo Grados brix) ------");
            Console.WriteLine();
            Console.WriteLine("----- 2.Informe  ------");
            Console.WriteLine();
            Console.WriteLine("----- 3.Salir ------------------");
            Console.WriteLine();
            Console.WriteLine("----------------------------------------------------------");
            Console.Write("Digite una opción: ");
            int opcion;

            if (!int.TryParse(Console.ReadLine(), out opcion))
            {
                opcion = 0;
            }

            return opcion;
        }

        public static int menu_produccion()
        {
            Utilidades.EscribirConColor("\n-------------------GESTION DE PRODUCCIÓN---------------------", ConsoleColor.DarkCyan);
            Console.WriteLine();
            Console.WriteLine("\n----- 1.Clasificación en tiempo real ------");
            Console.WriteLine();
            Console.WriteLine("----- 2.Ajustar Rangos del calculo grados brix  ------");
            Console.WriteLine();
            Console.WriteLine("----- 3.Salir ------------------");
            Console.WriteLine();
            Console.WriteLine("----------------------------------------------------------");
            Console.Write("Digite una opción: ");
            int opcion;

            if (!int.TryParse(Console.ReadLine(), out opcion))
            {
                opcion = 0;
            }

            return opcion;
        }

        public static bool Acceso(string usuario, string contraseña)
        {

            using (SqlConnection con = Conexion.GetConexion())
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }


                string query = @"SELECT COUNT(*) 
                             FROM inicio_sesion 
                             WHERE nombreUsuario = @usuario AND contraseña = contraseña";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    cmd.Parameters.AddWithValue("@contraseña", contraseña);

                    int existe = (int)cmd.ExecuteScalar();
                    if (existe > 0)
                    {

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }


            }

        }


        public static void EjecutarClasificacionTiempoReal()
        {
            Console.Clear();
            Console.WriteLine("----------------------------------------------------------");
            Utilidades.EscribirConColor("-       CLASIFICACIÓN EN TIEMPO REAL - GRADOS BRIX       -", ConsoleColor.DarkCyan);
            Console.WriteLine("\n----------------------------------------------------------\n");

            try
            {
                Console.Write("Ingrese el número de serie de la tolva: ");
                string identificadorTolva = Console.ReadLine();

                decimal[] muestras = new decimal[5];
                Console.WriteLine("\nIngrese las mediciones de las 5 muestras de piñas:");

                for (int i = 0; i < 5; i++)
                {
                    bool entradaValida = false;
                    while (!entradaValida)
                    {
                        Console.Write($"  Muestra {i + 1} (Brix): ");
                        string entrada = Console.ReadLine();

                        if (decimal.TryParse(entrada, out decimal valor) && valor >= 0)
                        {
                            muestras[i] = valor;
                            entradaValida = true;
                        }
                        else
                        {
                            Console.WriteLine("  Ingrese un número válido mayor o igual a 0.");
                        }
                    }
                }

                MedicionBrix medicion = new MedicionBrix();
                bool exito = medicion.ProcesarMedicionCompleta(identificadorTolva, muestras);

                if (exito)
                {
                    medicion.MostrarResultados();
                    Console.WriteLine(" Medición procesada y guardada exitosamente.");
                }
                else
                {
                    Console.WriteLine(" Error al procesar la medición.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n Error: {ex.Message}");
            }

            Console.WriteLine("\nPresione Enter para continuar...");
            Console.ReadLine();
        }


        public static void EstablecerRangos()
        {
            Console.Clear();
            Utilidades.EscribirConColor("--- ESTABLECER RANGOS ---", ConsoleColor.DarkCyan);

            Console.Write("\nIngrese mínimo para Exportación: ");
            decimal minExport = Convert.ToDecimal(Console.ReadLine());

            Console.Write("Ingrese mínimo para Jugo: ");
            decimal minJugo = Convert.ToDecimal(Console.ReadLine());

            bool resultado = GestionBrix.EstablecerRangos(minExport, minJugo);

            if (resultado)
            {
                Console.WriteLine("\n Los rangos se guardaron correctamente.");
            }
            else
            {
                Console.WriteLine("\nHubo un error al guardar los rangos.");
            }

        }



        public static void RangosBrix()
        {
            int opcion = 0;

            do
            {
                Console.Clear();
                Utilidades.EscribirConColor("\n---------- MENU RANGOS ----------", ConsoleColor.DarkCyan);
                Console.WriteLine("\n1. Establecer Rangos Brix");
                Console.WriteLine("2. Ver Rangos Brix");
                Console.WriteLine("3. Salir");
                Console.WriteLine("-------------------------------------");

                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.WriteLine("Opción inválida. Presione Enter...");
                    Console.ReadLine();
                    continue;
                }

                switch (opcion)
                {
                    case 1:
                        Utilidades.EstablecerRangos();
                        break;

                    case 2:
                        Conexion.VerTabla("ParametrosBrix");
                        break;

                    case 3:
                        Console.WriteLine("Volviendo a menu principal");
                        break;

                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }

                if (opcion != 3)
                {
                    Console.WriteLine("\nPresione Enter para continuar...");
                    Console.ReadLine();
                }

            } while (opcion != 3);
        }



    }




}
