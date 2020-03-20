using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SistemaDeIrrigacion
{
    public class Sistema
    {


        public Entrada[] entradas;
        public Salida[] salidas;
        public Valvula[] valvulas;
        public int NumConfiguraciones;

        private Sistema()
        {

        }

        public static Sistema CreateSystem(string[] inp2, StreamReader stream)
        {
            int entrada, salida, valvula;

            entrada = Convert.ToInt32(inp2[0]);
            salida = Convert.ToInt32(inp2[1]);
            valvula = Convert.ToInt32(inp2[2]);
            int[] flujosDeAgua;
            string[][] relations;
            string[] salidaNames;
            string[] valvulaNames;
            string[][] estructuraDeValvulas;


            flujosDeAgua = new int[entrada];
            var inp = ReadInput(stream);
            for (int i = 0; i < entrada; i++)
            {
                flujosDeAgua[i] = Convert.ToInt32(inp[i]);
            }
            relations = new string[entrada][];
            for (int i = 0; i < entrada; i++)
            {
                relations[i] = ReadInput(stream);
            }
            salidaNames = new string[salida];
            for (int i = 0; i < salida; i++)
            {
                salidaNames[i] = stream.ReadLine();
            }
            valvulaNames = new string[valvula];
            estructuraDeValvulas = new string[valvula][];
            for (int i = 0; i < valvula; i++)
            {
                estructuraDeValvulas[i] = ReadInput(stream);
            }
            var tempList = new List<string>();
            foreach (var item in estructuraDeValvulas)
            {
                foreach (var item2 in item)
                {
                    if (!valvulaNames.Contains(item2) && !salidaNames.Contains(item2))
                        tempList.Add(item2);
                }
            }
            
            valvulaNames = tempList.Distinct().ToList().Count > valvulaNames.Length ? null : tempList.Distinct().ToArray();
            if (valvulaNames == null)
                return null;
            List<string[]> configuraciones = new List<string[]>();
            string[] t;
            while ((t = ReadInput(stream))[0] != "*")
            {
                configuraciones.Add(t);
            }


            return CreateTrees(flujosDeAgua,
                               relations,
                               salidaNames,
                               valvulaNames,
                               estructuraDeValvulas,
                               configuraciones);
        }

        private static Sistema CreateTrees(int[] flujosDeAgua, string[][] relations, string[] salidaNames, string[] valvulaNames, string[][] estructuraDeValvulas, List<string[]> configuraciones)
        {
            Sistema sistema = new Sistema();
            Entrada[] In = new Entrada[relations.Length];
            Valvula[] Middle = new Valvula[valvulaNames.Length];
            Salida[] Out = new Salida[salidaNames.Length];

            for (int i = 0; i < salidaNames.Length; i++)
            {
                Out[i] = new Salida
                {
                    ID = i + 1,
                    Name = salidaNames[i]
                };
            }
            valvulaNames = valvulaNames.OrderBy(x => x).ToArray();
            for (int i = 0; i < valvulaNames.Length; i++)
            {
                Middle[i] = new Valvula
                {
                    Name = valvulaNames[i]
                };
            }
            for (int i = 0; i < relations.Length; i++)
            {
                In[i] = new Entrada
                {
                    WaterInput = flujosDeAgua[i],
                    Name = relations[i][0],
                    Left = Middle.Where(x => x.Name == relations[i][1]).First()
                };

            }
            foreach (var item in estructuraDeValvulas)
            {
                Valvula valvula = Middle.Where(x => x.Name == item[0]).First();
                valvula.Left = Middle.Where(x => x.Name == item[1]).FirstOrDefault() == null ? (INode)Out.Where(x => x.Name == item[1]).First() : Middle.Where(x => x.Name == item[1]).First();
                valvula.Right = Middle.Where(x => x.Name == item[2]).FirstOrDefault() == null ? (INode)Out.Where(x => x.Name == item[2]).First() : Middle.Where(x => x.Name == item[2]).First();
            }
            for (int i = 0; i < Middle.Length; i++)
            {
                List<string> dir = new List<string>();
                foreach (var item in configuraciones)
                {
                    dir.Add(item[i]);
                }
                Middle[i].Directions = dir.ToArray();

            }
            sistema.entradas = In;
            sistema.valvulas = Middle;
            sistema.salidas = Out;
            sistema.NumConfiguraciones = configuraciones.Count;

            return sistema;
        }

        public void Flow()
        {
            for (int i = 0; i < NumConfiguraciones; i++)
            {
                foreach (var item in entradas)
                {
                    item.Flow(i);
                }
                Imprimir(salidas, i+1);
                foreach (var item in salidas)
                {
                    item.Reset();
                }

            }
            //Debugging de los arboles

            //Console.Clear();
            //foreach (var item in entradas)
            //{
            //    Console.WriteLine(item.ToString());
            //}
            //Console.ReadKey();

            //Console.Clear();
        }

        public void Imprimir(Salida[] salidas, int numConfig)
        {
            Console.WriteLine($"Configuracion de valvulas # {numConfig}\n");
            foreach(var item in salidas)
            {
                Console.WriteLine(item.GetString());
            }
        }

        static string[] ReadInput(StreamReader stream)
        {
            return stream.ReadLine().Split(" ");
        }
    }
}
