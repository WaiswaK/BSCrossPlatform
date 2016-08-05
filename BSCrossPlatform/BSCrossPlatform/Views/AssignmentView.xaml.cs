using BSCrossPlatform.Models;
using BSCrossPlatform.ViewModels;

using Xamarin.Forms;

namespace BSCrossPlatform.Views
{
    public partial class AssignmentView : ContentPage
    {
        public AssignmentView(AssignmentModel assignment, string assignment_notes)
        {
            InitializeComponent();
            BindingContext = new AssignmentViewModel(assignment, assignment_notes);
        }
        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        => ((ListView)sender).SelectedItem = null;
        async void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var attachment = ((ListView)sender).SelectedItem as AttachmentModel;
            if (attachment == null)
                return;
            else await Navigation.PushAsync(new PDFReader(attachment));
        }
    }
}
