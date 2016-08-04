using BSCrossPlatform.Models;
using BSCrossPlatform.ViewModels;

using Xamarin.Forms;

namespace BSCrossPlatform.Views
{
    public partial class FoldersView : ContentPage
    {
        public FoldersView(CategoryModel Folder)
        {
            InitializeComponent();
            BindingContext = new CategoryViewModel(Folder);
        }
        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        => ((ListView)sender).SelectedItem = null;
        async void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var notes = ((ListView)sender).SelectedItem as FolderModel;
            if (notes == null)
                return;
            else await Navigation.PushAsync(new TopicsView(notes));
        }
    }
}
