namespace BankApp.Interfaces;

public interface IAccountService
{
    Task<IBankAccount> CreateAccountAsync(string name, AccountType accountType, string currency, decimal initialBalance);
    Task<List<IBankAccount>> GetAccountsAsync();
    Task DeleteAccountAsync(Guid accountId);
    Task<List<BankAccount>> GetAllAccounts();
    Task<BankAccount?> GetAccountById(Guid accountId);
    Task UpdateAccount(BankAccount account);
    Task TransferAsync(Guid fromAccountId, Guid toAccountId, decimal amount);
}
