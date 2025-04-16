using System;
namespace AirUFV
{
    public class CommercialAircraft : Aircraft
    {
        private int numberOfPassengers;
        public CommercialAircraft(string id, AircraftStatus status, int distance, int speed, double fuelCapacity, double consumoCombustible, double currentFuel, int numberOfPassengers) : base(id, status, distance,"Commercial", speed, fuelCapacity, consumoCombustible, currentFuel)

        {
            this.numberOfPassengers = numberOfPassengers;
        }
        public int GetNumberOfPassengers()
        {
            return this.numberOfPassengers;
        }
    }
}