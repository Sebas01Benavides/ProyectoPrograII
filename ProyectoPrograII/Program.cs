using ProyectoPrograII;

namespace Proyecto_I
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool continuarSistema = true;

            while (continuarSistema)
            {
                Console.WriteLine("Ingrese su Usuario:");
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


                            case 2:
                                Console.WriteLine("Informe");
                                Console.ReadKey();
                                break;

                            case 3:

                                mostrarMenuPrincipal = false;
                                break;

                            default:
                                Console.WriteLine("Opción inválida.");
                                Console.ReadKey();
                                break;
                        }
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("ACCESO DENEGADO");
                    Console.ReadKey();
                }
            }

        }


    }
}
