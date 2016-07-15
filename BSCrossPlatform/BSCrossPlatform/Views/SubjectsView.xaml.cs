using BSCrossPlatform.Models;
using BSCrossPlatform.ViewModels;
using Xamarin.Forms;

namespace BSCrossPlatform.Views
{
    public partial class SubjectsView : ContentPage
    {
        public SubjectsView(UserModel user)
        {
            InitializeComponent();
            BindingContext = new StudentViewModel(user);
        }
        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        => ((ListView)sender).SelectedItem = null;
        void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var subject = ((ListView)sender).SelectedItem as SubjectModel;
            if (subject == null)
                return; //Move to nextpage
        }
    }
}
