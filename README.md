
# AirUFV Landing Simulator 
A tick-based aircraft developed by the Winner_Team! -- OOP Final Project, UFV (2025). 

## Table of contents: 

1. [Introduction](#introduction)
2. [Project Description](#project-description)
3. [Class Diagram](#class-diagram)
4. [Problems Encountered](#problems-encountered)
5. [Conclusions](#conclusions)

-----------------------------------------------------------------------------------

## INTRODUCTION
Basically, this project simulates the landing process of multiple aircraft at an airport (with the different runways it may has). It models aircraft behavior using the object-orie**nted programming principles (inheritance, encapsulation and class interaction). 

**Team members**:
- Juan Cisneros Amengual
- Carlos Jiménez Pelegrín
- Hugo Sanz Hernández
- We are group `10`!

As PW says, the simulator is a tick-based simulator, updating every 15 minute interval, managing different situations:
- Aircraft `distance` to the airport and `fuel` consumption. 
- Runway assignament and the process of landing.
- What happens if the aircraft crashes due to fuel exhaustion.

## Project Description

### Structure overview
The system is structured in multiple classes:
- **Aircraft (Base Class)**  
  With common attributes: `id`, `status`, `distance`, `speed`, `fuelCapacity`, `consumoCombustible`, `currentFuel`, `type`
  
- **Subclasses**  
  - `CommercialAircraft`: adds `numberOfPassengers`
  - `CargoAircraft`: adds `maximumLoad`
  - `PrivateAircraft`: adds `owner`

- **Runway**  
  - Attributes: `id`, `status`, `currentAircraft`, `ticksRemaining`
  - Methods: `RequestRunway()`, `AdvanceTick()`, `ReleaseRunway()`

  - **Airport**  
  - Manages aircrafts and a 2D matrix of runways
  - Methods:
    - `AddAircraft()`
    - `LoadAircraftFromFile()`
    - `AddAircraftManually()`
    - `AdvanceTick()`
    - `ShowStatus()`

### Class diagram

## Difficulties Encountered and Solutions

### 1. Using a variable outside a foreach loop
- Error:
foreach (Runway runway in runways)
{
    Console.WriteLine("Runway info:");
}
if (runway.GetStatus() == RunwayStatus.Free) //here starts our error!
{
    Console.WriteLine("Free");
}
- Solution: move the if block inside the foreach block and add the corresponding keys {}.

### 2. Calling a method on an array directly 
- Error: 
Console.WriteLine("Runway ID: " + runways.GetId());

- Solution: basically, Runway[,] doesn't contain a definition for GetId, we should use a loop to access each runway:

foreach (Runway runway in runways)
{
    Console.WriteLine("Runway ID: " + runway.GetId());
}

### 3. Common errors
This section is not an specific one, but we have some troubles with the keys {} and the scope of some loops, in particular with foreach loops. 

### 4. Aircraft data was not updating correctly inside AdvanceTick()
- Error: 
While implementing AdvanceTick(), we initially calculated the new values for distance and fuel like this:

- double newDistance = aircraft.GetDistance() - kmFlown;
- double newFuel = aircraft.GetCurrentFuel() - boostUsed;

However, we forgot to apply those changes back to the object, so the aircraft status never transitioned to Waiting, even after reaching the airport.
Despite flying the required distance, the aircraft status remained as InFlight, and the system never allowed it to land.

- Solution: We created two new setter methods inside the Aircraft class, ass you can see in the sixth commit, and then we updated the actual aircraft values using these methods:

aircraft.SetDistance((int)newDistance); the int is because SetDistance needs an int attribute
aircraft.SetCurrentFuel(newFuel);

### 5. Cannot use breaks
Not using breaks posed a problem for us in dealing with state changes in aircrafts. For this, we used flags, which are bool variables that allow us to handle loops in a similar, but cleaner and more precise way than using breaks. This technique was taught to us by Hugo Cisneros, a fourth-year student.

### 6. Runways not refresh 
This is because we have created the correct methods in Runway class, but we haven't actualiced during the AdvanceTick() method! 
The solution was quite easy, just actualice each object runway of class Runway in the list runways! 
    foreach (Runway runway in runways) //We refresh the status of the runways
    {
        runway.AdvanceTick();
    }
### 7. Refresh problem! 
When ticks goes 0, there was one click delay between the runway and the airport: the runway was free but the aircraft still in landing status. The solution was quite easy, just refresh all the runways before we touch the aircrafts! 


## What we have done in each commit more explained 

### First commit - `Aircraft` Base Class done
· Implemented the `Aircraft` Class with the core attributes of the program:
· `id`,`status`,`distance`,`speed`,`fuelCapacity`,`fuelConsumption`,`currentFuel`.
· Defined the `AircraftStatus` enum to represent landing process states. 
· Constructor and full encapsulation via getter/setter methods.

### Second commit - Specialized Subclasses 
· Added three subclasses that extend "Aircraft" class:
- `ComercialAircraft` --> "numberofPassengers"
- `CargoAircraft` --> "maximumLoad" 
- `PrivateAircraft` --> "owner" 
· Each subclass includes an exclusive own attribute with a getter method. 

### Third commit - `Runway` Class 
- Implemented with:
    - `id`, `RunwayStatus`, `currentAircraft`,`ticksRemaining`.
- Created `RunwayStatus` enum with `Free` and `Occupied` options.
- Added simulation logic:
    - `RequestRunway()` assigns an aircraft for landing if the runway is free.
    - `AdvanceTick()` updates runway state over time (each tick = 15 mins in real life). 
    - `ReleaseRunway()` frees the runway after landing (after 3 ticks).

### Fourth commit - Attribute Name Standardization

- Replaced `fuelConsumption` with `consumoCombustible` across all classes, in order to follow the rules of the PW.
- Introduced `type` attribute that was previously missing from the Aircraft class.
- Subclasses updated to pass their corresponding type strings. 

### Fifth commit - `Airport` Class done!
- Created the `Airport` class to manage all aircraft and runways in the simulation. 
- Attributes:
- `aircrafts`: a List<Aircraft> containing all the planes in the system. 
- `runways`: a 2D array of `Runway` objects to simulate the physical layout of an airport.
- Constructor:
    - Recieves the number of rows and columns for the runways. 
    - Automatically generates runways ID (Runway-1, Runway-2 ... Runway-n)
    - Implemented `AddAircraft()`, to add aircrafts to the system. 
    - Implemented `ShowStatus()`, to: 
    - Display the status of each runway (`Free` or `Occupied` with ticks remaining)
    - Show the list of all aircraft with their own `ID`, `status`, `distance`, `fuel` and `type`.

### Sixth commit - `Airport` Class methods needed done! 
- Created the `AdvanceTick()` method! 
- This method is probably the most complicated that we have done during the PW. It simulates the passage of time (1 tick = 15 minutes)
- We have evaluated the different states of the aircrafts: inflight, waiting, landing and onground.
- `Inflight` state: it reduces the aircraft distance and fuel based on speed and consumption per km. 
    - If the distance reaches 0, the aircraft changes to waiting state, because we assume that the aircraft has already reached the airport.
- `Waiting` state: basically searches for a free runway. If is available, assigns it to the aircraft and set the status of this aircraft to landing. 
- `Landing` state: in this state, the aircraft states in the airport during 3 ticks, as the PW says. Once the aircraft has occupied a runway for 3 ticks, the runway is released.
    - The aircraft transitions to `OnGround` if it's no longer assigned to any runway. 
- Setters added in Aircraft class, to manipulate the distance and fuel of the aircrafts.

### Seventh Commit – LoadAircraftFromFile() method + Fuel Crash Logic done!
- Implemented the method LoadAircraftFromFile() inside the Airport class.
- Loads aircraft from a `.csv` file with fields: 
  - `ID`, `Status`, `Distance`, `Speed`, `Type`, `FuelCapacity`, `ConsumoCombustible`, and an extra depending on the type (passengers, cargo, or owner).
- Supports the 3 aircraft subclasses.
- Includes full input validation and error handling (format issues, unknown types, etc.).
- Added crash logic (as Moisés told us in class):
  - During `AdvanceTick()`, if an aircraft's fuel reaches 0 while flying, it's removed from the simulation and a crash message is shown.
    - To do this, we created an auxiliar list which has all the crashed aircrafts (because they have no fuel).

### Eight Commit - AddAircraftManually() from Console
- Added the method `AddAircraftManually()` in the Airport class.
- Allows the user to create aircraft manually via keyboard.
- Prompts user to select aircraft type and enter all required fields:
  - `ID`, `status`, `distance`, `speed`, `fuel capacity`, `fuel consumption` and `type-specific attribute`.

### Nine Commit - Detailed Document modified as Moisés wants! 
- Just ordered the information in a correct order and added our names, number group, etc...
- Table of contents added!
- We also added the Structure overview, which explains what are the different methods and attributes used in the different classes. 
- In the next commit, we will add the class diagram. 

### Tenth Commit - Modified airport class and modified the `Airport` class interface! 
- Moisés told us during a class that he created an animated ASCII airport... So we tried to upgrade our animations!


----------------------------------------------------------------------------------




