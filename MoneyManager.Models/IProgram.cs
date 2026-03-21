namespace MoneyManager.Models;

public interface IProgram
{
    void Main();
    void ListWallets();
    void CreateWallet();
    void RemoveWallet();
    void AddTransaction();
    void ListTransactions();
    void FilterTransactions();
    void OrderTransactions();
}