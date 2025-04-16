AirUFV Landing Simulator 
A tick-based aircraft developed by the Winner_Team! -- OOP Final Project, UFV. 
----------------------------------------------------------------------------------

First commit - Aircraft Base Class done
· Implemented the Aircraft Class with the core attributes of the program:
· "id","status","distance","speed","fuelCapacity","fuelConsumption","currentFuel".
· Defined the "AircraftStatus" enum to represent landing process states. 
· Constructor and full encapsulation via getter/setter methods.

Second commit - Specialized Subclasses 
· Added three subclasses that extend "Aircraft" class:
- ComercialAircraft --> "numberofPassengers"
- CargoAircraft --> "maximumLoad" 
- PrivateAircraft --> "owner" 
· Each subclass includes an exclusive own attribute with a getter method. 

Third commit - Runway Class 
· Implemented with:
- "id", "RunwayStatus", "currentAircraft","ticksRemaining".
· Created "RunwayStatus" enum with "Free" and "Occupied" options.
· Added simulation logic:
- RequestRunway() assigns an aircraft for landing if the runway is free.
- AdvanceTick() updates runway state over time (each tick = 15 mins in real life). 
- ReleaseRunway() frees the runway after landing (after 3 ticks).

----------------------------------------------------------------------------------



