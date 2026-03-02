using MoneyManager.Models;
using MoneyManager.Services;
using MoneyManager.Storage;

namespace MoneyManager.ConsoleApp;

class Program : IProgram
{
    private static readonly DataStorage storage = new DataStorage();
    private static readonly AppService service = new AppService(storage);

    AppService GetAppService()
    {
        return service;
    }

    DataStorage GetDataStorage()
    {
        return storage;
    }

    Program ()
    {
    }

    public static void Main()
    {
        Program program = new();
        ((IProgram)program).Main();
    }

    void IProgram.Main()
    {
        Program notIProgram = new();
        IProgram program = (IProgram)notIProgram;
        string choice = "";

        while (choice != "8")
        {
            Console.WriteLine("1. List Wallets");
            Console.WriteLine("2. Create New Wallet");
            Console.WriteLine("3. Remove Wallet");
            Console.WriteLine("4. Add Transaction");
            Console.WriteLine("5. View Transactions for a Wallet");
            Console.WriteLine("6. Filter Transactions");
            Console.WriteLine("7. Order Transactions");
            Console.WriteLine("8. Exit");
            Console.Write("\nEnter action: ");

            choice = Console.ReadLine() ?? "";

            // Do the action based on users choice
            switch (choice)
            {
                case "1":
                    program.ListWallets();
                    break;
                case "2":
                    program.CreateWallet();
                    break;
                case "3":
                    program.RemoveWallet();
                    break;
                case "4":
                    program.AddTransaction();
                    break;
                case "5":
                    program.ListTransactions();
                    break;
                case "6":
                    program.FilterTransactions();
                    break;
                case "7":
                    program.OrderTransactions();
                    break;
            }
        }
    }

    void IProgram.ListWallets()
    {
        service.ListWallets();
    }

    void IProgram.CreateWallet()
    {
        Console.Write("Enter Wallet Name: ");
        string name = Console.ReadLine() ?? "New Wallet";

        Console.WriteLine("Select Currency: 1. UAH, 2. USD, 3. EUR");
        string choice = Console.ReadLine() ?? "";
        Currencies currency = choice switch
        {
            "1" => Currencies.UAH,
            "2" => Currencies.USD,
            "3" => Currencies.EUR,
            _ => Currencies.None
        };

        if (currency == Currencies.None)
        {
            Console.WriteLine("Invalid currency.");
            return;
        }

        // Get the maximum id from all wallets and add 1, or if there aren't any start the ids from 1
        int id = service.FindMaxWalletID();
        Wallet wallet = new Wallet(id, name, currency);
        service.AddWallet(wallet);

        Console.WriteLine("Wallet added successfully!");
        Console.WriteLine();
    }

    void IProgram.RemoveWallet()
    {
        Console.Write("Enter Wallet ID to remove: ");
        if (!int.TryParse(Console.ReadLine() ?? "", out int walletId))
        {
            Console.WriteLine("ID must be a number");
            return;
        }

        // Find the wallet with the requested id in the storage and delete it
        service.RemoveWallet(walletId);
        Console.WriteLine("Wallet removed.");
        Console.WriteLine();
    }

    void IProgram.AddTransaction()
    {
        Console.Write("Enter Wallet ID: ");
        if (!int.TryParse(Console.ReadLine() ?? "", out int walletId))
        {
            Console.WriteLine("ID must be a number");
            Console.WriteLine();
            return;
        }

        Console.Write("Enter Sum: ");
        if (!decimal.TryParse(Console.ReadLine() ?? "", out decimal sum))
        {
            Console.WriteLine("Sum must be a decimal");
            Console.WriteLine();
            return;
        }

        Console.WriteLine("Select Spending Category: 1. Cafe, 2. Groceries, 3. Car, 4. Entertainment, 5. Utilities, 6. Other");
        string choice = Console.ReadLine() ?? "";

        // Find the spending type
        Spending type = choice switch
        {
            "1" => Spending.Cafe,
            "2" => Spending.Groceries,
            "3" => Spending.Car,
            _ => Spending.None,
        };


        Console.Write("Enter Description: ");
        string desc = Console.ReadLine() ?? "";

        var wallet = service.FindWallet(walletId);

        //If wallet was not found
        if (wallet == null)
        {
            Console.WriteLine("Wallet wasn't found.");
            Console.WriteLine();
            return;
        }

        //Find maximum free id
        int id = service.FindMaxTransactionID();

        var transaction = new Transaction(id, walletId, sum, type, desc);

        // Add the transaction to the storage and ID to the corresponding Wallet
        // DataStorage.Transactions.Add(transaction);
        // wallet.TransactionIds.Add(id);

        service.AddTransaction(transaction, wallet);
        Console.WriteLine("Transaction added!");
        Console.WriteLine();
    }

    void IProgram.ListTransactions()
    {
        Console.Write("Enter Wallet ID: ");
        if (!int.TryParse(Console.ReadLine(), out int walletId))
        {
            Console.WriteLine("ID must be a number");
            Console.WriteLine();
            return;
        }

        // get all transactions for this wallet
        var transactions = service.GetWalletTransactions(walletId);

        if (!transactions.Any())
        {
            Console.WriteLine("No transactions for this wallet.");
            Console.WriteLine();
            return;
        }

        foreach (var t in transactions)
        {
            Console.WriteLine(t);
        }

        Console.WriteLine();
    }

    void IProgram.FilterTransactions()
    {
        Console.Write("\nEnter the id of the Wallet to filter transactions for: ");
        string input = Console.ReadLine() ?? "";

        if (!int.TryParse(input, out int walletId))
        {
            Console.WriteLine("ID must be a number");
            return;
        }

        var walletTransactions = service.GetWalletTransactions(walletId);
        List<Transaction> filteredTransactions = new();

        Console.Write(@"

            Enter the field id:
                1. Sum
                2. Spending
                3. Loss
        ");

        Console.Write("\nEnter choice: ");

        input = Console.ReadLine() ?? "";

        if (input == "1")
        {
            string how;
            Console.Write(@"
            Enter how to filter:
                1. More then
                2. Less then
                3. Same as
            ");
            Console.Write("\nEnter choice: ");

            how = Console.ReadLine() ?? "";

            Console.Write("\nEnter the number to filter by: ");

            input = Console.ReadLine() ?? "";

            if (!decimal.TryParse(input, out decimal num))
            {
                Console.Write($"\nInvalid number.\n\n");
                return;
            }

            filteredTransactions = how switch
            {
                "1" => walletTransactions.Where(n => n.Sum > num).ToList(),
                "2" => walletTransactions.Where(n => n.Sum < num).ToList(),
                "3" => walletTransactions.Where(n => n.Sum == num).ToList(),
                _ => new(),
            };
        }

        else if (input == "2")
        {
            string how;
            Console.Write(@"
            Enter how to filter:
                1. Cafe
                2. Groceries
                3. Car
            ");

            Console.Write("\nEnter choice: ");

            how = Console.ReadLine() ?? "";

            filteredTransactions = how switch
            {
                "1" => walletTransactions.Where(n => n.Type == Spending.Cafe).ToList(),
                "2" => walletTransactions.Where(n => n.Type == Spending.Groceries).ToList(),
                "3" => walletTransactions.Where(n => n.Type == Spending.Car).ToList(),
                _ => new(),
            };
        }

        else if (input == "3")
        {
            string how;
            Console.Write(@"
            Enter how to filter:
                1. Earnings
                2. Expence
            ");

            Console.Write("\nEnter choice: ");

            how = Console.ReadLine() ?? "";

            filteredTransactions = how switch
            {
                "1" => walletTransactions.Where(n => !n.IsExpense).ToList(),
                "2" => walletTransactions.Where(n => n.IsExpense).ToList(),
                _ => new(),
            };
        }
        else
        {
            Console.Write("\nInvalid choice.");

            return;
        }

        foreach (Transaction transaction in filteredTransactions)
        {
            Console.WriteLine(transaction);
        }
        Console.WriteLine();
    }

    void IProgram.OrderTransactions()
    {
        Console.Write("\nEnter the id of the Wallet to order transactions for: ");
        string input = Console.ReadLine() ?? "";

        if (!int.TryParse(Console.ReadLine() ?? "", out int walletId))
        {
            Console.WriteLine("ID must be a number");
            return;
        }

        var walletTransactions = service.GetWalletTransactions(walletId);
        List<Transaction> orderedTransactions = new();

        Console.Write(@"

            Enter the field id:
                1. Id
                2. Sum
                3. Spending
                4. Loss
                5. Date
        ");

        Console.Write("\nEnter choice: ");

        input = Console.ReadLine() ?? "";

        string how;
        Console.Write(@"

                Enter how to filter:
                    1. Assending
                    2. Desending

            ");

        Console.Write("\nEnter choice: ");

        how = Console.ReadLine() ?? "";

        orderedTransactions = input switch
        {
            //Switch on what to orderBy and then how
            "1" => how switch
            {
                "1" => walletTransactions.OrderBy(n => n.Id).ToList(),
                "2" => walletTransactions.OrderByDescending(n => n.Id).ToList(),
                _ => new(),
            },
            "2" => how switch
            {
                "1" => walletTransactions.OrderBy(n => n.Sum).ToList(),
                "2" => walletTransactions.OrderByDescending(n => n.Sum).ToList(),
                _ => new(),
            },

            "3" => how switch
            {
                "1" => walletTransactions.OrderBy(n => n.Type).ToList(),
                "2" => walletTransactions.OrderByDescending(n => n.Type).ToList(),
                _ => new(),
            },

            "4" => how switch
            {
                "1" => walletTransactions.OrderBy(n => n.IsExpense).ToList(),
                "2" => walletTransactions.OrderByDescending(n => n.IsExpense).ToList(),
                _ => new(),
            },

            "5" => how switch
            {
                "1" => walletTransactions.OrderBy(n => n.Date).ToList(),
                "2" => walletTransactions.OrderByDescending(n => n.Date).ToList(),
                _ => new(),
            },

            _ => new(),
        };

        if (orderedTransactions.Count == 0)
        {
            Console.WriteLine("No transactions were found with given criteria");
            Console.WriteLine();
            return;
        }

        foreach (Transaction transaction in orderedTransactions)
        {
            Console.WriteLine(transaction);
        }
        Console.WriteLine();
    }
}
