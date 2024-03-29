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
    
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        Map = new Map();
        Location startLocation = new Location(39.036444, 22.389378);
        MapSpan mapSpan1 = new MapSpan(startLocation,0.01,0.01);
        Map.MoveToRegion(mapSpan1);
        Map.IsShowingUser = true;

        Location? location = await GetLocation();
        
        if (location != null)
        {
            MapSpan mapSpan = new MapSpan(location, 0.01, 0.01);
            Map.MapType = MapType.Hybrid;
            Map.MoveToRegion(mapSpan);
        }

        await GetPosts();
    }

    async Task GetPosts()
    {
        var posts = await App.Database.GetPostAsync();
        DisplayOnMap(posts);
    }

    public void DisplayOnMap(List<Post> posts)
    {
        if (posts.Count == 0)
            return;
        foreach (var post in posts)
        {
            Map.Pins.Add(new Microsoft.Maui.Controls.Maps.Pin
            {
                Location = new Location(post.Latitude, post.Longitude),
                Label = post.VenueName
            });

        }
        Content = Map;
    }

    async Task<Location?> GetLocation()
    {
        
        var currentlocation = await Geolocation.GetLocationAsync();

        return currentlocation;
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

