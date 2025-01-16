using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseTracker.Models;

namespace ExpenseTracker.Services
{
    public interface ITransactionService
    {
        Task<List<Transaction>> GetTransactionsAsync();
        Task SaveTransactionsAsync(List<Transaction> transactions);
        Task AddTransactionAsync(Transaction transaction);
        Task UpdateTransactionAsync(Transaction transaction);
        Task ExportTransactionsAsync(string exportFilePath);
        Task DeleteTransactionAsync(int transactionSno);

    }
}
