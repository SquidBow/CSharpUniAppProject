using MoneyManager.Models;
using MoneyManager.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MoneyManager.Services;

public class AppService : IAppService
{
    private readonly IWalletRepository walletRepo;
    private readonly ITransactionRepository transactionRepo;

    public AppService(IWalletRepository walletRepo, ITransactionRepository transactionRepo)
    {
        this.walletRepo = walletRepo;
        this.transactionRepo = transactionRepo;
    }

    public List<WalletListDto> GetWalletsForList()
    {
        var wallets = walletRepo.GetAll();
        return wallets.Select(w => new WalletListDto
        {
            Id = w.Id,
            Name = w.Name,
            Balance = transactionRepo.GetByWalletId(w.Id).Sum(t => t.Sum)
        }).ToList();
    }

    public void RemoveTransaction(int transactionId)
    {
        this.transactionRepo.Remove(transactionId);
    }

    public WalletDetailsDto? GetWalletDetails(int walletId)
    {
        var wallet = walletRepo.GetById(walletId);
        if (wallet == null) return null;

        var transactions = transactionRepo.GetByWalletId(walletId);

        return new WalletDetailsDto
        {
            Id = wallet.Id,
            Name = wallet.Name,
            Currency = wallet.Currency.ToString(),
            Balance = transactions.Sum(t => t.Sum),
            Transactions = transactions.Select(t => new TransactionListDto
            {
                Id = t.Id,
                Sum = t.Sum,
                Type = t.Type.ToString(),
                Description = t.Description,
                Date = t.Date
            }).ToList()
        };
    }

    public TransactionDetailsDto? GetTransactionDetails(int transactionId)
    {
        var t = transactionRepo.GetById(transactionId);
        if (t == null) return null;

        return new TransactionDetailsDto
        {
            Id = t.Id,
            Sum = t.Sum,
            Type = t.Type.ToString(),
            Description = t.Description,
            Date = t.Date
        };
    }

    public void AddWallet(string name, string currency)
    {
        var id = walletRepo.GetAll().Any() ? walletRepo.GetAll().Max(w => w.Id) + 1 : 1;
        var currencyEnum = Enum.Parse<Currencies>(currency);
        walletRepo.Add(new Wallet(id, name, currencyEnum));
    }

    public void RemoveWallet(int walletId)
    {
        transactionRepo.RemoveByWalletId(walletId);
        walletRepo.Remove(walletId);
    }

    public void AddTransaction(int walletId, decimal sum, string type, string description)
    {
        var wallet = walletRepo.GetById(walletId);
        if (wallet == null) return;

        int maxId = 0;
        foreach (var w in walletRepo.GetAll())
        {
            var ts = transactionRepo.GetByWalletId(w.Id);
            if (ts.Any())
            {
                var localMax = ts.Max(t => t.Id);
                if (localMax > maxId) maxId = localMax;
            }
        }

        var spendingType = Enum.Parse<Spending>(type);
        var transaction = new Transaction(maxId + 1, walletId, sum, spendingType, description);

        transactionRepo.Add(transaction);
        wallet.TransactionIds.Add(transaction.Id);
    }
}