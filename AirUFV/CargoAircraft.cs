using System;
namespace AirUFV
{
    public class CargoAircraft : Aircraft
    {
        private double maximumLoad; 
        public CargoAircraft(string id, AircraftStatus status, int distance, int speed, double fuelCapacity, double fuelConsumption, double currentFuel, double maximumLoad) : base (id, status, distance, speed, fuelCapacity, fuelConsumption, currentFuel)
        {
            this.maximumLoad = maximumLoad;
        }
        public double GetMaximumLoad()
        {
            return this.maximumLoad;   
        }
    }
}