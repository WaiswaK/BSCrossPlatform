﻿using BSCrossPlatform.Models;
using BSCrossPlatform.ViewModels;

using Xamarin.Forms;

namespace BSCrossPlatform.Views
{
    public partial class AssignmentView : ContentPage
    {
        string all_notes = null;
        public AssignmentView(AssignmentModel assignment)
        {
            InitializeComponent();
            BindingContext = new AssignmentViewModel(assignment);
            all_notes = assignment.description;
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