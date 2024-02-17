using SQLite;
using Venues.Model;

namespace Venues.ViewModels;

public class SQLiteDatabase
{
    readonly SQLiteAsyncConnection _database;

    // Read Data
    public async Task<List<Post>> GetPostAsync()
    {
        return await _database.Table<Post>().ToListAsync();
    }

    // Read particular data
    public async Task<Post> GetPostAsync(Post item)
    {
        return await _database.Table<Post>().Where(i => i.Id == item.Id).FirstOrDefaultAsync();
    }
     
    // Add data   
    public async Task<int> AddPostAsync(Post item)
    {
        return await _database.InsertAsync(item);
    }

    // Remove data
    public Task<int> DeletePostAsync(Post item)
    {            
        return _database.DeleteAsync(item);
    }

    // Update data
    public Task<int> UpdatePostAsync(Post item)
    {
        if (item.Id != 0)
            return _database.UpdateAsync(item);
        else
            return _database.InsertAsync(item);
    }
}