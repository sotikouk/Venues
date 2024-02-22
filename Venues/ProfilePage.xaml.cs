using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venues;

public partial class ProfilePage : ContentPage
{
    public ProfilePage()
    {
        InitializeComponent();
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        
        var postTable = await App.Database.GetPostAsync();
        var categories = (from p in postTable 
            orderby p.CategoryId select p.CategoryName).Distinct().ToList();
        Dictionary<string, int> categoriesCount = new Dictionary<string, int>();
        foreach (var category in categories)
        {
            var count = (from post in postTable 
                where post.CategoryName == category
                select post).ToList().Count;
            categoriesCount.Add(category,count);
        }

        categoriesListView.ItemsSource = categoriesCount;
        postCountLabel.Text = postTable.Count().ToString();
    }
}