namespace BankApp.Services;

public class AccountService : IAccountService
{
    private readonly List<IBankAccount> _accounts = new();

    public IBankAccount CreateAccount(string name, AccountType accountType, string currency, decimal initialBalance)
    {
        var account = new BankAccount(name, accountType, currency, initialBalance);
        _accounts.Add(account);
        return account;
    }
    
    public List<IBankAccount> GetAccounts() => _accounts;

    public async Task DeleteAccountAsync(Guid accountId)
    {
        var account = _accounts.FirstOrDefault(x => x.Id == accountId);
        if (account != null)
        {
            _accounts.Remove(account);
        }
        await Task.CompletedTask;
    }
}