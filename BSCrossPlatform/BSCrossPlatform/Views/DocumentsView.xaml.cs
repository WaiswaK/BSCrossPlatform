using BSCrossPlatform.Models;
using BSCrossPlatform.ViewModels;

using Xamarin.Forms;

namespace BSCrossPlatform.Views
{
    public partial class DocumentsView : ContentPage
    {
        public DocumentsView(CategoryModel books)
        {
            InitializeComponent();
            BindingContext = new CategoryViewModel(books);
        }
        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        => ((ListView)sender).SelectedItem = null;
        void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var file = ((ListView)sender).SelectedItem as AttachmentModel;
            if (file == null)
                return; //Move to nextpage
            //var item = e.ClickedItem;
            //FolderModel _folder = ((FolderModel)item);
            //Frame.Navigate(typeof(TopicsPage), _folder);
        }
    }
}
