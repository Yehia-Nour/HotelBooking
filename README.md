# 🏨 HotelBooking API

![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?logo=dotnet&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-Web%20API-5C2D91)
![SQL Server](https://img.shields.io/badge/SQL%20Server-Database-CC2927?logo=microsoftsqlserver&logoColor=white)
![CQRS](https://img.shields.io/badge/Architecture-CQRS%20%2B%20MediatR-blue)
![Clean Architecture](https://img.shields.io/badge/Architecture-Clean%20Architecture-success)

A modular **ASP.NET Core Web API** for hotel management: **rooms, room types, amenities, reservations, payments, refunds, cancellations**, and secure **authentication/authorization**.  
Built with **Clean Architecture**, **CQRS (MediatR)**, **EF Core + SQL Server**, and **ASP.NET Core Identity**.

---

## ✨ Key Highlights

- ✅ Clean Architecture (Presentation / Application / Infrastructure / Domain)
- ✅ CQRS + MediatR (Commands / Queries / Handlers)
- ✅ AutoMapper for mapping between Entities ↔ DTOs
- ✅ Specification Pattern for reusable query rules
- ✅ Repository Pattern + Unit of Work
- ✅ Result Pattern for consistent success/failure responses
- ✅ JWT Authentication + Refresh Tokens
- ✅ ASP.NET Core Identity for security & user management
- ✅ FluentValidation for request validation
- ✅ Global ExceptionHandlerMiddleware
- ✅ EF Core Fluent API configurations & constraints
- ✅ SOLID principles applied across layers
- ✅ Automatic DB migrations + JSON seed on startup
- ✅ Swagger/OpenAPI in Development

---

## 🎯 What Problem This Solves

Provides a backend API to manage hotel operations:
- Catalog management (RoomTypes / Amenities / Rooms)
- Booking lifecycle (availability search, reservations, guests)
- Payments & refunds
- Cancellation requests & cancellation policy rules
- Secure authentication and role-based access (**Admin / Manager / Guest**)

---

## 🧱 Architecture Overview

This solution follows a **Layered Clean Architecture-style** structure:

- **HotelBooking.Presentation**
  - API Controllers, Middlewares, Extensions, DI, Program.cs
- **HotelBooking.Application**
  - Features (CQRS), DTOs, Validators, Services Interfaces, Specifications, MappingProfiles, Result Types
- **HotelBooking.Infrastructure**
  - DbContext, Migrations, Identity, Security (JWT), Repositories, UnitOfWork, DataSeed
- **HotelBooking.Domain**
  - Entities, Contracts, Domain Services, ValueObjects

---

## 🗂️ Project Structure (Simplified)

```text
src/
 ├─ HotelBooking.Presentation
 │  ├─ Controllers
 │  ├─ CustomMiddlewares
 │  ├─ DependencyInjection
 │  ├─ Extensions / Factories
 │  ├─ appsettings.json
 │  └─ Program.cs
 │
 ├─ HotelBooking.Application
 │  ├─ Features
 │  │  ├─ Amenities (Commands / Queries)
 │  │  ├─ CancellationPolicies (Commands / Queries)
 │  │  ├─ Cancellations
 │  │  ├─ Countries
 │  │  ├─ Feedbacks
 │  │  ├─ HotelBooking
 │  │  ├─ HotelSearch
 │  │  ├─ Refunds
 │  │  ├─ RoomAmenities
 │  │  ├─ Rooms
 │  │  ├─ RoomTypes
 │  │  └─ States
 │  ├─ DTOs
 │  ├─ MappingProfiles (AutoMapper)
 │  ├─ Specifications
 │  ├─ Validators (FluentValidation)
 │  ├─ Results (Result Pattern)
 │  ├─ Interfaces / Services
 │  └─ DependencyInjection
 │
 ├─ HotelBooking.Infrastructure
 │  ├─ Data
 │  │  ├─ DbContexts
 │  │  ├─ Configurations (EF Fluent API)
 │  │  ├─ Migrations
 │  │  └─ DataSeed (JSON files + initializer)
 │  ├─ Identity
 │  │  ├─ Security (JwtService / JwtSettings / RefreshTokenService)
 │  │  └─ Entities
 │  ├─ Implementations (Repositories / UnitOfWork)
 │  ├─ SpecificationEvaluator.cs
 │  └─ DependencyInjection
 │
 └─ HotelBooking.Domain
    ├─ Entities
    ├─ Contracts
    ├─ Services
    └─ ValueObjects
```  

---
## 🧩 API Modules Overview (What each module does)

Below is a high-level, **module-based** explanation of the main controllers and their responsibilities.

---

### 🔐 Authentication Module (`AuthenticationController`)
**Purpose:** Handles the full authentication lifecycle using **JWT + Refresh Tokens** with **ASP.NET Core Identity**.

**Responsibilities:**
- User registration & login.
- Issuing **Access Tokens** + **Refresh Tokens**.
- Refreshing expired access tokens securely.
- Logout by invalidating refresh tokens.
- Password change for authenticated users.

---

### 👥 Users & Roles Module (`UsersController`) — **Admin Only**
**Purpose:** Admin dashboard backend for **managing users** and **role assignment**.

**Responsibilities:**
- CRUD operations for users (create, update, delete, view).
- Assigning roles to users (Admin / Manager / Guest).
- Ensuring only **Admins** can access sensitive user management functionality.

---

### 🏷️ Catalog Module: Room Types (`RoomTypesController`) — **Admin/Manager**
**Purpose:** Manage the hotel’s **room type catalog** (e.g., Single, Double, Suite).

**Responsibilities:**
- Create/update/delete room types.
- Enable/disable room types (active toggle).
- Acts as the base classification layer used by rooms, pricing, booking rules, and availability.


---

### 🛏️ Inventory Module: Rooms (`RoomsController`) — **Admin/Manager**
**Purpose:** Manage the actual **hotel rooms inventory**.

**Responsibilities:**
- Create/update/delete rooms (entities that can be booked).
- Enable/disable rooms (active toggle).
- List rooms with filtering/pagination (query params).
- Acts as the primary resource used in availability and reservations.

---

### 🧰 Catalog Module: Amenities (`AmenitiesController`) — **Admin/Manager**
**Purpose:** Manage amenity definitions (e.g., Wi-Fi, TV, Air Conditioning).

**Responsibilities:**
- Create/update/delete amenities.
- Bulk create amenities (useful for seeding/admin setup).
- Enable/disable amenities (active toggle).
- Maintains a reusable list of amenity items used by multiple room types.

---

### 🔗 Relationship Module: Room Amenities (`RoomAmenitiesController`) — **Admin/Manager**
**Purpose:** Handles the **many-to-many relation** between `RoomType` and `Amenity`.

**Responsibilities:**
- Attach amenities to a room type.
- Bulk attach amenities to a room type.
- Retrieve:
  - amenities for a specific room type
  - room types that contain a specific amenity
- Remove:
  - all amenities for a room type
  - a single amenity relation from a room type

---

### 📅 Booking Module (`HotelBookingController`) — **Admin/Manager/Guest**
**Purpose:** Core booking workflow: **pricing**, **reservation creation**, **guest assignment**, and **payments**.

**Responsibilities:**
- Calculate booking cost for selected room(s) & dates.
- Create reservations for authenticated users.
- Add guests to an existing reservation (with user context: id + email).
- Process payments for reservations.
- Update payment status (e.g., Paid, Pending, Failed, Refunded).

---

### 🔎 Search Module (`HotelSearchController`) — **Admin/Manager/Guest**
**Purpose:** Provides various **read-only search capabilities** for rooms based on filters.

**Responsibilities:**
- Search available rooms within date ranges.
- Filter by:
  - price range
  - room type name
  - view type
  - amenity name
  - minimum rating
- Provide room details + its amenities.
- Supports custom multi-filter search using a unified filter model.

---

### 📜 Policies Module: Cancellation Policies (`CancellationPoliciesController`) — **Admin/Manager**
**Purpose:** Manage cancellation policies that define **penalty rules** over time.

**Responsibilities:**
- Create/update/delete cancellation policies.
- List & retrieve policy details.
- Acts as the rule source for cancellation charge calculations.

---

### Cancellations Module (`CancellationsController`) — **Guest + Admin/Manager**
**Purpose:** Handles the cancellation lifecycle: **showing policies**, **calculating charges**, **creating requests**, and **admin review**.

**Responsibilities:**
- For Guests:
  - View active cancellation policies.
  - Calculate cancellation charges before submitting a request.
  - Create cancellation requests (linked to the authenticated user).
- For Admin/Manager:
  - View all cancellation requests (filtered/paginated via request object).
  - Review cancellation requests (approve/reject) with admin tracking.


---

### 💸 Refunds Module (`RefundsController`) — **Admin/Manager**
**Purpose:** Operational module to process refunds after cancellations.

**Responsibilities:**
- Retrieve cancellations eligible for refund (pending refunds queue).
- Process refund (admin action with adminId tracking).
- Update refund status (e.g., Pending → Processed/Failed).

---

### ⭐ Feedback Module (`FeedbackController`) — **Guest + Admin/Manager**
**Purpose:** Manage customer feedback on reservations (rating + comment).

**Responsibilities:**
- Guests:
  - Create feedback for their reservation.
  - Update/delete their own feedback (ownership by userId).
- Admin/Manager:
  - View a single feedback item.
  - View all feedback records for reporting/monitoring.

---

### 🌍 Location Modules: Countries & States (`CountriesController`, `StatesController`) — **Admin/Manager**
**Purpose:** Manage geographic reference data used across the system.

**Responsibilities:**
- Countries:
  - CRUD + active toggle
- States:
  - CRUD + active toggle
  - Supports querying with params (pagination/filtering)

---

## 🧠 Cross-Cutting Responsibilities (Shared Across Modules)

### ✅ `ApiBaseController`
A shared base controller that provides:
- Standardized response handling using **Result / Result<T>**
- Mapping domain/application errors → correct HTTP responses (ProblemDetails)
- Extracting identity claims from JWT:
  - `GetEmailFromToken()`
  - `GetUserIdFromToken()`

### ✅ Authorization (Roles)
- **Admin**: full access (including Users module)
- **Manager**: operational access (catalog, booking, refunds, etc.)
- **Guest**: booking, search, feedback, cancellation requests

### ✅ MediatR + CQRS
All controllers delegate logic to:
- Queries (read)
- Commands (write)
- Handlers in the Application layer

---
