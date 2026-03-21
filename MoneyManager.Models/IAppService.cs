namespace MoneyManager.Models;

public interface IAppService
{
    List<WalletListDto> GetWalletsForList();
    WalletDetailsDto? GetWalletDetails(int walletId);
    void AddWallet(string name, string currency);
    void RemoveWallet(int walletId);
    void AddTransaction(int walletId, decimal sum, string type, string description);
    void RemoveTransaction(int transactionId);
    TransactionDetailsDto? GetTransactionDetails(int transactionId);
}