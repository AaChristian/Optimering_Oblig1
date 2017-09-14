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
            Random rnd = new Random();
            int cities = 5;
            Console.Write("Hei!\n");

            //Create a multidimensional array to store cost between cities
            int[,] cost = new int[cities,cities];
            for (int j = 0; j < cities; j++) {
                for (int k = 0; k < cities; k++) {
                    if (j == k) {
                        cost[j, k] = 0; // The cost to go from a city to the same city is 0.
                        continue;
                    }
                    int rndCost = rnd.Next(1, 11); // Create a random integer between 1 and 10
                    cost[j, k] = rndCost; 
                    cost[k, j] = rndCost;
                }
            }

            // Initiale the random method
            RndMethod(cost, cities);
            // Write out the cost matrix
            WriteMultiArray(cost, cities, cities);

            Console.Read();
        }

        // Random method
        static void RndMethod(int[,] cost, int cities) {
            Random rnd = new Random();
            int city = rnd.Next(0, cities);
            int totalCost = 0;
            int i = 1; // Number of cities visited (starting at 1 as starting city is selected)
            Console.Write(String.Format("Start city: {0}\n", city));
            // Create array to store visited cities
            int[] visitedCities = new int[cities];
            // Initialize the array with a value of -1
            for (int j = 0; j < cities; j++) {
                visitedCities[j] = -1;
            }
            // Mark the start city as visited
            visitedCities[0] = city;
            // While all cities have not been visited
            while (visitedCities.Contains(-1)) {
                // Choose a random city
                city = rnd.Next(0, cities);
                // If the selected city already is visited, try again
                if (visitedCities.Contains(city)) {
                    //Console.Write("..\n");
                    continue;
                }
                // Mark the city as visited by entering the value in the array
                visitedCities[i] = city;
                Console.Write("Fra {0} til {1} - Kostnad: {2}\n", visitedCities[i - 1], visitedCities[i], cost[visitedCities[i - 1], visitedCities[i]]);
                // Add the cost between teh previous city and the newly selected city to the total cost variable
                totalCost += cost[visitedCities[i - 1], visitedCities[i]];
                // Increase i
                i++;
            }
            Console.Write("Fra {0} til {1} - Kostnad: {2} (Reiser fra den siste byen tilbake til start)\n", visitedCities[i -1], visitedCities[0], cost[visitedCities[0], visitedCities[cities - 1]]);
            // Add the cost of returning from the last city to the starting city
            totalCost += cost[visitedCities[0], visitedCities[cities - 1]];
            Console.Write("\nTotal kostnad: {0}\n", totalCost);
            // Print the array for the visited cities
            WriteArray(visitedCities);
        }


        // METHODS FOR TESTING BELOW //

        // Write array to console
        static void WriteArray(int[] a) {
            Console.Write("Allerede besøkte byer:\n");
            foreach (var item in a)
                Console.Write("{0} ", item);
            Console.Write("\n\n");
        }

        // Write Multi array to console
        static void WriteMultiArray(int[,] a, int numb, int numb2) {
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
