# 🏢 Complete Office Application (COA)

Welcome to the **Complete Office Application** — a secure and extensible internal web platform designed to manage employees, departments, and roles efficiently.

---

## 🚀 Project Overview

The **Complete Office Application (COA)** is a robust intranet tool built with modern Microsoft web technologies. It allows for fine-grained control over user roles, departmental visibility, and access permissions.

### ⚙️ Technology Stack

- **Frontend/Backend**: .NET Razor Pages (ASP.NET Core)
- **Authentication**: Identity with Claims-based security
- **Authorization**: Role-based with hierarchical access
- **Database**: SQL Server + Entity Framework Core
- **Deployment**: Dockerized for containerized environments

---

## 🎯 Key Features

- ✅ **User Registration & Management**
  - Register users with department and position
  - Super Admin can assign roles during signup
  - View/Edit/Delete users within department scope

- ✅ **Departmental Access Control**
  - Users restricted to their department's data
  - Super Admin sees all departments

- ✅ **Super Admin Panel**
  - Manage all users across departments
  - Create users with elevated access
  - Future-ready for full Department CRUD

- ✅ **Authorization System**
  - Positions (e.g., Manager, Director) influence access
  - Claims (`Position`, `Department`) control UI/logic

- ✅ **Docker Deployment**
  - Runs as a container on any Docker-enabled platform
  - Supports custom port mapping
  - Configurable via `appsettings.json`

---

## 📦 Installation & Setup

### 🛠 Requirements

- [.NET SDK 8.0](https://dotnet.microsoft.com/download)
- SQL Server or Azure SQL instance
- Docker Engine

---

### 🔧 Local Development (Windows/Linux/macOS)

1. **Clone the repository:**

   ```bash
   git clone https://github.com/your-org/CompleteOfficeApp.git
   cd CompleteOfficeApp
   
2. **Restore dependencies and migrate the database:

```bash
  dotnet restore
  dotnet ef database update

3. **Run the application:

dotnet run
Access in browser:

http://localhost:5239

or https://localhost:7113
