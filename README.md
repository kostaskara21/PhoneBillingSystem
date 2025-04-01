# Phone Billing System

## 📌 Project Overview
This project is a **web-based application** for managing mobile billing and customer accounts. It was developed as part of a university coursework assignment. The system allows different user roles (Clients, Sellers, and Administrators) to perform various tasks  such as viewing bills, making payments
## 🚀 Technologies Used
- **.NET Core (ASP.NET Core MVC)**
- **C#**
- **SQL Server**
- **Entity Framework Core**
- **Identity Framework (Authentication & Authorization)**
- **Bootstrap** (for UI)


## 🔑 Features & Functionality
### 🔹 Authentication & Authorization
- User login via a **Login Form**
- Role-based access control using **Identity Framework**

### 🔹 Client Features
- View account bills
- Pay bills 

### 🔹 Seller Features
- Register new clients
- Generate client bills
- Register new clients

### 🔹 Administrator Features
- Create new sellers
- Create new Program
- Modify Program (e.g., Description,price)

## 🏗️ Project Structure
This project follows the **Model-View-Controller (MVC)** architecture:
- **Models:** Define the database schema 
- **Views:** Handle UI rendering
- **Controllers:** Manage the application's logic and handle requests
- **Interfaces:** Define contracts for services, ensuring a clean separation of concerns between application layers.
- **Repository:** Contains methods for data access, acting as an intermediary between the application and the database.

## 🔧  Setup
### Steps
1. Clone the repository:
   ```bash
   git clone https://github.com/kostaskara21/PhoneBillingSystem.git
  
