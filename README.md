----------------------------------------------------------------------------------
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

Fourth commit - Attribute Name Standardization

· Replaced "fuelConsumption" with "consumoCombustible" across all classes, in order to follow the rules of the PW.
· Introduced "type" attribute that was previously missing from the Aircraft class.
· Subclasses updated to pass their corresponding type strings. 

Fifth commit - Airport Class done!
· Created the "Airport" class to manage all aircraft and runways in the simulation. 
· Attributes:
- aircrafts: a List<Aircraft> containing all the planes in the system. 
- runways: a 2D array of "Runway" objects to simulate the physical layout of an airport.
· Constructor:
- Recieves the number of rows and columns for the runways. 
- Automatically generates runways ID (Runway-1, Runway-2 ... Runway-n)
- Implemented "AddAircraft()", to add aircrafts to the system. 
- Implemented "ShowStatus()", to: 
- Display the status of each runway (Free or Occupied with ticks remaining)
- Show the list of all aircraft with their own ID, status, distance, fuel and type.
----------------------------------------------------------------------------------
Difficulties Encountered and Solutions

Airport Class:

#1. Using a variable outside a foreach loop
· Error:
foreach (Runway runway in runways)
{
    Console.WriteLine("Runway info:");
}
if (runway.GetStatus() == RunwayStatus.Free) //here starts our error!
{
    Console.WriteLine("Free");
}
· Solution: move the if block inside the foreach block and add the corresponding keys {}.

#2. Calling a method on an array directly 
· Error: 
Console.WriteLine("Runway ID: " + runways.GetId());

· Solution: basically, Runway[,] doesn't contain a definition for GetId, we should use a loop to access each runway:

foreach (Runway runway in runways)
{
    Console.WriteLine("Runway ID: " + runway.GetId());
}

#3. Common errors
This section is not an specific one, but we have some troubles with the keys {} and the scope of some loops, in particular with foreach loops. 




