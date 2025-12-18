# 🏨 Hotel Booking Management System

A robust, API-driven Hotel Booking Management System built with modern backend technologies. This enterprise-grade application provides comprehensive functionality for managing hotels, rooms, reservations, guests, payments, and more, all through secure and scalable APIs.

**Technologies:** .NET Core Web API | Entity Framework Core | SQL Server

---

## 📋 Project Overview

This Hotel Booking Management System is a backend-focused application designed to provide a complete solution for hotel operations. It supports full management of users, roles, hotels, rooms, amenities, reservations, guests, payments, and feedback. The system also includes advanced search and filtering capabilities, reporting on occupancy and revenue, and automated notifications. Built with a clean architecture and layered design, it ensures scalability, maintainability, and security, making it suitable for real-world hotel management scenarios.

---

## 🛠 Key Features

### 🔒 Authentication Module

This module handles all user authentication and authorization tasks, ensuring secure access to the system. It includes:  

- **User Registration:**  
  Users can register with their email, name, phone number, and password. Upon registration, a default role (Guest) is assigned, and JWT access and refresh tokens are issued.  

- **User Login:**  
  Users can log in using their email and password. On successful authentication, the system generates JWT access and refresh tokens for secure API access.  

- **Token Refresh:**  
  Supports refresh tokens to obtain new access tokens without requiring users to log in again.  

- **Logout:**  
  Revokes refresh tokens to log users out securely.  

- **Password Management:**  
  Users can change their password securely, with validation to ensure new passwords match the confirmation.

---
### 🛡 Admin / User Management Module

This module handles **all administrative tasks for managing users and roles**, ensuring proper role-based control. Access is restricted to users with the **Admin** role.

- **Get All Users:** Retrieve all registered users with their ID, name, email, and phone number.
- **Get User By ID:** Fetch a specific user's details using their unique ID.
- **Add User:** Admins can create new users with default role assignment (Guest).
- **Update User:** Update user details such as name, email, and phone number.
- **Delete User:** Admins can delete users. Admin users cannot delete themselves.
- **Assign Role to User:** Assign or change roles for users. Admin roles cannot be changed or reassigned.
- **Delete User:**  
  Admins can delete users, but **cannot delete an Admin** to prevent accidental self-removal.

- **Assign Role to User:**  
  Admins can assign a new role to users. Existing roles are removed before assigning a new one. Admins cannot have their roles changed.

