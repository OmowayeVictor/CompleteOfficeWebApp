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
   
2. **Restore dependencies and migrate the database:**

```bash
  dotnet restore
  dotnet ef database update
```
3. **Run the application:**
```bash
dotnet run
```
**Access in browser:**

 - [http://localhost:5239](http://localhost:5239)  
or
- [https://localhost:7113](https://localhost:7113)

### Docker Build & Deployment

**Build the Docker image:**
```
bash
docker build -t <image_name> .
```
**Run the container:**
```bash
docker run -d -p 3000:8080 --name complete-office-app <image_name
```
**Access the application:**

- [http://localhost:3000](http://localhost:3000)

**📌 You can map the port (-p) to any available host port.**

> **Note:** Make sure to run Migrations on DB.
 ```bash
dotnet ef migrations add <Migration_tag>
dotnet ef database update
```

## 🔧 Sample appsettings.json
**Below is a basic sample appsettings.json for local development or Dockerized environments:**
<pre lang="json"> 
{
  "Admin": {
    "Email": "<Admin Email>",
    "Password": "<Password>"
  },
  "AllowedHosts": "*",
  "App": {
    "BaseUrl": "http://localhost:7113" // Your Base Url
  },
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=(local);Initial Catalog=OfficeWebApp;Integrated Security=True;Trust Server Certificate=True;" //Edit as you feel.
  },
  "ContactUs": {
    "Email": "<Contact us mail>"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "SMTP": {
    "Host": "<>smtp host",
    "Port": 587,
    "Username": "<user name or email>",
    "Password": "Password",
    "Email": "<relay email>" // I used brevo you could try them too
  }
}
 </pre>

**Examples**
- `Register a user in the IT department as a Manager`

- `Login as Super Admin and edit access of users in another department`

- `Deploy the app in Docker for testing or production use`

## 🛠️ Troubleshooting

| Problem                             | Solution                                                                 |
|-------------------------------------|--------------------------------------------------------------------------|
| Can't connect to DB                 | Check connection string in `appsettings.json`.                           |
| Login fails after Docker deploy     | Ensure DB is accessible inside the container or use environment variables for DB config. |
| Permissions not applying correctly  | Verify claims are set correctly on the user and `IsAuthorized` logic is working. |

### 🤝 Contributors
**Initial Developer:** Omowaye Victor
Feel free to open issues or submit pull requests.
