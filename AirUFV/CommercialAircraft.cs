using System;
namespace AirUFV
{
    public class CommercialAircraft : Aircraft
    {
        private int numberOfPassengers;
        public CommercialAircraft(string id, AircraftStatus status, int distance, int speed, double fuelCapacity, double fuelConsumption, double currentFuel, int numberOfPassengers) : base(id, status, distance, speed, fuelCapacity, fuelConsumption, currentFuel)
        {
            this.numberOfPassengers = numberOfPassengers;
        }
        public int GetNumberOfPassengers()
        {
            return this.numberOfPassengers;
        }
    }
}