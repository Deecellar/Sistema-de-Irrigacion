using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SistemaDeIrrigacion
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Sistema> sistemas = new List<Sistema>();
            using (StreamReader t = new StreamReader(args[0]))
            {
                string[] inputs = t.ReadLine().Split(" ");
                while (!(Convert.ToInt32(inputs[0]) == 9999 && Convert.ToInt32(inputs[1]) == 9999 && Convert.ToInt32(inputs[2]) == 9999))
                {
                    sistemas.Add(Sistema.CreateSystem(inputs, t));
                    inputs = t.ReadLine().Split(" ");
                }
            }
            for (int i = 0; i < sistemas.Count; i++)
            {
                Console.WriteLine($"Sistema de Irrigacion # {i+1}");
                sistemas[i].Flow();
            }
            Console.ReadKey();
        }


    }
}
