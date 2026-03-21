namespace MoneyManager.Models;

public enum Currencies { UAH, USD, EUR }
public enum Spending { None, Cafe, Groceries, Car }

public class Wallet
{
    public int Id { get; }
    public string Name { get; set; }
    public Currencies Currency { get; set; }
    public List<int> TransactionIds { get; } = new();

    public Wallet(int id, string name, Currencies currency)
    {
        Id = id;
        Name = name;
        Currency = currency;
    }
}

public class Transaction
{
    public int Id { get; }
    public int WalletId { get; }
    public decimal Sum { get; set; }
    public Spending Type { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public bool IsExpense => Sum < 0;

    public Transaction(int id, int walletId, decimal sum, Spending type, string description, DateTime? date = null)
    {
        Id = id;
        WalletId = walletId;
        Sum = sum;
        Type = type;
        Description = description;
        Date = date ?? DateTime.Now;
    }

    public override string ToString()
    {
        return $"{Id}\t {Date:yyyy-MM-dd}\t {Type,-12}\t {Sum,10:F2}\t {Description}";
    }
}