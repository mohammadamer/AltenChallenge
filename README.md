### Alten Challenge
## Alten Challenge Solution

## Introduction:
Alten Challenge Solution is a solution for tracking vehicles statuses and the solution is built using Microservices Architecture.

## Solution Architecture:
1. Customer Service
Customer servive is a microservice for managing customers that has vehicles and the CRUD opertion for customers. It has its own database.
also it has its own Endpoints.

2. Vehicle Service
Vehicle Service is a microservice for managing vehicles and CRUD opertions for for Vehicles. It has its own database Endpoints.

3. The Front-End Portal
The front-end portal is a web portal built using .NET core and Angular JS that provides a user experienced user interface of vehicles with  statuses as a real time tracking. The interface will show ping statuses for all vehicles in a single page application.The end user will be able to filter by specific customer or specific vehicle status.

4. Simulation Web Job
The simulation web Job is an Azure Web Job that will simulate the real time ping that run in the back ground every minute and update the vehicles ping log.

## Solution Structure:
The soltion is developed using Visual Studio 2019 and .NET Core 2.2. Dependency Injection applied to every project solution.
every microservice has solution folder and every solution folder has a deployment project to deploy the microservice to Microsoft Azure account.


## Portal:
As explained web front-end is .NET Core 2.2 and angular js project.

## Web APIs:
Every Web API Project has Db initializer to add dummy data to database when the web API first run

## Customers:
[https://localhost:44332/api/Customers/GetCustomers]

## Vehicles:
[http://localhost:9319/api/Vehicles/GetVehicles]

[http://localhost:9319/api/Vehicles/UpdateVehicles]

[http://localhost:9319/api/VehiclesPing/AddPingData]


## Unit Testing
Unit testing is applied to Customers and Vehicles Microservices using nUnit and Moq testing frameworks.




