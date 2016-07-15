using BSCrossPlatform.Models;
using BSCrossPlatform.ViewModels;

using Xamarin.Forms;

namespace BSCrossPlatform.Views
{
    public partial class LibraryView : ContentPage
    {
        public LibraryView(UserModel user)
        {
            InitializeComponent();
            BindingContext = new StudentViewModel(user);
        }
        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        => ((ListView)sender).SelectedItem = null;
        void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var libraryCategory = ((ListView)sender).SelectedItem as LibCategoryModel;
            if (libraryCategory == null)
                return; //Move to nextpage
        }
    }
}
