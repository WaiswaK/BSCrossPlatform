using BSCrossPlatform.WinPhone.Tasks;
using System;
using Xamarin.Forms.Platform.WinRT;
using System.Net;
using Xamarin.Forms;
using BSCrossPlatform.Views;

[assembly: ExportRenderer(typeof(CustomWebView), typeof(CustomWebViewRenderer))]
namespace BSCrossPlatform.WinPhone.Tasks
{
    public class CustomWebViewRenderer : WebViewRenderer
    {
        public static Windows.Storage.StorageFolder appFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var customWebView = Element as CustomWebView;
                Control.Source = new Uri(string.Format("ms-appx-web:///Assets/pdfjs/web/viewer.html?file={0}", string.Format(appFolder.Path + "{0}", WebUtility.UrlEncode(customWebView.Uri))));
            }
        }
    }
}
