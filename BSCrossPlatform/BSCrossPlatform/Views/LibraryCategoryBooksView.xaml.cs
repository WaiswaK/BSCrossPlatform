using BSCrossPlatform.Models;
using BSCrossPlatform.ViewModels;

using Xamarin.Forms;

namespace BSCrossPlatform.Views
{
    public partial class LibraryCategoryBooksView : ContentPage
    {
        public LibraryCategoryBooksView(LibCategoryModel category)
        {
            InitializeComponent();
            BindingContext = new LibraryCategoryBooksViewModel(category);
        }
        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        => ((ListView)sender).SelectedItem = null;
        async void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var book = ((ListView)sender).SelectedItem as BookModel;
            if (book == null)
                return;
            else await Navigation.PushAsync(new PDFReader(book));
        }
    }
}
