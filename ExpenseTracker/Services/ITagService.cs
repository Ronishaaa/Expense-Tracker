using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseTracker.Models;

namespace ExpenseTracker.Services
{
    public interface ITagService
    {
        Task<List<Tag>> GetTagsAsync();
        Task SaveTagAsync(List<Tag> tags);

        Task AddTagAsync(Tag tag);
        Task DeleteTagAsync(int tagId);
    }
}
