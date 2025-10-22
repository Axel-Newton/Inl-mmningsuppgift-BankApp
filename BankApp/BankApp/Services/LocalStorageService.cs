using Microsoft.JSInterop;
using System.Text.Json;
using BankApp.Domain;

namespace BankApp.Services;

public class LocalStorageService : IStorageService
{
    private readonly IJSRuntime _js;
    private const string StorageKey = "accounts";

    public LocalStorageService(IJSRuntime js) //Constructor for LocalStorageService class
    {
        _js = js;
    }

    public async Task SaveAccountsAsync(List<IBankAccount> accounts) //Saves accounts to local storage
    {
        var bankAccounts = accounts.OfType<BankAccount>().ToList();
        var json = JsonSerializer.Serialize(bankAccounts);
        await _js.InvokeVoidAsync("localStorage.setItem", StorageKey, json);
    }

    public async Task<List<IBankAccount>> LoadAccountsAsync() //method that retrieves list of bank accounts stored in browser
    {
        var json = await _js.InvokeAsync<string>("localStorage.getItem", StorageKey);
        if (string.IsNullOrEmpty(json))
            return new List<IBankAccount>();
        
        var bankAccounts = JsonSerializer.Deserialize<List<BankAccount>>(json) ?? new List<BankAccount>();
        return bankAccounts.Cast<IBankAccount>().ToList();
    }

    public async Task DeleteAccount(Guid accountId) //method that deletes a specific bank account
    {
        var accounts = await LoadAccountsAsync();
        var updatedAccounts = accounts.Where(account => account.Id != accountId).ToList();
        await SaveAccountsAsync(updatedAccounts);
    }
    
}