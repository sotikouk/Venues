using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venues.Model;

namespace Venues;

public partial class PostDetailPage : ContentPage
{
    private Post SelectedPost;
    public PostDetailPage(Post selectedPost)
    {
        InitializeComponent();
        this.SelectedPost = selectedPost;
        experienceEntry.Text = SelectedPost.Experience;
        nameLabel.Text = SelectedPost.VenueName;
        addressLabel.Text = SelectedPost.Address;
    }
    
    private void DeleteButton_OnClicked(object? sender, EventArgs e)
    {
        App.Database.DeletePostAsync(SelectedPost);
        Navigation.PushAsync(new HomePage());
    }
}