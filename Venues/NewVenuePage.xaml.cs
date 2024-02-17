using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Venues.Model;

namespace Venues;

public partial class NewVenuePage : ContentPage
{
    public NewVenuePage()
    {
        InitializeComponent();
    }

    private void SaveButton_OnClicked(object? sender, EventArgs e)
    {
        Post post = new Post()
        {
            Experience = ExperienceEntry.Text
        };
        App.Database.AddPostAsync(post);
        Navigation.PushAsync(new HistoryPage());
    }
}