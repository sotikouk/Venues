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

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var location = await Geolocation.GetLocationAsync();
        var Pois = await VenuePlaces.GetPois(location.Latitude, location.Longitude);
        venueListView.ItemsSource = Pois;
    }

    private void SaveButton_OnClicked(object? sender, EventArgs e)
    {
        try
        {
            var selectedVenue = venueListView.SelectedItem as Result;
            var firstCategory = selectedVenue.categories.FirstOrDefault();
            Post post = new Post()
            {
                Experience = ExperienceEntry.Text,
                CategoryId = firstCategory.id,
                CategoryName = firstCategory.name,
                Address = selectedVenue.location.address,
                Distance = selectedVenue.distance,
                VenueName = selectedVenue.name,
                Latitude = selectedVenue.geocodes.main.latitude,
                Longitude = selectedVenue.geocodes.main.longitude
            };
            App.Database.AddPostAsync(post);
            Navigation.PushAsync(new HistoryPage());
        } 
        catch (NullReferenceException nre)
        {
                
        }
        catch (Exception ex)
        {
                
        }
    }
}