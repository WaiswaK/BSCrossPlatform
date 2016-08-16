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
        public static string AppFolderPath()
        {
            string appFolderName = "BrainShare";
            string externalStorageDirectory = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
            if (!Directory.Exists(Path.Combine(externalStorageDirectory, appFolderName)))
                Directory.CreateDirectory(Path.Combine(externalStorageDirectory, appFolderName));
            return Path.Combine(externalStorageDirectory, appFolderName);
        }
        #region Implementations
        public async Task ForceImageDownloader(string filepath, string fileName, string extension)
        {
            filepath = Core.CommonTask.httplink(filepath);
            var webClient = new WebClient();
            webClient.DownloadDataCompleted += (s, e) => {
                var bytes = e.Result; 
                string localPath = Path.Combine(AppFolderPath(), fileName + extension);
                File.WriteAllBytes(localPath, bytes); 
            };
            var url = new System.Uri(filepath);
            webClient.DownloadDataAsync(url);
        }
        public async Task ImageDownloader(string filepath, string fileName)
        {
            filepath = Core.CommonTask.httplink(filepath);
            string imageformat = Core.ImageTask.imageFormat(filepath);
            var webClient = new WebClient();
            webClient.DownloadDataCompleted += (s, e) => {
                var bytes = e.Result; 
                string localPath = Path.Combine(AppFolderPath(), fileName + imageformat);
                File.WriteAllBytes(localPath, bytes); 
            };
            var url = new System.Uri(filepath);
            webClient.DownloadDataAsync(url);
        }
        public string imagePath(string imagename)
        {
            string path = Path.Combine(AppFolderPath(), imagename);
            return path;
        }
        public async Task<string> LocalBase64(string image_path, string fileformat)
        {
            string image = Core.ImageTask.imageName(image_path + fileformat);
            if (!File.Exists(image)) return null;
            var bytes = File.ReadAllBytes(image);
            return Convert.ToBase64String(bytes);
        }
        public bool IsInternetConnectionAvailable()
        {
            try
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
            catch
            {
                return false;
            }
        }
        public async Task<bool> FileExists(string path)
        {
            if (!File.Exists(path))
                return false;
            else return true;
        }
        public async Task DownloadFile(string filepath, string fileName)
        {
            filepath = Core.CommonTask.httplink(filepath);
            fileName = fileName + Core.Constant.PDF_extension;
            fileName = fileName.Replace(' ', '_');
            var destination = Path.Combine(AppFolderPath(), fileName);
            await new WebClient().DownloadFileTaskAsync(new System.Uri(filepath), destination);
        }
        public string pdfPath(string pdfName)
        {
            string path = Path.Combine(AppFolderPath(), pdfName);
            return path;
        }
        #endregion
    }
}