using System;
namespace AirUFV 
{
    public class PrivateAircraft : Aircraft
    {
        private string owner;

        public PrivateAircraft(string id, AircraftStatus status, int distance, int speed, double fuelCapacity, double consumoCombustible, double currentFuel, string owner) : base(id, status, distance, "Private", speed, fuelCapacity, consumoCombustible, currentFuel)
        {
            this.owner = owner;
        }

        public string GetOwner()
        {
            return this.owner;
        }
    }
}