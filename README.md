# 🚀 SciTrack-Web-2025

A web application for managing and tracking scientific research projects, contracts, and assets. This is a university project (`đồ án`) built with ASP.NET Core.

---

### Frameworks and Languages
![C#](https://img.shields.io/badge/C%23-11.0-512BD4?logo=c-sharp)
![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-8.0-512BD4?logo=dotnet)
![Entity Framework Core](https://img.shields.io/badge/EF%20Core-8.0-512BD4?logo=dotnet)
![HTML5](https://img.shields.io/badge/HTML5-E34F26?logo=html5&logoColor=white)
![CSS3](https://img.shields.io/badge/CSS3-1572B6?logo=css3&logoColor=white)
![JavaScript](https://img.shields.io/badge/JavaScript-F7DF1E?logo=javascript&logoColor=black)
![Bootstrap](https://img.shields.io/badge/Bootstrap-5.3-7952B3?logo=bootstrap&logoColor=white)

---

## 📝 Project Description

**SciTrack** is a comprehensive management system designed to handle the lifecycle of scientific research projects (Khoa học Công nghệ - KHCN). It consists of two main parts:
1.  **`SciTrack.Api`**: An ASP.NET Core REST API backend that manages all business logic and database interactions using Entity Framework Core.
2.  **`SciTrack.web`**: An ASP.NET Core MVC frontend that consumes the API to provide a user-friendly interface for managing data.

The system allows users to track research topics (Đề tài), associated contracts (Hợp đồng), project outcomes (Kết quả), and the equipment (Thiết bị) and assets (Tài sản) used.

## ✨ Key Features

* **Project Management (Đề tài)**: Full CRUD operations for creating, reading, updating, and deleting research projects.
* **Contract Management (Hợp đồng)**: Track contracts associated with each research project.
* **Asset Management (Tài sản)**: Manage scientific assets and equipment (Thiết bị) linked to projects.
* **Results Tracking (Kết quả)**: Record and manage the outcomes and publications derived from research.
* **RESTful API**: A separate, decoupled API for data handling, allowing for potential future integrations (e.g., a mobile app).
* **MVC Frontend**: A clean, server-side rendered web interface for easy data management.

## ⚙️ Installation

To get the project up and running on your local machine, follow these steps.

**Prerequisites:**
* [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
* A SQL Server instance (like SQL Server Express or Developer Edition)

**Steps:**
1.  **Clone the repository:**
    ```sh
    git clone [https://github.com/giaphong2004/SciTrack-Web-2025.git](https://github.com/giaphong2004/SciTrack-Web-2025.git)
    cd SciTrack-Web-2025
    ```

2.  **Configure the Database:**
    * Open the `SciTrack.Api/appsettings.Development.json` file.
    * Find the `ConnectionStrings` section.
    * Update the `DefaultConnection` value to point to your local SQL Server instance.
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=KhcnDbNew;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
    }
    ```

3.  **Restore Dependencies and Build:**
    * Open a terminal in the root folder (where `SciTrack.sln` is).
    * Run the following commands:
    ```sh
    dotnet restore
    dotnet build
    ```

4.  **Run Database Migrations (if applicable):**
    * If you are using EF Core migrations, navigate to the API project folder and run:
    ```sh
    cd SciTrack.Api
    dotnet ef database update
    ```

## ▶️ Usage

You need to run **both** the API and the Web projects simultaneously.

1.  **Run the API:**
    * Open a terminal and navigate to the API project folder:
    ```sh
    cd SciTrack.Api
    dotnet run
    ```
    (By default, this will likely run on `https://localhost:7123` and `http://localhost:5123`. Check the terminal output.)

2.  **Run the Web App:**
    * Open a *new* terminal and navigate to the Web project folder:
    ```sh
    cd SciTrack
    dotnet run
    ```
    (By default, this will likely run on `https://localhost:7179` and `http://localhost:5179`. Check the terminal output.)

3.  Open your browser and navigate to the web app's URL (e.g., `https://localhost:7179`).

## 👨‍💻 Team Members

This project is developed by a team of 6 students:
* [**@giaphong2004**](https://github.com/giaphong2004) - (Leader)
* [**@ndt251004-afk**](https://github.com/ndt251004-afk) - (Front End)
* [**@NHLongg789**](https://github.com/NHLongg789) - (Back End)
* [**@dangthiphonglan**](https://github.com/dangthiphonglan) - (Business Analyst)
* [**@huuhhung9200**](https://github.com/huuhhung9200) - (Database Engineer)
* [**@TuanHai24**](https://github.com/TuanHai24) - (Tester)
