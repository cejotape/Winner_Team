using System;
using System.Net;
namespace AirUFV
{
    public enum RunwayStatus
    {
        Free, Occupied
    }
    public class Runway
    {
        private string id;
        private RunwayStatus status;
        private Aircraft currentAircraft;
        private int ticksRemaining;

        public Runway(string id)
        {
            this.id = id;
            this.status = RunwayStatus.Free;
            this.currentAircraft = null; 
            this.ticksRemaining = 0;
        }
        public string GetId()
        {
            return this.id;
        }
        public RunwayStatus GetStatus()
        {
            return this.status;
        }
        public Aircraft GetCurrentAircraft()
        {
            return this.currentAircraft;
        }
        public int GetTicksRemaining()
        {
            return this.ticksRemaining;
        }
        public bool IsFree()
        {
            return this.status == RunwayStatus.Free;
        }
        public bool RequestRunway(Aircraft aircraft)
        {
            if(this.status == RunwayStatus.Free)
            {
                    this.currentAircraft = aircraft;
                    this.status = RunwayStatus.Occupied;
                    this.ticksRemaining = 3;
                    return true;
            }
            return false;
        }
        public void AdvanceTick()
        {
            if(this.status == RunwayStatus.Occupied && ticksRemaining > 0)
            {
                this.ticksRemaining --;
                if(this.ticksRemaining == 0)
                {
                    ReleaseRunway();
                }
            }
        }
        public void ReleaseRunway()
        {
            this.currentAircraft = null;
            this.status = RunwayStatus.Free;
            this.ticksRemaining = 0;
        }
    }
}