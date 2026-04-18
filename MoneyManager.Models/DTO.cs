using System;
using System.Collections.Generic;

namespace MoneyManager.Models
{
    public class WalletListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
    }

    public class WalletDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Currency { get; set; }
        public decimal Balance { get; set; }
        public List<TransactionListDto> Transactions { get; set; }
    }

    public class TransactionListDto
    {
        public int Id { get; set; }
        public decimal Sum { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public DateTime? Date { get; set; }
    }

    public class TransactionDetailsDto
    {
        public int Id { get; set; }
        public decimal Sum { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public DateTime? Date { get; set; }
    }
}
