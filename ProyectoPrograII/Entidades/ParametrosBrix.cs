using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoPrograII.Entidades
{
    internal class ParametrosBrix
    {
        private int id;
        private decimal rangoMaximo;
        private decimal rangoMinimo;
        private string destino;

        public ParametrosBrix()
        {
            Id = 0;
            RangoMaximo = 0;
            RangoMinimo = 0;
            Destino = string.Empty;
        }

        public ParametrosBrix(int id, decimal rangoMaximo, decimal rangoMinimo, string destino)
        {
            this.Id = id;
            this.RangoMaximo = rangoMaximo;
            this.RangoMinimo = rangoMinimo;
            this.Destino = destino;
        }

        public int Id { get => id; set => id = value; }
        public decimal RangoMaximo { get => rangoMaximo; set => rangoMaximo = value; }
        public decimal RangoMinimo { get => rangoMinimo; set => rangoMinimo = value; }
        public string Destino { get => destino; set => destino = value; }

        // Método para mostrar la información
        public override string ToString()
        {
            return $"{Destino}: {RangoMinimo}°Brix - {RangoMaximo}°Brix";
        }
    }
}
