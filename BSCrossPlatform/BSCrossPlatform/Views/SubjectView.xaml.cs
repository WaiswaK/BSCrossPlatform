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
        async void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var category = ((ListView)sender).SelectedItem as CategoryModel;
            if (category == null)
                return; //Move to nextpage
            else
            {
                if (category.categoryName.Equals("Videos"))
                    await Navigation.PushAsync(new VideosView(category));
                if (category.categoryName.Equals("Files"))
                    await Navigation.PushAsync(new DocumentsView(category));
                if (category.categoryName.Equals("Assignments"))
                    await Navigation.PushAsync(new AssignmentsView(category));
                if (category.categoryName.Equals("Topics"))
                     await Navigation.PushAsync(new FoldersView(category));
            }
        }
    }
}
