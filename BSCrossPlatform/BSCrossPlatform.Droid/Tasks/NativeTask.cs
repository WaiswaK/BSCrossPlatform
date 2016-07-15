using Android.App;
using Android.Content;
using Android.Net;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(BSCrossPlatform.Droid.Tasks.NativeTask))]

namespace BSCrossPlatform.Droid.Tasks
{
    class NativeTask : Interfaces.ITask
    {
        #region Declarations
        string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        #endregion     
        public async Task ForceImageDownloader(string filepath, string fileName, string extension)
        {
            var webClient = new WebClient();
            webClient.DownloadDataCompleted += (s, e) => {
                var bytes = e.Result; // get the downloaded data
                string localPath = Path.Combine(documentsPath, fileName + extension);
                File.WriteAllBytes(localPath, bytes); // writes to local storage
            };
            var url = new System.Uri(filepath);
            webClient.DownloadDataAsync(url);
        }
        public async Task ImageDownloader(string filepath, string fileName)
        {
            var webClient = new WebClient();
            webClient.DownloadDataCompleted += (s, e) => {
                var bytes = e.Result; // get the downloaded data
                string localPath = Path.Combine(documentsPath, fileName);
                File.WriteAllBytes(localPath, bytes); // writes to local storage
            };
            var url = new System.Uri(filepath);
            webClient.DownloadDataAsync(url);
        }
        public string imagePath(string imagename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return path;
        }
        public async Task<string> LocalBase64(string image_path, string fileformat)
        {
            if (!File.Exists(image_path)) return null;
            var bytes = File.ReadAllBytes(image_path);
            return Convert.ToBase64String(bytes);
        }
        public bool IsInternetConnectionAvailable()
        {
            var connectivityManager = (ConnectivityManager)Application.Context.GetSystemService(Context.ConnectivityService);
            var activeNetworkInfo = connectivityManager.ActiveNetworkInfo;
            if (activeNetworkInfo != null && activeNetworkInfo.IsConnectedOrConnecting)
            {
               return true;
            }
            else
            {
                return false;
            }
        }
    }
}