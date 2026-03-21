using MoneyManager.Models;
namespace MoneyManager.Repositories;

public interface IWalletRepository
{
    List<Wallet> GetAll();
    Wallet? GetById(int id);
    void Add(Wallet wallet);
    void Remove(int id);
}