using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Map = Microsoft.Maui.Controls.Maps.Map;

namespace Venues;

public partial class MapPage : ContentPage
{
    public MapPage()
    {
        InitializeComponent();
        Map map = new Map();
        Content = map;
    }
}