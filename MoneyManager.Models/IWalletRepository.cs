using MoneyManager.Models;
namespace MoneyManager.Repositories;

using System.Threading.Tasks;
public interface IWalletRepository
{
    List<Wallet> GetAll();
    Wallet? GetById(int id);
    void Add(Wallet wallet);
    void Remove(int id);

    Task SaveData();
    Task LoadData();
}