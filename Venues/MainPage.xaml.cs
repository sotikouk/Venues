namespace Venues;

public partial class MainPage
{

    public MainPage()
    {
        InitializeComponent();
    }

    private void LoginBtn_OnClicked(object? sender, EventArgs e)
    {
        bool isEmailEmpty = string.IsNullOrEmpty(EmailEntry.Text);
        bool isPasswdEmpty = string.IsNullOrEmpty(PasswdEntry.Text);

        if (isEmailEmpty || isPasswdEmpty)
        {
            
        }
        else
        {
            Navigation.PushAsync(new HomePage());
        }
    }
}