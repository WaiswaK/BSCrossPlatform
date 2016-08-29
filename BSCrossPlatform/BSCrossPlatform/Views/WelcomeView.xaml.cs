using System;

using Xamarin.Forms;

namespace BSCrossPlatform.Views
{
    public partial class WelcomeView : ContentPage
    {
        public WelcomeView()
        {
            InitializeComponent();
        }
        async void OnPortalClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginView(true));
        }
        async void OnPastClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SamplePapersView(Core.JSONTask.GetPastPapers(Core.JSONTask.SampleJsonArray())));
        }
        
    }
}
