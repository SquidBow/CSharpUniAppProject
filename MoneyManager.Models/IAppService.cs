namespace MoneyManager.Models;
using System.Threading.Tasks;

public interface IAppService
{
    List<WalletListDto> GetWalletsForList();
    WalletDetailsDto? GetWalletDetails(int walletId);
    void AddWallet(string name, string currency);
    void RemoveWallet(int walletId);
    void AddTransaction(int walletId, decimal sum, string type, string description);
    void RemoveTransaction(int transactionId);
    TransactionDetailsDto? GetTransactionDetails(int transactionId);

    Task SaveData();
    Task LoadData();

    void UpdateWallet(int id, string name, string currency);
    
    Wallet? GetWalletById(int id);

    Transaction? GetTransactionById(int id);

    void UpdateTransaction(int id, decimal sum, string type, string description);
}