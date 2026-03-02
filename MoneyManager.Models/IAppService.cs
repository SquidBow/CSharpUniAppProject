namespace MoneyManager.Models;

public interface IAppService
{
    int FindMaxWalletID();
    void AddWallet(Wallet wallet);
    void RemoveWallet(int walletId);
    Wallet? FindWallet(int walletId);
    int FindMaxTransactionID();
    void AddTransaction(Transaction t, Wallet wallet);
    List<Transaction> GetWalletTransactions(int walletId);
}
