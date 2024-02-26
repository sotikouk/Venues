namespace Venues;

public partial class MainPage
{

    public MainPage()
    {
        InitializeComponent();
    }

    private void LoginBtn_OnClicked(object? sender, EventArgs e){
            Navigation.PushAsync(new HomePage());
    }
}