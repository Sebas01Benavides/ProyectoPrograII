using Microsoft.Data.SqlClient;
using ProyectoPrograII.DatosAcceso;
using ProyectoPrograII.Servicios;
using System;

namespace Proyecto_I
{
    internal class Program
    {
        static void Main(string[] args)
        {

            /* bool continuarSistema = true;

             while (continuarSistema)
             {
                 Console.WriteLine("Ingrese su Usuariossfgs:");
                 String usuario = Console.ReadLine();

                 Console.WriteLine("Ingrese su Contraseña:");
                 String contraseña = Console.ReadLine();

                 bool ingreso = Utilidades.Acceso(usuario, contraseña);

                 if (ingreso == true)
                 {
                     bool mostrarMenuPrincipal = true;

                     while (mostrarMenuPrincipal)
                     {
                         Console.Clear();
                         int opcion = Utilidades.menu_inicio();

                         switch (opcion)
                         {
                             case 1:

                                 Console.Clear();
                                 int opcProduccion = Utilidades.menu_produccion();

                                 if (opcProduccion == 1)
                                 {
                                     Console.WriteLine("Control en tiempo real");
                                     Console.ReadKey();
                                 }
                                 else if (opcProduccion == 2)
                                 {
                                     Console.WriteLine("Ajuste de rangos");
                                     Console.ReadKey();
                                 }
                                 else
                                 {
                                     Console.Clear();
                                     break;
                                 }

                                 break;

                         }


                     }
                 }
             }

         */

            //-------------------------

            int opcion = 0;

            do
            {
                Console.Clear();
                Console.WriteLine("---------- MENU PRINCIPAL ----------");
                Console.WriteLine("1. Establecer Rangos Brix");
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
                        EstablecerRangos();
                        break;

                    case 2:
                        Conexion.VerTabla("ParametrosBrix");
                        break;

                    case 3:
                        Console.WriteLine("Saliendo...");
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

        // --- ESTABLECER RANGOS ---
        static void EstablecerRangos()
        {
            Console.Clear();
            Console.WriteLine("--- ESTABLECER RANGOS ---");

            Console.Write("Ingrese mínimo para Exportación: ");
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
    }
}