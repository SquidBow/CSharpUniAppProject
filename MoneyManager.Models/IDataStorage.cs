using MoneyManager.Models;
namespace MoneyManager.Models;

using System.Threading.Tasks;

public interface IDataStorage
{
    List<Wallet> Wallets { get; }
    List<Transaction> Transactions { get; }

    Task SaveData();
    Task LoadData();
}