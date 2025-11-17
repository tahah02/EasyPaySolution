# 🏦 EasyPay - Enterprise Fintech Backend API (.NET 8)

EasyPay is a robust and secure Fintech REST API that simulates **Core Banking Operations** (Fund Transfer, Balance Check, Ledger Management) built on a **Clean N-Tier Architecture**. The project emphasizes data integrity, security, and auditability following professional banking standards.

---

## 🚀 Key Features & Achievements

* **Enterprise N-Tier Architecture:** Project is strictly divided into `Data`, `Logic` (Services), and `WebAPI` layers, ensuring high maintainability and testability.
* **💸 Double Entry Ledger System:** Implements professional accounting principles where every transfer results in corresponding **Debit** and **Credit** entries.
* **🔐 Data Security:** Sensitive data (like Secret PINs) is protected using **Base64 Encryption** before being stored in the database.
* **📊 Audit Trail & Logging:** Uses dedicated columns (`LogId`, `OpeningBalance`, `ClosingBalance`) to create a verifiable record of all transactions.
* **✅ Standardized Response:** All API responses follow a uniform structure (`LogId`, `Message`, `Data`, `IsSuccess`) for client consistency.
* **Transaction Control:** Logic uses database transactions to prevent **Race Conditions** (Double Spending) during fund transfers.
* **⚡ Dependency Injection (DI):** Managed services and logic are injected using DI for loose coupling.

---

## 🛠️ Tech Stack & Prerequisites

* **Framework:** .NET 8 (ASP.NET Core Web API)
* **Language:** C#
* **Database:** Microsoft SQL Server (SSMS)
* **ORM:** Entity Framework Core (Code First)
* **Tools:** Visual Studio 2022, Postman

---

## ⚙️ Setup Guide (How to Run)

The database is managed using the **Code-First** approach.

### 1. Update Connection String

Ensure your SQL Server name is correct in the `EasyPay.WebAPI/appsettings.json` file.

```json
"DefaultConnection": "Server=DESKTOP-BNT57LQ\\MSSQLSERVER01;Database=EasyPayDb;Trusted_Connection=True;TrustServerCertificate=True;"


2. Database Creation & Migration
Open the Package Manager Console in Visual Studio.

Set Default Project to EasyPay.Data.

//Run the following commands to build the database schema and apply all ledger structure changes:

**PowerShell**
Add-Migration InitialSetup
Update-Database

3. Seed Data (Optional but Recommended)
After the database is created, insert initial balances into the UserAccounts table via SSMS for testing transfers.

4. Run Project
Run the EasyPay.WebAPI project (F5) and use Postman to test the endpoints.

**📬 API Endpoints
**Money Transfer**
Method: POST
Endpoint: /api/Account/transfer
Description: Funds transfer ke liye use hota hai. Sender se deduct aur Receiver ko credit karta hai, sath hi dual audit entries banata hai.
Example URL: .../api/Account/transfer

**Balance Inquiry**
Method: GET
Endpoint: /api/Account/balance/{id}
Description: User ka current balance check karke return karta hai.
Example URL: .../api/Account/balance/Ali

**Transaction History**
Method: GET
Endpoint: /api/History/{id}
Description: User ki saari transactions (Debit/Credit) ki list fetch karta hai.
Example URL: .../api/History/Sara

**Manual Transaction**
Method: POST
Endpoint: /api/History
Description: System ya Admin entry ke liye manual transaction records database mein add karta hai.
Example URL: .../api/History**

**🔜 Future Roadmap**
This project is stable for core functionality but requires standard enterprise security and operations enhancements:

JWT Authentication: Implementing token-based security to secure all endpoints.

Unit Testing: Writing comprehensive Unit Tests for the Logic layer.

Background Services: Integrating Hangfire or similar tools for scheduled jobs (e.g., generating monthly statements).
