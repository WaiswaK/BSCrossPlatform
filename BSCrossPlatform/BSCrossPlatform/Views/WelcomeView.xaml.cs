using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            await Navigation.PushAsync(new LoginView());
        }
    }
}
