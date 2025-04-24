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
        private string type; //type of aircraft
        private int speed; //km/h
        private double fuelCapacity; //liters 
        private double consumoCombustible; // liters/km
        private double currentFuel; //liters

        public Aircraft (string id, AircraftStatus status, int distance, string type, int speed, double fuelCapacity, double consumoCombustible, double currentFuel)
        {
            this.id = id;
            this.status = status;
            this.distance = distance;
            this.type = type;
            this.speed = speed;
            this.fuelCapacity = fuelCapacity;
            this.consumoCombustible = consumoCombustible;
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
        public string GetTypeAircraft()
        {
            return this.type;
        }
        public int GetSpeed()
        {
            return this.speed;
        }
        public double GetFuelCapacity()
        {
            return this.fuelCapacity;
        }
        public double GetConsumoCombustible()
        {
            return this.consumoCombustible;
        }
        public double GetCurrentFuel()
        {
            return this.currentFuel;
        }

        //New setters used to create the AdvanceTick System in Airport class!
        public void SetDistance(int d)
        {
            this.distance = d;
        }
        public void SetCurrentFuel(double f)
        {
            this.currentFuel = f;
        }
    }
}