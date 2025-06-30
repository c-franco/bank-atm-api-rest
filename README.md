# BankATM API

## Description

BankATM API is a RESTful service built with .NET 8 using Clean Architecture principles and the CQRS pattern. The main purpose of this project is to simulate the operations of a basic ATM, including balance inquiries, deposits and withdrawals. It includes JWT-based Authentication and Authorization for secure access. The project is designed for educational and demonstration purposes, showcasing best practices in API development, testing and architecture.

---

## Technologies Used

- **ASP.NET Web API in .NET 8**
- **REST API** principles
- **Clean Architecture**
- **CQRS pattern** with **MediatR**
- **Entity Framework Core (InMemory Database)**
- **JWT Authentication & Authorization**
- **Swagger / OpenAPI**
- Testing with **xUnit**, **FluentAssertions**, and **Moq**

---

## API Endpoints

### Authentication

- `POST /api/auth` — Login and receive a JWT token.

### Accounts

- `GET /api/bank` — Get all bank accounts.
- `POST /api/bank/getByAccountNumber` — Get a bank account by account number.

### Transactions

- `POST /api/bank/deposit` — Deposit money into an account.
- `POST /api/bank/withdraw` — Withdraw money from an account.

---

## How to Run the Project

1. **Clone the repository:**

   ```bash
   git clone https://github.com/your-username/BankATM.API.git
   cd BankATM.API
   ```

2. **Run the API:**

   - Open the solution in **Visual Studio 2022+** or use the terminal.
   - Run:
     ```bash
     dotnet restore
     dotnet build
     dotnet run --project src/BankATM.API
     ```

3. **Swagger UI:**

   - Navigate to: `https://localhost:7251/swagger`

4. **Authentication:**

   - Use `/api/auth` with predefined credentials to obtain a JWT token.

   ```bash
   username = "admin"
   password = "123456"
   ```

   - Use the "Authorize" button in Swagger to authenticate.

---

## Notes

- The database is configured in-memory; data resets on each application restart.
- All secrets and JWT settings are stored in `appsettings.json` for demonstration purposes.
