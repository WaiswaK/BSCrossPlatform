using BSCrossPlatform.Models;
using BSCrossPlatform.ViewModels;

using Xamarin.Forms;

namespace BSCrossPlatform.Views
{
    public partial class TopicsView : ContentPage
    {
        public TopicsView(FolderModel MainTopic)
        {
            InitializeComponent();
            BindingContext = new TopicsViewModel(MainTopic);
        }
        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        => ((ListView)sender).SelectedItem = null;
        async void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var notes = ((ListView)sender).SelectedItem as TopicModel;
            if (notes == null)
                return;
            else await Navigation.PushAsync(new TopicView(notes));
        }
    }
}
