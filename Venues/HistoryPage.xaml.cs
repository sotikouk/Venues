using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using SQLite;
using Venues.Model;

namespace Venues;

public partial class HistoryPage : ContentPage
{
    public HistoryPage()
    {
        InitializeComponent();
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        PostListView.ItemsSource = await App.Database.GetPostAsync();
    }

    private void PostListView_OnItemSelected(object? sender, SelectedItemChangedEventArgs e)
    {
        {
            var selectedPost = PostListView.SelectedItem as Post;

            if (selectedPost != null)
            {
                Navigation.PushAsync(new PostDetailPage(selectedPost));
            }
        }
    }
}