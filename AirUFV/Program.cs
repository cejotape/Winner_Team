using System;

namespace AirUFV
{
    class Program
    {
        static void Main(string[] args)
        {
            Airport airport = new Airport(2, 2); // Creating a new airport with 4 runways in total (2x2)
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n");
                Console.WriteLine("|-----------------------------------|");
                Console.WriteLine("| ========== AIRPORT MENÚ ========= |");
                Console.WriteLine("|-----------------------------------|");
                Console.WriteLine("| 1. Load flights from file         |");
                Console.WriteLine("| 2. Add flight manually            |");
                Console.WriteLine("| 3. Start simulation (Advance Tick)|");
                Console.WriteLine("| 4. Exit                           |");
                Console.WriteLine("|-----------------------------------|");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();
                Console.WriteLine("\n");
                if (choice == "1")
                {
                    Console.Write("Enter file path (e.g., aircrafts.csv): ");
                    string filePath = Console.ReadLine();
                    airport.LoadAircraftFromFile(filePath);
                }
                else if (choice == "2")
                {
                    airport.AddAircraftManually();
                }
                else if (choice == "3")
                {
                    Console.WriteLine("Starting tick-based simulation...");
                    Console.WriteLine("Press ENTER to advance tick. Type 'exit' to return to the menu.");

                    string userInput;
                    do
                    {
                        Console.Write(">>> ");
                        userInput = Console.ReadLine();

                        if (userInput.ToLower() != "exit")
                        {
                            airport.AdvanceTick();
                            airport.ShowStatus();
                        }

                    } while (userInput.ToLower() != "exit");
                }   

                else if (choice == "4")
                {
                    exit = true; // For exit the program without using break! 
                    Console.WriteLine("Exiting program. Goodbye!");
                }
                else
                {
                    Console.WriteLine("Invalid option. Try again.");
                }
            }
        }
    }
}
