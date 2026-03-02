using MoneyManager.Models;
using System.Collections.Generic;

namespace MoneyManager.Storage;

public class DataStorage : IDataStorage
{
    public List<Wallet> Wallets { get; } = new();
    public List<Transaction> Transactions { get; } = new();

    public DataStorage()
    {
        AddInitialTransactions();
    }

    void AddInitialTransactions()
    {
        Wallet wallet1 = new Wallet(1, "Main Card", Currencies.UAH);
        Wallet wallet2 = new Wallet(2, "Cash", Currencies.USD);
        Wallet wallet3 = new Wallet(3, "Savings", Currencies.EUR);

        Transaction t;

        // 12 Transactions (10 for wallet 1, 2 for wallet 2)
        for (int i = 1; i < 11; i++)
        {
            t = new Transaction(i, 1, -100 * i, Spending.Groceries, "Nothing", DateTime.Now.Date);
            wallet1.TransactionIds.Add(i);
            Transactions.Add(t);
        }

        t = new Transaction(11, 2, 500, Spending.Car, "Nothing", DateTime.Now.Date);
        wallet2.TransactionIds.Add(11);
        Transactions.Add(t);

        t = new Transaction(12, 3, 1000, Spending.Cafe, "Nothing", DateTime.Now.Date);
        wallet3.TransactionIds.Add(12);
        Transactions.Add(t);

        Wallets.Add(wallet1);
        Wallets.Add(wallet2);
        Wallets.Add(wallet3);
    }
}
