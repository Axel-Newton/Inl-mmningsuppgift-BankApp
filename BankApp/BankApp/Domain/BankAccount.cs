using System.ComponentModel;
using Bankapp.Domain;

namespace BankApp.Domain;

public class BankAccount : IBankAccount

{
    public TransactionType TransactionType { get; set; }
    public List<Transaction> Transactions => _transactions;

    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; }
    public AccountType AccountType { get; private set; }
    public string Currency { get; private set; }
    public decimal Balance { get; private set; }
    public DateTime LastUpdated { get; private set; }

    private readonly List<Transaction> _transactions = new List<Transaction>();

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

        _transactions.Add(new Transaction(
            fromAccountId,
            toAccountId,
            amount,
            DateTime.Now,
            description,
            TransactionType.Withdraw
        ));
    }

    public void Deposit(decimal amount, Guid toAccountId, Guid fromAccountId, string description,
        TransactionType transactionType)
    {
        if (amount <= 0) throw new ArgumentException("Deposit amount cannot be negative");
        Balance += amount;
        LastUpdated = DateTime.Now;

        _transactions.Add(new Transaction(
            fromAccountId,
            toAccountId,
            amount,
            DateTime.Now,
            description,
            TransactionType.Deposit
        ));
    }

    public void TransferTo(BankAccount toAccount, decimal amount)
    {
        Balance -= amount;
        LastUpdated = DateTime.Now;
        _transactions.Add(new Transaction
        (
            Id,
            toAccount.Id,
            amount,
            DateTime.Now,
            "Transfer",
            TransactionType.TransferOut
        ));

        toAccount.Balance += amount;
        toAccount.LastUpdated = DateTime.UtcNow;
        toAccount._transactions.Add(new Transaction(
            Id,
            toAccount.Id,
            amount,
            DateTime.Now,
            "Transfer",
            TransactionType.TransferIn
        ));


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