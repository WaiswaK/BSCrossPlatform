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
                return;
            else
            {
                string content = Core.WebViewContentHelper.WrapHtml(assignment.description, 100, 100);
            }          
        }
    }
}
