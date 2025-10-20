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
    
    public async Task<BankAccount?> GetAccountById(Guid accountId)
    {
        var account = _accounts.OfType<BankAccount>().FirstOrDefault(x => x.Id == accountId);
        return await Task.FromResult(account);
    }

    public async Task<List<BankAccount>> GetAllAccounts()
    {
        var bankAccounts = _accounts.OfType<BankAccount>().ToList();
        return await Task.FromResult(bankAccounts);
    }

    public async Task UpdateAccount(BankAccount account)
    {
        var existingAccount = _accounts.OfType<BankAccount>().FirstOrDefault(x => x.Id == account.Id);
        if (existingAccount != null)
        {
            _accounts.Remove(existingAccount);
            _accounts.Add(account);
        }
        await Task.CompletedTask;
    }

    public void Transfer(Guid fromAccountId, Guid toAccountId, decimal amount)
    {
        var fromAccount = _accounts.OfType<BankAccount>().FirstOrDefault(x => x.Id == fromAccountId)
            ?? throw new KeyNotFoundException($"Account with ID {fromAccountId} not found");
        
        var toAccount = _accounts.OfType<BankAccount>().FirstOrDefault(x => x.Id == toAccountId)
            ?? throw new KeyNotFoundException($"Account with ID {toAccountId} not found");
        
        fromAccount.TransferTo(toAccount, amount);
    }
}