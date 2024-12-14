# Yazilim Academy Payments

A .NET-based payment processing system integrated with PayTR payment gateway, built with modern technologies and best practices.

## 🚀 Technologies

- .NET 9.0
- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL
- Serilog for logging
- PayTR Payment Gateway Integration

## 📋 Prerequisites

- .NET 9.0 SDK
- PostgreSQL Database
- IDE (Visual Studio 2022 or VS Code recommended)
- PayTR Merchant Account and API Credentials

## 🛠️ Project Structure

```
src/
├── YazilimAcademyPayments.WebApi/
    ├── Controllers/         # API endpoints
    ├── Domain/             # Business logic and entities
    │   ├── Common/         # Shared components
    │   ├── Entities/       # Domain entities
    │   ├── Enums/          # Enumeration types
    │   └── ValueObjects/   # Value objects
    ├── Persistence/        # Data access layer
    └── Properties/         # Application properties
```

## 🔧 Installation

1. Clone the repository
```bash
git clone [repository-url]
```

2. Navigate to the project directory
```bash
cd YazilimAcademyPayments/src/YazilimAcademyPayments.WebApi
```

3. Install dependencies
```bash
dotnet restore
```

4. Update the database connection string in `appsettings.json`

5. Configure PayTR credentials in `appsettings.json`:
```json
{
  "PayTR": {
    "MerchantId": "your-merchant-id",
    "ApiKey": "your-api-key",
    "ApiSecret": "your-api-secret"
  }
}
```

6. Run the application
```bash
dotnet run
```

## 🔑 Features

- Secure payment processing through PayTR gateway
- Support for various payment methods (Credit Card, Bank Transfer)
- Real-time payment status tracking
- Entity management
- Structured domain model
- PostgreSQL database integration
- Comprehensive logging with Serilog

## 🤝 Contributing

1. Fork the repository
2. Create your feature branch
3. Commit your changes
4. Push to the branch
5. Create a new Pull Request

## 📝 License

This project is licensed under the MIT License - see the LICENSE file for details.