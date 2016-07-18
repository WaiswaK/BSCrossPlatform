using BSCrossPlatform.Models;
using BSCrossPlatform.ViewModels;

using Xamarin.Forms;

namespace BSCrossPlatform.Views
{
    public partial class LibraryView : ContentPage
    {
        public LibraryView(ModulesModel module)
        {
            InitializeComponent();
            BindingContext = new ModulesViewModel(module);
        }
        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        => ((ListView)sender).SelectedItem = null;
        async void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var libraryCategory = ((ListView)sender).SelectedItem as LibCategoryModel;
            if (libraryCategory == null)
                return;
            else await Navigation.PushAsync(new LibraryCategoryBooksView(libraryCategory));
        }
    }
}
