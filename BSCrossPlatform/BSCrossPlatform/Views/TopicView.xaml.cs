using BSCrossPlatform.Models;
using BSCrossPlatform.ViewModels;

using Xamarin.Forms;

namespace BSCrossPlatform.Views
{
    public partial class TopicView : ContentPage
    {
        //TopicModel Current_Topic = null;
        public TopicView(TopicModel topic, string notes)
        {
            InitializeComponent();
            BindingContext = new TopicViewModel(topic, notes);
        }
    }
}
