namespace BankApp.Interfaces;
using Bankapp.Domain;
/// <summary>
/// Interface containing the BankAccount methods
/// </summary>
public interface IBankAccount
{
    Guid Id { get; }
    string Name { get; }
    AccountType AccountType { get; }
    string Currency { get; }
    decimal Balance { get; }
    DateTime LastUpdated { get; }
    TransactionType TransactionType { get; }
    List<Transaction> Transactions { get; }
    
    void Withdraw(decimal amount, Guid toAccountId,  Guid fromAccountId, string description, TransactionType transactionType);
    void Deposit(decimal amount, Guid toAccountId,  Guid fromAccountId, string description, TransactionType transactionType);
    public void TransferTo(BankAccount toAccount, decimal amount);

}
