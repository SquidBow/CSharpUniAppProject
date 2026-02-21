using MoneyManager.Models;

namespace MoneyManager.Storage;

public static class DataStorage
{
    public static List<Wallet> Wallets { get; } = new();
    public static List<Transaction> Transactions { get; } = new();

    static DataStorage()
    {
        AddInitialTransactions();
    }

    private static void AddInitialTransactions()
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
