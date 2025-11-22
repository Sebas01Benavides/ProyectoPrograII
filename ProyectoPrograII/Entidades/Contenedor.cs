using ProyectoPrograII.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProyectoPrograII.Entidades
{
    internal class Contenedor:ProductoAgricola,IClasificacion, IReporte
    {
        private static int cons = 100;
        private string carnet;
        private string nombre;

        public Contenedor()
        {
            carnet = string.Empty;
            nombre = string.Empty;
        }
        
        public Contenedor(string carnet, string nombre)
        {
            this.carnet = GenerarCarnet();
            this.nombre = nombre;
        }

        public string Carnet { get => carnet; set => carnet = value; }
        public string Nombre { get => nombre; set => nombre = value; }


        public string GenerarCarnet()
        {
            string pref = "CONT";
            string numero = cons.ToString("0000");
            cons++;
            return pref + numero;
        }
    }
}
