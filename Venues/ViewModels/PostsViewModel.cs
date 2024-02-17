using System.Collections.ObjectModel;
using System.ComponentModel;
using Venues.Model;


namespace Venues.ViewModels;

public class PostsViewModel : INotifyPropertyChanged
{
    #region Fields

    private ObservableCollection<Post> PostInfo;
    private Post selectedPost;

    #endregion

    #region Properties
    public Post SelectedItem
    {
        get 
        {
            return selectedPost;
        }
        set
        {
            selectedPost = value;
            OnPropertyChanged("SelectedItem");
        }
    }
    public ObservableCollection<Post> PostsInfo
    {
        get
        {
            return PostInfo;
        }
        set
        {
            PostInfo = value;
            OnPropertyChanged("PostInfo");
        }
    }

    #endregion

    #region Constructor
    public PostsViewModel()
    {
        GenerateContacts();
    }
    #endregion

    #region Methods

    private void GenerateContacts()
    {
        PostsInfo = new ObservableCollection<Post>();
        //PostsInfo = new ContactsInfoRepository().GetContactDetails(20);
        PopulateDB();
    }

    private async void PopulateDB()
    {
        foreach (Post post in PostInfo)
        {
            var item = await App.Database.GetPostAsync(post);
            if(item == null)
                await App.Database.AddPostAsync(post);
        }
    }
    private async void OnAddNewItem()
    {
        await App.Database.AddPostAsync(SelectedItem);
        PostsInfo.Add(SelectedItem);
        await App.Current.MainPage.Navigation.PopAsync();
    }

    #endregion

    #region Interface Member

    public event PropertyChangedEventHandler PropertyChanged;

    public void OnPropertyChanged(string name)
    {
        if (this.PropertyChanged != null)
            this.PropertyChanged(this, new PropertyChangedEventArgs(name));
    }

    #endregion
}