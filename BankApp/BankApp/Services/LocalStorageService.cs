using Microsoft.JSInterop;
using System.Text.Json;

public class LocalStorageService : IStorageService
{
    private readonly IJSRuntime _js;
    private const string StorageKey = "accounts";

    public LocalStorageService(IJSRuntime js)
    {
        _js = js;
    }

    public async Task SaveAccountsAsync(List<IBankAccount> accounts)
    {
        var json = JsonSerializer.Serialize(accounts);
        await _js.InvokeVoidAsync("localStorage.setItem", StorageKey, json);
    }

    public async Task<List<IBankAccount>> LoadAccountsAsync()
    {
        var json = await _js.InvokeAsync<string>("localStorage.getItem", StorageKey);
        return json is null
            ? new List<IBankAccount>()
            : JsonSerializer.Deserialize<List<IBankAccount>>(json) ?? new List<IBankAccount>();
    }

    public async Task DeleteAccount(Guid accountId)
    {
        var accounts = await LoadAccountsAsync();
        var updatedAccounts = accounts.Where(account => account.Id != accountId).ToList();
        await SaveAccountsAsync(updatedAccounts);
    }
    
}