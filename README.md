# Restaurant Management System

A restaurant management web application with admin panel, built using ASP.NET Core.

## About

This project consists of two main parts:
- **WebAPI** - REST API backend running on port 7020
- **WebUI** - Admin panel and frontend running on port 7002

## Features

- Product and category management
- Chef management
- Reservation system (individual & group)
- Customer messages
- Testimonials
- Event management
- Real-time chat with SignalR
- Dashboard with statistics

## Technologies

- ASP.NET Core 6.0
- Entity Framework Core
- SQL Server
- SignalR
- Bootstrap (Otika Admin Template)

## How to Run

1. Clone the repo
2. Update connection string in `appsettings.json`
3. Run database migrations
4. Start both projects:

```bash
# Terminal 1 - API
cd ApiProjeKampi.WebApi
dotnet run

# Terminal 2 - Web UI
cd ApiProjeKampi.WebUI
dotnet run
```

5. Open `https://localhost:7002` in browser

## Project Structure

```
├── ApiProjeKampi.WebApi/    # REST API
├── ApiProjeKampi.WebUI/     # MVC Frontend
└── ApiProjeKampi.sln
```
