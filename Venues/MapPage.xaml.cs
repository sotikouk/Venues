using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Venues.Model;
using Location = Microsoft.Maui.Devices.Sensors.Location;
using Map = Microsoft.Maui.Controls.Maps.Map;


namespace Venues;

public partial class MapPage : ContentPage
{
    public MapPage()
    {
        InitializeComponent();
    }
    
    protected override void OnAppearing()
    {
        base.OnAppearing();

        GetLocation();

        GetPosts();
    }

    private async Task GetPosts()
    {
        var posts = await App.Database.GetPostAsync();
        DisplayOnMap(posts);
    }

    private void DisplayOnMap(List<Post> posts)
    {
        List<Pin> LocPins = new List<Pin>();
        foreach (var post in posts)
        {
            var pin = new Pin();
            {
                pin.Location = new Location(post.Latitude, post.Longitude);
                pin.Address = post.Address;
                pin.Label = post.VenueName;
            }
            LocationsMap.Pins.Add(pin);
            LocPins.Add(pin);
        }

        LocationsMap.ItemsSource = LocPins;
    }

    private async void GetLocation()
    {
        var status = await CheckAndRequestLocationPermission();

        if(status == PermissionStatus.Granted)
        {
            var location = await Geolocation.GetLocationAsync();
            
            MapSpan mapSpan = new MapSpan(location, 0.01, 0.01);
            LocationsMap = new Map(mapSpan);
            LocationsMap.MapType = MapType.Hybrid;
            LocationsMap.IsShowingUser = true;
            LocationsMap.MoveToRegion(mapSpan);
            
            Content = LocationsMap;
        }
    }
    
    
    
    private async Task<PermissionStatus> CheckAndRequestLocationPermission()
    {
        var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

        if (status == PermissionStatus.Granted)
            return status;

        if(status == PermissionStatus.Denied && DeviceInfo.Platform == DevicePlatform.iOS)
        {
            // promt the user to turn on the permission in settings
            return status;
        }

        status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
        return status;
    }
}

