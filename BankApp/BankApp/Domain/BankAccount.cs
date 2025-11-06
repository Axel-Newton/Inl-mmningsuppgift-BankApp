using System.ComponentModel;

namespace BankApp.Domain;

public class BankAccount : IBankAccount

{
    public TransactionType TransactionType { get; set; }
    public List<Transaction> Transactions { get; set; } = new List<Transaction>();

    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public AccountType AccountType { get; set; }
    public string Currency { get; set; } = string.Empty;
    public decimal Balance { get; set; }
    public DateTime LastUpdated { get; set; }
    public string Description { get; set; } = string.Empty;

    // Parameterless constructor for JSON deserialization
    public BankAccount()
    {
    }

    public BankAccount(string name, AccountType accountType, string currency, decimal initialBalance)
    {
        Name = name;
        AccountType = accountType;
        Currency = currency;
        Balance = initialBalance;
        LastUpdated = DateTime.Now;
    }

    public void Withdraw(decimal amount, Guid toAccountId, Guid fromAccountId, string description,
        TransactionType transactionType)
    {
        if (amount <= 0) throw new ArgumentException("Withdrawal amount cannot be negative");
        Balance -= amount;
        LastUpdated = DateTime.Now;

        var tx = new Transaction(
            fromAccountId,
            toAccountId,
            amount,
            DateTime.Now,
            description,
            TransactionType.Withdraw
        )
        {
            BalanceAfter = Balance
        };

        Transactions.Add(tx);
    }

    public void Deposit(decimal amount, Guid toAccountId, Guid fromAccountId, string description,
        TransactionType transactionType)
    {
        if (amount <= 0) throw new ArgumentException("Deposit amount cannot be negative");
        Balance += amount;
        LastUpdated = DateTime.Now;

        var tx = new Transaction(
            fromAccountId,
            toAccountId,
            amount,
            DateTime.Now,
            description,
            TransactionType.Deposit
        )
        {
            BalanceAfter = Balance
        };

        Transactions.Add(tx);
    }

    public void TransferTo(BankAccount toAccount, decimal amount)
    {
        Balance -= amount;
        LastUpdated = DateTime.Now;
        var txOut = new Transaction
        (
            Id,
            toAccount.Id,
            amount,
            DateTime.Now,
            "Transfer",
            TransactionType.TransferOut
        )
        {
            BalanceAfter = Balance
        };
        Transactions.Add(txOut);

        toAccount.Balance += amount;
        toAccount.LastUpdated = DateTime.UtcNow;
        var txIn = new Transaction(
            Id,
            toAccount.Id,
            amount,
            DateTime.Now,
            "Transfer",
            TransactionType.TransferIn
        )
        {
            BalanceAfter = toAccount.Balance
        };
        toAccount.Transactions.Add(txIn);

        /*
        public void TransferTo(BankAccount toAccount, decimal amount)
        {
            Balance -= amount;
            LastUpdated = DateTime.Now;
            _transactions.Add(new Transaction
            {
                TransactionType = TransactionType.TransferOut,
                Amount = amount,
                BalanceAfter = Balance,
                FromAccountId = Id,
                ToAccountId = toAccount.Id
            });
        }
        */
    }
}