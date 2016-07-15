using BSCrossPlatform.Models;
using BSCrossPlatform.ViewModels;

using Xamarin.Forms;

namespace BSCrossPlatform.Views
{
    public partial class AssignmentsView : ContentPage
    {
        public AssignmentsView(CategoryModel assignments)
        {
            InitializeComponent();
            BindingContext = new CategoryViewModel(assignments);
        }
        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        => ((ListView)sender).SelectedItem = null;
        void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var assignment = ((ListView)sender).SelectedItem as AssignmentModel;
            if (assignment == null)
                return; //Move to nextpage
            //var item = e.ClickedItem;
            //FolderModel _folder = ((FolderModel)item);
            //Frame.Navigate(typeof(TopicsPage), _folder);
        }
    }
}
