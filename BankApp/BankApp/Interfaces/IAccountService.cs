namespace BankApp.Interfaces;

public interface IAccountService
{
    IBankAccount CreateAccount(string name, AccountType accountType, string currency, decimal initialBalance);
    List<IBankAccount> GetAccounts();
    Task DeleteAccountAsync(Guid accountId);
    Task<List<BankAccount>> GetAllAccounts();
    Task<BankAccount?> GetAccountById(Guid accountId);
    Task UpdateAccount(BankAccount account);
    void Transfer(Guid fromAccountId, Guid toAccountId, decimal amount);
}