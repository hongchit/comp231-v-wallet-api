# comp231-v-wallet-api
COMP231 Group 6

## Prerequisites
- .NET 8 SDK
- Visual Studio 2019 or higher or any other preferred IDE that can run a .NET Project
- Setup the SQL Database (please refer to **Connecting to the database section** on how to setup)

## Steps to Run the Project
1. Clone the repository to your local machine.
2. Navigate to the project directory.
3. Open the project in Visual Studio 2019+ or your preferred IDE.
4. Rebuild the solution

The API should be running on your local machine showing the registered endpoints in swagger.

## Testing the API

You can test the API endpoints using any API client like Postman, Thunder client, Fiddler or directly from the Swagger.


# Connecting to the database

## Prerequisites
- SQL Server 2016 or higher (2022 is recommended)
- SQL Server Management Studio

## Steps to setup the v-wallet-db
1. Open SQL Server Management Studio
2. Execute _v-wallet-schema-sqldb_ script
3. Execute _v-wallet-data-sqldb_ script


# REST API
## Swagger Documentation
Manually generated [Swagger Documentation](https://editor.swagger.io/?url=https://raw.githubusercontent.com/hongchit/comp231-v-wallet-api/main/swagger.json)