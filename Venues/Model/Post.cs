using SQLite;

namespace Venues.Model;

public class Post
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    
    [MaxLength(250)]
    public string Experience { get; set; }
    
}