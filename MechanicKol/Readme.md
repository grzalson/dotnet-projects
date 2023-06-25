# About this project
## Introduction
This project is a basic REST API that could be used in a mechanic's workshop. 
It was written in a codefirst aproach using entitty framework.
## Structure

### Controller

#### GET
This method allows user to get a mechanic with given id along with a list of cars that he's worked on. 
If there is no mechanic with that id the method returns Not Found response code with an according message for the user.

#### POST
This method allows user to add a new car, 
then assgin this car to a make (if make with given name doesn't exist, adds this make to database), 
lastly assigns this car to a mechanic.
It checks if a car with given registration plate doesn't already exist in a database,
checks if there is a mechanic with given id abd makes sure that
the production date of this car is not from the future. 
If any of these cases is true then it returns proper status code with an accoring message for the user.

### Service
With a usage of dependency injection it serves the controller to handle all of the tasks

1. DoesMechanicExist(id)
    - Checks if a mechanic with given id exists in database

2. DoesCarExist(id)
    - Checks if a car with given id exists in database 

3. GetMechanicsCars(id)
    - Returns a mechanic with given id along with a list of cars that he's worked on. 

4. AddNewCar(car)
    - Adds a new car to databes, assigns this car to a make 
and if this make does not exist already adds it to database, assigns this car to a mechanic.

## What I learnedd
This project showed me how is it like to work with .NET 
