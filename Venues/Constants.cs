using SQLite;
using Venues.Model;

namespace Venues;
public static class Constants

{
    public static string fourSqrKey = "fsq3g9ObupoLPrsMhgWFVxP76Yys9iILqROi9h4cVz74a28=";
    public const string DatabaseFilename = "SQLiteDB.db";

    public const SQLite.SQLiteOpenFlags Flags =
        // open the database in read/write mode
        SQLite.SQLiteOpenFlags.ReadWrite |
        // create the database if it doesn't exist
        SQLite.SQLiteOpenFlags.Create |
        // enable multi-threaded database access
        SQLite.SQLiteOpenFlags.SharedCache;

    public static string DatabasePath =>
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DatabaseFilename);
}

public class SQLiteDatabase
{
    readonly SQLiteAsyncConnection _database;

    public SQLiteDatabase()
    {
        _database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        _database.CreateTableAsync<Post>();
    }
    
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