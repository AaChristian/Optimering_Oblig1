using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimering_Oblig1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Random rnd = new Random();
            int cities = 5;
            Console.Write("Hei!\n");

            //Create a multidimensional array to store cost between cities
            int[,] cost = new int[5, 5] {
                                                    {0, 1, 7, 6, 3},
                                                    {1, 0, 9, 2, 1},
                                                    {7, 9, 0, 8, 3},
                                                    {6, 2, 8, 0, 8},
                                                    {3, 1, 3, 8, 0 }
            };

            // Initiale the random method
            RndMethod(cities);
            // Write out the cost matrix
            WriteMultiArray(cost, cities, cities);

            Console.Read();
        }

        // Random method
        static void RndMethod(int cities)
        {
            Random rnd = new Random();
            int start = rnd.Next(1, cities);
            Console.Write(String.Format("Start city: {0}\n", start));
            int[] visitedCities = new int[cities];
            visitedCities[0] = start;
            WriteArray(visitedCities);
        }

        // Write array to console
        static void WriteArray(int[] a)
        {
            Console.Write("Allerede besøkte byer:\n");
            foreach (var item in a)
                Console.Write("{0} ", item);
            Console.Write("\n\n");
        }

        // Write Multi array to console
        static void WriteMultiArray(int[,] a, int numb, int numb2)
        {
            Console.Write("Kostnads matrisen:\n");
            for (int i = 0; i < numb; i++) {
                for (int j = 0; j < numb2; j++) {
                    Console.Write(a[i, j] + " ");
                }
                Console.Write("\n");
            }
            Console.Write("\n");
        }
    }
}
