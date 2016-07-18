using BSCrossPlatform.Models;
using BSCrossPlatform.ViewModels;
using Xamarin.Forms;

namespace BSCrossPlatform.Views
{
    public partial class SubjectsView : ContentPage
    {
        public SubjectsView(ModulesModel subjects)
        {
            InitializeComponent();
            BindingContext = new ModulesViewModel(subjects);
        }
        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        => ((ListView)sender).SelectedItem = null;
        async void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var subject = ((ListView)sender).SelectedItem as SubjectModel;
            if (subject == null)
                return; //Move to nextpage
            else
                await Navigation.PushAsync(new SubjectView(subject));
        }
    }
}
