//using Android.OS;
using BSCrossPlatform.Droid.Tasks;
using BSCrossPlatform.Views;
using System.Net;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportRenderer(typeof(CustomWebView), typeof(CustomWebViewRenderer))]

namespace BSCrossPlatform.Droid.Tasks
{
    public class CustomWebViewRenderer : WebViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var customWebView = Element as CustomWebView;
                Control.Settings.AllowUniversalAccessFromFileURLs = true;
                Control.LoadUrl(string.Format("file:///android_asset/pdfjs/web/viewer.html?file={0}", string.Format("file://" + NativeTask.AppFolderPath() + "/{0}", WebUtility.UrlEncode(customWebView.Uri))));                
            }
        }
    }
}