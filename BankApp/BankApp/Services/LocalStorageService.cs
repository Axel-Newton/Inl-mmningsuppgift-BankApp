using Microsoft.JSInterop;
using System.Text.Json;

namespace BankApp.Services;

public class LocalStorageService : IStorageService
{
    private readonly IJSRuntime _js;
    private const string StorageKey = "accounts";

    public LocalStorageService(IJSRuntime js) //Konstruktor för LocalStorageService klass
    {
        _js = js;
    }

    public async Task SaveAccountsAsync(List<IBankAccount> accounts) //Sparar konton till local storage
    {
        var bankAccounts = accounts.OfType<BankAccount>().ToList();
        var json = JsonSerializer.Serialize(bankAccounts);
        await _js.InvokeVoidAsync("localStorage.setItem", StorageKey, json);
    }

    public async Task<List<IBankAccount>> LoadAccountsAsync() //metod som hämtar lista av bankkonton sparad i webbläsare
    {
        var json = await _js.InvokeAsync<string>("localStorage.getItem", StorageKey);
        if (string.IsNullOrEmpty(json))
            return new List<IBankAccount>();
        
        var bankAccounts = JsonSerializer.Deserialize<List<BankAccount>>(json) ?? new List<BankAccount>();
        return bankAccounts.Cast<IBankAccount>().ToList();
    }

    public async Task DeleteAccount(Guid accountId) //metod som raderar ett specifikt konto
    {
        var accounts = await LoadAccountsAsync();
        var updatedAccounts = accounts.Where(account => account.Id != accountId).ToList();
        await SaveAccountsAsync(updatedAccounts);
    }
    
}