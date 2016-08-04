using BSCrossPlatform.Core;
using BSCrossPlatform.Droid.Tasks;
using System.Net;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportRenderer(typeof(CustomWebView), typeof(CustomWebViewRenderer))]

namespace BSCrossPlatform.Droid.Tasks
{
    public class CustomWebViewRenderer : WebViewRenderer
    {
        string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var customWebView = Element as CustomWebView;
                Control.Settings.AllowUniversalAccessFromFileURLs = true;
                Control.LoadUrl(string.Format("file:///android_asset/pdfjs/web/viewer.html?file={0}", string.Format(documentsPath + "/{0}", WebUtility.UrlEncode(customWebView.Uri))));
            }
        }
    }
}