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