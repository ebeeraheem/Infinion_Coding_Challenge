# Infinion Coding Challenge
This project is a robust web application built using ASP.Net Web API (C#) that allows users to register, login, and perform CRUD operations on products with pagination and filter support. The project follows the Earth Layer Architecture.

## Table of Contents
- [Features](#features)
- [Prerequisites](#prerequisites)
- [Setup and Installation](#setup-and-installation)
- [Running the Application](#running-the-application)
- [API Endpoints](#api-endpoints)

## Features

1. **User Registration API**:
   - POST endpoint for user registration.
   - Validates email, password, first name, and last name.
   - Sends a confirmation email upon successful registration.
   - Saves user details in the database.

2. **User Login API**:
   - POST endpoint for user login.
   - Authenticates users using email and password.
   - Generates and returns a JWT token upon successful authentication.
   - Requires JWT token for subsequent requests.

3. **Product CRUD Operations**:
   - Endpoints to Create, Read, Update, and Delete (CRUD) products.
   - Supports pagination and filtering for product listings. 

## Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or any other compatible database
- [MailKit](https://www.nuget.org/packages/MailKit/) version 4.6.0 by _Jeffrey Stedfast_ for email confirmation
- [Swashbuckle.AspNetCore.Filters](https://www.nuget.org/packages/Swashbuckle.AspNetCore.Filters/)

## Setup and Installation

1. Clone the repository: 
   ```bash
   git clone https://github.com/yourusername/infinion-coding-challenge.git
   cd infinion-coding-challenge
   ```
2. Restore the NuGet packages:
   ``` bash
   dotnet restore
   ```
3. Update the 'appsettings.json' file with your database connection string and email SMTP settings:
   ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": ""
      },
      "Jwt": {
        "Key": "",
        "Issuer": "",
        "Audience": ""
      },
      "AppSettings": {
        "AppName": "",
        "BaseUrl": ""
      },
      "EmailSettings": {
        "From": "",
        "SmtpServer": "",
        "Port": "",
        "Username": "",
        "Password": ""
      }
    }
    ```
4. Apply the database migrations:
   ```bash
   dotnet ef database update
   ```
5. Run the app:
   ```bash
    dotnet run
    ```
## Running the Application
- Open your browser and navigate to https://localhost:7274/swagger to access the Swagger UI.
- Use the Swagger UI to test the various API endpoints.

## API Endpoints
### User Registration
- POST /api/auth/register
    - Request Body:
      ```json
      { 
       "email": "user@example.com", 
       "password": "Password123!", 
       "firstName": "John", 
       "lastName": "Doe" 
      }
      ```
     
    - Response: 200 OK

### User Login
- POST /api/auth/login
    - Request Body: 
     ```json
     { 
    "email": "user@example.com", 
    "password": "Password123!"
     }
     ```
    - Response: 200 OK (with JWT token)

### Product CRUD Operations
- GET /api/products
    - Response: 200 OK (with pagination and filtering)
- GET /api/products/{id}
    - Response: 200 OK
- POST /api/products
    - Request Body: 
     ```json
     {
    Name = "Office Chair",
    Description = "Comfortable ergonomic office chair.",
    Price = 20000.00m,
    Stock = 200,
    Category = "Furniture",
    ImageUrl = new Uri("https://example.com/images/office-chair.jpg")
    }
     ```
    - Response: 201 Created
- PUT /api/products/{id}
    - Request Body: 
     ```json
     {
    Id = 3,
    Name = "Office Chair",
    Description = "Comfortable ergonomic office chair.",
    Price = 20000.00m,
    Stock = 200,
    Category = "Furniture",
    ImageUrl = new Uri("https://example.com/images/office-chair.jpg"),
    CreatedAt = 2024-06-19 15:02:32.7273568,
    LastUpdatedAt = 2024-06-19 15:02:32.7273568
    }
     ```
    - Response: 204 No Content
- DELETE /api/products/{id}
    - Response: 204 No Content


