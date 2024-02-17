namespace Venues;

public partial class HomePage
{
    public HomePage()
    {
        InitializeComponent();
    }

    private void AddButton_OnClicked(object? sender, EventArgs e)
    {
        Navigation.PushAsync(new NewVenuePage());
    }
}

