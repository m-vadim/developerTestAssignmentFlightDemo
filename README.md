
**Test Assignment for a Developer: API for a Flight Search Aggregator**

**Goal:**

Develop an API that aggregates information about available flights from various sources and provides this information upon client request.

**Tasks:**

1.  **Flight Data Aggregation API**  
    Develop an API to aggregate flight data. The API should be able to handle flight search requests, aggregate data from various sources (at least 2 fictional test sources), and return it in a unified format.
    
2.  **Filtering and Sorting of Results**  
    The API should provide filtering capabilities (by date, price, number of layovers, airline) and allow sorting of search results.
    
3.  **Booking Selected Flights**  
    The API should provide functionality to book a selected flight by sending a corresponding booking request to the source from which the flight was retrieved. The request format can be designed at your discretion.
    
4.  **Request Caching**  
    Implement a caching mechanism for frequently requested queries to reduce server load and improve response times.
    
5.  **Request Logging**  
    All requests to the API must be logged for further analysis.
    
6.  **Handling Long Response Times from Sources**  
    Ensure proper handling of unpredictably long response times from sources.


# FlySearch

## Overview

The FlySearch solution is a C# project designed to handle flight search and booking operations. It includes multiple components such as APIs, console clients, and query handlers to manage flight-related data and operations.

## Features

- **Flight Search**: Search for flights based on various criteria such as flight number, origin, destination, and date.
- **Flight Booking**: Book flights by providing airline, flight number, seat number, and username.
- **Console Client**: A demo application to interact with the flight API and showcase its functionality.
- Vertical slices architecture with CQRS pattern

## Project Structure

### `FlySearch.AirwaysApi`
- Contains demo api for 2 dummy airlines: Royal Air and Hot Air
- See [Swagger UI](http://localhost:5090/swagger/index.html) for API documentation
- RandomDelayMiddleware can be enabled in Program to simulate long waits

### `FlySearch.AggregateApi`
- Contains api to aggregate results from demo airlines
	- AirwaysClient - contains code to work with airline api
	- Features - vertical slices based architecture implementation for find flight feature and book flight feature
	- Infrastructure - common code
	- Options - options for project
- See [Swagger UI](http://localhost:5283/swagger/index.html) for API documentation
- Timeout 3 seconds set for Refit client factor


### `FlySearch.AggregateApi.UnitTests`
- Unit tests project for FlySearch.AggregateApi
- Example: `FlySearch.AggregateApi.UnitTests.AirwaysClient.HotAir.HotAirAirlineClientTests.cs` 

### `FlySearch.ConsoleClient`
- A console-based client application to demonstrate the usage of the flight API.
- Example: `Demo.cs` contains a demo workflow for searching and booking flights.

### `FlySearch.CommandQueryDispatcher`
- Simple implementation of the command-query responsibility segregation (CQRS) pattern. 
- Example: `FindFlightsQueryHandler` processes flight search queries.


## Technologies Used

- **C#**: Primary programming language.
- **ASP.NET Core**: For building APIs.
- **Refit**: For making HTTP requests in the console client.
- **LINQ**: For querying and filtering data.
- **Task-based Asynchronous Programming**: For handling asynchronous operations.

## How to Run

1. Clone the repository:
   ```bash
   git clone <repository-url>
   cd FlySearch
   ```

2. Open the solution in your IDE (e.g., JetBrains Rider).

3. Restore NuGet packages:
   ```bash
   dotnet restore
   ```

4. Build the solution:
   ```bash
   dotnet build
   ```

5. Run the Airways api project
   ```bash
   dotnet run --project FlySearch.AirwaysApi
   ```
6. Run the Aggregate api project
   ```bash
   dotnet run --project FlySearch.AggregateApi
   ```

7. Run the console client to see the demo:
   ```bash
   dotnet run --project FlySearch.ConsoleClient
   ```
