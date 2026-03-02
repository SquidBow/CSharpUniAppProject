using MoneyManager.Models;
using MoneyManager.Storage;

namespace MoneyManager.Services;

public class AppService(IDataStorage storage) : IAppService
{
    private readonly IDataStorage storage = storage;

    public int FindMaxWalletID()
    {
        return storage.Wallets.Any() ? storage.Wallets.Max(w => w.Id) + 1 : 1;
    }

    public void AddWallet(Wallet wallet)
    {
        storage.Wallets.Add(wallet);
    }

    public void RemoveWallet(int walletId)
    {
        Wallet? wallet = storage.Wallets.Find(w => w.Id == walletId);

        if (wallet == null)
        {
            Console.WriteLine("Wallet wasn't found.");
            Console.WriteLine();
            return;
        }

        storage.Transactions.RemoveAll(t => t.WalletId == walletId);
        storage.Wallets.Remove(wallet);
    }

    public Wallet? FindWallet(int walletId)
    {
        return storage.Wallets.Find(w => w.Id == walletId);
    }

    public int FindMaxTransactionID()
    {
        return storage.Transactions.Any() ? storage.Transactions.Max(t => t.Id) + 1 : 1;
    }

    public void AddTransaction(Transaction t, Wallet wallet)
    {
        storage.Transactions.Add(t);
        wallet.TransactionIds.Add(t.Id);
    }

    public List<Transaction> GetWalletTransactions(int walletId)
    {
        // Return all transactions which have the same wallet connected as the wallet we are searching for
        return storage.Transactions
            .Where(t => t.WalletId == walletId)
            .ToList();
    }

    public void ListWallets()
    {
        var wallets = storage.Wallets;
        if (!wallets.Any())
        {
            return;
        }

        foreach (var w in wallets)
        {
            // Get the ballance of the wallet by summing all the transactions
            decimal balance = GetWalletTransactions(w.Id).Sum(t => t.Sum);
            Console.WriteLine($"ID: {w.Id}\t Name: {w.Name}\t Currency: {w.Currency}\t Balance: {balance:F2}");
        }

        Console.WriteLine();
    }
}
