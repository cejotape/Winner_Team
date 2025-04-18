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
    }
}