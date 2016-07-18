using BSCrossPlatform.Models;
using BSCrossPlatform.ViewModels;

using Xamarin.Forms;

namespace BSCrossPlatform.Views
{
    public partial class VideosView : ContentPage
    {
        public VideosView(CategoryModel videos)
        {
            InitializeComponent();
            BindingContext = new CategoryViewModel(videos);
        }
        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        => ((ListView)sender).SelectedItem = null;
        async void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var videofile = ((ListView)sender).SelectedItem as VideoModel;
            if (videofile == null)
                return; 
            else await Navigation.PushAsync(new PlayView(videofile));
        }
    }
}
