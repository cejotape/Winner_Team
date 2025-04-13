using System;
using System.Net;
namespace AirUFV
{
    public class Aircraft
    {
        public enum AircraftStatus //the state of any aircraft, each could have differents states
        {
            Inflight, Waiting, Landing, OnGround
        } 
        private AircraftStatus status; 
        private string id; //to identify any aircraft
        private int distance; //kilometers
        private int speed; //km/h
        private double fuelCapacity; //liters 
        private double fuelConsumption; // liters/km
        private double currentFuel; //liters

        public Aircraft (string id, AircraftStatus status, int distance, int speed, double fuelCapacity, double fuelConsumption, double currentFuel)
        {
            this.id = id;
            this.status = status;
            this.distance = distance;
            this.speed = speed;
            this.fuelCapacity = fuelCapacity;
            this.fuelConsumption = fuelConsumption;
            this.currentFuel = currentFuel;
        }
        public string GetId()
        {
            return this.id;
        }
        public AircraftStatus GetStatus ()
        {
            return this.status;
        }
        public void SetStatus(AircraftStatus newStatus) //This setter is used to change the status of each aircraft based on clicks, as the PWI says 
        {
            this.status = newStatus;
        }
        public int GetDistance()
        {
            return this.distance;
        }
        public int GetSpeed()
        {
            return this.speed;
        }
        public double GetFuelCapacity()
        {
            return this.fuelCapacity;
        }
        public double GetFuelConsumption()
        {
            return this.fuelConsumption;
        }
        public double GetCurrentFuel()
        {
            return this.currentFuel;
        }
    }
}