using BSCrossPlatform.Models;
using BSCrossPlatform.ViewModels;

using Xamarin.Forms;

namespace BSCrossPlatform.Views
{
    public partial class NotesView : ContentPage
    {
        public NotesView(CategoryModel notes)
        {
            InitializeComponent();
            BindingContext = new CategoryViewModel(notes);
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
