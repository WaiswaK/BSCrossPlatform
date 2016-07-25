using BSCrossPlatform.Core;
using BSCrossPlatform.Models;
using BSCrossPlatform.ViewModels;

using Xamarin.Forms;

namespace BSCrossPlatform.Views
{
    public partial class TopicView : ContentPage
    {
        TopicModel Current_Topic = null;
        public TopicView(TopicModel topic, string notes)
        {
            InitializeComponent();
            BindingContext = new TopicViewModel(topic, notes);
        }
        /*public TopicView(TopicModel topic)
        {
            InitializeComponent();
            BindingContext = new TopicViewModel(topic);
        }*/
        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        => ((ListView)sender).SelectedItem = null;
        async void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var attachment = ((ListView)sender).SelectedItem as AttachmentModel;
            if (attachment == null)
                return;
            else await Navigation.PushAsync(new PDFReader(attachment));
        }
        /*private async void WebView2_Loaded(object sender, RoutedEventArgs e)
        {
            string new_notes = await Core.NotesTask.Notes_loader(Current_Topic);
            var WebView = (WebView)sender;
            //string content = WebViewContentHelper.WrapHtml(all_notes, WebView.ActualWidth, WebView.ActualHeight);
            string content = WebViewContentHelper.WrapHtml(new_notes, WebView.ActualWidth, WebView.ActualHeight);
            WebView.NavigateToString(content);
        }*/
        /// <summary>
		/// Called when the webview starts navigating. Displays the loading label.
		/// </summary>
		/*async void webviewNavigating(object sender, WebNavigatingEventArgs e)
        {
            //this.labelLoading.IsVisible = true; //display the label when navigating starts
            string new_notes = await Core.NotesTask.Notes_loader(Current_Topic);
            var WebView = (WebView)sender;
            //string content = WebViewContentHelper.WrapHtml(all_notes, WebView.ActualWidth, WebView.ActualHeight);
            //string content = WebViewContentHelper.WrapHtml(new_notes, 1000, 100);

            //Just added here
            var htmlSource = new HtmlWebViewSource();
            htmlSource.Html = new_notes;
            //htmlSource.Html = content;
            WebView.Source = htmlSource;

            //WebView.Source = content;
        }
        */

        /// <summary>
        /// Called when the webview finished navigating. Hides the loading label.
        /// </summary>
        /*async void webviewNavigated(object sender, WebNavigatedEventArgs e)
        {
            //this.labelLoading.IsVisible = false; //remove the loading indicator when navigating is finished
            //this.labelLoading.IsVisible = true; //display the label when navigating starts
            string new_notes = await Core.NotesTask.Notes_loader(Current_Topic);
            var WebView = (WebView)sender;
            //string content = WebViewContentHelper.WrapHtml(all_notes, WebView.ActualWidth, WebView.ActualHeight);
           // string content = WebViewContentHelper.WrapHtml(new_notes, 1000, 100);
            //WebView.Source = content;

            //Just added here
            var htmlSource = new HtmlWebViewSource();
            htmlSource.Html = new_notes;
            //htmlSource.Html = content;
            WebView.Source = htmlSource;
        }
        */
        
    }
}
