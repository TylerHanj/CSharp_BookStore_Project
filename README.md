# C# & PostgreSQL Bookstore Management System

A console-based application written in C# (.NET) integrated with a PostgreSQL relational database. 
This project was built to practice software design principles, Object-Oriented Programming (OOP), 
and database management using raw SQL queries and standard database interfaces.

---

## Key Features

* **Book & Inventory Management:** Add, update, search, and manage book collections in real time.
* **Database Integration:** Direct interaction with PostgreSQL via `Npgsql` to perform persistent CRUD operations.
* **Clean Data Layer:** Encapsulated database connection factory pattern returning standard `IDbConnection` instances for better code maintainability.

---

## Tech Stack & Architecture

* **Language:** C# (.NET)
* **Database:** PostgreSQL
* **Database Driver:** Npgsql
* **Architecture:** 2-Tier Client-Server Architecture / Data Access Layer (DAL)
* **Key Concepts:** Object-Oriented Programming (OOP), Relational Database Design, ADO.NET abstraction (`IDbConnection`).

---

## Project Structure & Code Highlights

* **`Project123.Data.DbConnect`**: Handles database connection creation using `NpgsqlConnection` implementing `IDbConnection`.
* **Core Logic**: Manages domain models, user input processing, and SQL data queries.

---
