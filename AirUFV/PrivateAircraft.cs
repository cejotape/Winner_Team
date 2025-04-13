using System;
namespace AirUFV 
{
    public class PrivateAircraft : Aircraft
    {
        private string owner;

        public PrivateAircraft(string id, AircraftStatus status, int distance, int speed, double fuelCapacity, double fuelConsumption, double currentFuel, string owner) : base(id, status, distance, speed, fuelCapacity, fuelConsumption, currentFuel)
        {
            this.owner = owner;
        }

        public string GetOwner()
        {
            return this.owner;
        }
    }
}