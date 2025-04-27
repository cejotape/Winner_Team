using System;
namespace AirUFV
{
    public class CargoAircraft : Aircraft
    {
        private double maximumLoad; 
        public CargoAircraft(string id, AircraftStatus status, int distance, string type, int speed, double fuelCapacity, double consumoCombustible, double currentFuel, double maximumLoad) : base (id, status, distance, "Cargo", speed, fuelCapacity, consumoCombustible, currentFuel)
        {
            this.maximumLoad = maximumLoad;
        }
        public double GetMaximumLoad()
        {
            return this.maximumLoad;   
        }
    }
}