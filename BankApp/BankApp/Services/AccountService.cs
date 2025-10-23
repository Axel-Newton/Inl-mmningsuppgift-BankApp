namespace BankApp.Services;

public class AccountService : IAccountService
{
    private const string StorageKey = "accounts";
    private readonly IStorageService _storageService;
    private List<IBankAccount> _accounts = new();
    private bool _isInitialized = false;

    public AccountService(IStorageService storageService)
    {
        _storageService = storageService;
    }

    private async Task EnsureInitializedAsync()
    {
        if (_isInitialized) return;
        _accounts = await _storageService.LoadAccountsAsync();
        _isInitialized = true;
    }

    public async Task<IBankAccount> CreateAccountAsync(string name, AccountType accountType, string currency, decimal initialBalance)
    {
        await EnsureInitializedAsync();
        var account = new BankAccount(name, accountType, currency, initialBalance);
        _accounts.Add(account);
        await _storageService.SaveAccountsAsync(_accounts);
        return account;
    }
    
    public async Task<List<IBankAccount>> GetAccountsAsync()
    {
        await EnsureInitializedAsync();
        return _accounts;
    }

    public async Task DeleteAccountAsync(Guid accountId)
    {
        await EnsureInitializedAsync();
        var account = _accounts.FirstOrDefault(x => x.Id == accountId);
        if (account != null)
        {
            _accounts.Remove(account);
            await _storageService.SaveAccountsAsync(_accounts);
        }
    }
    
    public async Task<BankAccount?> GetAccountById(Guid accountId)
    {
        await EnsureInitializedAsync();
        var account = _accounts.OfType<BankAccount>().FirstOrDefault(x => x.Id == accountId);
        return account;
    }

    public async Task<List<BankAccount>> GetAllAccounts()
    {
        await EnsureInitializedAsync();
        var bankAccounts = _accounts.OfType<BankAccount>().ToList();
        return bankAccounts;
    }

    public async Task UpdateAccount(BankAccount account)
    {
        await EnsureInitializedAsync();
        var existingAccount = _accounts.OfType<BankAccount>().FirstOrDefault(x => x.Id == account.Id);
        if (existingAccount != null)
        {
            _accounts.Remove(existingAccount);
            _accounts.Add(account);
            await _storageService.SaveAccountsAsync(_accounts);
        }
    }

    public async Task TransferAsync(Guid fromAccountId, Guid toAccountId, decimal amount)
    {
        await EnsureInitializedAsync();
        var fromAccount = _accounts.OfType<BankAccount>().FirstOrDefault(x => x.Id == fromAccountId)
            ?? throw new KeyNotFoundException($"Account with ID {fromAccountId} not found");
        
        var toAccount = _accounts.OfType<BankAccount>().FirstOrDefault(x => x.Id == toAccountId)
            ?? throw new KeyNotFoundException($"Account with ID {toAccountId} not found");
        
        fromAccount.TransferTo(toAccount, amount);
        await _storageService.SaveAccountsAsync(_accounts);
    }
}
