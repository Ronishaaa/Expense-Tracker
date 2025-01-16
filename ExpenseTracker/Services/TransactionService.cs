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

        public async Task UpdateTransactionAsync(Transaction transaction)
        {
            // Load transactions from the file
            var transactions = await GetTransactionsAsync();

            // Find the existing transaction
            var existingTransaction = transactions.FirstOrDefault(t => t.Sno == transaction.Sno);
            if (existingTransaction != null)
            {
                // Update properties
                existingTransaction.Description = transaction.Description;
                existingTransaction.Amount = transaction.Amount;
                existingTransaction.Type = transaction.Type;
                existingTransaction.DueDate = transaction.DueDate;
                existingTransaction.Status = transaction.Status;

                // Save the updated list back to the file
                await SaveTransactionsAsync(transactions);
            }
            else
            {
                throw new Exception("Transaction not found.");
            }
        }

        public async Task ExportTransactionsAsync(string exportFilePath)
        {
            var transactions = await GetTransactionsAsync();

            var json = JsonSerializer.Serialize(transactions, new JsonSerializerOptions { WriteIndented = true });

            await File.WriteAllTextAsync(exportFilePath, json);
        }

        public async Task DeleteTransactionAsync(int transactionSno)
        {
            var transactions = await GetTransactionsAsync();
            var transactionToRemove = transactions.FirstOrDefault(t => t.Sno == transactionSno);

            if (transactionToRemove != null)
            {
                transactions.Remove(transactionToRemove);

                for (int i = 0; i < transactions.Count; i++)
                {
                    transactions[i].Sno = i + 1;
                }

                await SaveTransactionsAsync(transactions);
            }
            else
            {
                throw new Exception("Transaction not found.");
            }
        }


    }
}

