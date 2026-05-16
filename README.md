# 🎬 CineReserve

<div align="center">
  <img src="./cinereserve_banner.png" alt="CineReserve Banner" width="800" onerror="this.style.display='none'"/>
  <h3><strong>Experience Cinema Like Never Before.</strong></h3>
  <p>A premium, real-time movie ticket booking platform.</p>
</div>

---

## 🏆 Hackathon Project Overview

**CineReserve** was developed as a comprehensive solution for modernizing the cinema ticketing experience. Built during a fast-paced hackathon, this project demonstrates full-stack engineering capabilities, clean architecture principles, and premium UI/UX design. 

Our goal was to solve the common issues in existing ticketing systems: high latency during peak booking times, poor visual representation of seating, and disjointed admin management. CineReserve delivers a responsive, real-time, and visually stunning platform that caters to both cinema-goers and theater administrators.

---

## 🚀 Key Features & Impact

- 🎟️ **Real-Time Booking Engine**: High-performance, concurrent ticket reservation system preventing double-booking through robust backend transaction handling.
- 💺 **Interactive Visual Seat Mapping**: High-fidelity, dynamic theater layouts built in Angular, allowing precise and intuitive seat selection.
- 📊 **Comprehensive Admin Power-Hub**: A dedicated administrative dashboard for managing movies, screening schedules, and monitoring analytics.
- 💳 **Integrated Wallet System**: Secure, simulated top-up wallet system enabling one-click, seamless payments.
- 🔐 **Secure Authentication**: Industry-standard JWT (JSON Web Tokens) authentication ensuring data protection and role-based access control (RBAC).
- 📱 **Responsive & Premium Design**: Custom-crafted, high-contrast UI tailored for modern devices, avoiding generic templates to deliver a "WOW" factor.

---

## 🏗️ Architecture & Technology Stack

CineReserve is built using a highly scalable and decoupled architecture, separating concerns between the client and server.

### **Frontend** (Angular 18)
- **Framework**: Angular 18 (Standalone Components)
- **Language**: TypeScript
- **State Management & Reactivity**: RxJS, Signals
- **Styling**: Custom CSS / Bootstrap for grid layouts

### **Backend** (.NET 8 Clean Architecture)
- **Framework**: ASP.NET Core Web API (.NET 8)
- **Language**: C#
- **Database**: SQL Server
- **ORM**: Entity Framework Core
- **Design Pattern**: Clean / Layered Architecture, Repository Pattern, Dependency Injection

---

## 📂 Project Folder Structure

The repository is divided into two main applications: Frontend and Backend.

```text
CineReserve/
├── Backend/                            # .NET 8 Backend Solution
│   └── CineReserve/
│       ├── CineReserve.API/            # Presentation Layer: Controllers, Middlewares, Program.cs
│       ├── CineReserve.Application/    # Business Logic Layer: Services, DTOs, Mapping Profiles
│       ├── CineReserve.Domain/         # Core Layer: Entities, Models, Enums
│       ├── CineReserve.Infrastruture/  # Data Layer: EF Core DbContext, Migrations, Repositories
│       └── CineReserve.Tests/          # Unit and Integration Tests
│
├── Frontend/                           # Angular 18 Frontend Application
│   └── CineReserve/
│       ├── src/
│       │   ├── app/
│       │   │   ├── components/         # UI Components (Admin, Dashboard, Home, Movie, etc.)
│       │   │   ├── interceptors/       # HTTP Interceptors (JWT attaching, Error handling)
│       │   │   ├── models/             # TypeScript Interfaces and Types
│       │   │   └── services/           # Angular Services (API integration, Auth, State)
│       │   ├── assets/                 # Static assets (images, icons)
│       │   └── index.html              # Main HTML entry point
│       ├── angular.json                # Angular configuration
│       └── package.json                # Node.js dependencies
│
└── README.md                           # Project Documentation
```

---

## ⚙️ Installation & Setup Guide

### **1. Prerequisites**
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Node.js (v18+)](https://nodejs.org/) & npm
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or LocalDB

### **2. Database Setup**
Update the connection string in `Backend/CineReserve/CineReserve.API/appsettings.json` to point to your local SQL Server instance.
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=CineReserveDb;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False"
}
```

### **3. Backend Setup**
Open a terminal and run the following commands to restore dependencies, apply migrations, and start the API:
```bash
cd Backend/CineReserve/CineReserve.API
dotnet restore
dotnet ef database update  # Optional if auto-migration is not enabled
dotnet run
```
*The API will typically start on `https://localhost:7116` or `http://localhost:5249`. A Swagger UI is available at `/swagger` for API exploration.*

### **4. Frontend Setup**
Open a separate terminal and run:
```bash
cd Frontend/CineReserve
npm install
npm start
```
*Visit `http://localhost:4200` in your browser to experience CineReserve.*

---

## 📸 Application Previews

| User Dashboard | Admin Console | Seat Selection |
| :---: | :---: | :---: |
| ![User View](https://placehold.co/300x200/2563eb/white?text=User+UI) | ![Admin View](https://placehold.co/300x200/0f172a/white?text=Admin+Dashboard) | ![Seats](https://placehold.co/300x200/1e293b/white?text=Interactive+Seats) |

*(Note: Replace placeholder images with actual high-quality screenshots before final presentation)*

---

## 🛡️ License

This project is licensed under the **MIT License**.

---

<p align="center">
  Crafted with passion by the <strong>CineReserve Team</strong> for the Hackathon Evaluation.
</p>
