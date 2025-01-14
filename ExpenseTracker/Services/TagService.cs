using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ExpenseTracker.Models;


namespace ExpenseTracker.Services
{
    public class TagService : ITagService
    {
        private readonly string tagsFilePath = Path.Combine(AppContext.BaseDirectory, "Tags.json");

        public async Task<List<Tag>> GetTagsAsync()
        {
            if (!File.Exists(tagsFilePath))
            {
                return new List<Tag>();
            }

            var json = await File.ReadAllTextAsync(tagsFilePath);
            return JsonSerializer.Deserialize<List<Tag>>(json) ?? new List<Tag>();
        }
        public async Task SaveTagAsync(List<Tag> tags)
        {
            var json = JsonSerializer.Serialize(tags, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(tagsFilePath, json);
        }

        public async Task AddTagAsync(Tag tag)
        {
            var tags = await GetTagsAsync();
            tag.Id = tags.Count > 0 ? tags.Max(t => t.Id) + 1 : 1;
            tags.Add(tag);
            await SaveTagAsync(tags);
        }

        public async Task DeleteTagAsync(int tagId)
        {
            var tags = await GetTagsAsync();
            tags.RemoveAll(t => t.Id == tagId);
            await SaveTagAsync(tags);
        }

    }
}
