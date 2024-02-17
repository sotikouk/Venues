namespace Venues;

public partial class App : Application
{
    public static string DatabaseLocation = string.Empty;
    
    public App()
    {
       InitializeComponent();

       MainPage = new NavigationPage(new MainPage());
    }
    static SQLiteDatabase database;

    // Create the database connection as a singleton.
    public static SQLiteDatabase Database
    {
        get
        {
            if (database == null)
            {
                database = new SQLiteDatabase();
            }
            return database;
        }
    }
}