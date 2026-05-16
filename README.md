<div align="center">
  <h1>🎬 CineReserve</h1>
  <p><b>Experience Cinema Like Never Before.</b></p>
  <p>A premium, real-time movie ticket booking platform.</p>

  <div>
    <img src="https://img.shields.io/badge/.NET-10.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt=".NET 10" />
    <img src="https://img.shields.io/badge/Angular-21-DD0031?style=for-the-badge&logo=angular&logoColor=white" alt="Angular 21" />
    <img src="https://img.shields.io/badge/TypeScript-007ACC?style=for-the-badge&logo=typescript&logoColor=white" alt="TypeScript" />
    <img src="https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white" alt="SQL Server" />
  </div>
</div>

<br />

## 📖 About The Project

**CineReserve** is a comprehensive, modern cinema ticketing system designed to deliver a seamless user experience. It tackles the complexities of real-time seat reservation, scheduling, and administrative management through a highly scalable, decoupled architecture. 

With a focus on performance and aesthetics, CineReserve offers high-fidelity visual seat mapping and robust transactional integrity to prevent double-booking, ensuring a flawless booking flow from start to finish.

---

## ✨ Key Features

- 🎟️ **Real-Time Booking Engine**: High-performance, concurrent ticket reservation system preventing double-booking through robust backend transaction handling.
- 💺 **Interactive Visual Seat Mapping**: High-fidelity, dynamic theater layouts built in Angular, allowing precise and intuitive seat selection.
- 📊 **Comprehensive Admin Power-Hub**: A dedicated administrative dashboard for managing movies, screening schedules, and monitoring analytics.
- 🔐 **Secure Authentication**: Industry-standard JWT (JSON Web Tokens) authentication ensuring data protection and role-based access control (RBAC).
- 📱 **Responsive & Premium Design**: Custom-crafted, high-contrast UI tailored for modern devices, avoiding generic templates to deliver a premium user experience.

---

## 🛠️ Technology Stack

### **Frontend**
- **Framework**: Angular 21 (Standalone Components)
- **Language**: TypeScript
- **State Management & Reactivity**: RxJS, Signals
- **Styling**: Custom CSS / Bootstrap for grid layouts

### **Backend**
- **Framework**: ASP.NET Core Web API (.NET 10)
- **Language**: C#
- **Database**: SQL Server
- **ORM**: Entity Framework Core
- **Design Pattern**: Clean Architecture, Repository Pattern, Dependency Injection

---

## 🏗️ Architecture & Folder Structure

The repository is built using **Clean Architecture** principles, effectively separating concerns between presentation, business logic, and data access layers.

```text
CineReserve/
├── Backend/                            # .NET 10 Solution
│   └── CineReserve/
│       ├── CineReserve.API/            # Presentation Layer: Controllers, Middlewares
│       ├── CineReserve.Application/    # Business Logic: Services, DTOs, Mappings
│       ├── CineReserve.Domain/         # Core: Entities, Interfaces, Enums
│       ├── CineReserve.Infrastruture/  # Data: EF Core DbContext, Repositories
│       └── CineReserve.Tests/          # Unit and Integration Tests
│
├── Frontend/                           # Angular 21 Application
│   └── CineReserve/
│       ├── src/
│       │   ├── app/
│       │   │   ├── components/         # Modular UI Components (Admin, Home, etc.)
│       │   │   ├── interceptors/       # HTTP Interceptors (JWT, Error handling)
│       │   │   ├── models/             # Shared TypeScript Interfaces
│       │   │   └── services/           # API Integration and State Management
│       │   ├── assets/                 # Static assets (images, fonts, icons)
│       │   └── index.html              # Entry point
│       ├── angular.json                # Angular workspace configuration
│       └── package.json                # Dependency definitions
│
└── README.md                           # Project Documentation
```

---

## ⚙️ Getting Started

Follow these instructions to get a copy of the project up and running on your local machine for development and testing purposes.

### **1. Prerequisites**
- [.NET 10 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/10.0)
- [Node.js (v18+)](https://nodejs.org/) & npm
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or LocalDB

### **2. Database Setup**
Update the connection string in `Backend/CineReserve/CineReserve.API/appsettings.json` to point to your local SQL Server instance:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=CineReserveDb;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False"
}
```

### **3. Backend Setup**
Open your terminal and run the following commands to restore dependencies, apply migrations, and launch the API:
```bash
cd Backend/CineReserve/CineReserve.API
dotnet restore
dotnet ef database update
dotnet run
```
> **Note:** The API will typically start on `https://localhost:7116` or `http://localhost:5249`. You can explore the endpoints via the Swagger UI available at `/swagger`.

### **4. Frontend Setup**
Open a separate terminal to initialize and run the Angular frontend:
```bash
cd Frontend/CineReserve
npm install
npm start
```
> **Note:** Visit `http://localhost:4200` in your browser to experience CineReserve.

---

## 📸 Application Previews

| User Dashboard | Admin Console | Seat Selection |
| :---: | :---: | :---: |
| ![User View](https://placehold.co/300x200/2563eb/white?text=User+UI) | ![Admin View](https://placehold.co/300x200/0f172a/white?text=Admin+Dashboard) | ![Seats](https://placehold.co/300x200/1e293b/white?text=Interactive+Seats) |

*(Add high-quality screenshots here to showcase the platform's UI/UX)*

---

## 🛡️ License

Distributed under the **MIT License**. See `LICENSE` for more information.

---

<p align="center">
  Crafted with ❤️ by Prithviraj
</p>
