using System;

using Xamarin.Forms;

namespace BSCrossPlatform.Views
{
    public partial class BrowserView : ContentPage
    {
        public BrowserView(string URL)
        {
            InitializeComponent();
            webView.Source = URL;
        }
        /// <summary>
		/// fired when the back button is clicked. If the browser can go back, navigate back.
		/// If the browser can't go back, leave the in-app browser page.
		/// </summary>
        void backButtonClicked(object sender, EventArgs e)
        {
            if (webView.CanGoBack)
            {
                webView.GoBack();
            }
            else
            {
                this.Navigation.PopAsync(); // closes the in-app browser view.
            }
        }

        /// <summary>
        /// Navigates forward
        /// </summary>
        void forwardButtonClicked(object sender, EventArgs e)
        {
            if (webView.CanGoForward)
            {
                webView.GoForward();
            }
        }
    }
}
