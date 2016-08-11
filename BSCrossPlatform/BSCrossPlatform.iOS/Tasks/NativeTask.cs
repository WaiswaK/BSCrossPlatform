using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(BSCrossPlatform.iOS.Tasks.NativeTask))]

namespace BSCrossPlatform.iOS.Tasks
{
    class NativeTask : Interfaces.ITask
    {
        #region Declarations
        string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        #endregion
        #region Implementations
        public async Task ForceImageDownloader(string filepath, string fileName, string extension)
        {
            filepath = Core.CommonTask.httplink(filepath);
            var webClient = new WebClient();
            webClient.DownloadDataCompleted += (s, e) => {
                var bytes = e.Result; // get the downloaded data
                string localPath = Path.Combine(documentsPath, fileName + extension);
                File.WriteAllBytes(localPath, bytes); // writes to local storage
            };
            var url = new Uri(filepath);
            webClient.DownloadDataAsync(url);
        }
        public async Task ImageDownloader(string filepath, string fileName)
        {
            filepath = Core.CommonTask.httplink(filepath);
            string imageformat = Core.ImageTask.imageFormat(filepath);
            var webClient = new WebClient();
            webClient.DownloadDataCompleted += (s, e) => {
                var bytes = e.Result; // get the downloaded data
                string localPath = Path.Combine(documentsPath, fileName + imageformat);
                File.WriteAllBytes(localPath, bytes); // writes to local storage
            };
            var url = new Uri(filepath);
            webClient.DownloadDataAsync(url);
        }
        public string imagePath(string imagename)
        {
            string path = Path.Combine(documentsPath, imagename);
            return path;
        }
        public bool IsInternetConnectionAvailable()
        {
            throw new NotImplementedException();
        }
        public async Task<string> LocalBase64(string image_path, string fileformat)
        {
            if (!File.Exists(image_path)) return null;
            var bytes = File.ReadAllBytes(image_path);
            return Convert.ToBase64String(bytes);
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
            var destination = Path.Combine(documentsPath, fileName);
            await new WebClient().DownloadFileTaskAsync(new Uri(filepath), destination);
        }
        public string pdfPath(string pdfName)
        {
            string path = Path.Combine(documentsPath, pdfName);
            return path;
        }
        #endregion
    }
}