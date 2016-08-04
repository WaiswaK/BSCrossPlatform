using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

[assembly: Xamarin.Forms.Dependency(typeof(BSCrossPlatform.WinPhone.Tasks.NativeTask))]

namespace BSCrossPlatform.WinPhone.Tasks
{
    class NativeTask : Interfaces.ITask
    {
        #region Declarations
        public static StorageFolder appFolder = ApplicationData.Current.LocalFolder;
        #endregion
        #region ITask Implementations
        public async Task ForceImageDownloader(string filepath, string fileName, string extension)
        {
            filepath = Core.CommonTask.httplink(filepath); //Format download link
            Uri uri = new Uri(filepath);

            // download pic
            var bitmapImage = new BitmapImage();
            var httpClient = new HttpClient();
            var httpResponse = await httpClient.GetAsync(uri);
            byte[] b = await httpResponse.Content.ReadAsByteArrayAsync();

            // create a new in memory stream and datawriter
            using (var stream = new InMemoryRandomAccessStream())
            {
                using (DataWriter dw = new DataWriter(stream))
                {
                    // write the raw bytes and store
                    dw.WriteBytes(b);
                    await dw.StoreAsync();

                    // set the image source
                    stream.Seek(0);
                    bitmapImage.SetSource(stream);

                    // write to local pictures
                    StorageFile storageFile;
                    try
                    {
                        storageFile = await appFolder.CreateFileAsync(fileName + extension,
                            CreationCollisionOption.ReplaceExisting);
                        using (var storageStream = await storageFile.OpenAsync(FileAccessMode.ReadWrite))
                        {
                            await RandomAccessStream.CopyAndCloseAsync(stream.GetInputStreamAt(0), storageStream.GetOutputStreamAt(0));
                        }
                    }
                    catch //(Exception ex)
                    {
                        //ErrorLogTask Logfile = new ErrorLogTask();
                        //Logfile.Error_details = ex.ToString();
                        //Logfile.Error_title = "ForceImagedownloader Method";
                        //Logfile.Location = "CommonTask";
                        //ErrorLogTask.LogFileSaveAsync(Logfile);
                    }
                }
            }
        }
        public async Task ImageDownloader(string filepath, string fileName)
        {
            filepath = Core.CommonTask.httplink(filepath); //Format download link
            Uri uri = new Uri(filepath);
            string imageformat = Core.ImageTask.imageFormat(filepath);

            // download pic
            var bitmapImage = new BitmapImage();
            var httpClient = new HttpClient();
            var httpResponse = await httpClient.GetAsync(uri);
            byte[] b = await httpResponse.Content.ReadAsByteArrayAsync();

            // create a new in memory stream and datawriter
            using (var stream = new InMemoryRandomAccessStream())
            {
                using (DataWriter dw = new DataWriter(stream))
                {
                    // write the raw bytes and store
                    dw.WriteBytes(b);
                    await dw.StoreAsync();

                    // set the image source
                    stream.Seek(0);
                    bitmapImage.SetSource(stream);

                    // write to local pictures
                    StorageFile storageFile = await appFolder.CreateFileAsync(fileName + imageformat,
                        CreationCollisionOption.FailIfExists);
                    using (var storageStream = await storageFile.OpenAsync(FileAccessMode.ReadWrite))
                    {
                        await RandomAccessStream.CopyAndCloseAsync(stream.GetInputStreamAt(0), storageStream.GetOutputStreamAt(0));
                    }
                }
            }
        }
        public string imagePath(string imagename)
        {
            string path = Path.Combine(appFolder.Path, imagename);
            return path;
        }
        public bool IsInternetConnectionAvailable()
        {
            ConnectionProfile connection = NetworkInformation.GetInternetConnectionProfile();
            bool internet = connection != null && connection.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;
            return internet;
        }
        public async Task<string> LocalBase64(string image_path, string fileformat)
        {
            string image = Core.ImageTask.imageName(image_path + fileformat);
            string base64 = string.Empty;
            try
            {
                StorageFile file = await appFolder.GetFileAsync(image);
                using (var stream = await file.OpenAsync(FileAccessMode.Read))
                {
                    var reader = new DataReader(stream.GetInputStreamAt(0));
                    var bytes = new byte[stream.Size];
                    await reader.LoadAsync((uint)stream.Size);
                    reader.ReadBytes(bytes);
                    base64 = Convert.ToBase64String(bytes);
                }
            }
            catch
            {

            }
            return base64;
        }
        public async Task<bool> FileExists(string path)
        {
            bool exist = false;
            try
            {
                StorageFile file = await appFolder.GetFileAsync(path);
                exist = true;
            }
            catch
            {
                exist = false;
            }
            return exist;
        }
        public async Task DownloadFile(string filepath, string fileName)
        {
            StorageFile storageFile = await appFolder.CreateFileAsync(fileName + Core.Constant.PDF_extension, CreationCollisionOption.ReplaceExisting);
            string newpath = Core.Constant.BaseUri + filepath;
            try
            {
                var downloader = new Windows.Networking.BackgroundTransfer.BackgroundDownloader();
                Uri uri = new Uri(newpath);
                Windows.Networking.BackgroundTransfer.DownloadOperation op = downloader.CreateDownload(uri, storageFile);
                await op.StartAsync();              
            }
            catch 
            {
            }
        }
        #endregion
    }
}
