# Task Tracker App

A **full-stack Task Tracker application** built with **.NET 8** and **Angular 16**.  
This app follows a **clean architecture** approach to separate concerns and improve maintainability, scalability, and testability.

---

## Table of Contents

- [Features](#features)  
- [Tech Stack](#tech-stack)  
- [Architecture](#architecture)  
- [Getting Started](#getting-started)  
  - [Prerequisites](#prerequisites)  
  - [Running the Backend](#running-the-backend)  
  - [Running the Frontend](#running-the-frontend)  
- [API Endpoints](#api-endpoints)  
- [License](#license)  

---

## Features

- Create, update, delete, and view tasks  
- Task prioritization and due dates  
- User-friendly interface with Angular Material  
- RESTful API built with .NET 8  
- Clean architecture separating layers for better maintainability  

---

## Tech Stack

- **Frontend:** Angular 16, TypeScript, Angular Material  
- **Backend:** .NET 8, C#, ASP.NET Core Web API, Entity Framework Core, xUnit  
- **Tools:** VS Code, Node.js, npm  

---

## Architecture

The backend follows **Clean Architecture principles**:

- **Domain Layer:** Core business logic, entities, and enums  
- **Application Layer:** DTOs, interfaces, and services  
- **Infrastructure Layer:** Database access, repositories, and external services  
- **API Layer:** Controllers, AutoMapper, and API-specific logic  
- **Test Layer:** Unit tests with xUnit 

The frontend Angular app communicates with the backend API via HTTP and is structured with feature modules and reusable components.

---

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)  
- [Node.js LTS](https://nodejs.org/) and npm  
- [Angular CLI](https://angular.io/cli)  

---

### Running the Backend

1. Clone the repository:  
```bash
git clone https://github.com/your-username/task-tracker.git
cd task-tracker/Api