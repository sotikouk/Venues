using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Maps;
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