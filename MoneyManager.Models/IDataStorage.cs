using MoneyManager.Models;
namespace MoneyManager.Models;

public interface IDataStorage
{
    List<Wallet> Wallets { get; }
    List<Transaction> Transactions { get; }
}