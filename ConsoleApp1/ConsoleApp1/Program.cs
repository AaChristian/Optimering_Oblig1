using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Optimering_Oblig1
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int iter = 0; iter < 50; iter++) {
                Random rnd = new Random();
                int cities = 10000;
                int randomCost = 0;
                int randomItCost = 0;
                int greedyCost = 0;
                int improveRandomCost = 0;
                int improveIterativeCost = 0;
                int improveGreedyCost = 0;
                //string txtpath = "C:\\Users\\Student\\Desktop\\test.csv";
                string txtpath = "C:\\Users\\ChristianAashamar\\Desktop\\test.csv";

                //Create a multidimensional array to store cost between cities
                int[,] cost = new int[cities, cities];
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
                  // Create arrays for the initial solutions
                int[] solutionRandom = new int[cities];
                int[] solutionRandomIt = new int[cities];
                int[] solutionGreedy = new int[cities];

                // Write out the cost matrix
                //WriteMultiArray(cost, cities, cities);

                // Initiale the random method
                Console.Write("\n---Random method---\n");
                randomCost = RndMethod(cost, solutionRandom, cities, rnd);
                Console.Write("\nTotal cost: {0}\n", randomCost);

                // Start random iterativ
                Console.Write("\n---Random iterative method---\n");
                // Initialize lowest cost variable as max integer value
                randomItCost = int.MaxValue;
                int iterativeCost;
                // Run the random method five times
                for (int i = 0; i < 5; i++) {
                    // Get the cost of the random method
                    iterativeCost = RndMethod(cost, solutionRandomIt, cities, rnd);
                    // Compare the cost with the current lowest cost
                    if (iterativeCost < randomItCost) {
                        // If the cost is lower, set it as the new lowest cost
                        randomItCost = iterativeCost;
                    }
                } // End for loop
                Console.Write("\nTotal cost: {0}\n", randomItCost);

                // Start greedy method
                greedyCost = GreedyMethod(cost, solutionGreedy, cities, rnd);
                Console.Write("\n---Greedy method---\n");
                Console.Write("\nTotal cost: {0}\n", greedyCost);

                // Improvement method
                improveRandomCost = ImproveSolution(cost, solutionRandom, randomCost, cities, rnd);
                improveIterativeCost = ImproveSolution(cost, solutionRandomIt, randomItCost, cities, rnd);
                improveGreedyCost = ImproveSolution(cost, solutionGreedy, greedyCost, cities, rnd);
                Console.Write("\n---Improvements method---\n");
                Console.Write("\nImprove Random cost: {0}\n", improveRandomCost);
                Console.Write("\nImprove Random Iterative cost: {0}\n", improveIterativeCost);
                Console.Write("\nImprove Greedy cost: {0}\n", improveGreedyCost);


                using (StreamWriter sw = File.AppendText(txtpath)) {
                    sw.WriteLine(randomCost + ", " + randomItCost + ", " + greedyCost + ", " + improveRandomCost + ", " + improveIterativeCost + ", " + improveGreedyCost);
                }
            }

            Console.Read();
        } // End main method

        // Random method (Also use for random iterative method)
        static int RndMethod(int[,] cost, int[] visitedCities, int cities, Random rnd) {
            int totalCost = 0;
            int i = 1; // Number of cities visited (starting at 1 as starting city is selected)
            // Select a random starting city
            int city = rnd.Next(0, cities);
            // Initialize the visited city array with a value of -1
            InitArray(visitedCities, -1, cities);
            // Mark the start city as visited
            visitedCities[0] = city;
            // While all cities have not been visited
            while (visitedCities.Contains(-1)) {
                // Choose a random city
                city = rnd.Next(0, cities);
                // If the selected city already is visited, try again
                if (visitedCities.Contains(city)) {
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
            // Return the total cost
            return totalCost;
        } // End Random method

        // Greedy method
        static int GreedyMethod(int[,] cost, int[] visitedCities, int cities, Random rnd) {
            int totalCost = 0;
            int i = 1; // Number of cities visited (starting at 1 as starting city is selected)
            // Select a random starting city
            int city = rnd.Next(0, cities);
            // Initialize the visited array with a value of -1
            InitArray(visitedCities, -1, cities);
            // Mark the start city as visited
            visitedCities[0] = city;
            // While all cities have not been visited
            while (visitedCities.Contains(-1)) {
                int lowestNeighbour = int.MaxValue;
                int lowestNeighbourCost = int.MaxValue;
                // Go through all the cities neighbours
                for (int k = 0; k < cities; k++) {
                    //Console.Write("\nKostnad fra {0} til {1}: {2}", visitedCities[i - 1], k, cost[visitedCities[i - 1], k]);
                    // If the cost of neighbour is lower the the current selected neighbour and it isnt already visited
                    if (cost[visitedCities[i - 1], k] < lowestNeighbourCost && !visitedCities.Contains(k)) {
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
                // Increase i
                i++;
            }
            return totalCost;
        } // End Greedy method

        // Improve initial solution
        static int ImproveSolution(int [,] cost, int[] initSolution, int initCost, int cities, Random rnd) {
            int i = 0; // Number of iterations
            int[] bestSolution = new int[cities];
            initSolution.CopyTo(bestSolution, 0);
            int city1;
            int city2;
            int temp;
            int newCost = 0;
            // Run the algorithm multiple times
            while (i < 1000) {
                // Choose 2 random cities
                city1 = rnd.Next(0, cities);
                city2 = rnd.Next(0, cities);
                // If the 2 cities is equal, choose a new random city2
                while (city1 == city2) {
                    city2 = rnd.Next(0, cities);
                }
                // Swap their locations
                //WriteArray(initSolution);
                temp = initSolution[city1];
                initSolution[city1] = initSolution[city2];
                initSolution[city2] = temp;
                //WriteArray(initSolution);
                // Calculate the new cost
                for (int j = 0; j < cities - 1; j++) {
                    //Console.Write("From {0} to {1} - Cost: {2}\n", initSolution[j], initSolution[j+1], cost[initSolution[j], initSolution[j+1]]);
                    newCost += cost[initSolution[j], initSolution[j + 1]];
                    //Console.Write("New cost: {0}\n", newCost);
                }
                // If the cost has decreased
                if (newCost < initCost) {
                    // Save the new cost as the cost to be used in further iterations
                    initCost = newCost;
                    // Copy the current best solution
                    initSolution.CopyTo(bestSolution, 0);
                    // Reset i
                    i = 0;
                } else {
                    // Increase I if the new cost  isn't lower the the current cost

                    i++;
                }
                newCost = 0;
                bestSolution.CopyTo(initSolution, 0);
            }
            // Return the new cost
            return initCost;
            //Console.Write("Ny kostnad: {0}\n", initCost);
        }

        // Initialize arrays method
        static void InitArray(int [] array, int value, int k) {
            // Initialize the array with a value of -1
            for (int j = 0; j < k; j++) {
                array[j] = value;
            }
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
