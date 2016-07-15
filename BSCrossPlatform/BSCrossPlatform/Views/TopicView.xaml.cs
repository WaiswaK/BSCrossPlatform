using BSCrossPlatform.Core;
using BSCrossPlatform.Models;
using BSCrossPlatform.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace BSCrossPlatform.Views
{
    public partial class TopicView : ContentPage
    {
        public TopicView(TopicModel topic)
        {
            InitializeComponent();
            BindingContext = new TopicViewModel(topic);
        }
        /*private async void WebView2_Loaded(object sender, RoutedEventArgs e)
        {
            string new_notes = await Core.NotesTask.Notes_loader(Current_Topic);
            var WebView = (WebView)sender;
            //string content = WebViewContentHelper.WrapHtml(all_notes, WebView.ActualWidth, WebView.ActualHeight);
            string content = WebViewContentHelper.WrapHtml(new_notes, WebView.ActualWidth, WebView.ActualHeight);
            WebView.NavigateToString(content);
        }*/
    }
}
