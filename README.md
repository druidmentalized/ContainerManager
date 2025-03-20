# Container Management System

The **Container Management System** is a C# application designed to manage different types of cargo containers on ships. It supports **refrigerated**, **liquid**, and **gas** containers, while enforcing rules about hazardous materials, maximum capacity, weight limits, and temperature requirements.

## Features

- **Refrigerated Containers**  
  - Enforces minimum temperature requirements per product type.  
  - Stores cargo of only a single product type.  
- **Liquid Containers**  
  - Supports hazardous (50% capacity limit) or non-hazardous (90% capacity limit) cargo.  
  - Implements `IHazardNotifier` for sending notifications in dangerous situations.  
- **Gas Containers**  
  - Contains additional attributes like pressure.  
  - Leaves 5% cargo on emptying.  
- **Ship Management**  
  - Stores containers in a dictionary keyed by their serial numbers.  
  - Enforces maximum container count and weight capacity.  
  - Offers operations like adding, removing, replacing containers, and transferring containers between ships.  
- **Exception Handling**  
  - Throws custom exceptions (`OverfillException`, `TemperatureDiscrepancyException`, etc.) when rules are violated.  
  - Forces the caller to handle invalid states (e.g., overfilling, mismatched product types).

## Usage

1. **Clone or download** the repository.
2. **Open** the project in your favorite C# IDE (e.g., Visual Studio, Rider, VS Code).
3. **Compile** and **run** the application.

## Built with 
- C# 10 / .NET 6 or higher (recommended)
- Dictionary and LINQ (for container storage and lookups)
- Custom exception classes for rule enforcement

## Project Structure
- 'Program.cs' - Entry point of the program
- 'ContainerManager/containers' - All Containers
- 'ContainerManager/exceptions' - Custom exception classes
- 'ContainerManager/main' - Main classes
- 'ContainerManager/transports' - All Transports in the application
- 'ContainerManager/utils' - Utility classes

## License
This project is licensed under the MIT License
