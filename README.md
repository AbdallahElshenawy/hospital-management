# Hospital Management System

## Overview

The **Hospital Management System** is a comprehensive web application built using ASP.NET Core MVC, designed to streamline and manage various hospital operations. The system includes features for patient registration, doctor management, appointment scheduling, and more. This project was developed as part of a collaborative effort by five team members.

## Table of Contents

- [Features](#features)
- [Technologies Used](#technologies-used)
- [Getting Started](#getting-started)
- [Installation](#installation)
- [Usage](#usage)
- [Team Members](#team-members)
- [Contributing](#contributing)
- [License](#license)

## Features

- **Patient Management**: Register and manage patient records.
- **Doctor Management**: Manage doctor profiles and specializations.
- **Appointment Scheduling**: Book and manage appointments with doctors.
- **Department Management**: Manage hospital departments and their associated doctors.
- **Email Service**: 
  - Account confirmation emails
  - Password reset functionality
  - Reservation complete notifications
- **Role-Based Access Control**: Different levels of access for Admin, Doctors, Assistants, and Patients.
- **Responsive Design**: User-friendly interface compatible with all devices.
- **Authentication & Authorization**: Secure login and user management with ASP.NET Core Identity.

## Technologies Used

- **Frontend**: HTML5, CSS3, Bootstrap
- **Backend**: ASP.NET Core MVC
- **Database**: SQL Server
- **ORM**: Entity Framework Core
- **Version Control**: Git, GitHub
- **Others**: Identity for authentication, AutoMapper for object mapping

## Getting Started

### Prerequisites

- [.NET SDK 7.0 or later](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Visual Studio 2022 or later](https://visualstudio.microsoft.com/)

### Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/ku18m/hospital-management.git
   cd hospital-management
   ```

2. Set up the database:

   - Update the `appsettings.json` file with your SQL Server connection string.
   - Run the following command in the Package Manager Console to apply migrations and seed the database:

     ```bash
     Update-Database
     ```

3. Build and run the project:

   - In Visual Studio, press `F5` or use the following command to run the application:

     ```bash
     dotnet run
     ```

4. Access the application at `https://localhost:{portnumber}/` or `http://localhost:{portnumber}/`.

### Usage

- **Admin Dashboard**: Manage users, doctors, departments, and view reports.
- **Doctor Dashboard**: View and manage appointments, patient records.
- **Patient Portal**: Book appointments, view medical history.
- **Assistant Dashboard**: Manage patient records, assist doctors.

## Team Members

This project was developed by the following team members:

- **[Mohamed Kamal](https://github.com/ku18m)**: Main App, Services.
- **[Abdallah El-Shenawy](https://github.com/AbdallahElshenawy)**: Doctor Portal.
- **[Hazem Mohamed Salah](https://github.com/hazem-mohamed-salah)**: Profile Section.
- **[Ibrahim Saad](https://github.com/IBRAHIM4626)**: Admin Portal
- **[Raneem Makhlouf](https://github.com/RaneemMakhlouf)**: Assistant Portal

## Contributing

We welcome contributions! Please read our [Contributing Guidelines](CONTRIBUTING.md) for details on the process for submitting pull requests.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
