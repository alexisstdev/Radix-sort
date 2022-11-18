using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Radix_Sort
{
    public static class Ordenador
    {
        //Método principal que ordena el arreglo usando Radix Sort
        public static void RadixSort(Estudiante[] arreglo, bool ascendente)
        {
            int tamaño = arreglo.Length;
            // Recorre el arreglo para obterner el valor máximo del arreglos
            // para sabr el número de dígitos
            int max = arreglo[0].NumControl;
            for (int i = 1; i < tamaño; i++)
            {
                if (arreglo[i].NumControl > max) max = arreglo[i].NumControl;
            }

            //Realiza el ordenamiento por conteo para cada dígito
            for (int exp = 1; max / exp > 0; exp *= 10)
            {
                if (ascendente) CountingSortAscendente(arreglo, tamaño, exp);
                else CountingSortDescendente(arreglo, tamaño, exp);
            }
        }

        //Método para ordenar con Counting sort el arreglo dado acorde
        //a exp(unidades, decenas, centenas, etc)
        public static void CountingSortAscendente(Estudiante[] arreglo, int tamaño, int exp)
        {
            Estudiante[] temp = new Estudiante[tamaño]; //Arreglo temporal para almacenar la salida

            int i; //Contador de los ciclos

            //Creación de los buckets
            int[] count = new int[10];

            // Almacena el conteo de los digitos del arreglo en count[]
            for (i = 0; i < tamaño; i++)
            {
                count[(arreglo[i].NumControl / exp) % 10]++;
            }

            //Cambia count[i] para que contenga la posición actual
            //del digito en temp[]
            for (i = 1; i < 10; i++)
                count[i] += count[i - 1];

            // Coloca los elementos en orden
            for (i = tamaño - 1; i >= 0; i--)
                temp[--count[(arreglo[i].NumControl / exp) % 10]] = arreglo[i];

            //Copia el arreglo de salida al arreglo para que contenga los
            //elementos ordenados acorde al dígito acutal
            for (i = 0; i < tamaño; i++)
                arreglo[i] = temp[i];
        }

        public static void CountingSortDescendente(Estudiante[] arreglo, int tamaño, int exp)
        {
            Estudiante[] temp = new Estudiante[tamaño]; //Arreglo temporal para almacenar la salida

            int i; //Contador de los ciclos

            //Creación de los buckets
            int[] count = new int[10];

            // Almacena el conteo de los digitos del arreglo en count[]
            for (i = 0; i < tamaño; i++)
                count[9 - arreglo[i].NumControl / exp % 10]++;

            //Cambia count[i] para que contenga la posición actual
            //del digito en temp[]
            for (i = 1; i < 10; i++)
                count[i] += count[i - 1];

            // Coloca los elementos en orden
            for (i = tamaño - 1; i >= 0; i--)
                temp[--count[9 - arreglo[i].NumControl / exp % 10]] = arreglo[i];

            //Copia el arreglo de salida al arreglo para que contenga los
            //elementos ordenados acorde al dígito acutal
            for (i = 0; i < tamaño; i++)
                arreglo[i] = temp[i];
        }
    }
}

/*if (n <= 0)
    return;

List<int>[] buckets = new List<int>[10];

for (int i = 0; i < n; i++)
{
    buckets[i] = new List<int>();
}

for (int i = 0; i < n; i++)
{
    int idx = arr[i] * n;
    buckets[(int)idx].Add(arr[i]);
}

for (int i = 0; i < n; i++)
{
    buckets[i].Sort();
}

int index = 0;
for (int i = 0; i < n; i++)
{
    for (int j = 0; j < buckets[i].Count; j++)
    {
        arr[index++] = buckets[i][j];
    }
}*/