using BSCrossPlatform.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
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
        public async Task DeleteTemporaryFiles() //Could do without it here
        {
            StorageFolder tempFolder = ApplicationData.Current.TemporaryFolder;
            IReadOnlyList<StorageFile> images = await tempFolder.GetFilesAsync();
            foreach (var image in images)
            {
                await image.DeleteAsync();
            }
        }
        public async Task ForceImageDownloader(string filepath, string fileName, string extension)
        {
            filepath = httplink(filepath); //Format download link
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
            filepath = httplink(filepath); //Format download link
            Uri uri = new Uri(filepath);
            string imageformat = imageFormat(filepath);

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
        public async Task<string> LocalBase64(string image_path, string fileformat)
        {
            string image = imageName(image_path + fileformat);
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
        #endregion

        #region Support Methods
        //Method to Format a weblink for download of Images
        public static string httplink(string filepath)
        {
            int f = 0;
            string weblink = string.Empty;
            int l = filepath.Length;
            string link = "http";
            int http = link.Length;

            //Search for http in link 
            for (int i = 0; i < l; i++)
            {
                if (filepath[i] == link[0])
                {
                    for (int K = i + 1, j = 1; j < http; j++, K++)
                    {
                        if (filepath[K] == link[j])
                        {
                            f++;
                        }
                    }
                }
            }
            if (f == http - 1)
            {
                weblink = filepath;
            }
            else
            {
                weblink = Constant.BaseUri + filepath;
            }
            return weblink;
        }
        //Getting image format from a string 
        public static string imageFormat(string filepath)
        {
            int f = 0;
            string imageformat = string.Empty;
            filepath = filepath.ToLower(); //Changing to lower-case
            int l = filepath.Length;
            int JPG_extension = Constant.JPG_extension.Length;
            int PNG_extension = Constant.PNG_extension.Length;
            int GIF_extension = Constant.GIF_extension.Length;
            int TIFF_extension = Constant.TIFF_extension.Length;
            int BMP_extension = Constant.BMP_extension.Length;

            //Search for jpg 
            for (int i = 0; i < l; i++)
            {
                if (filepath[i] == Constant.JPG_extension[0])
                {
                    for (int K = i + 1, j = 1; j < JPG_extension; j++, K++)
                    {
                        if (filepath[K] == Constant.JPG_extension[j])
                        {
                            f++;
                        }
                    }
                }
            }
            if (f == JPG_extension - 1)
            {
                imageformat = Constant.JPG_extension;
            }

            if (imageformat == string.Empty)
            {
                //Search for png 
                f = 0;
                for (int i = 0; i < l; i++)
                {
                    if (filepath[i] == Constant.PNG_extension[0])
                    {
                        for (int K = i + 1, j = 1; j < PNG_extension; j++, K++)
                        {
                            if (filepath[K] == Constant.PNG_extension[j])
                            {
                                f++;
                            }
                        }
                    }
                }
                if (f == PNG_extension - 1)
                {
                    imageformat = Constant.PNG_extension;
                }
                if (imageformat == string.Empty)
                {
                    //Search for gif 
                    f = 0;
                    for (int i = 0; i < l; i++)
                    {
                        if (filepath[i] == Constant.GIF_extension[0])
                        {
                            for (int K = i + 1, j = 1; j < GIF_extension; j++, K++)
                            {
                                if (filepath[K] == Constant.GIF_extension[j])
                                {
                                    f++;
                                }
                            }
                        }
                    }
                    if (f == GIF_extension - 1)
                    {
                        imageformat = Constant.GIF_extension;
                    }
                    if (imageformat == string.Empty)
                    {
                        //Search for bmp 
                        f = 0;
                        for (int i = 0; i < l; i++)
                        {
                            if (filepath[i] == Constant.BMP_extension[0])
                            {
                                for (int K = i + 1, j = 1; j < BMP_extension; j++, K++)
                                {
                                    if (filepath[K] == Constant.BMP_extension[j])
                                    {
                                        f++;
                                    }
                                }
                            }
                        }
                        if (f == BMP_extension - 1)
                        {
                            imageformat = Constant.BMP_extension;
                        }
                        if (imageformat == string.Empty)
                        {
                            //Search for tiff
                            f = 0;
                            for (int i = 0; i < l; i++)
                            {
                                if (filepath[i] == Constant.TIFF_extension[0])
                                {
                                    for (int K = i + 1, j = 1; j < TIFF_extension; j++, K++)
                                    {
                                        if (filepath[K] == Constant.TIFF_extension[j])
                                        {
                                            f++;
                                        }
                                    }
                                }
                            }
                            if (f == TIFF_extension - 1)
                            {
                                imageformat = Constant.TIFF_extension;
                            }
                        }
                    }
                }
            }
            return imageformat;
        }
        //Getting image name from a string
        public static string imageName(string filepath)
        {
            string imagename = string.Empty;
            char[] delimiter = { '/' };
            string[] linksplit = filepath.Split(delimiter);
            List<string> linklist = linksplit.ToList();
            imagename = linklist.Last();
            return imagename;
        }
        #endregion

    }
}
