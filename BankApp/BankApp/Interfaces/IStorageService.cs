namespace BankApp.Interfaces
{
    public interface IStorageService
    {
        Task SaveAccountsAsync(List<IBankAccount> accounts);
        Task<List<IBankAccount>> LoadAccountsAsync();
        Task DeleteAccount(Guid id);
    }
}