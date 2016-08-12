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
            else
            {
                string new_notes = await Core.NotesTask.Notes_loader(notes);
                string content = Core.WebViewContentHelper.WrapHtml(new_notes, 100, 100);
                await Navigation.PushAsync(new TopicView(notes, content));
            }
        }
    }
}
