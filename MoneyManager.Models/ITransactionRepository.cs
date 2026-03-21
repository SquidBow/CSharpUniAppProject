using MoneyManager.Models;
namespace MoneyManager.Repositories;

public interface ITransactionRepository
{
    List<Transaction> GetByWalletId(int walletId);
    void Add(Transaction transaction);
    void RemoveByWalletId(int walletId);
    void Remove(int id);
    Transaction? GetById(int transactionId);
}