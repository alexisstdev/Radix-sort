using System;

namespace Radix_Sort
{
    public class Estudiante
    {
        public int NumControl { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public decimal Promedio { get; set; }

        public override string ToString()
        {
            return NumControl.ToString();
        }
    }
}