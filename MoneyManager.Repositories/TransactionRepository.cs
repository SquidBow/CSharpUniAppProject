using MoneyManager.Models;
using MoneyManager.Storage;
using System.Collections.Generic;
using System.Linq;

namespace MoneyManager.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly IDataStorage storage;

    public TransactionRepository(IDataStorage storage)
    {
        this.storage = storage;
    }

    public List<Transaction> GetByWalletId(int walletId)
    {
        return storage.Transactions.Where(t => t.WalletId == walletId).ToList();
    }

    public void Add(Transaction transaction)
    {
        storage.Transactions.Add(transaction);
    }

    public void RemoveByWalletId(int walletId)
    {
        storage.Transactions.RemoveAll(t => t.WalletId == walletId);
    }

    public void Remove(int id)
    {
        this.storage.Transactions.RemoveAll(t => t.Id == id);
    }

    public Transaction? GetById(int transactionId)
    {
        return storage.Transactions.Find(t => t.Id == transactionId);
    }
}