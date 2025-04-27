using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
namespace AirUFV
{
    public class Airport
    {
        private List<Aircraft> aircrafts; //the wherehouse of all the plains in the system, no matters type
        private Runway[,] runways; //2d matrix that stores the runways
        public Airport(int rows, int cols)
        {
            aircrafts = new List<Aircraft>();
            runways = new Runway[rows, cols];
            int counter = 1;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    runways[i, j] = new Runway("Runway-" + counter);
                    counter++;
                }
            }
        }
        public void AddAircraft(Aircraft aircraft)
        {
            aircrafts.Add(aircraft);
        }
        public void ShowStatus()
        {
            Console.WriteLine("|----------------------------|");
            Console.WriteLine("| ======== RUNWAYS ========= |");
            Console.WriteLine("|----------------------------|");
            foreach (Runway runway in runways)
            {
                Console.Write($"[{runway.GetId()}] - ");
                if (runway.GetStatus() == RunwayStatus.Free)
                {
                    Console.WriteLine("Free");
                }
                else
                {
                    Console.WriteLine($"Occupied by {runway.GetCurrentAircraft().GetId()} ({runway.GetTicksRemaining()} ticks remaining)");
                }
            }
            Console.WriteLine("|----------------------------|");
            Console.WriteLine("| ======= AIRCRAFTS ======== |");
            Console.WriteLine("|----------------------------|");
            foreach (Aircraft a in aircrafts)
            {
                Console.WriteLine($"{a.GetId()} | {a.GetStatus()} | Distance: {a.GetDistance()} km | Fuel: {a.GetCurrentFuel():0.00}L | Type: {a.GetTypeAircraft()}");
            }
        }
        public void AdvanceTick()
        {
            List<Aircraft> crashedAircrafts = new List<Aircraft>(); //A list of aircrafts that not have enough fuel to get to the airport
            Console.WriteLine("|----------------------------|");
            Console.WriteLine("| ADVANCING SIMULATION TICk! |");
            Console.WriteLine("|----------------------------|");
            
            foreach (Runway runway in runways) //We refresh the status of the runways
            {
                runway.AdvanceTick();
            }
            foreach (Aircraft aircraft in aircrafts)
            {
                //Inflight: update distance and fuel. 
                if (aircraft.GetStatus() == Aircraft.AircraftStatus.Inflight)
                {
                    double kmFlown = (aircraft.GetSpeed() / 60.0) * 15; // 1 tick = 15 mins and the speed is in km/h. We need the speed in km/min
                    double boostUsed = kmFlown * aircraft.GetConsumoCombustible();

                    double newDistance = aircraft.GetDistance() - kmFlown;
                    double newFuel = aircraft.GetCurrentFuel() - boostUsed;
                    if (newDistance < 0)
                    {
                        newDistance = 0;
                    }
                    if (newFuel < 0)
                    {
                        newFuel = 0;
                    }
                    if (newFuel == 0)
                    {
                        Console.WriteLine("     \\ | /");
                        Console.WriteLine("      \\*/ ");
                        Console.WriteLine("       |");
                        Console.WriteLine("     .-'-.");
                        Console.WriteLine("   /       \\");
                        Console.WriteLine("  |  BOOM!  |");
                        Console.WriteLine("   \\       /");
                        Console.WriteLine("     `---`");
                        Console.WriteLine("    (_____)");
                        Console.WriteLine("     /   \\");

                        Console.WriteLine($"Aircraft {aircraft.GetId()} has crashed because it not has enough fuel!!");
                        crashedAircrafts.Add(aircraft);
                    }
                    else
                    {
                        aircraft.SetDistance((int)newDistance);
                        aircraft.SetCurrentFuel(newFuel);
                        if (newDistance == 0)
                        {
                            aircraft.SetStatus(Aircraft.AircraftStatus.Waiting);
                        }
                    }
                }
                //Waiting: try to assing a free runway. Aircraft waiting for a free runway.
                if (Aircraft.AircraftStatus.Waiting == aircraft.GetStatus())
                {
                    bool flag = false;
                    foreach (Runway runway in runways)
                    {
                        if (!flag && runway.IsFree())
                        {
                            bool success = runway.RequestRunway(aircraft);
                            if (success)
                            {
                                aircraft.SetStatus(Aircraft.AircraftStatus.Landing);
                                flag = true;
                            }

                        }
                    }
                    if (!flag)
                    {
                        Console.WriteLine($"No runway available for aircraft {aircraft.GetId()}");
                    }
                }
                //Landing: check if done, then set to OnGround. Aircraft currently landing.
                if (Aircraft.AircraftStatus.Landing == aircraft.GetStatus())
                {
                    bool stillLanding = false;
                    foreach (Runway runway in runways)
                    {
                        if (runway.GetCurrentAircraft() == aircraft)
                        {
                            stillLanding = true;
                        }
                    }

                    if (!stillLanding)
                    {
                        aircraft.SetStatus(Aircraft.AircraftStatus.OnGround);
                        Console.WriteLine($"Aircraft {aircraft.GetId()} has landed successfully");
                    }
                }
            }
            foreach (Aircraft crashed in crashedAircrafts) //We remove the aircraft that not have enough fuel to land
            {
                aircrafts.Remove(crashed);
            }
        }
        public void LoadAircraftFromFile(string filepath)
        {   
            if (!File.Exists(filepath))
            {
                Console.WriteLine("ERROR: File not found.");
                return;
            }

            string[] lines = File.ReadAllLines(filepath);

            foreach (string line in lines)
            {
                try
                {
                    string[] parts = line.Split(','); //The splitter between the different attributes is a comma ","

                    if (parts.Length >= 8) //If the line has a valid length:
                    {
                        string id = parts[0];
                        Aircraft.AircraftStatus status = Enum.Parse<Aircraft.AircraftStatus>(parts[1]);
                        int distance = int.Parse(parts[2]);
                        int speed = int.Parse(parts[3]);
                        string type = parts[4];
                        double fuelCapacity = double.Parse(parts[5]);
                        double consumoCombustible = double.Parse(parts[6]);
                        Aircraft aircraft = null;

                        if (type == "Commercial")
                        {
                            int passengers = int.Parse(parts[7]); //int because the extra attribute of this subclass is an int type
                            aircraft = new CommercialAircraft(id, status, distance, type, speed, fuelCapacity, consumoCombustible, fuelCapacity, passengers);
                        }
                        else if (type == "Cargo") 
                        {
                            double maxLoad = double.Parse(parts[7]); //double because the extra attribute of this subclass is a double type
                            aircraft = new CargoAircraft(id, status, distance, type, speed, fuelCapacity, consumoCombustible, fuelCapacity, maxLoad);
                        }
                        else if (type == "Private") 
                        {
                            string owner = parts[7];
                            aircraft = new PrivateAircraft(id, status, distance, type, speed, fuelCapacity, consumoCombustible, fuelCapacity, owner);
                        } 
                        else //If there is an error, the aircraft is declared as null
                        {
                            Console.WriteLine($"ERROR: Unknown aircraft type: {type}");
                            aircraft = null;
                        }

                        if (aircraft != null) //If the aircraft has the correct length and everything is ok, we call the method AddAircraft to add it to aircrafts list!
                        {
                            AddAircraft(aircraft);
                            Console.WriteLine($"Aircraft {id} loaded successfully.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"ERROR: Invalid format in line: {line}");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"ERROR processing line: {line}");
                    Console.WriteLine($"Details: {e.Message}");
                }
            }       
        }
        public void AddAircraftManually()
        {
            Console.WriteLine("|----------------------------|");
            Console.WriteLine("| Choose aircraft type:      |");
            Console.WriteLine("|----------------------------|");
            Console.WriteLine("| 1. Commercial              |");
            Console.WriteLine("| 2. Cargo                   |");
            Console.WriteLine("| 3. Private                 |");
            Console.WriteLine("|----------------------------|");
            Console.Write("Option:");

                string option = Console.ReadLine();

            Aircraft aircraft = null;

            try
            {
                Console.Write("Enter ID: ");
                string id = Console.ReadLine();

                Aircraft.AircraftStatus status = Aircraft.AircraftStatus.Inflight; //If we created a new plane, we expect that is not already in the airport

                Console.Write("Enter distance (km): ");
                int distance = int.Parse(Console.ReadLine());

                Console.Write("Enter speed (km/h): ");
                int speed = int.Parse(Console.ReadLine());

                Console.Write("Enter fuel capacity (L): ");
                double fuelCapacity = double.Parse(Console.ReadLine());

                Console.Write("Enter fuel consumption (L/km): ");
                double consumoCombustible = double.Parse(Console.ReadLine());

                double currentFuel = fuelCapacity; // default full tank

                if (option == "1")
                {
                    Console.Write("Enter number of passengers: ");
                    int passengers = int.Parse(Console.ReadLine());
                    aircraft = new CommercialAircraft(id, status, distance, "Commercial", speed, fuelCapacity, consumoCombustible, currentFuel, passengers);
                }
                else if (option == "2")
                {
                    Console.Write("Enter max load (kg): ");
                    double maxLoad = double.Parse(Console.ReadLine());
                    aircraft = new CargoAircraft(id, status, distance, "Cargo", speed, fuelCapacity, consumoCombustible, currentFuel, maxLoad);
                }       
                else if (option == "3")
                {
                    Console.Write("Enter owner name: ");
                    string owner = Console.ReadLine();
                    aircraft = new PrivateAircraft(id, status, distance, "Private", speed, fuelCapacity, consumoCombustible, currentFuel, owner);
                }
                else
                {
                    Console.WriteLine("Invalid option. Aircraft not added.");
                    return;
                }

                AddAircraft(aircraft);
                Console.WriteLine($"Aircraft {id} added successfully!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: Invalid input.");
                Console.WriteLine($"Details: {e.Message}");
            }
        }
    }
}
