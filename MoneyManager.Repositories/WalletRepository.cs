using MoneyManager.Models;
using MoneyManager.Storage;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManager.Repositories;

public class WalletRepository : IWalletRepository
{
    private readonly IDataStorage storage;

    public WalletRepository(IDataStorage storage)
    {
        this.storage = storage;
    }

    public List<Wallet> GetAll()
    {
        return storage.Wallets;
    }

    public Wallet? GetById(int id)
    {
        return storage.Wallets.Find(w => w.Id == id);
    }

    public void Add(Wallet wallet)
    {
        storage.Wallets.Add(wallet);
    }

    public void Remove(int id)
    {
        storage.Wallets.RemoveAll(w => w.Id == id);
    }

    public async Task SaveData()
    {
        await storage.SaveData();
    }

    public async Task LoadData()
    {
        await storage.LoadData();
    }
}
