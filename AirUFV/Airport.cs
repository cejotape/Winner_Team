using System;
using System.Collections.Generic;
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
            Console.WriteLine("/////RUNWAYS/////");
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
            Console.WriteLine("\n/////AIRCRAFTS/////");
            foreach (Aircraft a in aircrafts)
            {
                Console.WriteLine($"{a.GetId()} | {a.GetStatus()} | Distance: {a.GetDistance()} km | Fuel: {a.GetCurrentFuel():0.00}L | Type: {a.GetTypeAircraft()}");
            }
        }
        public void AdvanceTick()
        {
            Console.WriteLine("ADVANCING SIMULATION TICK");
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
                    aircraft.SetDistance((int)newDistance);
                    aircraft.SetCurrentFuel(newFuel);

                    if (newDistance == 0)
                    {
                        aircraft.SetStatus(Aircraft.AircraftStatus.Waiting);
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
        }
    }
}