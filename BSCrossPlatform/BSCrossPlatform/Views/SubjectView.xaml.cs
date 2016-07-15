using BSCrossPlatform.Models;
using BSCrossPlatform.ViewModels;

using Xamarin.Forms;

namespace BSCrossPlatform.Views
{
    public partial class SubjectView : ContentPage
    {
        public SubjectView(SubjectModel subject)
        {
            InitializeComponent();
            BindingContext = new SubjectViewModel(subject);
        }
        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        => ((ListView)sender).SelectedItem = null;
        void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var category = ((ListView)sender).SelectedItem as CategoryModel;
            if (category == null)
                return; //Move to nextpage
            /*
                var item = e.ClickedItem;
            CategoryModel _category = ((CategoryModel)item);
            if (_category.categoryName.Equals("Quiz"))
            {
                Frame.Navigate(typeof(TopicsPage), _category);
            }
            if (_category.categoryName.Equals("Videos"))
            {
                Frame.Navigate(typeof(VideosView), _category);
            }
            if (_category.categoryName.Equals("Files"))
            {
                Frame.Navigate(typeof(DocumentsView), _category);
            }
            if (_category.categoryName.Equals("Assignments"))
            {
                Frame.Navigate(typeof(AssignmentsView), _category);
            }*/


        }
    }
}
