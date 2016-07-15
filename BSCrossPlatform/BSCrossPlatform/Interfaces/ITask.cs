﻿using System.Threading.Tasks;

namespace BSCrossPlatform.Interfaces
{
    public interface ITask
    {
        Task ImageDownloader(string filepath, string fileName);
        Task ForceImageDownloader(string filepath, string fileName, string extension);
        #region ImageTasks
        Task<string> LocalBase64(string image_path, string fileformat);
        string imagePath(string imagename);
        #endregion
        bool IsInternetConnectionAvailable();
    }
}