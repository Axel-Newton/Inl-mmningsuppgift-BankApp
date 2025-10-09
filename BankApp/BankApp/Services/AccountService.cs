using BankApp.Domain;

namespace BankApp.Services;

public class AccountService : IAccountService
{
    public IBankAccount CreateAccount(string name, string currency, decimal initialBalance)
    {
        return new BankAccount();
    }

    private readonly List<IBankAccount> _accounts = new();
    
    public List<IBankAccount> GetAccounts()
    {
        return _accounts;
    }
}