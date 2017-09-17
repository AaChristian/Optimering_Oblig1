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
            } // End for loop

            // Write out the cost matrix
            WriteMultiArray(cost, cities, cities);
            // Initiale the random method
            Console.Write("\n---Random method---\n");
            Console.Write("\nTotal cost: {0}\n", RndMethod(cost, cities, rnd));
            // Start random iterativ
            Console.Write("\n---Random iterative method---\n");
            // Initialize lowest cost variable as max integer value
            int lowestCost = int.MaxValue;
            int iterativeCost;
            // Run the random method five times
            for (int i = 0; i < 5; i++) {
                //Console.Write("Iterative test {0}:\n", i+1);
                // Get the cost of the random method
                iterativeCost = RndMethod(cost, cities, rnd);
                //Console.Write("Current cost {0}:\n", iterativeCost);
                // Compare the cost with the current lowest cost
                if (iterativeCost < lowestCost) {
                    // If the cost is lower, set it as the new lowest cost
                    lowestCost = iterativeCost;
                }
            } // End for loop
            Console.Write("\nTotal cost: {0}\n", lowestCost);
            // Start greedy method
            Console.Write("\n---Greedy method---\n");
            GreedyMethod(cost, cities, rnd);
            Console.Read();
        }

        // Random method (Also use for random iterative method)
        static int RndMethod(int[,] cost, int cities, Random rnd) {
            //Random rnd = new Random();
            int city = rnd.Next(0, cities);
            int totalCost = 0;
            int i = 1; // Number of cities visited (starting at 1 as starting city is selected)
            //Console.Write(String.Format("Start city: {0}\n", city));
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
                //Console.Write("From {0} to {1} - Cost: {2}\n", visitedCities[i - 1], visitedCities[i], cost[visitedCities[i - 1], visitedCities[i]]);
                // Add the cost between the previous city and the newly selected city to the total cost variable
                totalCost += cost[visitedCities[i - 1], visitedCities[i]];
                // Increase i
                i++;
            }
            //Console.Write("From {0} to {1} - Cost: {2} (Travels from the last city back to start)\n", visitedCities[i -1], visitedCities[0], cost[visitedCities[0], visitedCities[cities - 1]]);
            // Add the cost of returning from the last city to the starting city
            totalCost += cost[visitedCities[0], visitedCities[cities - 1]];
            //Console.Write("\nTotal cost: {0}\n", totalCost);
            //Print the array for the visited cities
            //WriteArray(visitedCities);
            return totalCost;
        }

        // Greedy method
        static void GreedyMethod(int[,] cost, int cities, Random rnd) {
            // Select a random starting city
            int city = rnd.Next(0, cities);
            int totalCost = 0;
            int i = 1; // Number of cities visited (starting at 1 as starting city is selected)
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
                int lowestNeighbour = int.MaxValue;
                int lowestNeighbourCost = int.MaxValue;
                // Go through all the cities neighbours
                for (int k = 0; k < cities; k++) {
                    //Console.Write("\nk: {0}, i: {1}\n", k, i);
                    //Console.Write("\nKostnad fra {0} til {1}: {2}", visitedCities[i - 1], k, cost[visitedCities[i - 1], k]);
                    // If the cost of neighbour is lower the the current selected neighbour and it isnt already visited
                    if (cost[visitedCities[i - 1], k] < lowestNeighbourCost && !visitedCities.Contains(k)) {
                        //Console.Write(" LAVEST!\n");
                        // Set the lowest neighbour to the current city
                        lowestNeighbour = k;
                        // Set the cost of the lowest neighbour
                        lowestNeighbourCost = cost[visitedCities[i - 1], k];
                    }
                }
                // Add the city as visited
                visitedCities[i] = lowestNeighbour;
                // Add the cost of the nearest neighbour (lowest cost)
                totalCost += lowestNeighbourCost;
                //WriteArray(visitedCities);
                // Increase i
                i++;
            }
            Console.Write("\nTotal cost: {0}\n", totalCost);
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
