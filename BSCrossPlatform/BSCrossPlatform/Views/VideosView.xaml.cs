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
        void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var notes = ((ListView)sender).SelectedItem as VideoModel;
            if (notes == null)
                return; //Move to nextpage
            //var item = e.ClickedItem;
            //FolderModel _folder = ((FolderModel)item);
            //Frame.Navigate(typeof(TopicsPage), _folder);
        }
    }
}
