using System.Collections.Generic;
using System.Threading.Tasks;
using ExpenseTracker.Models;

namespace ExpenseTracker.Services
{
    public interface IUserService
    {
      

        Task<bool> ValidateUserAsync(string username, string password);

        Task UpdateUserAsync(string newUsername, string newPassword);

       
    }
}