using BSCrossPlatform.Core;
using BSCrossPlatform.iOS.Tasks;
using Xamarin.Forms;
using System.IO;
using System.Net;
using Foundation;
using UIKit;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BSCrossPlatform.Views.CustomWebView), typeof(CustomWebViewRenderer))]
namespace BSCrossPlatform.iOS.Tasks
{
    public class CustomWebViewRenderer : ViewRenderer<Views.CustomWebView, UIWebView>
    {
        string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        protected override void OnElementChanged(ElementChangedEventArgs<Views.CustomWebView> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                SetNativeControl(new UIWebView());
            }
            if (e.OldElement != null)
            {
                // Cleanup
            }
            if (e.NewElement != null)
            {
                var customWebView = Element as Views.CustomWebView;
                string fileName = Path.Combine(NSBundle.MainBundle.BundlePath, string.Format(documentsPath + "{0}", WebUtility.UrlEncode(customWebView.Uri)));
                Control.LoadRequest(new NSUrlRequest(new NSUrl(fileName, false)));
                Control.ScalesPageToFit = true;
            }
        }
    }
}
