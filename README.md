# BankApp
A simple Blazor WebAssemby banking app for creating accounts, transactions in the form of deposits/Withdrawals and transfering funds between accounts. Transacations have simple descriptions and can be viewed in transactionhistory.

## Features
- Creation and management of accounts in the 'CreateAccount' page
- Making transactions with descriptions(optional) in 'NewTransaction' page
- Viewing, filtering and sorting of transactionhistory in 'TransactionHistory' page
- Persistent storage via browser localStorage
- Domain models: 'BankAccount, Transaction', 'AccountType'
- Clear error and success messages

## Prerequisites
- .Net 7+ SDK (or the SDK version used in this project)
-  Modern browser (Chrome, Edge, Safari)

## Run locally 
1. Restore and build:
    ```bash
   dotnet restore
   dotnet build
1. Run the app:
    ```bash
   dotnet run --project BankApp
2. Open the URL printed in console (usually https://localhost:5001 or http://localhost:5000)

## How to use 
### Create account
- Navigate to the 'Create Acount' tab in the navigation menu to the left of the screen 
- Input desired account name, type, currency and start balance 
- Press 'Create Account'

### Make a transaction
- Navigate to the 'New Transaction' tab in the navigation menu 
- Select account using the drop-down menu
- Select transaction type (Deposit, Withdraw)
- Input the desired amount
- Choose description (Optional), if no description is chosen it will automatically be set to the transaction type
- Press the submit button

### View transaction history
- Navigate to the 'Transaction History' tab in the navigation menu
- Choose account from the drop-down menu
- Input the desired time interval (optional)
- Sort by date, amount or transaction type

### Transfer between accounts 
- Make sure that there are at least two accounts created
- Navigate to the 'Transfer' tab in the navigation menu
- Choose sender, recipient and amount to transfer
- Press the transfer button

## Credits
### Technologies used:
- C# with .Net (Blazor WebAssembly)
- HTML/CSS and Bootstrap
- JavaScript interop (IJSRuntime)
- Dependency injection and scoped services
- Domain-driven classes (models: BankAccount, Transaction, enums)
- JetBrains Rider 2025.2.2.1 (IDE) on macOS
- Git / Github for source control and project hosting