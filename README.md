# RouteeFailoverMessages.API

A robust .NET Core API designed to manage failover messaging using Routee's communication platform. This solution ensures message delivery reliability by implementing failover strategies across multiple channels.

## Features

- **Failover Messaging**: Automatically reroutes messages through alternative channels upon failure.
- **Modular Architecture**: Clean separation of concerns with distinct layers:
  - `RouteeFailoverMessages.API`: API endpoints and controllers.
  - `RouteeFailoverMessages.Domain`: Core business logic and domain entities.
  - `RouteeFailoverMessages.Data`: Data access and persistence.
  - `RouteeFailoverMessages.Library`: Shared utilities and services.
- **Extensibility**: Easily add support for new messaging channels or providers.
- **Logging & Monitoring**: Integrated logging for monitoring message flows and failures.

## Technologies Used

- [.NET Core](https://dotnet.microsoft.com/)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [Swagger](https://swagger.io/) for API documentation
- [xUnit](https://xunit.net/) for unit testing

### Prerequisites

- [.NET Core SDK 3.1 or later](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server) or any other supported database
