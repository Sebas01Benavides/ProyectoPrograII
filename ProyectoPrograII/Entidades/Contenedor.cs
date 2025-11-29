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
    internal class Contenedor
    {
        private string nombre;

        public Contenedor()
        {
            nombre = string.Empty;
        }
        
        public Contenedor(string carnet, string nombre)
        {
            this.nombre = nombre;
        }

        public string Nombre { get => nombre; set => nombre = value; }

    }
}
