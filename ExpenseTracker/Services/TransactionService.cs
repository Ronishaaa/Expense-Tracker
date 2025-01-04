using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ExpenseTracker.Models;

namespace ExpenseTracker.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly string usersFilePath = Path.Combine(AppContext.BaseDirectory, "Transaction.json");

        public async Task<List<Transaction>> GetTransactionsAsync()
        {
            if (!File.Exists(usersFilePath))
            {
                return new List<Transaction>();
            }

                var json = await File.ReadAllTextAsync(usersFilePath);
                return JsonSerializer.Deserialize<List<Transaction>>(json) ?? new List<Transaction>();
            
        }

        public async Task SaveTransactionsAsync(List<Transaction> transactions)
        {
            var json = JsonSerializer.Serialize(transactions , new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(usersFilePath, json);
        }

        public async Task AddTransactionAsync(Transaction transaction)
        {
            var transactions = await GetTransactionsAsync();
            transaction.Sno = transactions.Count + 1;
            transactions.Add(transaction);
            await SaveTransactionsAsync(transactions);
        }
    }
}

