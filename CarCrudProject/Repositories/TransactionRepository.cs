using CarCrudProject.Models;

namespace CarCrudProject.Repositories;

public static class TransactionRepository
{
    public static List<Transaction> Transactions = new List<Transaction>();
    private static int _nextId = 1;

    public static void Add(Transaction transaction)
    {
        Transactions.Add(transaction);
        _nextId++;
    }
    public static int NextId => _nextId;
}